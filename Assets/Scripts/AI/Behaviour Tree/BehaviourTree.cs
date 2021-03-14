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

    public enum Node_Kind
    {
        Root,
        Decorator,
        Sequence,
        Action,
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
        Node_State INode.Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
            INode thisNode = this;

            thisNode.OnStart();

            if (Condition(ref blackBoard) == isTrue)
            {
                thisNode.OnEnd();
                return nextNode.Tick(ref blackBoard, ref parentStack);
            }
            thisNode.OnEnd();
            return Node_State.Running;
        }

        protected virtual bool Condition(ref BlackBoard blackBoard)
        {
            return true;
        }


        void INode.OnStart()
        {
           
        }

        void INode.OnEnd()
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


        Node_State INode.Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
            INode thisNode = this;
            thisNode.OnStart();

            // 여러 노드들은 전부 득록한다.
            parentStack.Push(this);
            //KeyValuePair<int,INode> desceningNodes = nodes.OrderByDescending(x => x.Key);
            foreach (var item in nodes)
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
                    thisNode.OnEnd();
                    return Node_State.Failure;
                }

                if (node.Tick(ref blackBoard, ref parentStack) == Node_State.Suceess)
                {
                    thisNode.OnEnd();
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

        void INode.OnStart()
        {

        }
        void INode.OnEnd()
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
            // 키값으로 내림 차순으로 정렬
            //nodes.OrderByDescending(x => x.Key);
        }

        Node_State INode.Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
            INode thisNode = this;
            thisNode.OnStart();

            // 여러 노드들은 전부 득록한다.
            parentStack.Push(this);

            //var desceningNodes = nodes.OrderByDescending(x => x.Key);
            foreach (var item in nodes)
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
                    thisNode.OnEnd();
                    return Node_State.Suceess;
                }

                if (node.Tick(ref blackBoard,ref parentStack) == Node_State.Failure)
                {
                    thisNode.OnEnd();
                    return Node_State.Failure;
                }
            }
        }

        void INode.OnStart()
        {

        }
        void INode.OnEnd()
        {

        }
    }

    /*
     *  실재 행동
     */
    public class Action : INode
    {
        void INode.OnStart()
        {

        }
        void INode.OnEnd()
        {

        }


        protected virtual Node_State BT_Update(ref BlackBoard blackBoard)
        {
            return Node_State.Failure;
        }

        Node_State INode.Tick(ref BlackBoard blackBoard, ref Stack<INode> parentStack)
        {
           
            // 액션 실행부분 
            INode node = (INode)(this);
            node.OnStart();
            Node_State ret;
            while (true)
            {
                ret = this.BT_Update(ref blackBoard);
                // 러닝상태면 반복
                if (ret != Node_State.Running)
                {
                    // 액션 끝나는 부분
                    node.OnEnd();
                    return ret;
                }
            }
            
        }
    }
}
