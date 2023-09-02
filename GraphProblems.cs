using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA_Prac2
{
    /*
     https://www.youtube.com/watch?v=tWVWeAqZ0WU

    https://www.geeksforgeeks.org/connected-components-in-an-undirected-graph/
    https://www.geeksforgeeks.org/find-number-of-islands/

     */


    public class GraphProblems
    {
        public class Node
        {
            public string name;
            public int value;
            public List<Node> children = new List<Node>();

            public Node(string name)
            {
                this.name = name;
            }
            public Node(int value)
            {
                this.value = value;
            }
        }

        public static void Test()
        {
            BFS();

            DFS();
        }

        #region BFS

        private static void BFS()
        {
            /*
                Imagine BFS like concentric circles
                Each circle covers the set of nodes attached (neighbous) to it with the same distance (or radius)
             */

            BreadthFirstSearchTest();

            CanReachNodeBFSTest();

            CanReachNodeUnDirectedCyclicBFSTest();

            FindShortestPathTest();

            FindRiversTest();


            WordLadderTest();
        }

        private static void BreadthFirstSearchTest()
        {
            //      9
            //  4           20
            //1   6     15     170

            var root = new Node(9);
            var child1 = new Node(4);
            var child2 = new Node(20);
            root.children = new List<Node> { child1, child2 };

            var child11 = new Node(1);
            var child12 = new Node(6);
            child1.children = new List<Node> { child11, child12 };


            var child21 = new Node(15);
            var child22 = new Node(170);
            child1.children = new List<Node> { child21, child22 };

            var arr = BreadthFirstSearch(root);
            arr.ForEach(item => Console.WriteLine(item));
        }

        private static List<int> BreadthFirstSearch(Node root)
        {
            // Write your code here.
            var queue = new Queue<Node>();
            var array = new List<int>();

            queue.Enqueue(root);//this represents root node

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                array.Add(currentNode.value);

                //Store all the current node children
                if (currentNode.children != null)
                    currentNode.children.ForEach(rec => queue.Enqueue(rec));
            }
            return array;
        }

       

        private static void CanReachNodeBFSTest()
        {
            /*
               1->2
               |
               3<-8
               |
               5->4
            */

            /*
             Source 8, Dest 1
            Ans: False

            Source 1, Dest 5
            Ans: True
             */

            var root = new Node(1);
            var child1 = new Node(2);
            var child2 = new Node(3);
            root.children = new List<Node> { child1, child2 };

            var child8 = new Node(8);
            child8.children = new List<Node> { child2 };

            var child5 = new Node(5);
            child2.children = new List<Node> { child5 };

            var child4 = new Node(4);
            child5.children = new List<Node> { child4 };

            var source = child8;
            var dest = root;
            var val = CanReachNodeBFS(source, dest);
            Console.WriteLine($"Can reach={val}");

            var dest1 = child5;
            val = CanReachNodeBFS(source, dest1);
            Console.WriteLine($"Can reach={val}");
        }

        private static bool CanReachNodeBFS(Node source, Node dest)
        {
            var q = new Queue<Node>();

            q.Enqueue(source);

            while (q != null && q.Count > 0)
            {
                var current = q.Dequeue();

                if (current.value == dest.value) return true;

                if (current.children != null && current.children.Count > 0)
                {
                    foreach (var neighbour in current.children)
                    {
                        q.Enqueue(neighbour);
                    }
                }
            }

            return false;
        }

        private static void CanReachNodeUnDirectedCyclicBFSTest()
        {
            /*
               1<->2
               |
               3<->8
               |
               5<->4

            and 8<->1 also
            */

            /*
             Source 8, Dest 1
            Ans: True

            Source 1, Dest 5
            Ans: True
             */

            /*
             For UnDirected cyclic graphs we need to check if it is already 'visited'
             */

            var root = new Node(1);
            var child1 = new Node(2);
            var child2 = new Node(3);
            child1.children = new List<Node> { root };


            var child8 = new Node(8);
            root.children = new List<Node> { child1, child2, child8 };
            child8.children = new List<Node> { child2, root };

            var child5 = new Node(5);
            child2.children = new List<Node> { root, child5, child8 };

            var child4 = new Node(4);
            child5.children = new List<Node> { child4, child2 };
            child4.children = new List<Node> { child5 };

            var source = child8;
            var dest = root;
            var val = CanReachNodeUnDirectedCyclicBFS(source, dest);
            Console.WriteLine($"Can reach={val}");

            var dest1 = child5;
            val = CanReachNodeUnDirectedCyclicBFS(source, dest1);
            Console.WriteLine($"Can reach={val}");
        }

        private static bool CanReachNodeUnDirectedCyclicBFS(Node source, Node dest)
        {
            var visited = new HashSet<Node>();

            var q = new Queue<Node>();
            q.Enqueue(source);

            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (visited.Contains(current)) continue;

                visited.Add(current);

                if (current.value == dest.value) return true;

                if (current.children != null)
                {
                    foreach (var neighbour in current.children)
                    {
                        if (!visited.Contains(neighbour))
                        {
                            q.Enqueue(neighbour);
                        }
                    }
                }
            }

            return false;
        }

        private static void FindShortestPathTest()
        {
            /*
             Finding shortest path is possible only using BFS and not DFS

             */

            int[,] graph = new int[,]{{ 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                      { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                      { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                      { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                      { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                      { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
                                      { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
                                      { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                      { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };

            //start = 0,0
            //dest = graph.Len, graph.Len

            var visited = new HashSet<string>();

            var q = new Queue<(int row, int col, int distance)>();

            q.Enqueue((0, 0, 0));

            var shortestPath = FindShortestPath(graph, q, visited, (4, 3));

            Console.WriteLine($"Shortest Path = {shortestPath}");
        }

        private static int FindShortestPath(int[,] graph, Queue<(int row, int col, int distance)> q, HashSet<string> visited, (int row, int col) dest)
        {
            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (visited.Contains($"{current.row}:{current.col}"))
                    continue;

                if (current.row == dest.row && current.col == dest.col)
                    return current.distance;

                visited.Add($"{current.row }:{current.col}");


                //The neighbours are top, left, right and bottom

                //top
                if (current.row - 1 >= 0 && current.col < graph.GetLength(1) && !visited.Contains($"{current.row - 1}:{current.col}"))
                    q.Enqueue((current.row - 1, current.col, current.distance + 1));

                //bottom
                if (current.row + 1 < graph.GetLength(0) && current.col < graph.GetLength(1) && !visited.Contains($"{current.row + 1}:{current.col}"))
                    q.Enqueue((current.row + 1, current.col, current.distance + 1));

                //left
                if (current.row < graph.GetLength(0) && current.col - 1 >= 0 && !visited.Contains($"{current.row}:{current.col - 1}"))
                    q.Enqueue((current.row, current.col - 1, current.distance + 1));

                //right
                if (current.col + 1 < graph.GetLength(1) && !visited.Contains($"{current.row}:{current.col + 1}"))
                    q.Enqueue((current.row, current.col + 1, current.distance + 1));
            }
            return -1;
        }

        private static void WordLadderTest()
        {
            /*Imagine as shortest path problem - hence bfs
             https://www.geeksforgeeks.org/word-ladder-length-of-shortest-chain-to-reach-a-target-word/
            Given a dictionary, and two words ‘start’ and ‘target’ (both of same length). Find length of the smallest chain from ‘start’ to ‘target’ if it exists, such that adjacent words in the chain only differ by one character and each word in the chain is a valid word i.e., it exists in the dictionary. It may be assumed that the ‘target’ word exists in dictionary and length of all dictionary words is same.

            Input: Dictionary = {POON, PLEE, SAME, POIE, PLEA, PLIE, POIN},
            start = TOON, target = PLEA
            Output: 7
            Explanation: TOON – POON – POIN – POIE – PLIE – PLEE – PLEA

            Input: Dictionary = {ABCD, EBAD, EBCD, XYZA},
            start = ABCV, target = EBAD
            Output: 4
            Explanation: ABCV – ABCD – EBCD – EBAD

            Convert one word to another using the words in the dictionary by changing only one letter at-a-time

            ABCV-1)ABCD-2)EBCD-3)EBAD

             Input: [ABC, ABD, CBC, CBF, CEF]
            Start: ABC Target: CEF

            ABC->1)ABD or 1) CBC -> 2) CBF (possible only from CBF) -> 3)CEF
             */

            string start = "TOON", target = "PLEA";
            var dictionary = new HashSet<string> { "POON", "PLEE", "SAME", "POIE", "PLEA", "PLIE", "POIN" };
            var steps = WordLadder(start, target, dictionary);

            Console.WriteLine($"Number of steps={steps}");   
        }

        private static int WordLadder(string start, string target, HashSet<string> dictionary)
        {
            var queue = new Queue<string>();
            var level = 0;//at each level there can be multiple matches (e.g. ABD and ABF for ABC above)


            if (start == target) return 0;
            if (!dictionary.Contains(target)) return 0;

            dictionary.Remove(start);//no need to search for the matched word

            queue.Enqueue(start);

            while(queue.Count > 0)
            {
                level++;

                var countOfCurrentLevel = queue.Count;

                for (int i = 0; i < countOfCurrentLevel; i++)
                {
                    var word = queue.Dequeue();

                    //check for replacing the letter from 'A' to 'Z' and
                    //check if that exists in the dictionary

                    for (int charIndex = 0; charIndex < word.Length; charIndex++)
                    {
                        for (char c = 'A'; c <= 'Z'; c++)
                        {
                            var clonedWord = string.Join("", word);

                            if (c == clonedWord[charIndex]) continue;

                            var charArr = clonedWord.ToArray();

                            charArr[charIndex] = c;

                            var newWord = string.Join("", charArr);// clonedWord.su(clonedWord[charIndex], c);


                            if(newWord == target)
                            {
                                return level+1;
                            }

                            if(dictionary.Contains(newWord))
                            {
                                queue.Enqueue(newWord);
                                dictionary.Remove(newWord);//remove the matched word
                            }
                        }
                    }
                }

            }

            return 0;
        }

        

        

        //Using BFS but possible in DFS
        private static void FindRiversTest()
        {
            int[,] mat = {
                    {0, 0, 1, 1, 0},
                    {1, 0, 1, 1, 0},
                    {0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 1},
                    {0, 0, 1, 1, 0}
                  };
            //Ans: 5 rivers

            bool[,] visited = new bool[mat.GetLength(0), mat.GetLength(1)];

            FindRivers(mat, visited);
        }

        private static int FindRivers(int[,] mat, bool[,] visited)
        {
            int rowLength = mat.GetLength(0);
            int colLength = mat.GetLength(1);
            int totalIsLands = 0;
            for (int row = 0; row < mat.GetLength(0); row++)
            {
                for (int col = 0; col < mat.GetLength(1); col++)
                {
                    if (mat[row, col] == 1 && !visited[row, col])
                    {
                        TraverseConnectedLands(mat, visited, row, col);
                        totalIsLands++;
                    }
                }
            }
            return totalIsLands;
        }

        private static void TraverseConnectedLands(int[,] mat, bool[,] visited, int row, int col)
        {

            //check for all the 4 sides for each item

            //using queue to add the nodes to be explored
            var queue = new Queue<int[]>();
            queue.Enqueue(new int[] { row, col });

            while (queue.Count > 0)
            {
                var currenyNode = queue.Dequeue();
                row = currenyNode[0];
                col = currenyNode[1];
                visited[row, col] = true;


                //check top
                if (IsSafeToNavigate(mat, visited, row - 1, col))
                {
                    queue.Enqueue(new int[] { row - 1, col });
                }

                //check bottom
                if (IsSafeToNavigate(mat, visited, row + 1, col))
                {
                    queue.Enqueue(new int[] { row + 1, col });
                }

                //check right
                if (IsSafeToNavigate(mat, visited, row, col + 1))
                {
                    queue.Enqueue(new int[] { row, col + 1 });
                }

                //check left
                if (IsSafeToNavigate(mat, visited, row, col - 1))
                {
                    queue.Enqueue(new int[] { row, col - 1 });
                }
            }
        }

        private static bool IsSafeToNavigate(int[,] mat, bool[,] visited, int row, int col)
        {
            int rowLength = mat.GetLength(0);
            int colLength = mat.GetLength(0);
            var isSafe = row < 0 || row >= rowLength || col < 0 || col >= colLength ? false : true;

            if (!isSafe) return false;
            if (visited[row, col]) return false;
            return mat[row, col] == 1;
        }
        #endregion BFS

        #region DFS

        private static void DFS()
        {
            DFSTest();

            CanReachNodeByDFSTest();

            CanReachNodeUnDirectedCyclicDFSTest();

            GetIndependentNodesTest();

            FindNumberOfIslandsTest();

            FindLongestPathInMatrixTest();
        }

        private static void FindNumberOfIslandsTest()
        {
            //Typically this has to be the DFS since we can traverse to all the connected nodes
            //https://www.geeksforgeeks.org/find-number-of-islands/
            /*
             Input : mat[][] = {{1, 1, 0, 0, 0},
                   {0, 1, 0, 0, 1},
                   {1, 0, 0, 1, 1},
                   {0, 0, 0, 0, 0},
                   {1, 0, 1, 0, 1}}
                Output : 5
             */

            var mat = new int[,]{
                    { 1, 1, 0, 0, 0},
                    { 0, 1, 0, 0, 1},
                    { 1, 0, 0, 1, 1},
                    { 0, 0, 0, 0, 0},
                    { 1, 0, 1, 0, 1}
            };

            var visited = new HashSet<string>();
            var count = 0;
            var minSize = int.MaxValue;

            for (int row = 0; row < mat.GetLength(0); row++)
            {
                for (int col = 0; col < mat.GetLength(1); col++)
                {
                    if (!visited.Contains($"{row}:{col}") && mat[row, col] == 1)
                    {
                        var sizeOfIsland = 0;
                        FindNumberOfIslands(mat, row, col, visited, ref sizeOfIsland);

                        minSize = Math.Min(minSize, sizeOfIsland);
                        count++;
                    }
                }
            }

            Console.WriteLine($"Number of Islands={count}");
            Console.WriteLine($"Island with min size={minSize}");

        }

        private static void FindNumberOfIslands(int[,] mat, int row, int col, HashSet<string> visited, ref int sizeOfIsland)
        {
            //Base condition
            if (row < 0 || row >= mat.GetLength(0) || col < 0 || col >= mat.GetLength(1) || mat[row, col] == 0)
                return;

            if (visited.Contains($"{row}:{col}")) return;

            visited.Add($"{row}:{col}");
            sizeOfIsland++;

            //Check if there are connected islands in left top right and left, and diagonals

            //left
            FindNumberOfIslands(mat, row, col - 1, visited, ref sizeOfIsland);

            //right
            FindNumberOfIslands(mat, row, col + 1, visited, ref sizeOfIsland);

            //top
            FindNumberOfIslands(mat, row - 1, col, visited, ref sizeOfIsland);

            //bottom
            FindNumberOfIslands(mat, row + 1, col, visited, ref sizeOfIsland);

            //top-left diagonal
            FindNumberOfIslands(mat, row - 1, col - 1, visited, ref sizeOfIsland);

            //bottom-left diagonal
            FindNumberOfIslands(mat, row + 1, col - 1, visited, ref sizeOfIsland);

            //top-right diagonal
            FindNumberOfIslands(mat, row - 1, col + 1, visited, ref sizeOfIsland);

            //bottom-right diagonal
            FindNumberOfIslands(mat, row + 1, col + 1, visited, ref sizeOfIsland);
        }

        private static void DFSTest()
        {
            //      9
            //  4           20
            //1   6     15     170

            var root = new Node(9);
            var child1 = new Node(4);
            var child2 = new Node(20);
            root.children = new List<Node> { child1, child2 };

            var child11 = new Node(1);
            var child12 = new Node(6);
            child1.children = new List<Node> { child11, child12 };


            var child21 = new Node(15);
            var child22 = new Node(170);
            child1.children = new List<Node> { child21, child22 };

            var arr = DepthFirstSearch(root);
            arr.ForEach(item => Console.WriteLine(item));

            var resultList = new List<int>();
            DepthFirstRecursive(root, resultList);
            resultList.ForEach(item => Console.WriteLine(item));
        }

        private static List<int> DepthFirstSearch(Node root)
        {
            var stack = new Stack<Node>();
            var list = new List<int>();
            stack.Push(root);

            while (stack != null && stack.Count > 0)
            {
                var currentNode = stack.Pop();
                list.Add(currentNode.value);

                Console.WriteLine($"Current Node= {currentNode.value}");

                if (currentNode.children != null)
                {
                    foreach (var neighbour in currentNode.children)
                    {
                        stack.Push(neighbour);
                    }
                }
            }

            return list;
        }

        private static void DepthFirstRecursive(Node currentNode, List<int> list)
        {
            if (currentNode == null)
            {
                return;
            }

            list.Add(currentNode.value);
            Console.WriteLine($"current node = {currentNode.value}");

            if (currentNode.children != null)
            {
                foreach (var neighbour in currentNode.children)
                {
                    DepthFirstRecursive(neighbour, list);
                }
            }
        }

        private static void CanReachNodeByDFSTest()
        {
            /*
                1->2
                |
                3<-8
                |
                5->4
             */

            /*
             Source 8, Dest 1
            Ans: False

            Source 1, Dest 5
            Ans: True
             */

            var root = new Node(1);
            var child1 = new Node(2);
            var child2 = new Node(3);
            root.children = new List<Node> { child1, child2 };

            var child8 = new Node(8);
            child8.children = new List<Node> { child2 };

            var child5 = new Node(5);
            child2.children = new List<Node> { child5 };

            var child4 = new Node(4);
            child5.children = new List<Node> { child4 };

            var source = child8;
            var dest = root;
            var val = CanReachNodeByDFS(source, dest);
            Console.WriteLine($"Can reach={val}");

            val = CanReachNodeByDFSRecursive(source, dest);
            Console.WriteLine($"Can reach={val}");

            var dest1 = child5;
            val = CanReachNodeByDFS(source, dest1);
            Console.WriteLine($"Can reach={val}");

            val = CanReachNodeByDFSRecursive(source, dest1);
            Console.WriteLine($"Can reach={val}");
        }

        private static bool CanReachNodeByDFS(Node source, Node dest)
        {
            var stack = new Stack<Node>();
            stack.Push(source);

            while (stack != null && stack.Count > 0)
            {
                var currentNode = stack.Pop();

                //I am able to reach
                if (currentNode.value == dest.value) return true;

                if (currentNode.children != null)
                {
                    foreach (var neighbour in currentNode.children)
                    {
                        stack.Push(neighbour);
                    }
                }
            }
            return false;
        }

        private static bool CanReachNodeByDFSRecursive(Node currentNode, Node dest)
        {
            if (currentNode.value == dest.value)
                return true;

            if (currentNode.children == null || currentNode.children.Count == 0) return false;

            foreach (var neighbour in currentNode.children)
            {
                if (CanReachNodeByDFSRecursive(neighbour, dest))
                    return true;
            }

            return false;
        }

        private static void CanReachNodeUnDirectedCyclicDFSTest()
        {
            /*
               1<->2
               |
               3<->8
               |
               5<->4

            and 8<->1 also
            */

            /*
             Source 8, Dest 1
            Ans: True

            Source 1, Dest 5
            Ans: True
             */

            /*
             For UnDirected cyclic graphs we need to check if it is already 'visited'
             */

            var root = new Node(1);
            var child1 = new Node(2);
            var child2 = new Node(3);
            child1.children = new List<Node> { root };


            var child8 = new Node(8);
            root.children = new List<Node> { child1, child2, child8 };
            child8.children = new List<Node> { child2, root };

            var child5 = new Node(5);
            child2.children = new List<Node> { root, child5, child8 };

            var child4 = new Node(4);
            child5.children = new List<Node> { child4, child2 };
            child4.children = new List<Node> { child5 };

            var source = child8;
            var dest = root;
            var val = CanReachNodeUnDirectedCyclicDFSRecursive(source, dest, new HashSet<Node>());
            Console.WriteLine($"Can reach={val}");

            var dest1 = child5;
            val = CanReachNodeUnDirectedCyclicDFSRecursive(source, dest1, new HashSet<Node>());
            Console.WriteLine($"Can reach={val}");
        }

        private static bool CanReachNodeUnDirectedCyclicDFSRecursive(Node currentNode, Node dest, HashSet<Node> visited)
        {
            if (currentNode.value == dest.value)
                return true;

            if(!visited.Contains(currentNode))
            {
                visited.Add(currentNode);

                if (currentNode.children.Count > 0)
                {
                    foreach (var neighbour in currentNode.children)
                    {
                        if (CanReachNodeUnDirectedCyclicDFSRecursive(neighbour, dest, visited))
                            return true;
                    }
                }
            }

            return false;
        }

        #region GetIndependentNodes
        private static void GetIndependentNodesTest()
        {
            /*
            0-8
            0-1
            0-5-8
            2-3
              |
            2-4
            Ans: 2 for the below case

            Same logic as in islands problem
             */

            var nodes = new Dictionary<int, List<int>>();
            nodes[0] = new List<int> { 8, 1, 5 };
            nodes[1] = new List<int> { 0 };
            nodes[5] = new List<int> { 0, 8 };
            nodes[8] = new List<int> { 0, 5 };
            nodes[2] = new List<int> { 3, 4 };
            nodes[3] = new List<int> { 2, 4 };
            nodes[4] = new List<int> { 3, 2 };

            var visited = new HashSet<int>();
            var count = 0;

            foreach (var item in nodes)
            {
                if (!visited.Contains(item.Key))
                {
                    FindConnectedNodes(item, visited, nodes);
                    count++;
                }
            }

            Console.WriteLine($"Total independents = {count}");
        }

        private static void FindConnectedNodes(KeyValuePair<int, List<int>> item, HashSet<int> visited, Dictionary<int, List<int>> nodes)
        {
            if (visited.Contains(item.Key)) return;

            visited.Add(item.Key);

            foreach (var child in item.Value)
            {
                if (!visited.Contains(child))
                    FindConnectedNodes(new KeyValuePair<int, List<int>>(child, nodes[child]), visited, nodes);
            }
        }
        #endregion GetIndependentNodes

        #region FindLongestPathInMatrixTest

        private static void FindLongestPathInMatrixTest()
        {
            /*
             * such that all cells along the path are in increasing order with a difference of 1. 
             * 
             Input:  mat[][] = {{1, 2, 9}
                               {5, 3, 8}
                               {4, 6, 7}}
            Output: 4
            The longest path is 6-7-8-9. 
             */

            var mat = new int[3, 3] {{ 1, 2, 9},
                                    { 5, 3, 8},
                                    { 4, 6, 7}
                                };

            var result = 0;
            var visited = new Dictionary<string, int>();

            for (int row = 0; row < mat.GetLength(0); row++)
            {
                for (int col = 0; col < mat.GetLength(1); col++)
                {
                    if (!visited.ContainsKey($"{row}:{col}"))
                    {
                        var maxPathLength = FindLongestPathForThisCell(mat, row, col, visited);
                        visited[$"{row}:{col}"] = maxPathLength;
                    }

                    result = Math.Max(result, visited[$"{row}:{col}"]);

                }
            }
        }

        private static int FindLongestPathForThisCell(int[,] mat, int row, int col, Dictionary<string, int> visited)
        {
            if (row < 0 || row >= mat.GetLength(1) || col < 0 || col >= mat.GetLength(0))
                return 0;

            if (visited.ContainsKey($"{row}:{col}"))
                return visited[$"{row}:{col}"];

            //check in all 4 directions if it matches the constraints i.e. should be greater by '1' from the current cell

            //Left
            var left = 0;
            if (col - 1 >= 0 && mat[row, col - 1] == (mat[row, col] + 1))
            {
                left = FindLongestPathForThisCell(mat, row, col - 1, visited);
            }

            //right
            var right = 0;
            if (col + 1 < mat.GetLength(0) && mat[row, col + 1] == (mat[row, col] + 1))
            {
                right = FindLongestPathForThisCell(mat, row, col + 1, visited);
            }

            //top
            var top = 0;
            if (row - 1 >= 0 && mat[row - 1, col] == (mat[row, col] + 1))
            {
                top = FindLongestPathForThisCell(mat, row - 1, col, visited);
            }

            //bottom
            var bottom = 0;
            if (row + 1 < mat.GetLength(1) && mat[row + 1, col] == (mat[row, col] + 1))
            {
                bottom = FindLongestPathForThisCell(mat, row + 1, col, visited);
            }

            //Move in the direction which is max
            var val = Math.Max(bottom, Math.Max(top, Math.Max(left, right))) + 1;

            visited[$"{row}:{col}"] = val;

            return val;
        }

        #endregion FindLongestPathInMatrixTest

        //same as above
        public static int LongestIncreasingPathTest()
        {
            int logestPath = 0;
            /*
             Input: matrix =    [[9,9,4],
                                [6,6,8],
                                [2,1,1]]
            Output: 4
            Explanation: The longest increasing path is [1, 2, 6, 9].
             */

            int[][] matrix = new int[3][];
            matrix[0] = new int[] { 9, 9, 4 };
            matrix[1] = new int[] { 6, 6, 8 };
            matrix[2] = new int[] { 2, 1, 1 };


            if (matrix == null || matrix.GetLength(0) == 0 || matrix[0].GetLength(0) == 0)
                return 0;

            var visited = new Dictionary<string, int>();

            var rows = matrix.Length;
            var cols = matrix[0].Length;

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    if (!visited.ContainsKey($"{rowIndex}:{colIndex}"))
                        LongestIncreasingPath(matrix, rowIndex, colIndex, visited, ref logestPath);
                }
            }

            //LongestIncreasingPath(matrix, 0, 0, visited, ref logestPath);
            return logestPath;
        }

        private static int LongestIncreasingPath(int[][] matrix, int x, int y, Dictionary<string, int> visited, ref int logestPath)
        {
            var rows = matrix.Length;
            var cols = matrix[0].Length;

            if (x < 0 || x >= rows || y < 0 || y >= cols)
                return 0;

            if (visited.ContainsKey($"{x}:{y}"))
                return visited[$"{x}:{y}"];

            int left = 0, right = 0, top = 0, bottom = 0;

            if (y - 1 >= 0 && matrix[x][y] < matrix[x][y - 1])
                left = //visited[$"{x}:{y - 1}"] =
                       LongestIncreasingPath(matrix, x, y - 1, visited, ref logestPath);
            if (y + 1 < cols && matrix[x][y] < matrix[x][y + 1])
                right = //visited[$"{x}:{y + 1}"] =
                        LongestIncreasingPath(matrix, x, y + 1, visited, ref logestPath);
            if (x - 1 >= 0 && matrix[x][y] < matrix[x - 1][y])
                top = //visited[$"{x - 1}:{y}"] =
                      LongestIncreasingPath(matrix, x - 1, y, visited, ref logestPath);
            if (x + 1 < rows && matrix[x + 1][y] > matrix[x][y])
                bottom = //visited[$"{x + 1}:{y}"] =
                         LongestIncreasingPath(matrix, x + 1, y, visited, ref logestPath);

            var max = Math.Max(Math.Max(Math.Max(left, right), top), bottom);

            logestPath = Math.Max(logestPath, max + 1);

            visited[$"{x}:{y}"] = max >= 0 ? max + 1 : 0;
            return max >= 0 ? max + 1 : 0;
        }


        #endregion DFS
    }
}
