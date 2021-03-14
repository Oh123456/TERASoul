using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using FreeMan_BehaviourTrees;

public class FreeMan_BT : BehaviourTree.BehaviourTree
{
    #region Selector
    // 루트에서 시작되는 셀렉터
    Selector rootSelecotr = new Selector();
    // 거리가 멀어지면 점프 공격할지 아니면 그냥 이동할지 정하는 Selector
    Selector moveAttackSelector = new Selector();
    // 공격 범위안인지 확인하는 Selector;
    Selector attackSelector = new Selector();
    #endregion

    #region Sequence
    // 이동관련 
    Sequence move_Sequence = new Sequence();


    #endregion

    #region Decortaor
    DIe_Decortaor           dIe_Decortaor           = new DIe_Decortaor();
    Attack_Decortaor        attack_Decortaor        = new Attack_Decortaor();
    MoveAttack_Decorator    moveAttack_Decorator    = new MoveAttack_Decorator();
    AttackRange_Decortaor   attackRange_Decortaor   = new AttackRange_Decortaor();
    #endregion

    #region Action
    MoveAttackMove_Action moveAttackMove = new MoveAttackMove_Action();
    MoveAttack_Action MoveAttack_Action = new MoveAttack_Action();
    Move_Action move_Action = new Move_Action();
    Look_Acotion look_Acotion = new Look_Acotion();
    Die_Action die_Action = new Die_Action();
    Attack_Action attack_Action = new Attack_Action();
    #endregion

    private void Awake()
    {

        rootSelecotr.AddNode(2, moveAttackMove);
        rootSelecotr.AddNode(1, attack_Decortaor);
        rootSelecotr.AddNode(0, dIe_Decortaor);

        moveAttackSelector.AddNode(1, attackSelector);
        moveAttackSelector.AddNode(0, moveAttack_Decorator);

        attackSelector.AddNode(1, move_Action);
        attackSelector.AddNode(0, attackRange_Decortaor);

        move_Sequence.AddNode(1, moveAttackSelector);
        move_Sequence.AddNode(0, look_Acotion);

        dIe_Decortaor.nextNode = die_Action;

        attack_Decortaor.nextNode = move_Sequence;

        moveAttack_Decorator.nextNode = MoveAttack_Action;

        attackRange_Decortaor.nextNode = attack_Action;


        root  = new Root(rootSelecotr);
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    void Move_Start()
    {
        CharacterMoveMent characterMoveMent = GetComponent<CharacterMoveMent>();
        FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
        freeManBlackBoard.isMove = true;
        characterMoveMent.ChangeSpeed((freeManBlackBoard.target.transform.position - transform.position).magnitude * 100.0f, ref freeManBlackBoard.originSpeed);
    }

    void Move_End()
    {
        CharacterMoveMent characterMoveMent = GetComponent<CharacterMoveMent>();
        FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
        freeManBlackBoard.isMove = false;
        characterMoveMent.CharacterMove(0.0f, 0.0f, 0.0f, Time.deltaTime);
        characterMoveMent.ChangeSpeed(freeManBlackBoard.originSpeed);
    }


    void ResetAI_State()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Attack", false);
        animator.SetBool("ComboAttack", false);
        animator.SetBool("Slash", false);
        animator.SetInteger("AttackKinds", 0);
        animator.applyRootMotion = false;
    }

    void AttackEnd()
    {
        ResetAI_State();
    }

    void Move_ON()
    {
        
        Animator animator = GetComponent<Animator>();
        animator.applyRootMotion = true;

    }



    //
}


namespace FreeMan_BehaviourTrees
{
    #region Decrator
    // 일정거리 이상일떄 점프하면서 다가오는 공격할지 말지 정하는 Decortator;
    public class MoveAttack_Decorator : Decorator
    {
        public MoveAttack_Decorator()
        {
            nextNode = new MoveAttack_Action();
        }

        protected override bool Condition(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
            float lenght = (freeManBlackBoard.target.transform.position - freeManBlackBoard.owner.transform.position).magnitude;
            if (freeManBlackBoard.attack3_dir < lenght)
                return true;
            return false;
        }
    }
    // 공격 중인지 확인하는 Dectortator 공격중이라면 BT를 빠져나온다.
    public class Attack_Decortaor : Decorator
    {
        public Attack_Decortaor()
        {
            isTrue = false;
            // 다음 노드
            //nextNode = new Root_Selector();
        }


        protected override bool Condition(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
            return freeManBlackBoard.owner.GetComponent<Animator>().GetBool("Attack");
        }
    }

    public class AttackRange_Decortaor : Decorator
    {
        public AttackRange_Decortaor()
        {
            // 다음 노드
            //nextNode = new Attack_Action();
        }


        protected override bool Condition(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;

            Transform target = freeManBlackBoard.target.transform;
            Transform transform = freeManBlackBoard.owner.transform;

            Vector3 dir = (target.position - transform.position);
            float dist = dir.magnitude;
            // 공격 범위 밖이면 이동
            if (dist <= freeManBlackBoard.attackRange)
                return true;
            return false;
        }
    }

    public class DIe_Decortaor : Decorator
    {
        public DIe_Decortaor()
        {
            // 다음 노드
            //nextNode = new Die_Action();
        }

        protected override bool Condition(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
            if (freeManBlackBoard.owner.GetComponent<Character>().isDeath)
                return true;
            return false;
        }
    }
    #endregion

    #region Selector
    //public class Root_Selector : Selector
    //{
    //    //public Root_Selector()
    //    //{
    //    //    AddNode(0, new Die_Action());
    //    //    AddNode(1, new Attack_Decortaor());
    //    //    AddNode(2, new MoveAttackMove_Action());
    //    //
    //    //}
    //}   //
    //// 
    //public class MoveAttack_Selector : Selector
    //{
    //    public MoveAttack_Selector()
    //    {
    //        AddNode(0, new MoveAttack_Decorator());
    //        AddNode(1, new Move_Sequence());
    //
    //    }
    //}
    //
    //public class Attack_Selector : Selector
    //{
    //    public Attack_Selector()
    //    {
    //        AddNode(0, new AttackRange_Decortaor());
    //        AddNode(1, new Move_Action());
    //
    //    }
    //}
    //

    #endregion

    #region Sequence
    //public class Move_Sequence : Sequence
    //{
    //    public Move_Sequence()
    //    {
    //        AddNode(0, new Look_Acotion());
    //        AddNode(1, new Move_Action());
    //        AddNode(2, new AttackRange_Decortaor());
    //    }
    //}
    #endregion

    #region Action

    public class MoveAttackMove_Action : Action
    {
        protected override Node_State BT_Update(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
            if (freeManBlackBoard.isMove)
            {
                freeManBlackBoard.GetComponent<CharacterMoveMent>().CharacterMove(0.0f, 1.0f, 20.0f, Time.deltaTime);
                return Node_State.Suceess;
            }
            return Node_State.Failure;
        }
    }

    public class MoveAttack_Action : Action 
    {
        protected override Node_State BT_Update(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
            Animator animator = freeManBlackBoard.owner.GetComponent<Animator>();
            animator.SetBool("Attack", true);
            animator.SetInteger("AttackKinds", 99);
            return Node_State.Suceess;
        }
    }
    public class Look_Acotion : Action
    {
        protected override Node_State BT_Update(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
            GameObject owner = freeManBlackBoard.owner;
            GameObject target = freeManBlackBoard.target;
            Transform transform = owner.transform;

            Vector3 targetPosition = target.transform.position;
            Vector3 position = transform.position;
            targetPosition.y = 0.0f;
            position.y = 0.0f;

            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - position, Vector3.up);
            //Debug.Log(targetRotation.eulerAngles);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 6.0f * Time.deltaTime);
            transform.rotation = targetRotation;
            return Node_State.Suceess;
        }

    }

    public class Move_Action : Action
    {
        protected override Node_State BT_Update(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)blackBoard;
            CharacterMoveMent characterMoveMent = freeManBlackBoard.owner.GetComponent<CharacterMoveMent>();
            characterMoveMent.CharacterMove(0.0f, 1.0f, Time.deltaTime);
            return Node_State.Suceess;
        }
    }
    public class Attack_Action : Action
    {


        protected override Node_State BT_Update(ref BlackBoard blackBoard)
        {
            FreeManBlackBoard freeManBlackBoard = (FreeManBlackBoard)(blackBoard);
            Animator animator = freeManBlackBoard.owner.GetComponent<Animator>();
            switch (RandomAttack(freeManBlackBoard.Probability))
            {
                // 유니티 엔진에서 추가하면서 맞는 애니메이션 실행시키기
                case "Attack1":
                    animator.SetBool("Attack", true);
                    animator.SetInteger("AttackKinds", 1);
                    break;
                case "Attack2":
                    animator.SetBool("Attack", true);
                    animator.SetInteger("AttackKinds", 2);
                    break;
                case "ComboAttack":
                    animator.SetBool("Attack", true);
                    animator.SetInteger("AttackKinds", 3);
                    break;
                case "Attack3":
                    animator.SetBool("Attack", true);
                    animator.SetInteger("AttackKinds", 4);
                    break;
                case "JumpAttack":
                    animator.SetBool("Attack", true);
                    animator.SetInteger("AttackKinds", 5);
                    break;
                case "Kick":
                    animator.SetBool("Attack", true);
                    animator.SetInteger("AttackKinds", 6);
                    break;
                default:
                    break;
            }
            return Node_State.Suceess;
        }

        string RandomAttack(Dictionary<string, float> probability)
        {
            float max = 0.0f;
            foreach (var item in probability)
                max += item.Value;

            float rand = Random.Range(0.0f,max);

            float current = 0.0f;
            foreach (var item in probability)
            {
                current += item.Value;
                if (current >= rand)
                    return item.Key;
            }
            return "NoAttack";
        }
    }

    public class Die_Action : Action
    {
        protected override Node_State BT_Update(ref BlackBoard blackBoard)
        {
            return Node_State.Suceess;
        }
    }
    #endregion

}