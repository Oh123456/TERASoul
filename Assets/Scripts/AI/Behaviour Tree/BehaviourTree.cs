using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviourTree
{
    public enum Node_State
    {
        // 성공
        Suceess,
        // 실패
        Failure,
        // 성패가 결정 되지 않았고 노드가 아직 실행중
        // 이시점에서 체크하여 다음 번에 Running을 반환 노드를 다시 실행
        Running,
    }

    public class BehaviourTree : MonoBehaviour
    {
        protected Root root;
        [SerializeField]
        protected BlackBoard blackBoard;

        private void Awake()
        {
         
        }

        void Update()
        {
            root.OnStart();
            root.BT_Update(ref blackBoard);
            root.OnEnd();
        }
    }

    public interface INode
    {
        // 생각 하는 부분
        Node_State Tick(ref BlackBoard blackBoard , ref Stack<INode> parentStack);

        void OnStart();
        void OnEnd();
    }

    // BT의 시작 노드
    public class Root
    {
        protected Stack<INode> node_Stack;
        public INode startNode;


        public Root(INode node)
        {
            startNode = node;
            node_Stack = new Stack<INode>();
        }

        public Node_State BT_Update(ref BlackBoard blackBoard)
        {
            return startNode.Tick(ref blackBoard, ref node_Stack);
        }

        public void OnStart()
        {
            node_Stack.Clear();
        }

        public void OnEnd()
        {

        }
    }

    /*
     * 하나의 자식 노드만 가질 수 있음
     * 조건을 만족하면 자식을 실행
     */
    public class Decorator : INode
    {
        public INode nextNode;
        /*
         * 조건이 참인지 거짓인지 정하는것입니다.
         * Condition 함수가 반환하는 값을가지고 참과 거짓중 다음 노드를 실행할지 말지 정하는 변수
         */
        protected bool isTrue = true;
        public Node_State Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
        
            this.OnStart();

            if (Condition(ref blackBoard) == isTrue)
            {
                this.OnEnd();
                return nextNode.Tick(ref blackBoard, ref parentStack);
            }
            this.OnEnd();
            return Node_State.Running;
        }

        protected virtual bool Condition(ref BlackBoard blackBoard)
        {
            return true;
        }

        public void OnStart()
        {
           
        }
        public void OnEnd()
        {
    
        }
    }

    public class Selector : INode
    {
        private Dictionary<int, INode> nodes;

        public Selector()
        {
            nodes = new Dictionary<int, INode>();
        }

        public void AddNode(int priority, INode node)
        {
            nodes.Add(priority, node);
        }


        public Node_State Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
            
            this.OnStart();

            // 여러 노드들은 전부 득록한다.
            parentStack.Push(this);
            var desceningNodes = nodes.Reverse();
            foreach (var item in desceningNodes)
            {
                parentStack.Push(item.Value);
            }

            INode node;
            while (true)
            {
                node = parentStack.Pop();
                // 스텍에서 자기 자신이 나오면 빠져나온다.
                if (node == this)
                {
                    this.OnEnd();
                    return Node_State.Failure;
                }

                if (node.Tick(ref blackBoard, ref parentStack) == Node_State.Suceess)
                {
                    this.OnEnd();
                    while (node != this)
                    {
                        node = parentStack.Pop();
                        if (node == this)
                            break;
                    }
                    return Node_State.Suceess;
                }
            }
        }

        public void OnStart()
        {

        }
        public void OnEnd()
        {

        }
    }

    /*
     * 자식 노드를 순서대로 실행
     * 깊이 우선탐색
     */

    public class Sequence : INode
    {  
        private Dictionary<int, INode> nodes;

        public Sequence()
        {
            nodes = new Dictionary<int, INode>();
        }

        public void AddNode(int priority, INode node)
        {
            nodes.Add(priority, node);
        }

        public Node_State Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
            this.OnStart();

            parentStack.Push(this);

            var desceningNodes = nodes.Reverse();
            foreach (var item in desceningNodes)
            {
                parentStack.Push(item.Value);
            }
            
            INode node;
            while (true)
            {
                node = parentStack.Pop();
                // 스텍에서 자기 자신이 나오면 빠져나온다.
                if (node == this)
                {
                    this.OnEnd();
                    return Node_State.Suceess;
                }

                if (node.Tick(ref blackBoard,ref parentStack) == Node_State.Failure)
                {
                    this.OnEnd();
                    while (node != this)
                    {
                        node = parentStack.Pop();
                        if (node == this)
                            break;
                    }
                    return Node_State.Failure;
                }
            }
        }

        public void OnStart()
        {

        }
        public void OnEnd()
        {

        }
    }

    /*
     *  실재 행동
     */
    public class Action : INode
    {
        public void OnStart()
        {

        }
        public void OnEnd()
        {

        }


        protected virtual Node_State BT_Update(ref BlackBoard blackBoard)
        {
            return Node_State.Failure;
        }

        public Node_State Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
           
            // 액션 실행부분 
            this.OnStart();
            Node_State ret = this.BT_Update(ref blackBoard);
            this.OnEnd();
            return ret;
        }
    }
}
