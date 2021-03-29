using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_State
{
    Idle,
    Attack1,
    Attack2,
    Attack3,
    Trace,
}

public class FreeManAI : MonoBehaviour
{
    CharacterMoveMent characterMoveMent;
    Animator animator;

    [SerializeField]
    Transform target;
    [SerializeField]
    float attack3_dir = 8.0f;

    bool isMove;
    bool isattackRange;
    float originSpeed;
    [SerializeField]
    float attackRange = 3.0f;
    [SerializeField]
    List<float> attack_Frequency = new List<float>(4);

    float dist;
    AI_State aI_State;

    [SerializeField]
    Collider testcollider;

    private void Awake()
    {
        // 순서대로 공격확률 마지막은 1->2
        attack_Frequency.Add(0.3f);
        attack_Frequency.Add(0.3f);
        attack_Frequency.Add(0.3f);
        attack_Frequency.Add(0.2f);
        attack_Frequency.Add(0.4f);
        attack_Frequency.Add(0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        characterMoveMent = GetComponent<CharacterMoveMent>();
        animator = GetComponent<Animator>();

        //StartCoroutine(Coroutine_Thinking());

        isMove = false;
        isattackRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    testcollider.enabled = !testcollider.enabled;
        //AlwaysUpdate();

        //switch (aI_State)
        //{
        //    case AI_State.Trace:
        //        Update_Trace();
        //        break;
        //}
    }

    //void AlwaysUpdate()
    //{
    //    bool isAttack = animator.GetBool("Attack");

    //    if (!isAttack)
    //    {
    //        //transform.LookAt(target);
    //        Vector3 targetPosition = target.position;
    //        Vector3 position = transform.position;
    //        targetPosition.y = 0.0f;
    //        position.y = 0.0f;

    //        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - position, Vector3.up);
    //        //Debug.Log(targetRotation.eulerAngles);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 6.0f * Time.deltaTime);
    //        transform.rotation = targetRotation;


    //    }
    //    if (isMove)
    //        characterMoveMent.CharacterMove(0.0f, 1.0f, 20.0f, Time.deltaTime);
    //}


    //void Update_Trace()
    //{
    //    bool isAttack = animator.GetBool("Attack");
    //    if (isAttack)
    //        return;
    //    Vector3 dir = (target.position - transform.position);
    //    dist = dir.magnitude;

    //    if (dist <= attackRange)
    //    {
    //        aI_State = AI_State.Idle;
    //        animator.SetFloat("Vertical", 0.0f);
    //        animator.SetFloat("Horizontal", 0.0f);
    //        isattackRange = true;
    //        characterMoveMent.CharacterMove(0.0f, 0.0f, Time.deltaTime);
    //        return;
    //    }
    //    characterMoveMent.CharacterMove(0.0f, 1.0f, Time.deltaTime);
    //}

    void Updat_Idle()
    {

    }

    //void ResetAI_State()
    //{
    //    animator.SetBool("Attack", false);
    //    animator.SetBool("ComboAttack", false);
    //    animator.SetBool("Slash", false);
    //    animator.SetInteger("AttackKinds", 0);
    //    animator.applyRootMotion = false;
    //}

    //void AttackEnd()
    //{
    //    ResetAI_State();
    //}


    //void Move_Start()
    //{
    //    isMove = true;
    //    characterMoveMent.ChangeSpeed((target.position - transform.position).magnitude * 100.0f, ref originSpeed);
    //}

    //void Move_End()
    //{
    //    isMove = false;
    //    characterMoveMent.CharacterMove(0.0f, 0.0f, 0.0f, Time.deltaTime);
    //    characterMoveMent.ChangeSpeed(originSpeed);
    //}

    //void Move_ON()
    //{
    //    Debug.Log("asdf");
    //    animator.applyRootMotion = true;
        
    //}

    //void Look()
    //{
    //    Vector3 targetPosition = target.position;
    //    Vector3 position = transform.position;
    //    targetPosition.y = 0.0f;
    //    position.y = 0.0f;

    //    Quaternion targetRotation = Quaternion.LookRotation(targetPosition - position, Vector3.up);
    //    //Debug.Log(targetRotation.eulerAngles);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 6.0f * Time.deltaTime);
    //    transform.rotation = targetRotation;
    //}

    #region Coroutine
    //IEnumerator Coroutine_Thinking()
    //{
    //    // 상태가 바뀌고 최초에 한번 실행
    //    ResetAI_State();

    //    while (true)
    //    {
    //        // n초뒤..  이코드 이후에 들어온다.
    //        yield return new WaitForSeconds(1.0f);

    //        float lenght = (target.position - transform.position).magnitude;
    //        if (lenght > attack3_dir)
    //        {
    //            animator.SetBool("Attack", true);
    //            animator.SetInteger("AttackKinds", 99);

    //        }
    //        else
    //        {
    //            if (!isattackRange)
    //            {
    //                aI_State = AI_State.Trace;
                   
    //            }
    //            else
    //            {
    //                if (!animator.GetBool("ComboAttack"))
    //                {
    //                    if (target.gameObject.GetComponent<Character>().isGuard & (Random.Range(0.0f,1.0f) < 0.8f))
    //                    {
    //                        animator.SetBool("Attack", true);
    //                        animator.SetInteger("AttackKinds", 6);
    //                        isattackRange = false;
    //                    }
    //                    else
    //                    {
    //                        for (int i = 0; i < attack_Frequency.Count - 1; i++)
    //                        {
    //                            if (Random.Range(0.0f, 1.0f) < attack_Frequency[i])
    //                            {
    //                                animator.SetBool("Attack", true);
    //                                animator.SetInteger("AttackKinds", i + 1);
    //                                animator.applyRootMotion = true;
    //                                isattackRange = false;
    //                                if (i == 0 | i == 4)
    //                                {
    //                                    if (Random.Range(0.0f, 1.0f) < attack_Frequency[attack_Frequency.Count - 1])
    //                                        animator.SetBool("ComboAttack", true);
    //                                }
    //                                break;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    #endregion
}
