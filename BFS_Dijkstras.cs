using System;
using System.Collections.Generic;

namespace DSA_Prac2
{
    public class BFS_Dijkstras
    {
        /*
         * https://www.freecodecamp.org/news/dijkstras-shortest-path-algorithm-visual-introduction/
         * 
         For weighted graphs and acyclic (Directed Acyclic Graphs) use Dijkastras
            For unweighted use BFS
         */
        public static void BFS_DijkstrasTest()
        {
            //BFS
            MinKnightStepsTest();

        }
        

        private static void MinKnightStepsTest()
        {
            /*
             * https://www.techiedelight.com/chess-knight-problem-find-shortest-path-source-destination/
             The shortest path is always solved using BFS
            BFS means using queue

            Input:
 
            N = 8 (8 × 8 board)
            Source = (7, 0)
            Destination = (0, 7)
 
            Output: Minimum number of steps required is 6

            Knight is horse
             */
            var knightRows = new int[] { 2, 2, -2, -2, 1, 1, -1, -1 };
            var knightCols = new int[] { 1, -1, 1, -1, 2, -2, 2, -2 };

            int sourceRow = 7, sourceCol = 0;
            int destRow = 0, destCol = 7;

            //{row:col}-'dist'
            var visited = new Dictionary<string, int>();
            var queue = new Queue<(int row, int col, int currentStep)>();
            queue.Enqueue((sourceRow, sourceCol, 0));

            while (queue.Count > 0)
            {
                //check if reached the destination
                var currentNode = queue.Dequeue();

                if (currentNode.row == destRow && currentNode.col == destCol)
                {
                    Console.WriteLine($"Distance is {currentNode.currentStep }");
                    return;
                }

                var key = $"{currentNode.row }:{currentNode.col}";

                if (visited.ContainsKey(key))
                    continue;

                visited.Add(key, currentNode.Item3);

                for (int i = 0; i < knightRows.Length; i++)
                {
                    var row = currentNode.Item1 + knightRows[i];
                    var col = currentNode.Item2 + knightCols[i];

                    //adding the move conditionally
                    if (row >= 0 && row < knightRows.Length && col >= 0 && col < knightCols.Length && !visited.ContainsKey($"{row}:{col}"))
                    {
                        queue.Enqueue((row, col, currentNode.currentStep + 1));
                    }
                }
            }
        }
    }
}
