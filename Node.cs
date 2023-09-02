using System;

namespace ConsoleApp1
{
    public class NodeTest
    {
        public static void Test()
        {
            var rootNode = new Node<int>(1);

            var l1 = new Node<int>(2);
            var r1 = new Node<int>(3);

            rootNode.Left = l1;
            rootNode.Right = r1;

            var l2 = new Node<int>(4);
            var r2 = new Node<int>(5);

            l1.Left = l2;
            l1.Right = r2;

            var l3 = new Node<int>(6);
            var r3 = new Node<int>(7);

            r1.Left = l3;
            r1.Right = r3;

            var l4 = new Node<int>(8);

            var r4 = new Node<int>(9);

            l2.Left = l4;

            r3.Right = r4;

            Console.WriteLine("InOrder Traversal");
            Node<int>.InOrderTraverse(rootNode);

            Console.WriteLine("PostOrder Traversal");
            Node<int>.PostOrderTraverse(rootNode);

            TestConnect();


        }

        private static void TestConnect()
        {
            var rootNode = new Node<string>("A");

            var l1 = new Node<string>("B");
            var r1 = new Node<string>("C");

            rootNode.Left = l1;
            rootNode.Right = r1;

            var l2 = new Node<string>("D");
            var r2 = new Node<string>("E");

            l1.Left = l2;
            l1.Right = r2;

            var l3 = new Node<string>("F");
            //var r3 = new Node<string>("G");

            r1.Left = l3;
            //r1.Right = r3;

            //var l4 = new Node<int>(8);

            //var r4 = new Node<int>(9);

            //l2.Left = l4;

            //r3.Right = r4;

            //A
            //B-C
            //D-E, F-Null

            ConnectSiblings.Connect(rootNode);
            Node<string>.InOrderTraverseWithNextSibling(rootNode);
        }
    }
    public class Node<T>
    {
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> NextSibling { get; set; }

        public T Data { get; private set; }
        public Node(T data)
        {
            Data = data;
            Left = Right = NextSibling = null;
        }

        public static void InOrderTraverseWithNextSibling(Node<T> node)
        {
            if (node == null) return;

            //Traverse left
            InOrderTraverseWithNextSibling(node.Left);

            Console.WriteLine("Node Data= {0}",node.Data);
            Console.WriteLine("Node Sibling Data= {0}", node.NextSibling != null ? node.NextSibling.Data.ToString() : "");
            //Traverse right
            InOrderTraverseWithNextSibling(node.Right);
        }

        public static void InOrderTraverse(Node<T> node)
        {
            if (node == null) return;

            //Traverse left
            InOrderTraverse(node.Left);

            Console.WriteLine(node.Data);

            //Traverse right
            InOrderTraverse(node.Right);
        }

        public static void PostOrderTraverse(Node<T> node)
        {
            if (node == null) return;

            //Traverse left
            PostOrderTraverse(node.Left);

            //Traverse right
            PostOrderTraverse(node.Right);

            Console.WriteLine(node.Data);
            Console.WriteLine("{0}", node.NextSibling != null ? node.NextSibling.Data.ToString() : "");

        }

    }

    public class ConnectSiblings
    {
        //Nodes are formed already using the left and right node

        public static void Connect<T>(Node<T> currentNode)
        {
            if (currentNode == null) return;

            //Iterate vertically Depth-wise and Horizontally Breadth-wise 
            //At each level - iterate breadth-wise and assign and then move to next level

            //Vertically iterating
            while (currentNode != null)
            {
                var horizontalCurrentNode = currentNode;

                //Horizontal movement - start with current node and move to right next
                while (horizontalCurrentNode != null)
                {
                    if (horizontalCurrentNode.Left != null)
                    {
                        if (horizontalCurrentNode.Right != null)
                        {
                            horizontalCurrentNode.Left.NextSibling = horizontalCurrentNode.Right;
                        }
                        else//check in the sibling of the current node
                        {
                            horizontalCurrentNode.Left.NextSibling = AssignFromCNSibling(horizontalCurrentNode);
                        }
                    }

                    if (horizontalCurrentNode.Right != null)
                    {
                        horizontalCurrentNode.Right.NextSibling = AssignFromCNSibling(horizontalCurrentNode);
                    }

                    horizontalCurrentNode = horizontalCurrentNode.NextSibling;
                }

                //Move to next level
                currentNode = currentNode.Left ?? currentNode.Right ?? AssignFromCNSibling(currentNode);
            }
        }

        private static Node<T> AssignFromCNSibling<T>(Node<T> currentNode)
        {
            if (currentNode.NextSibling == null) return null;

            var cn = currentNode;

            while (cn != null && cn.NextSibling != null)
            {
                if (cn.NextSibling.Left != null) return cn.NextSibling.Left;
                else if (cn.NextSibling.Right != null) return cn.NextSibling.Right;
                else cn = cn.NextSibling ?? null;
            }

            return null;
        }
    }
}


