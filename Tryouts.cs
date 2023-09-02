using System;
using System.Collections.Generic;

namespace DSA_Prac2
{
    public class Tryouts
    {
        public static void Test()
        {
            //      9
            //  4           20
            //1   6     15     170
            var root = new Tryouts.Node(9);

            root.Left = new Tryouts.Node(4);
            root.Left.Left = new Tryouts.Node(1);
            root.Left.Right = new Tryouts.Node(6);

            root.Right = new Tryouts.Node(20);
            root.Right.Left = new Tryouts.Node(15);
            root.Right.Right = new Tryouts.Node(170);

            var cls = new Tryouts();

            //cls.BinaryTreeTraversal(root);

            cls.BreadthFirstTraversal(root);

            var queue = new Queue<Node>();
            queue.Enqueue(root);
            var collector = new List<int>();

            cls.BreadthFirstTraversalR(queue, collector);
        }

        public class Node
        {
            public int NodeVal { get; set; }
            public Node Left { get; set; } = null;
            public Node Right { get; set; } = null;
            public Node Next { get; set; } = null;
            public Node Previous { get; set; } = null;

            public Node(int val)
            {
                NodeVal = val;
            }
        }


        

        public void BreadthFirstTraversalR(Queue<Node> treeInQ, List<int> collector)
        {
            //      9
            //  4           20
            //1   6     15     170
            //Ans: 9, 4, 20, 1, 6, 15, 170

            if (treeInQ.Count == 0) return;

            var node = treeInQ.Dequeue();
            collector.Add(node.NodeVal);

            Console.WriteLine(node.NodeVal);

            if (node.Left != null)
                treeInQ.Enqueue(node.Left);

            if (node.Right != null)
                treeInQ.Enqueue(node.Right);

            BreadthFirstTraversalR(treeInQ, collector);
        }

        public List<int> BreadthFirstTraversal(Node root)
        {
            //      9
            //  4           20
            //1   6     15     170
            //Ans: 9, 4, 20, 1, 6, 15, 170
            if (root == null) return null;

            var collector = new List<int>();
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                collector.Add(node.NodeVal);

                Console.WriteLine(node.NodeVal);

                if(node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }
                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }

            return collector;
        }

        public void BinaryTreeTraversal(Node root)
        {
            var collector = new List<int>();
            PreOrderTraversal(root, collector);
            InOrderTraversal(root);
        }

        /// <summary>
        /// Root,Left,Right
        /// </summary>
        /// <param name="root"></param>
        private void PreOrderTraversal(Node node, List<int> collector)
        {
            if (node == null) return;

            Console.WriteLine(node.NodeVal);
            collector.Add(node.NodeVal);
            PreOrderTraversal(node.Left, collector);
            PreOrderTraversal(node.Right, collector);

        }

        /// <summary>
        /// Left,Root,Right
        /// </summary>
        /// <param name="root"></param>
        private void InOrderTraversal(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
