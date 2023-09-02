using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DSA_Prac2.BinaryTreeSample;

namespace DSA_Prac2
{
    public class Practice1
    {
        public static void Test()
        {
            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            //ConvertTreeToDLL(root);

            //SpiralOrderTree();

            MaxValuePath(root);

            SerializeAndDeSerialize.Test();

            SerializeAndDeSerialize.Test2();

            var newNode = new Node(200);
            AAAA(newNode);
            Console.WriteLine(newNode.NodeVal);
        }

        private static void AAAA(Node node)
        {
            node = new Node(400);
            Console.WriteLine(node.NodeVal);
        }

        public class Node
        {
            public int NodeVal { get; set; }
            public Node Left { get; set; } = null;
            public Node Right { get; set; } = null;
            public Node Next { get; set; } = null;
            public int NodeLevel { get; set; } = 0;
            public Node(int val)
            {
                NodeVal = val;
            }
        }
        public class NodeWithLevel
        {
            public Node Node { get; set; } = null;
            public int NodeLevel { get; set; } = 0;
        }

        public static void SpiralOrderTree()
        {
            var root = new Node(1);

            root.Left = new Node(2);
            root.Left.Left = new Node(4);
            root.Left.Right = new Node(5);

            root.Right = new Node(3);
            root.Right.Left = new Node(6);
            root.Right.Right = new Node(7);

            root.Left.Left.Left = new Node(8);
            root.Left.Left.Right = new Node(4);

            root.Left.Right.Left = new Node(10);

            root.Right.Left.Left = new Node(16);

            root.Right.Right.Left = new Node(17);

            var q = new Stack<NodeWithLevel>();

            q.Push(new NodeWithLevel { Node = root, NodeLevel = 0 });

            SpiralOrderTree(q);
        }

        private static void SpiralOrderTree(Stack<NodeWithLevel> q)
        {
            if (q.Count == 0) return;

            var nodeWithLevel = q.Pop();

            var node = nodeWithLevel.Node;
            var level = nodeWithLevel.NodeLevel;

           
            if (level % 2 == 1)
            {
                //Print Right and then left
                if (node != null)
                {
                    Console.WriteLine(node.NodeVal);
                }
                if (node.Left != null)
                {
                    q.Push(new NodeWithLevel { Node = node.Left, NodeLevel = node.NodeLevel + 1 });
                }
                if (node.Right != null)
                {
                    q.Push(new NodeWithLevel { Node = node.Right, NodeLevel = node.NodeLevel + 1 });
                }

                SpiralOrderTree(q);
            }
            else if (level % 2 == 0)
            {
                //Print Left and then Right
                if (node != null)
                {
                    Console.WriteLine(node.NodeVal);
                }
                if (node.Right != null)
                {
                    q.Push(new NodeWithLevel { Node = node.Right, NodeLevel = node.NodeLevel + 1 });
                }
                if (node.Left != null)
                {
                    q.Push(new NodeWithLevel { Node = node.Left, NodeLevel = node.NodeLevel + 1 });
                }

                SpiralOrderTree(q);
            }
            
        }

        public static int MaxValuePath(Node node)
        {
            var maxPath = 0;
            MaxValuePath(node, 0, ref maxPath);
            return maxPath;

        }

        private static void MaxValuePath(Node node, int levelSum, ref int maxPathVal)
        {
            if (node == null) return;

            levelSum = levelSum + node.NodeVal;

            if (node.Left == null && node.Right == null)
            {
                maxPathVal = Math.Max(maxPathVal, levelSum);
            }
            MaxValuePath(node.Left, levelSum, ref maxPathVal);
            MaxValuePath(node.Right, levelSum, ref maxPathVal);
        }

        public void InOrderIterative(Node node)
        {
            if (node == null) return;

            var stack = new Stack<Node>();
            var currentNode = node;

            while(currentNode != null || stack.Count > 0)
            {
                while(currentNode != null)
                {
                    stack.Push(currentNode);

                    currentNode = currentNode.Left;
                }

                var lastNode = stack.Pop();
                Console.WriteLine(lastNode.NodeVal);

                currentNode = lastNode.Right;
            }

        }


        public static Node ConvertTreeToDLL(Node node)
        {
            //      9
            //  4           20
            //1   6     15     170
            //Starts from 1
            //should be in-order to be in sequence

            Node head = null;
            GetDLLFromTree(node, head);

            return head;

        }

        private static void GetDLLFromTree(Node node, Node head, Node parentPointer = null)
        {
            if (node == null) return;

            GetDLLFromTree(node.Left,head, parentPointer);

            if (parentPointer == null)//this will indicate it is the very first leaf node that has been hit
                parentPointer = head = node;
            else
            {
                node.Left = parentPointer;
                parentPointer.Right = node;
                parentPointer = node;
            }

            
            GetDLLFromTree(node.Right, head, parentPointer);
        }
    }

    public class SerializeAndDeSerialize
    {
        public static void Test()
        {
            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var serializedList = Serialize(root);
            int index = 0;
            var rootAgain = Deserialize(serializedList, ref index);

        }

        public static List<int> Serialize(Node node)
        {
            var list = new List<int>();
            GetSerializedList(node, list);
            return list;
        }

        /// <summary>
        /// Serialization will be in the pre-order
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list"></param>
        private static void GetSerializedList(Node node, List<int> list)
        {
            if (node == null)
            {
                list.Add(-1);
                return;
            }

            list.Add(node.NodeVal);
            GetSerializedList(node.Left, list);
            GetSerializedList(node.Right, list);
        }

        public static Node Deserialize(List<int> list, ref int index)
        {
            if(list[index] == -1)
            {
                return null;
            }

            var node = new Node(list[index]);
            index++;
            node.Left = Deserialize(list, ref index) ;
            index++;
            node.Right = Deserialize(list, ref index);
            return node;
        }

        public static void Test2()
        {
            var x = t1().GetAwaiter();
            
            x.GetResult();
            Console.WriteLine("Here it is");
        }

        public static async Task t1()
        {
            await Task.Delay(4000);
            Console.WriteLine("Here it is in t1");
        }
    }
}
