using System;
using System.Collections.Generic;
using ConsoleApp1;

namespace DSA_Prac2.Programming
{
    public class BackTracking
    {

        /// <summary>
        /// If everything is a solution, then it should be backtracking
        /// https://igotanoffer.com/blogs/tech/backtracking-interview-questions
        /// https://www.youtube.com/watch?v=XovjRfHumDU
        /// https://www.youtube.com/watch?v=DKCbsiDBN6c
        /// https://medium.com/algorithms-and-leetcode/backtracking-e001561b9f28
        /// https://algo.monster/problems/dfs_with_states
        /// https://algo.monster/problems/backtracking
        /// https://www.educative.io/courses/algorithms-ds-interview/RLzMZoXMoMO
        /// https://algodaily.com/lessons/recursive-backtracking-for-combinatorial-path-finding-and-sudoku-solver-algorithms
        /// </summary>
        public static void BackTrackingTest()
        {
            //Pattern:
            /*
             * All follow this approach
             Check whether it can be placed
                if yes, recursively move to next
                if no, return false and backtrack
                
             */

            //practice
            PrintPathOfTreeTest();

            //Easy:
            ReadBinaryWatch();

            PrintAllPermutationsTest();

            SubsetsTest();

            CombinationSumTest();

            WordsSearchTest();

            NQueenTest();

            RatAndMazeTest();

            FillSudoku();

            //note: this hangs- but right approach
            //KnightProblemTest();
        }

        private static void PrintPathOfTreeTest()
        {
            /*https://algo.monster/problems/backtracking
                1
             2      3

            Ans:    1->2
                    1->3
             */

            var root = new GraphProblems.Node(1);
            var ch1 = new GraphProblems.Node(2);
            var ch2 = new GraphProblems.Node(3);

            root.children.Add(ch1);
            root.children.Add(ch2);

            var pathList = new List<GraphProblems.Node>();
            pathList.Add(root);

            PrintPathOfTree(root, pathList);

        }

        private static void PrintPathOfTree(GraphProblems.Node node, List<GraphProblems.Node> pathList)
        {
            //reached leaf node
            if(node.children == null || node.children.Count == 0)
            {
                //print path
                Console.WriteLine(string.Join("->", pathList));
                return;
            }

            foreach (var child in node.children)
            {
                pathList.Add(child);
                PrintPathOfTree(child, pathList);

                //backtrack and remove
                pathList.Remove(child);
            }
        }

        private static void ReadBinaryWatch()
        {
            /*
             There are 4 hour slots: * * * *
             There are 6 min slots:  * * * * * *

            If any 1 slot is 'on' the possible time can be:
            Input: n = 1
            Return: ["1:00", "2:00", "4:00", "8:00", "0:01", "0:02", "0:04", "0:08", "0:16", "0:32"]

            Question for the passed 'n' find the possible times
             */

            var n = 1;
            for (int hour = 0; hour < 12; hour++)
            {
                for (int min = 0; min < 60; min++)
                {
                    var hr = BitProblems.GetNumberOfSetBits(hour);
                    var mins = BitProblems.GetNumberOfSetBits(min);
                    //hour: 4 i.e. 100 = hr:  1(number of setbits) + min: 0 = mins: 0(number of setbits) = 1 --true
                    //hour: 0 = hr:  0 + min: 16 = mins: 1  = 1 --true
                    //hour: 0 = hr:  0 + min: 15 = 01100 = mins: 2(number of setbits)  = 3 --false
                    if (hr + mins == n)
                        Console.WriteLine($"{hour}:{min.ToString("D2")}");
                }
            }
        }



        private static void SubsetsTest()
        {
            /*
             * https://www.youtube.com/watch?v=Yg5a2FxU4Fo&list=PL_z_8CaSLPWeT1ffjiImo0sYTcnLzo-wY&index=12
             * https://afteracademy.com/blog/print-all-subsets-of-a-given-set
             * https://github.com/ndesai15/coding-java/blob/master/src/com/coding/patterns/subsets/Subsets.java
             Input: array = {1, 2} 
                Output:  []   // this space denotes null element. 
                         1
                         1, 2
                         2

            Approach: consider that element or do not consider.
            Think this as binarytree and do DFS - one node considering the current element and other node not considering it.
            Check whether the condition is satisfied everytime. Leaf nodes should give the results.
             */

            var results = new List<List<int>>();
            FindSubsets(0, new List<int> { 1, 2 }, results, new List<int>());

            FindSubsets(new List<int> { 1, 2 }, new List<List<int>>(), new List<int>(), 0);
        }

        //can refer this as well
        private static void FindSubsets(List<int> inputArr, List<List<int>> results, List<int> path, int start)
        {
            //1,2
            //https://www.youtube.com/watch?v=LdtQAYdYLcE&t=434s
            //if (start >= inputArr.Count)
            //{
            //    Console.WriteLine($"The subset is: {string.Join(",", path)}");
            //    return;
            //}
            results.Add(new List<int>(path));//initially empty is added

            for (int index = start; index < inputArr.Count; index++)
            {
                path.Add(inputArr[index]);
                FindSubsets(inputArr, results, path, index + 1);

                path.Remove(inputArr[index]);
            }
        }

        private static void FindSubsets(int currentIndex, List<int> inputArr, List<List<int>> results, List<int> currentSubset)
        {
            if (currentIndex == inputArr.Count)
            {
                results.Add(new List<int>(currentSubset));
                return;
            }

            //Consider the current element
            currentSubset.Insert(0, inputArr[currentIndex]);//Adding at 0th index
            FindSubsets(currentIndex + 1, inputArr, results, currentSubset);

            //Do not consider the current element
            currentSubset.RemoveAt(0);//Adding at 0th index previously, removing now
            FindSubsets(currentIndex + 1, inputArr, results, currentSubset);
        }

        private static void CombinationSumTest()
        {
            /*
            1,2,3,6,7 and total = 7
            Ans: 6,1 and 7

        Approach: Think in the same approach as Subsets problem.
            Think this as binarytree and do DFS - one node considering the current element and other node not considering it.
            Check whether the condition is satisfied everytime.
        */
            var results = new List<List<int>>();
            CombinationSum(new int[] { 1, 2, 3, 6, 7 }, 7, new List<int>(), results, 0, 0);

            var input = new int[] { 21, 1, 3, 4 };
            var path = new List<int>();
            var target = 4;

            CombinationSum(input, target, path, results, 0);
        }

        //can refer this aswell
        //https://afteracademy.com/blog/combination-sum //another variant
        private static void CombinationSum(int[] inputArr, int target, List<int> path, List<List<int>> results, int start)
        {
            if (target < 0)
                return;

            if(target == 0)
            {
                results.Add(path);
                Console.WriteLine($"CombinationSum = [{string.Join(",", path)}]");
                return;
            }

            //the start will always makes to move in fwd direction only
            for (int i = start; i < inputArr.Length; i++)
            {
                target = target - inputArr[i];
                path.Insert(0, inputArr[i]);
                CombinationSum(inputArr, target, path, results, i + 1);

                target = target + inputArr[i];
                path.RemoveAt(0);
            }
        }

        //refer this
        private static void CombinationSum(int[] inputArr, int  target, List<int> current, List<List<int>> results, int currentIndex, int currentTotal)
        {
            
            if (currentTotal == target)
            {
                var elems = new List<int>();
                current.ForEach(i =>
                {
                    elems.Add(i);
                });

                results.Add(elems);
                Console.WriteLine($"CombinationSum = [{string.Join(",", elems)}]");
                return;
            }

            if(currentIndex >= inputArr.Length || currentTotal > target)
            {
                return;
            }

            // Include i
            currentTotal = currentTotal + inputArr[currentIndex];
            current.Insert(0, inputArr[currentIndex]);
            CombinationSum(inputArr, target, current, results, currentIndex + 1, currentTotal);

            // Not include i
            currentTotal = currentTotal - inputArr[currentIndex];//added previously removing now
            current.RemoveAt(0);
            CombinationSum(inputArr, target, current, results, currentIndex + 1, currentTotal);
        }

        private static void WordsSearchTest()
        {
            /*
                Input: board = [["A","B","C","E"],
                                ["S","F","C","S"],
                                ["A","D","E","E"]],
                    word = "ABCCED"
                Output: true
             */

            var mat = new string[,]
                                {{"A", "B", "C", "E" },
                                { "S", "F", "C", "S"},
                                { "A", "D", "E", "E" } };
            var word = "ABCCED";

            var visited = new HashSet<string>();

            for (int row = 0; row < mat.GetLength(0); row++)
            {
                for (int col = 0; col < mat.GetLength(1); col++)
                {
                    //if matrix cell starts with the word--then do the dfs to check for other characters
                    if(word[0].ToString() == mat[row, col] && !visited.Contains($"{row}:{col}"))
                    {
                        //DFS for that to check all 4 directions
                        if (WordsSearch(mat, word, row, col, visited, 0))
                        {
                            Console.WriteLine("Can find this word");
                            return;
                        }
                    }
                }
            }

        }

        private static bool WordsSearch(string[,] mat, string word, int row, int col, HashSet<string> visited, int currentLetterIndex)
        {
            if(row < 0 || row >= mat.GetLength(0) || col < 0 || col >= mat.GetLength(1) || visited.Contains($"{row}:{col}") || currentLetterIndex >= word.Length || word[currentLetterIndex].ToString() != mat[row, col])
            {
                return false;
            }

            if (currentLetterIndex == word.Length - 1)
                return true;

            visited.Add($"{row}:{col}");

            //check for next letter in the 'word' in all 4 directions
            var top = WordsSearch(mat, word, row - 1, col, visited, currentLetterIndex + 1);
            var bottom = WordsSearch(mat, word, row + 1, col, visited, currentLetterIndex + 1);
            var left = WordsSearch(mat, word, row, col -1, visited, currentLetterIndex + 1);
            var right = WordsSearch(mat, word, row, col + 1, visited, currentLetterIndex + 1);

            //remove from the visited -- Imp - hence added in backtracking
            visited.Remove($"{row}:{col}");
            return left || right || top || bottom;

        }


        private static void PrintAllPermutationsTest()
        {
            /*
             * Input: ABC
                Output: ABC ACB BAC BCA CBA CAB
             */

            PrintAllPermutations("ABC", 0, "ABC".Length - 1);

            var result = new List<List<char>>();
            var visited = new HashSet<int>();
            var path = new List<char>();

            PrintAllPermutations("ABC", path, result, visited);
        }

        //Refer this
        private static void PrintAllPermutations(string input, List<char> path, List<List<char>> result, HashSet<int> visited)
        {
            if(path.Count == input.Length)
            {
                result.Add(path);
                Console.WriteLine($"New Path is: {string.Join("", path)}");
                return;
            }

            //A - 0
            //A, B -0,1
            //A, B, C - 0,1,2
            //- returns
            //A, B , c (remove) and iterate to next but already reached '2' hence- return
            //A,B (remove) and iterate to next i.e. '2' and hence add 'C'
            //a,c,b (added)
            for (int i = 0; i < input.Length; i++)
            {
                if (visited.Contains(i)) continue;

                var visitedChar = input[i];
                visited.Add(i);
                path.Add(visitedChar);
                PrintAllPermutations(input, path, result, visited);

                //backtrack
                visited.Remove(i);
                path.Remove(visitedChar);
            }
        }



        private static void PrintAllPermutations(string input, int start, int end)
        {
            //https://algo.monster/problems/dfs_with_states
            //https://www.javatpoint.com/program-to-find-all-permutations-of-a-string
            //https://logicmojo.com/sub_videos/38

            if (start == end)
            {
                Console.WriteLine(input);
                return;
            }

            for (int index = start; index <= end; index++)
            {
                input = Swap(input, start, index);
                PrintAllPermutations(input, start + 1, end);
                input = Swap(input, start, index);//swap it back
            }

        }

        private static string Swap(string input, int start, int index)
        {
            var chars = input.ToCharArray();
            var temp = chars[start];
            chars[start] = chars[index];
            chars[index] = temp;
            return new String(chars);
        }

        private static void FillSudoku()
        {
            /*
             * https://www.geeksforgeeks.org/sudoku-backtracking-7/
             * https://dev.to/christinamcmahon/use-backtracking-algorithm-to-solve-sudoku-270
             Input:
                grid = { {3, 0, 6, 5, 0, 8, 4, 0, 0}, 
                         {5, 2, 0, 0, 0, 0, 0, 0, 0}, 
                         {0, 8, 7, 0, 0, 0, 0, 3, 1}, 
                         {0, 0, 3, 0, 1, 0, 0, 8, 0}, 
                         {9, 0, 0, 8, 6, 3, 0, 0, 5}, 
                         {0, 5, 0, 0, 9, 0, 6, 0, 0}, 
                         {1, 3, 0, 0, 0, 0, 2, 5, 0}, 
                         {0, 0, 0, 0, 0, 0, 0, 7, 4}, 
                         {0, 0, 5, 2, 0, 6, 3, 0, 0} }
                Output:
                          3 1 6 5 7 8 4 9 2
                          5 2 9 1 3 4 7 6 8
                          4 8 7 6 2 9 5 3 1
                          2 6 3 4 1 5 9 8 7
                          9 7 4 8 6 3 1 2 5
                          8 5 1 7 9 2 6 4 3
                          1 3 8 9 4 7 2 5 6
                          6 9 2 3 5 1 8 7 4
                          7 4 5 2 8 6 3 1 9
             */

            int[,] inputArr =   { { 3, 0, 6, 5, 0, 8, 4, 0, 0 },
                                   { 5, 2, 0, 0, 0, 0, 0, 0, 0 },
                                   { 0, 8, 7, 0, 0, 0, 0, 3, 1 },
                                   { 0, 0, 3, 0, 1, 0, 0, 8, 0 },
                                   { 9, 0, 0, 8, 6, 3, 0, 0, 5 },
                                   { 0, 5, 0, 0, 9, 0, 6, 0, 0 },
                                   { 1, 3, 0, 0, 0, 0, 2, 5, 0 },
                                   { 0, 0, 0, 0, 0, 0, 0, 7, 4 },
                                   { 0, 0, 5, 2, 0, 6, 3, 0, 0 } };

            SolveSudoku(inputArr);

        }

        private static bool SolveSudoku(int[,] inputArr)
        {
            var cell = GetEmptyCell(inputArr);

            //all cells filled
            if (cell.row == -1 || cell.col == -1)
                return true;

            //try to fill any of the numbers from 1to9

            for (int num = 0; num <= 9; num++)
            {
                if(IsSafeToPutThisNum(inputArr, cell.row, cell.col, num))
                {
                    inputArr[cell.row, cell.col] = num;

                    if (SolveSudoku(inputArr))
                    {
                        return true;
                    }

                    //Backtrack by putting 0
                    inputArr[cell.row, cell.col] = 0;

                }
            }

            return false;
        }

        private static bool IsSafeToPutThisNum(int[,] inputArr, int row, int col, int num)
        {
            //check in entire row that this number does not exist
            for (int index = 0; index <= 8; index++)
            {
                if (inputArr[row, index] == num) return false;
            }


            //check in entire col that this number does not exist
            for (int index = 0; index <= 8; index++)
            {
                if (inputArr[index, col] == num) return false;
            }

            var subRowStart = row - (row % 3);
            var subColStart = col - (col % 3);

            for (int subRow = subRowStart; subRow < 3; subRow++)
            {
                for (int subCol = subColStart; subCol < 3; subCol++)
                {
                    if (inputArr[subRow, subCol] == num) return false;
                }
            }

            return true;
        }

        private static (int row, int col) GetEmptyCell(int[,] inputArr)
        {
            for (int row = 0; row < inputArr.GetLength(0); row++)
            {
                for (int col = 0; col < inputArr.GetLength(1); col++)
                {
                    if (inputArr[row, col] == 0)
                        return (row, col);
                }
            }

            return (-1, -1);
        }

        

        private static void NQueenTest()
        {
            /*
             Let us discuss N Queen as another example problem that can be solved using backtracking. 
The N Queen is the problem of placing N chess queens on an N×N chessboard so that no two queens attack each other. For example, following is a solution for 4 Queen problem.

            The expected output is a binary matrix which has 1s for the blocks where queens are placed. For example, the following is the output matrix for above 4 queen solution.
 

              { 0,  1,  0,  0}
              { 0,  0,  0,  1}
              { 1,  0,  0,  0}
              { 0,  0,  1,  0}
             */

            var board = new int[4,4];

            //for (int row = 0; row < board.GetLength(0); row++)
            //{
            //    for (int col = 0; col < board.GetLength(1); col++)
            //    {
            //        board[row, col] = 0;
            //    }
            //}

            //Placing in the very first column
            //Quuen is moving only in vertical direction (one direction only)
            Console.WriteLine($"Can place  all queens {CanPlaceQueen(board, 0)}");
        }

        private static bool CanPlaceQueen(int[,] board, int col)
        {
            if (col >= board.GetLength(1))
                return true;

            for (int row = 0; row < board.GetLength(0); row++)
            {
                if(CheckIsSafePositionForQueen(board, row, col))
                {
                    board[row, col] = 1;
                    if (CanPlaceQueen(board, (col + 1)))
                    {
                        return true;
                    }

                    //backtracking - bcoz the next one cannot be placed bcoz of current queen
                    board[row, col] = 0;
                }
                
            }

            return false;

        }

        private static bool CheckIsSafePositionForQueen(int[,] board, int row, int col)
        {
            //If diagonally top left is not empty then false
            //If diagonally bottom left is not empty then false

            //If left row is not empty then false


            //If diagonally top left is not empty then false
            var tempRow = row;
            var tempCol = col;
            while (tempRow - 1 >= 0 && tempCol - 1 >= 0)
            {
                if (board[tempRow - 1, tempCol - 1] == 1)
                {
                    return false;
                }
                tempRow--;
                tempCol--;
            }

            tempRow = row;
            tempCol = col;
            while (tempRow + 1 < board.GetLength(0) && tempCol - 1 >= 0)
            {
                if (board[tempRow + 1, tempCol - 1] == 1)
                {
                    return false;
                }
                tempRow++;
                tempCol--;
            }
            
            tempRow = row;
            tempCol = col;
            while (tempCol - 1 >= 0)
            {
                if (board[tempRow, tempCol - 1] == 1)
                {
                    return false;
                }
                tempCol--;
            }

            return true;
        }


        private static void RatAndMazeTest()
        {
            /*
             * Rat can move only on 1
                {1, 0, 0, 0}
                {1, 1, 0, 1}
                {0, 1, 0, 0}
                {1, 1, 1, 1}

            Output:
            {1, 0, 0, 0}
            {1, 1, 0, 0}
            {0, 1, 0, 0}
            {0, 1, 1, 1}
            All entries in solution path are marked as 1.
             */
            int[,] inputArr = new int[4, 4]
                {   {1, 0, 0, 0},
                    {1, 1, 0, 0},
                    {0, 1, 0, 0},
                    {0, 1, 1, 1}};

            //Not reachable
            //int[,] inputArr = new int[4, 4]
            //    {{1, 0, 0, 0},
            //    {1, 1, 0, 0},
            //    {0, 1, 0, 0},
            //    {0, 1, 1, 0 }};

            int[,] output = new int[inputArr.GetLength(0), inputArr.GetLength(1)];

            RatAndMaze(inputArr, 0, 0, output);

            for (int i = 0; i < output.GetLength(0); i++)
            {
                Console.WriteLine(" ");
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    Console.Write($"{output[i, j]} ");
                }
            }
        }

        private static bool RatAndMaze(int[,] inputArr, int row, int col, int[,] output)
        {
            if (row == inputArr.GetLength(0) -1 && col == inputArr.GetLength(1) -1 && inputArr[row, col] == 1)
            {
                output[row, col] = 1;
                return true;//reached end
            }

            if (IsValidRatMove(inputArr, row, col))
            {
                output[row, col] = 1;
                if (RatAndMaze(inputArr, row + 1, col, output))
                    return true;

                if (RatAndMaze(inputArr, row, col + 1, output))
                    return true;

                output[row, col] = 0;
            }

            return false;
        }

        private static bool IsValidRatMove(int[,] inputArr, int row, int col)
        {
            return (row >= 0 && row < inputArr.GetLength(0) && col >= 0 && col < inputArr.GetLength(1) && inputArr[row, col] == 1) ;
        }


        //note: this hangs- but right approach
        private static void KnightProblemTest()
        {
            /*
             * https://www.tutorialspoint.com/The-Knight-s-tour-problem
             * https://medium.com/geekculture/knights-tour-with-backtracking-6d19a3934c7b
             https://www.geeksforgeeks.org/the-knights-tour-problem-backtracking-1/?ref=lbp
            https://www.codesdope.com/course/algorithms-knights-tour-problem/

            Problem: Check whether Knight Should cover all the cells
             */

            var board = new int[8, 8];

            //knight can move in 8 ways
            var possibleMovesLkp = new HashSet<(int xmove, int ymove)>() { (2,1), (1,2),(-2, 1), (1, -2), (-1, 2), (2,-1),(-1, -2), (-2,-1) };

            board[0, 0] = 1;//starts from first position
            var canCover = KnightProblem(board, possibleMovesLkp, 0, 0, 1);
            Console.WriteLine($"can kight cover all: {canCover}");
        }

        private static bool KnightProblem(int[,] board, HashSet<(int xmove, int ymove)> possibleMovesLkp, int row, int col, int totalMoves)
        {
            if (totalMoves == (board.GetLength(0) * board.GetLength(1)))
                return true;

            int nextRow, nextCol;

            foreach (var item in possibleMovesLkp)
            {
                nextRow = row + item.xmove;
                nextCol = col + item.ymove;

                if(CanPlaceKnight(board, nextRow, nextCol))
                {
                    board[nextRow, nextCol] =  1;

                    //backtrack if next moves fails in dfs
                    if (KnightProblem(board, possibleMovesLkp, nextRow, nextCol, totalMoves + 1))
                    {
                        return true;
                    }
                    else
                    {
                        //backtrack
                        board[nextRow, nextCol] = 0;
                    }
                    
                }
            }

            return false;

        }

        private static bool CanPlaceKnight(int[,] board, int nextRow, int nextCol)
        {
            return nextRow >= 0 && nextRow < board.GetLength(0) && nextCol >= 0 && nextCol < board.GetLength(1)
                && board[nextRow, nextCol] == 0;
        }
    }
}
