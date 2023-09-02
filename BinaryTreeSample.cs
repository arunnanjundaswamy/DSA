using System;
using System.Collections.Generic;
using static DSA_Prac2.BinaryTreeSample;

namespace DSA_Prac2
{
    //1. Diff Binary tree and Binary search tree
    // In BST, all the nodes towards left should have the value less than the root and right of its parent node
    public class BinaryTreeSample
    {
        public Node nodeBeginnerPointer = null;
        public Node previousNodePointer = null;

        public class Node
        {
            public int NodeVal { get; set; }
            public Node Left { get; set; } = null;
            public Node Right { get; set; } = null;
            public Node Next { get; set; } = null;

            public Node(int val)
            {
                NodeVal = val;
            }
        }
        public static void  Test()
        {
            CloneBinaryTreeTest();

            BreadthFirstTraversalTest();


            TraverseNodeTest();

            TraverseNodeIerativePreOrderTest();

            InOrderIterativeTest();

            PrintSprialTraverseOfTreeTest();


            InvertBinaryTreeTest();

            FindKthSmallestInBSTTest();

            FindKthLargestInBSTTest();

            FindDiameterTest();

            FindLongestSequenceTest();

            VerticalSumTest();

            ConnectToRightSiblingTest();

            IsValidBSTTest();

            ValidateIsBSTAndGetSizeOfBSTTest();

            //Imp
            FindSumOfNodesAtSpecificLevelTest();

            //Imp
            CalculateSumOfEachPathTest();

            //Imp
            MaxValuePathTest();

            //Imp
            GetSumOfDepthOfEachNodeTest();

            //Imp
            GetBottomViewOfBinaryTreeTest();

            //Imp
            GetLeftViewOfBinaryTreeTest();

            //Imp
            GetRightViewOfBinaryTreeTest();

            //Imp
            OverwriteRightPropertyToSibling();


            PrintSpiralMatrix();

            //Imp -- To be revisited
            //ConvertToDLL()


            //BFS
            //BreadthFirstTraversal(root);








            //Imp
            //var cltr = new List<int>();
            //CalculateSumOfEachPath(root, 0, cltr, false);
            //Same as above
            //MaxValuePath(root);

            //Imp:
            //Console.WriteLine($"GetSumOfDepthOfEachNode: {GetSumOfDepthOfEachNode(root, 0)}");


            //Invertbinary tree
            //tree.InvertBinaryTree(root);
            //Console.WriteLine("After reversal");
            //tree.BreadthFirstTraversal(root);//this is just to test

            //Console.WriteLine("ValidateIsBSTAndGetSizeOfBST");  
            //Console.WriteLine(tree.ValidateIsBSTAndGetSizeOfBST(root).Ans);

            //Console.WriteLine("ConvertToDLL");
            //tree.ConvertToDLL(root);
            //Console.WriteLine(tree.nodeBeginnerPointer);

            //var dict = new Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail>();
            //GetBottomViewOfBinaryTree(root, 0, 0, dict);

            //var dictLeft = new Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail>();
            //GetLeftViewOfBinaryTree(root, 0, dictLeft);

            //dictLeft = new Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail>();
            //GetRightViewOfBinaryTree(root, 0, dictLeft);


        }

        public static void CloneBinaryTreeTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            CloneBinaryTree(root);
        }

        public static Node CloneBinaryTree(Node root)
        {
            if (root == null) return null;

            var newNode = new Node(root.NodeVal);

            newNode.Left = CloneBinaryTree(root.Left);
            newNode.Right = CloneBinaryTree(root.Right);

            return newNode;
        }

        private static void BreadthFirstTraversalTest()
        {
             //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            BreadthFirstTraversal(root);

            //Incorrect
            //BreadthFirstTraversalR(root);

        }


        private static List<int> BreadthFirstTraversal(Node root)
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

                if (node.Left != null)
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

        private static void BreadthFirstTraversalR(Queue<Node> treeInQ, List<int> collector)
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

        

        private static void TraverseNodeTest()
        {
            //Forming BST since that is also a BT
            var root = new Node(6);

            root.Left = new Node(2);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(5);

            root.Right = new Node(8);
            root.Right.Left = new Node(7);
            root.Right.Right = new Node(9);

            //Pre-Order: Root should be the first and then left, right: Root, Left, Right
            //In-Order: Root should be at the center: Left, Root, Right
            //Post-Order: Root Should be at the end: Left, Right, Root

            //Root:6
            //In-order
            //1,2,5,6,7,8,9
            //Pre-order
            //6,2,1,5,8,7,9
            //Post-order
            //1,5,2,7,9,8,6

            TraverseNode(root);


            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            TraverseNode(root);
        }


        private static void TraverseNode(Node node)
        {
            //Pre-Order: Root should be the first and then left, right: Root, Left, Right
            //In-Order: Root should be at the center: Left, Root, Right
            //Post-Order: Root Should be at the end: Left, Right, Root

            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            if (node == null)
                return;

            //Root:6
            //In-order
            //1,2,5,6,7,8,9

            //Root:6
            //Pre-order
            //1,2,5,6,7,8,9

            //Pre-order
            //Console.WriteLine("Pre-order");
            //Console.WriteLine(node.NodeVal);
            TraverseNode(node.Left);
            //In-order
            //Console.WriteLine("In-order");
            //Console.WriteLine(node.NodeVal);
            TraverseNode(node.Right);
            //post-order
            //Console.WriteLine("post-order");
            Console.WriteLine(node.NodeVal);

        }

        private static void TraverseNodeIerativePreOrderTest()
        {
            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var list = TraverseNodeIerativePreOrder(root);

            Console.WriteLine($"Pre-order data {string.Join(",", list)}");
        }

        /// <summary>
        /// Depthfirst approach
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<int> TraverseNodeIerativePreOrder(Node node)
        {
            //This can be in the pre-order mode
            //We shall take that data to an array
            //      9
            //  4           20
            //1   6     15     170
            //Pre-order
            //9,4,1,6,20,15,170

            var collector = new List<int>();

            if (node == null) return null;

            var processingStack = new Stack<Node>();
            processingStack.Push(node);

            while (processingStack.Count > 0)
            {
                var currentNode = processingStack.Pop();
                collector.Add(currentNode.NodeVal);
                Console.WriteLine(currentNode.NodeVal);

                //Will add the right node and then the left node to the stack, so that the left node gets processed firt
                //Pre-order is: Root, Left, Right

                if (currentNode.Right != null)
                    processingStack.Push(currentNode.Right);//Important observation

                if (currentNode.Left != null)
                    processingStack.Push(currentNode.Left);

            }
            return collector;

        }

        public static void TraverseNodeIerativeInOrderTest()
        {
            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var list = TraverseNodeIerativeInOrder(root);

            Console.WriteLine($"In-order data {string.Join(",", list)}");
        }

        /// <summary>
        /// Depthfirst approach
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<int> TraverseNodeIerativeInOrder(Node node)
        {
            //This can be in the In-order mode
            //We shall take that data to an array
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170

            //Ref: https://www.geeksforgeeks.org/inorder-tree-traversal-without-recursion/
            //Take into stack all the left nodes till the leaf node
            //Pop that last node and consider the right of that node
            //Take all the left nodes of that right node again and follow this (above step again and again)
            var stack = new Stack<Node>();
            var collector = new List<int>();

            var currentNode = node;
            var done = false;

            while (!done)
            {
                //Go to the depth towards left
                if (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.Left;
                }
                else //once left is done
                {
                    if (stack.Count > 0)
                    {
                        currentNode = stack.Pop();
                        collector.Add(currentNode.NodeVal);
                        Console.WriteLine(currentNode.NodeVal);
                        currentNode = currentNode.Right;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }

            return collector;
        }

        private static void InOrderIterativeTest()
        {
            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            InOrderIterative(root);
        }

        /// <summary>
        /// Good Approach
        /// </summary>
        public static void InOrderIterative(Node node)
        {
            if (node == null) return;

            var currentNode = node;
            var stack = new Stack<Node>();

            currentNode = node;

            while (currentNode != null || stack.Count > 0)
            {
                while (currentNode != null)
                {
                    stack.Push(currentNode);

                    currentNode = currentNode.Left;
                }

                var lastNode = stack.Pop();

                Console.WriteLine(lastNode.NodeVal);

                currentNode = lastNode.Right;
            }
        }

        public static void InvertBinaryTreeTest()
        {
            //Input:
            //      9
            //  4           20
            //1   6     15     170

            //Output:
            //      9
            //  20           4
            //170   15     6     1
            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            InvertBinaryTree(root);
        }

        public static void InvertBinaryTree(Node node)
        {
            if (node == null) return;

            var temp = node.Left;
            node.Left = node.Right;
            node.Right = temp;

            InvertBinaryTree(node.Left);
            InvertBinaryTree(node.Right);
        }

        private static void FindKthSmallestInBSTTest()
        {
            //Forming BST since that is also a BT
            var root = new Node(6);

            root.Left = new Node(2);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(5);

            root.Right = new Node(8);
            root.Right.Left = new Node(7);
            root.Right.Right = new Node(9);

            FindKthSmallestInBST(root, 7);
        }

        /// <summary>
        /// In BST all the elements towards left or either smallest or largest than the current element
        /// E.g. if 4 is root then it can be 1,2,4,5,6
        /// So if applied In-line traverse, since it traverses from left to right (or right to left) it will return ordered.
        /// If left is small then, it will be in ascending
        /// </summary>
        private static void FindKthSmallestInBST(Node node, int k)
        {
            //In-order: Left, Root, Right
            //1,2,5,6,7,8,9

            int val = -1000;
            int cnt = -1;
            FindKthSmallestInBST_TraverseNode1(node, ref cnt, k, ref val);
            Console.Write("Kth Smallest Number=");
            Console.WriteLine(val);

            var itemsAsFlatList = new List<int>();
            FindKthSmallestInBST_TraverseNode(node, itemsAsFlatList);
            Console.Write("Kth Smallest Number=");
            Console.WriteLine(itemsAsFlatList[k - 1]);
        }

        

        /// <summary>
        /// This is most efficient, since it only iterates k number of times
        /// </summary>
        /// <param name="node"></param>
        /// <param name="count"></param>
        /// <param name="k"></param>
        /// <param name="val"></param>
        public static void FindKthSmallestInBST_TraverseNode1(Node node, ref int count, int k, ref int val)
        {
            if (node == null || count >= k)
                return;

            count = count + 1;
            val = node.NodeVal;
            FindKthSmallestInBST_TraverseNode1(node.Left, ref count, k, ref val);


            if (count < k)
            {
                Console.WriteLine("Count={0}", count);
                Console.WriteLine("node.NodeVal={0}", node.NodeVal);
                val = node.NodeVal;
                FindKthSmallestInBST_TraverseNode1(node.Right, ref count, k, ref val);
            }
        }

        public static void FindKthSmallestInBST_TraverseNode(Node node, List<int> itemsAsFlatList)
        {
            if (node == null)
                return;

            FindKthSmallestInBST_TraverseNode(node.Left, itemsAsFlatList);
            //In-order
            //Console.WriteLine("In-order");
            Console.WriteLine(node.NodeVal);
            itemsAsFlatList.Add(node.NodeVal);

            FindKthSmallestInBST_TraverseNode(node.Right, itemsAsFlatList);
        }




        private static void FindKthLargestInBSTTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            FindKthLargestInBST(root, 20);

        }

        /// <summary>
        /// In BST all the elements towards left or either smallest or largest then the current element
        /// E.g. if 4 is root then it can be 1,2,4,5,6
        /// So if applied In-line traverse, since it traverses from left to right (or right to left) it will return ordered.
        /// If left is small then, it will be in ascending
        /// Here the order can be reverse i.e. it will be in descending order by changing traversing from 'right-left'
        /// </summary>
        private static void FindKthLargestInBST(Node node, int k)
        {
            var val = 0;
            int cnt = -1;
            FindKthLargestInBST_TraverseNode1(node, ref cnt, k, ref val);
            Console.WriteLine("Kth Largest Number");
            Console.WriteLine(val);

            var itemsAsFlatList = new List<int>();

            FindKthLargestInBST_TraverseNode(node, itemsAsFlatList);

            Console.WriteLine("Kth Largest Number");
            Console.WriteLine(itemsAsFlatList[k - 1]);
        }

        private static void FindKthLargestInBST_TraverseNode(Node node, List<int> itemsAsFlatList)
        {
            if (node == null)
                return;

            FindKthLargestInBST_TraverseNode(node.Right, itemsAsFlatList);
            //In-order
            //Console.WriteLine("In-order");
            Console.WriteLine(node.NodeVal);
            itemsAsFlatList.Add(node.NodeVal);
            FindKthLargestInBST_TraverseNode(node.Left, itemsAsFlatList);
        }

        /// <summary>
        /// This is most efficient, since it only iterates k number of times
        /// </summary>
        /// <param name="node"></param>
        /// <param name="loopCount"></param>
        /// <param name="k"></param>
        /// <param name="val"></param>
        private static void FindKthLargestInBST_TraverseNode1(Node node, ref int loopCount, int k, ref int val)
        {
            if (node == null || loopCount >= k)
                return;

            FindKthLargestInBST_TraverseNode1(node.Right, ref loopCount, k, ref val);
            //In-order
            //Console.WriteLine("In-order");
            loopCount++;

            if (loopCount < k)
            {
                Console.WriteLine(node.NodeVal);
                val = node.NodeVal;
                FindKthLargestInBST_TraverseNode1(node.Left, ref loopCount, k, ref val);
            }
        }

        private static void ConnectToRightSiblingTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            ConnectToRightSibling(root);
        }


        private static void ConnectToRightSibling(Node node)
        {
            //Top down approach
            //Travese through all the nodes and connect each node to the right of it

            //Since we are connecting each node to the right sibling, we need to start from the right of the root

            //Conditions we check to assign the Next property of the node
            //1. Check whether node has both left and right
            //--Yes:
            //Assign: Node.Next = Node.Right
            //2. If Node has any one (either left or right), then check whether the next sibling has left or right
            //-If Next sibling has left or right, consider that
            //      9
            //  4           20
            //1   6     15     170

            if (node == null) return;

            if (node.Left != null)
            {
                node.Left.Next = node.Right != null ? node.Right : FindFromSiblingNode(node.Next);
            }
            if (node.Right != null)
            {
                node.Right.Next = FindFromSiblingNode(node.Next);
            }

            ConnectToRightSibling(node.Right);
            ConnectToRightSibling(node.Left);
        }

        private static Node FindFromSiblingNode(Node node)
        {
            if (node == null) return null;

            if (node.Left != null) return node.Left;

            if (node.Right != null) return node.Right;

            if (node.Next != null) return FindFromSiblingNode(node.Next);

            return null;
        }


        private static void IsValidBSTTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            IsValidBST(root);

            var q = new Queue<Node>();
            q.Enqueue(root);

            IsValidBSTR(q);
        }

        /// <summary>
        /// The Breadth first approach helps, since it verifies at the each level
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool IsValidBST(Node node)
        {
            if (node == null) return false;

            var q = new Queue<Node>();
            q.Enqueue(node);

            while (q.Count > 0)
            {
                var currentNode = q.Dequeue();
                Console.WriteLine(currentNode.NodeVal);

                if (currentNode.Left != null)
                {
                    if (currentNode.Left.NodeVal > currentNode.NodeVal)
                    {
                        return false;
                    }
                    q.Enqueue(currentNode.Left);
                }

                if (currentNode.Right != null)
                {
                    if (currentNode.Right.NodeVal < currentNode.NodeVal)
                    {
                        return false;
                    }
                    q.Enqueue(currentNode.Right);
                }
            }

            return true;
        }

        /// <summary>
        /// The Breadth first apporach helps, since it verifies at the each level
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool IsValidBSTR(Queue<Node> q)
        {
            if (q == null || q.Count == 0) return true;

            var currentNode = q.Dequeue();
            Console.WriteLine(currentNode.NodeVal);

            if (currentNode.Left != null)
            {
                if (currentNode.Left.NodeVal > currentNode.NodeVal)
                {
                    return false;
                }
                q.Enqueue(currentNode.Left);
            }

            if (currentNode.Right != null)
            {
                if (currentNode.Right.NodeVal < currentNode.NodeVal)
                {
                    return false;
                }
                q.Enqueue(currentNode.Right);
            }

            return IsValidBSTR(q);
        }


        private static void ValidateIsBSTAndGetSizeOfBSTTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            ValidateIsBSTAndGetSizeOfBST(root);
        }

        private static ValidateIsBSTAndGetSizeOfBSTProps ValidateIsBSTAndGetSizeOfBST(Node node)
        {
            //      9
            //  4           20
            //1   6     15     170

            if (node == null)
            {
                return new ValidateIsBSTAndGetSizeOfBSTProps(0, Int32.MinValue, Int32.MaxValue, 0, true);
            }
            if (node.Left == null && node.Right == null)
            {
                return new ValidateIsBSTAndGetSizeOfBSTProps(1, node.NodeVal, node.NodeVal, 1, true);
            }

            var left = ValidateIsBSTAndGetSizeOfBST(node.Left);
            var right = ValidateIsBSTAndGetSizeOfBST(node.Right);

            var size = left.Size + right.Size + 1;

            if (left.IsValidBST && right.IsValidBST && left.MaxVal < node.NodeVal && right.MinVal > node.NodeVal)
            {

                var min = Math.Min(left.MinVal, Math.Min(right.MinVal, node.NodeVal));
                var max = Math.Min(left.MaxVal, Math.Min(right.MaxVal, node.NodeVal));

                return new ValidateIsBSTAndGetSizeOfBSTProps(size, min, max, size, true);
            }

            var ans = Math.Max(left.Ans, right.Ans);
            return new ValidateIsBSTAndGetSizeOfBSTProps(size, -1, -1, ans, false);
        }

        public static void FindSumOfNodesAtSpecificLevelTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var interestedLevel = 2;

            int collector = 0;
            collector = FindSumAtLevel(root, interestedLevel, 0, 0);
            Console.WriteLine(collector);

            collector = FindSumAtLevelBFS(root, 2);
            Console.WriteLine($"Current level sum = {collector}");

            collector = 0;
            //Reference keep the object in heap
            FindSumAtLevelRef(root, interestedLevel, 0, ref collector);
            Console.WriteLine(collector);
        }

        //Can refer this as well
        private static int FindSumAtLevelBFS(Node root, int interestedLevel)
        {
            var q = new Queue<(Node node, int level)>();
            var collector = 0;

            q.Enqueue((root, 1));

            while(q.Count > 0)
            {
                var currentEl = q.Dequeue();

                if (currentEl.level > interestedLevel) break;

                if (currentEl.level == interestedLevel)
                    collector = collector + currentEl.node.NodeVal;

                if(currentEl.node.Left != null)
                {
                    q.Enqueue((currentEl.node.Left, currentEl.level + 1));
                }

                if (currentEl.node.Right != null)
                {
                    q.Enqueue((currentEl.node.Right, currentEl.level + 1));
                }
            }

            return collector;
        }


        private static int FindSumAtLevel(Node node, int interestedLevel, int nodeLevel, int collector)
        {
            if (node == null) return collector;

            if (nodeLevel > interestedLevel)
            {
                return collector;
            }

            if (nodeLevel == interestedLevel)
            {
                collector += node.NodeVal;
            }

            if (node.Left != null)
            {
                collector = FindSumAtLevel(node.Left, interestedLevel, nodeLevel + 1, collector);
            }

            if (node.Right != null)
            {
                collector = FindSumAtLevel(node.Right, interestedLevel, nodeLevel + 1, collector);
            }

            return collector;
        }

        private static void FindSumAtLevelRef(Node node, int interestedLevel, int nodeLevel, ref int collector)
        {
            if (node == null) return;

            if (nodeLevel > interestedLevel)
            {
                return;
            }

            if (nodeLevel == interestedLevel)
            {
                collector += node.NodeVal;
            }

            if (node.Left != null)
            {
                FindSumAtLevelRef(node.Left, interestedLevel, nodeLevel + 1, ref collector);
            }

            if (node.Right != null)
            {
                FindSumAtLevelRef(node.Right, interestedLevel, nodeLevel + 1, ref collector);
            }
        }

        private static void FindDiameterTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var maxDia = 0;
            FindDiameter(root, ref maxDia);
            Console.WriteLine($"max dia = {maxDia}");
        }


        /// <summary>
        /// Diameter is the longest path from leaf node in one side to the leaf node in the other side of a node
        /// Let each node return it's longest path on its right or left and that will be the input for the node above to calc dia
        /// Dia= left + right
        /// MaxPath (returned) = Max(left, right) + 1 (ie current node)
        /// //Dia variable - keep it as reference and keep updating it
        /// Post-order : since we need to get both left, right for each node (ie l,r,node)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        private static int FindDiameter(Node node, ref int dia)
        {
            if (node == null) return 0;

            int left = FindDiameter(node.Left, ref dia);
            int right = FindDiameter(node.Right, ref dia);

            //return the longest path
            var longestPathOfThisNode = Math.Max(left, right) + 1;

            //check if this node has max dia
            int diaOfThisNode = left + right;
            dia = Math.Max(dia, diaOfThisNode);

            return longestPathOfThisNode;
        }

        public static void FindLongestSequenceTest()
        {
            var root = new Node(1);

            root.Left = new Node(2);
            root.Left.Left = new Node(3);

            root.Right = new Node(4);
            root.Right.Left = new Node(5);
            root.Right.Right = new Node(6);
            root.Right.Left = new Node(7);


            int res = 0, currLength = 0;
            FindLongestSequence(root, root.NodeVal, ref currLength, ref res);
            Console.WriteLine("Longest numbers in sequence={0}", res);
        }

        private static void FindLongestSequence(Node node, int interestedNumber, ref int currLength, ref int res)
        {
            //https://www.geeksforgeeks.org/longest-consecutive-sequence-binary-tree/
            //It should be in sequence i.e. each child node should have an increment of 1 from its parent node
            //      1
            //  2          4
            //3         5     6
            //             7   
            //Ans: 1,2,3 - length or count:3 and not 1,4,6,7 - since it is not in sequence

            if (node == null) return;

            if (node.NodeVal == interestedNumber)
            {
                currLength++;
            }
            else
            {
                currLength = 0;
            }

            res = Math.Max(currLength, res);

            FindLongestSequence(node.Left, node.NodeVal + 1, ref currLength, ref res);
            FindLongestSequence(node.Right, node.NodeVal + 1, ref currLength, ref res);

        }

        /// <summary>
        /// Using Aux memory
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static void VerticalSumTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var collector = new Dictionary<int, int>();
            VerticalSum(root, 0, collector);

        }

        private static void VerticalSum(Node node, int d, Dictionary<int, int> collector)
        {
            if (node == null) return;

            if (collector.ContainsKey(d))
                collector[d] = collector[d] + node.NodeVal;
            else
                collector[d] = node.NodeVal;

            VerticalSum(node.Left, d - 1, collector);
            VerticalSum(node.Right, d + 1, collector);
        }

        private static void CalculateSumOfEachPathTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            CalculateSumOfEachPath(root, 0, new List<int>(), false);
        }


        //DFS
        //      9
        //  4           20
        //1   6     15     170
        //Once the leaf node is reached - add the sum to the list
        private static void CalculateSumOfEachPath(Node node, int sumOfThisPath, List<int> collector, bool isLeft)
        {
            //This means it has completed the leaf nodes 
            if (node == null) return;

            sumOfThisPath = sumOfThisPath + node.NodeVal;

            //That is in leaf node
            if (isLeft && node.Left == null)
            {
                Console.WriteLine(sumOfThisPath);
                collector.Add(sumOfThisPath);
                return;
            }
            else if (node.Right == null)
            {
                Console.WriteLine(sumOfThisPath);
                collector.Add(sumOfThisPath);
                return;
            }

            CalculateSumOfEachPath(node.Left, sumOfThisPath, collector, true);
            CalculateSumOfEachPath(node.Right, sumOfThisPath, collector, false);
        }

        private static void MaxValuePathTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var maxVal = MaxValuePath(root);
            Console.WriteLine($"Max value={maxVal}");
        }

        /// <summary>
        /// Finds the path that has max value for sum of nodes
        ///
        /// Note: the path can start from anywhere in the tree and need not be from the bottom
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static int MaxValuePath(Node node)
        {
            //      9
            //  4           -20
            //1   -6     15     17
            //Ans: 1+4+9 = 14

            var maxPath = 0;
            //MaxValuePath(node, 0, ref maxPath);
            maxPath = MaxValuePathUtil(node);
            return maxPath;
        }



        private static int MaxValuePathUtil(Node node)
        {
            if (node == null) return 0;

            var left = MaxValuePathUtil(node.Left);
            var right = MaxValuePathUtil(node.Right);

            var nodeValWithLeft = node.NodeVal + left;
            var nodeValWithRight = node.NodeVal + right;

            //Take whichever is max: left + current node or right + current node or only the current node
            var maxValOfBelow = Math.Max(nodeValWithLeft, nodeValWithRight);

            var max = Math.Max(node.NodeVal, maxValOfBelow);

            return max;
        }

        public static void GetSumOfDepthOfEachNodeTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var sum = GetSumOfDepthOfEachNode(root, 2);
            Console.WriteLine($"Sum = {sum}");
        }


        /// <summary>
        /// Each node should sum up the level of its child nodes
        /// So it should be like: Left, Right, Root I.e. Post-order I.e. complete the child and come to parent
        /// E.g For '4': The returned value should be 2(Left child1 '1') + 2(Right child 2 '6') + 1 (i.e for the '4' node or self)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <param name="isLeft"></param>
        /// <returns></returns>
        private static int GetSumOfDepthOfEachNode(Node node, int level)
        {
            //      9
            //  4           20
            //1   6     15     170
            //Ans: 10
            if (node.Left == null && node.Right == null)
                return level;

            int leftVal = 0, rightVal = 0;

            if (node.Left != null)
                leftVal = GetSumOfDepthOfEachNode(node.Left, level + 1);

            if (node.Right != null)
                rightVal = GetSumOfDepthOfEachNode(node.Right, level + 1);

            return leftVal + rightVal + level;

        }


        private static void GetBottomViewOfBinaryTreeTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            var dict = new Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail>();
            GetBottomViewOfBinaryTree(root, 0, 0, dict);
        }


        /// <summary>
        /// This gets the bottom view of the tree
        /// Unique Horizontal levels 'hlevel' and highest 'level' should be printed or collected in dict
        /// </summary>
        /// <param name="node"></param>
        /// <param name="hLevel"></param>
        /// <param name="level"></param>
        /// <param name="nodeDetail"></param>
        private static void GetBottomViewOfBinaryTree(Node node, int hLevel, int level, Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail> nodeDetail)
        {
            //      9
            //  4           20
            //1    6     15     170
            //Bottom View:1,4,6,15,170
            //15 will overlap 6


            if (node == null) return;

            if (!nodeDetail.ContainsKey(hLevel))
            {
                nodeDetail[hLevel] = new GetBottomViewOfBinaryTreeNodeDetail() { Level = level, Node = node };
            }
            else if (level >= nodeDetail[hLevel].Level)//Only if the level is greater than the previous level and hence put the level in dictionary
            {
                nodeDetail[hLevel] = new GetBottomViewOfBinaryTreeNodeDetail() { Level = level, Node = node };
            }

            GetBottomViewOfBinaryTree(node.Left, hLevel - 1, level + 1, nodeDetail);
            GetBottomViewOfBinaryTree(node.Right, hLevel + 1, level + 1, nodeDetail);
        }

        private static void GetLeftViewOfBinaryTreeTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            GetLeftViewOfBinaryTree(root, 0, new Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail>());
        }




        /// <summary>
        /// This gets the left view of the tree
        /// Unique Horizontal levels 'hlevel' and highest 'level' should be printed or collected in dict
        /// </summary>
        /// <param name="node"></param>
        /// <param name="hLevel"></param>
        /// <param name="level"></param>
        /// <param name="nodeDetail"></param>
        private static void GetLeftViewOfBinaryTree(Node node, int level, Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail> nodeDetail)
        {

            //int[,] arr = new int[2, 2] { { 1, 1 }, { 2, 2 } };
            //arr.GetLength(0);
            //arr.GetLength(1);
            //      9
            //  4           20
            //1    6     15     170
            //Left View:9,4,1

            if (node == null) return;

            if (!nodeDetail.ContainsKey(level))
            {
                nodeDetail[level] = new GetBottomViewOfBinaryTreeNodeDetail() { Level = level, Node = node };
            }

            GetLeftViewOfBinaryTree(node.Left, level + 1, nodeDetail);
            GetLeftViewOfBinaryTree(node.Right, level + 1, nodeDetail);
        }

        private static void GetRightViewOfBinaryTreeTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //In-order
            //1,4,6,9,15,20,170
            //Pre-order
            //9,4,1,6,20,15,170
            //Post-order
            //1,6,4,15,170,20,9

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            GetRightViewOfBinaryTree(root, 0, new Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail>());

        }
        private static void GetRightViewOfBinaryTree(Node node, int level, Dictionary<int, GetBottomViewOfBinaryTreeNodeDetail> nodeDetail)
        {
            //      9
            //  4           20
            //1    6     15     170
            //Right View:9, 20, 170

            if (node == null) return;

            //If a node with the same level is already traversed it will not be added again -- so no override
            if (!nodeDetail.ContainsKey(level))
            {
                nodeDetail[level] = new GetBottomViewOfBinaryTreeNodeDetail() { Level = level, Node = node };
            }

            GetRightViewOfBinaryTree(node.Right, level + 1, nodeDetail);
            GetRightViewOfBinaryTree(node.Left, level + 1, nodeDetail);
        }

        public static void PrintSprialTraverseOfTreeTest()
        {
            //      9
            //  4           20
            //1   6     15     170
            //Ans: 9,4,20,170,15,6,1

            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            //PrintSprialTraverseOfTree(root);

            // To be tested
            PrintSprialTraverseOfTreeFromViggu(root);
        }

        //
        private static void PrintSprialTraverseOfTreeFromViggu(Node node)
        {
            if(node == null) return;

            var ltr = true;

            var q = new Queue<Node>();
            q.Enqueue(node);

            while(q.Count > 0)
            {
                int levelSize = q.Count;
                var levelNodes = new List<int>();

                for(var levelCnt = 0; levelCnt < levelSize; levelCnt++)
                {
                    var nodeDqd = q.Dequeue();
                    if (ltr)
                        levelNodes.Add(nodeDqd.NodeVal);
                    else
                        levelNodes.Insert(0, nodeDqd.NodeVal);

                    if(node.Left != null)
                        q.Enqueue((Node)node.Left);

                    if (node.Right != null)
                        q.Enqueue((Node)node.Right);
                }

                Console.WriteLine(string.Join(",", levelNodes));

                ltr = !ltr;
            }

        }

        private static void PrintSprialTraverseOfTree(Node node)
        {
            var s1 = new Stack<Node>();
            var s2 = new Stack<Node>();

            s1.Push(node);
            while (s1.Count > 0 || s2.Count > 0)
            {
                while (s1.Count > 0)
                {
                    var nodeInStack = s1.Pop();
                    Console.WriteLine("Node Val {0}", nodeInStack.NodeVal);

                    if (nodeInStack.Right != null)
                        s2.Push(nodeInStack.Right);

                    if (nodeInStack.Left != null)
                        s2.Push(nodeInStack.Left);
                }

                while (s2.Count > 0)
                {
                    var nodeInStack = s2.Pop();
                    Console.WriteLine("Node Val {0}", nodeInStack.NodeVal);

                    if (nodeInStack.Left != null)
                        s1.Push(nodeInStack.Left);

                    if (nodeInStack.Right != null)
                        s1.Push(nodeInStack.Right);
                }
            }
        }



        ///// <summary>
        ///// Need to be in the sequence and hence in-order
        ///// </summary>
        ///// <param name="node"></param>
        //public static void ConvertToDLL(Node node)
        //{
        //    //      9
        //    //  4           20
        //    //1   6     15     170
        //    //Starts from 1
        //    if (node == null) return;

        //    ConvertToDLL(node.Left);

        //    //this will indicate it is the very first leaf node that has been hit
        //    if (previousNodePointer == null)
        //    {
        //        nodeBeginnerPointer = node;//Points to 1
        //        previousNodePointer = node;
        //    }
        //    else
        //    {
        //        node.Left = previousNodePointer;
        //        previousNodePointer.Right = node;

        //        previousNodePointer = node;
        //    }

        //    ConvertToDLL(node.Right);
        //}




        /// <summary>
        /// Here rather than the right property pointing to the right child of it, it should point to the right sibling
        /// Bottom-up approach
        /// </summary>
        public static void OverwriteRightPropertyToSibling()
        {
            var root = new Node(9);

            root.Left = new Node(4);
            root.Left.Left = new Node(1);
            root.Left.Right = new Node(6);

            root.Right = new Node(20);
            root.Right.Left = new Node(15);
            root.Right.Right = new Node(170);

            MutateRightProperty(root, null, false);
        }

        private static void MutateRightProperty(Node node, Node parent, bool isLeftNode)
        {
            if (node == null) return;

            var left = node.Left;
            var right = node.Right;

            MutateRightProperty(left, node, true);

            //In-Order since you get access to Left, Root and Right
            if (parent == null)
            {
                node.Right = null;
            }
            else if (isLeftNode)
            {
                node.Right = parent.Right;
            }
            else
            {
                if (parent.Right != null)
                    node.Right = parent.Right.Left;
                else
                    node.Right = null;
            }

            MutateRightProperty(right, node, false);
        }











        

        

        

        

        //private void MaxValuePath(Node node, int levelSum, ref int maxPathVal)
        //{
        //    if (node == null) return;

        //    levelSum = levelSum + node.NodeVal;

        //    if (node.Left == null && node.Right == null)
        //    {
        //        maxPathVal = Math.Max(maxPathVal, levelSum);
        //    }
        //    MaxValuePath(node.Left, levelSum, ref maxPathVal);
        //    MaxValuePath(node.Right, levelSum, ref maxPathVal);
        //}


        

        

        

        

        public static void PrintSpiralMatrix()
        {
            /*
             * https://www.educative.io/edpresso/spiral-matrix-algorithm
             Input:     {{1,    2,   3,   4},
                        {5,    6,   7,   8},
                        {9,   10,  11,  12},
                        {13,  14,  15,  16 }}
Output: 1 2 3 4 8 12 16 15 14 13 9 5 6 7 11 10 
Explanation: The output is matrix in spiral format.

            1. Row: Top and Col: L->R (should not vist again this row so Top++)
            2. Col: Right and Row: T->B (should not vist again this Col so Right--)
            3. Row: Bottom and Col: R-> L (should not vist again this Row so Bottom--)
            4. Col: Top and Row: B->T (should not vist again this Col so Left++)
             */

            int[,] arr = { { 1, 2, 3, 4 },
                            { 5, 6, 7, 8 },
                            { 9, 10, 11, 12 },
                            { 13, 14, 15, 16 } };


            int left = 0, top = 0, right = arr.GetLength(1) - 1, bottom = arr.GetLength(0) - 1;
            //direction keep chanding based on the 
            int dir = 0;

            Console.WriteLine("Spiral form");
            Console.WriteLine("");

            while (left <= right && top <= bottom)
            {
                if (dir == 0)
                {
                    for (int index = left; index <= right; index++)
                    {
                        Console.Write(" {0}", arr[top, index]);
                    }
                    top++;
                    dir = 1;
                }
                if (dir == 1)
                {
                    for (int index = top; index <= bottom; index++)
                    {
                        Console.Write(" {0}", arr[index, right]);
                    }
                    right--;
                    dir = 2;
                }
                if (dir == 2)
                {
                    for (int index = right; index >= left; index--)
                    {
                        Console.Write(" {0}", arr[bottom, index]);
                    }
                    bottom--;
                    dir = 3;
                }
                if (dir == 3)
                {
                    for (int index = bottom; index <= top; index++)
                    {
                        Console.Write(" {0}", arr[index, left]);
                    }
                    left++;
                    dir = 0;
                }
            }

        }
    }

    public class SerializeAndDeSerializeTree
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
            int index = -1;
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
            index++;

            if (list[index] == -1 || index >= list.Count)
            {
                return null;
            }

            var node = new Node(list[index]);

            node.Left = Deserialize(list, ref index);
            node.Right = Deserialize(list, ref index);

            return node;
        }        
    }

    

    public class GetBottomViewOfBinaryTreeNodeDetail
    {
        public int Level { get; set; }
        public Node Node { get; set; }
    }

    public class ValidateIsBSTAndGetSizeOfBSTProps
    {
        public int Size { get; set; }
        public int MinVal { get; set; }
        public int MaxVal { get; set; }
        public int Ans { get; set; }
        public bool IsValidBST { get; set; }

        public ValidateIsBSTAndGetSizeOfBSTProps(int size, int minVal, int maxVal, int ans, bool isValid)
        {
            this.Ans = ans;
            this.IsValidBST = isValid;
            this.MaxVal = maxVal;
            this.MinVal = minVal;
            this.Size = size;
        }
    }
}
