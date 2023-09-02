using System;
using System.Collections.Generic;
using static DSA_Prac2.GraphProblems;

namespace DSA_Prac2
{
    public class Heap
    {
        public static void Test()
        {
            FindKClosestPointsTest();

            FindKClosestNumbersToANumberTest();

            MergeKSortedLinkList();

            FindKthLargestInArrayTest();

            FindTopKFrequentNumsInArrayTest();

            FindKThLargestInStream();

            FindKthSmallestInMatrixTest();
        }

        

        private static void FindKClosestPointsTest()
        {
            /*
             * https://www.tutorialcup.com/leetcode-solutions/k-closest-points-to-origin-leetcode-solution.htm
             Given a list of points on a 2D plane. Find k closest points to origin (0, 0).
            Input: [(1, 1), (2, 2), (3, 3)], K=1
            Output: [(1, 1)]

            Explanation:

            Square of Distance of (1, 1) from the origin is 1
            Square of Distance of (2, 2) from the origin is 8
            Square of Distance of (3, 3) from the origin is 18 
            Therefore for K = 1, the closest points are (1, 1)

            Input:
                [(2, 1), (-3, -2), (3, 0), (0, -2), (3, -1)]   K = 3

                Output:
                [(0, -2), (2, 1), (3, 0)] 

                Explanation:

                Square of Distance of (2, 1) from the origin is 5
                Square of Distance of (-3, -2) form the origin is 13
                Square of Distance of (3, 0) from the origin is 9
                Square of Distance of (0, -2) from the origin is 4
                Square of Distance of (3, -1) from the origin is 10
                Therefore for K = 3, the closest points are (0, -2), (2, 1) & (3, 0)
             */

            var inputArr = new List<(int x, int y)> { (1, 1), (2, 2), (3, 3) };
            var k = 2;
            var minHeap = new MinHeap<(int x, int y)>(inputArr.Count);

            var output = new List<(int x, int y)>();

            for (int i = 0; i < inputArr.Count; i++)
            {
                var el = inputArr[i];
                var sq = el.x * el.x + el.y * el.y;//x2 + y2
                minHeap.Push(((el.x, el.y), sq));
            }

            Console.WriteLine($"Nearest k elements =");

            for (int i = 0; i < k; i++)
            {
                var val = minHeap.Pop();
                output.Add(val);
                Console.Write($"Nearest k elements = {val.x},{val.y}");
            }
        }

        private static void FindKClosestNumbersToANumberTest()
        {
            /*
                Input: [5, 6, 7, 8, 9], K = 3, X = 7
                Output: [6, 7, 8]

                Approach: subtract the number from index and use that as priority

                7-5,7-6,7-7,7-8,7-9
                2,1,0,1,2
             */
            var input = new int[] { 5, 6, 7, 8, 9 };
            var k = 3;
            var X = 7;
            var minHeap = new MinHeap1<int>(input.Length);

            foreach (var item in input)
            {
                minHeap.Push(new HeapItem<int>(item, Math.Abs(X - item)));
            }

            var output = new int[k];
            for (int i = 0; i < k; i++)
            {
                output[i] = minHeap.Pop();
            }

            Console.WriteLine($"Min elements={string.Join(",", output)}");

        }

        private static void MergeKSortedLinkList()
        {
            /*
             Input:  k = 3
 
            List 1: 1 —> 5 —> 7 —> NULL
            List 2: 2 —> 3 —> 6 —> 9 —> NULL
            List 3: 4 —> 8 —> 10 —> NULL
 
            Output: 1 —> 2 —> 3 —> 4 —> 5 —> 6 —> 7 —> 8 —> 9 —> 10 —> NULL
             */

            var ll1 = new LinkedList<int>();
            ll1.AddFirst(1);
            ll1.AddLast(5);
            ll1.AddLast(7);

            var ll2 = new LinkedList<int>();
            ll2.AddFirst(2);
            ll2.AddLast(3);
            ll2.AddLast(6);

            var ll3 = new LinkedList<int>();
            ll3.AddFirst(4);
            ll3.AddLast(8);
            ll3.AddLast(10);

            var input = new List<LinkedList<int>>()
            {
                ll1, ll2, ll3
            };

            //an output linked list
            var outputLL = new LinkedList<int>();
            var k = 3;

            var minHeap = new MinHeap1<LinkedListNode<int>>(k);

            //Add first data from all Linked lists into minheap
            for (int i = 0; i < k; i++)
            {
                var ll = input[i];
                minHeap.Push(new HeapItem<LinkedListNode<int>>(ll.First, ll.First.Value));
            }
            Console.WriteLine("Merged list is:");

            while (!minHeap.IsEmpty())
            {
                var node = minHeap.Pop();
                if (outputLL.First == null)
                {
                    outputLL.AddFirst(node.Value);
                    Console.Write(node.Value);
                }
                else
                {
                    var lastNode = outputLL.Last;
                    outputLL.AddAfter(lastNode, new LinkedListNode<int>(node.Value));//add as last node
                    Console.Write($",{node.Value}");
                }

                if (node.Next != null)
                    minHeap.Push(new HeapItem<LinkedListNode<int>>(node.Next, node.Next.Value));
            }
        }


        private static void FindKthLargestInArrayTest()
        {
            /*
            Given an integer array nums and an integer k, return the kth largest element in the array.
            Note that it is the kth largest element in the sorted order, not the kth distinct element.

            Input: nums = [3,2,1,5,6,4], k = 2
            Output: 5

            Input: nums = [3,2,3,1,2,4,5,5,6], k = 4
            Output: 4
             */

            var input = new int[] { 3, 2, 1, 5, 6, 4 };
            var k = 2;

            //Once sorted: 1,2,3,4,5,6 --will take nlogn
            //use MinHeap - logn

            //3,2,1 --gets 1
            //3,2,5 --gets - 2
            //3,5,6 -- gets - 3
            //5,6,4 -- gets - 4
            //this makes the number of elements in heap ony 2 (or k) elements

            //After this getMin() from: 5,6 -- gives 5

            var minHeap = new MinHeap1<int>(input.Length);//len doesn't matter
            for (int i = 0; i < input.Length; i++)//not a strean so add all
            {
                minHeap.Push(new HeapItem<int>(input[i], input[i]));

                if (minHeap.Count > k)//When more than k + 1 in the heap, keep popping
                {
                    var min = minHeap.Pop();
                    Console.WriteLine($"Min {min}");
                }
            }

            var kthLargest = minHeap.Pop();//take-out kth now
            Console.WriteLine($"kthLargest = {kthLargest}");
        }

        private static void FindTopKFrequentNumsInArrayTest()
        {
            /*
             Input: [1, 3, 5, 12, 11, 12, 11], K = 2
                Output: [12, 11]
                Explanation: Both '11' and '12' apeared twice.
             */

            //Note: Here the priority will be the frequency of that element and map provides the frequency

            var input = new int[] { 1, 3, 5, 12, 11, 12, 11 };
            var k = 2;

            var map = new Dictionary<int, int>();
            foreach (var item in input)
            {
                if (map.ContainsKey(item))
                    map[item] = map[item] + 1;
                else
                    map[item] = 1;
            }

            var minHeap = new MinHeap1<int>(int.MaxValue);

            foreach (var item in map)
            {
                minHeap.Push(new HeapItem<int>(item.Key, item.Value));

                if (minHeap.Count > k)// maintain only k elements in the heap and start poping others 
                    minHeap.Pop();
            }

            var topNumbers = new List<int>();

            while (!minHeap.IsEmpty())
            {
                topNumbers.Add(minHeap.Pop());
            }

            Console.WriteLine($"Top numbers are:{string.Join(",", topNumbers)}");
        }


        private static void FindKThLargestInStream()
        {
            /*
         * https://www.youtube.com/watch?v=hOjcdrqMoQ8&t=338s
         * https://www.geeksforgeeks.org/kth-largest-element-in-a-stream/
         * 
         * Given an infinite stream of integers, find the k’th largest element at any point of time.
Example: Input:
stream[] = {10, 20, 11, 70, 50, 40, 100, 5, ...}
k = 3
Output:    {_,   _, 10, 11, 20, 40, 50,  50, ...}

         */

            var initialArr = new int[] {  };
            var k = 3;
            var kthLargestUtil = new KthLargestStream(initialArr, k);

            foreach (var item in new int[] { 10, 20, 11, 70, 50, 40, 100, 5 })
            {
                kthLargestUtil.AddToStream(item);
                Console.WriteLine($"Current kth largest in stream= {kthLargestUtil.GetKthLargest()}");
            }

            kthLargestUtil.AddToStream(22);
            Console.WriteLine($"Current kth largest in stream= {kthLargestUtil.GetKthLargest()}");


            kthLargestUtil.AddToStream(200);
            Console.WriteLine($"Current kth largest in stream= {kthLargestUtil.GetKthLargest()}");

        }

        private static void FindKthSmallestInMatrixTest()
        {
            /*
             Given an N ∗ N matrix where each row and column is sorted in
                ascending order, find the Kth smallest element in the matrix.

            Input: Matrix=[
                [2, 6, 8],
                [3, 7, 10],
                [5, 8, 11]
                ],
                K=5
                Output: 7
                Explanation: The 5th smallest number in the matrix is 7.
             */

            int[,] matrix = new int[3, 3] { { 2, 6, 8 }, { 3, 7, 10 }, { 5, 8, 11 } };
            var k = 5;//find 5th samllest

            var minHeap = new MinHeap1<(int row, int col)>(matrix.GetLength(0) * matrix.GetLength(1));

            // put the 1st element of each row in the min heap
            // we don't need to push more than 'k' elements in the heap
            for (int i = 0; i < matrix.GetLength(0) && i < k; i++)
                minHeap.Push(new HeapItem<(int row, int col)>((i,0), matrix[i, 0]));


            //take the min element and add the next element of that row
            var numberCount = 0;
            while(minHeap.Count > 0)
            {
                (int currentRow, int currentCol) = minHeap.Pop();
                //take next Col
                numberCount++;

                if (numberCount == k)
                {
                    var kthSmallest = matrix[currentRow, currentCol];
                    Console.WriteLine($"Kth Smallest={kthSmallest}");
                    break;
                }

                if((currentCol + 1) < matrix.GetLength(1))
                {
                    minHeap.Push(new HeapItem<(int row, int col)>((currentRow, currentCol + 1), matrix[currentRow, currentCol + 1]));
                }
            }
        }

    }


    public class KthLargestStream
    {
        private MinHeap1<int> _minHeap = new MinHeap1<int>(int.MaxValue);//initial capacity does not matter
        private int _k;

        public KthLargestStream(int[] initialDataInStream, int k)
        {
            //keep upto k+1 elements in the heap and remove
            _k = k;
            if(initialDataInStream.Length > 0)
            {
                for (int i = 0; i < initialDataInStream.Length; i++)
                {
                    _minHeap.Push(new HeapItem<int>(initialDataInStream[i], initialDataInStream[i]));

                    if((i + 1) > _k)
                    {
                        _minHeap.Pop();
                    }
                }
            }
        }

        public void AddToStream(int newData)
        {
            _minHeap.Push(new HeapItem<int>(newData, newData));
            if (_minHeap.Count > _k)
            {
                _minHeap.Pop();
            }
        }

        public int GetKthLargest()
        {
            if (_minHeap.Count < _k) return -1;

            while (_minHeap.Count > _k)
                _minHeap.Pop();

            return _minHeap.Pop();//this should give the kth largest
        }
    }




    //ACBT - Almost complete binary tree
    //in the tree-parent should be complete before child
    //first left and then right
    //https://egorikas.com/max-and-min-heap-implementation-with-csharp/
    public class MinHeap<T>
    {
        public static void Test()
        {
            var cls = new MinHeap<int>(7);

            //tree form
            var minHeapRoot = new Node(1);
            var child1 = new Node(2);
            var child2 = new Node(3);
            minHeapRoot.children.Add(child1);
            minHeapRoot.children.Add(child2);

            var child3 = new Node(4);
            var child4 = new Node(5);
            child1.children.Add(child3);
            child1.children.Add(child4);

            var child5 = new Node(6);
            var child6 = new Node(7);
            child2.children.Add(child5);
            child2.children.Add(child6);

            var serializedArr = cls.SerializeToArray(minHeapRoot);

            var deserialized = cls.DeSerializeToArray(serializedArr);

        }

        public List<int> SerializeToArray(Node node)
        {
            //store in the array form
            /*
             For node i, its children are stored at 2i+1 and 2i+2, and its parent is at floor((i-1)/2).
            So instead of node.left in binary tree we'd do 2*i+1.
             */

            var arr = new List<int>();

            //BFS
            var q = new Queue<(Node node, int index)>();
            q.Enqueue((node, 0));
            arr[0] = node.value;
            while (q.Count >= 0)
            {
                var parentNode = q.Dequeue();

                var childCount = 0;
                foreach (var child in parentNode.node.children)
                {
                    var childIndex = childCount % 2 == 0 ? (2 * parentNode.index + 1) : (2 * parentNode.index + 2);

                    arr[childIndex] = child.value;

                    q.Enqueue((node, childIndex));
                    childCount++;
                }
            }

            Console.WriteLine("serialized min heap=");
            Console.WriteLine(string.Join(",", arr));

            return arr;
        }

        private Node DeSerializeToArray(List<int> serializedArr)
        {
            /*
             For index i, its parent are stored at 2i+1 and 2i+2, and its parent is at floor((i-1)/2).
            So instead of node.left we'd do 2*i+1.
             //c1=2p+1
            //p = floor((c1-1)/2)
             //c2=2p+2
            //p = floor((c2-2)/2)
             */

            Node rootNode = null;
            for (int i = 0; i < serializedArr.Count; i++)
            {
                //it was already an perfect heap and hence no need to check and bubble-up or down.
                //so do not get confuse with the heapify process which need that check
                //check has children
                var node = new Node(serializedArr[i]);

                if (IsRoot(i))
                {
                    rootNode = node;
                }

                if (HasLeftChild(i) || HasRightChild(i))
                {
                    if (HasLeftChild(i))
                        node.children.Add(new Node(int.Parse(GetLeftChild(i).el.ToString())));

                    if (HasRightChild(i))
                        node.children.Add(new Node(int.Parse(GetRightChild(i).el.ToString())));
                }
            }

            return rootNode;
        }

        private readonly (T el, int priority)[] _elements;
        private int _size;

        public MinHeap(int size)
        {
            _elements = new (T el, int index)[size];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private (T el, int priority) GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private (T el, int priority) GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private (T el, int priority) GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        public T Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0].el;
        }

        public T Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            BubbleDown();

            return result.el;
        }


        public void Push((T el, int index) element)
        {
            if (_size == _elements.Length)
                throw new IndexOutOfRangeException();

            // insert the new node at the end of the heap array
            _elements[_size] = element;
            _size++;

            // find the correct position for the new node
            BubbleUp();
        }

        private void BubbleDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).priority < GetLeftChild(index).priority)
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex].priority >= _elements[index].priority)
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void BubbleUp()
        {
            var index = _size - 1;
            while (!IsRoot(index) && _elements[index].priority < GetParent(index).priority)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }

    }


    //refer the below once
    public class HeapItem<T>
    {
        public T Item { get; set; }
        public int Priority { get; set; }

        public HeapItem(T item, int priority)
        {
            this.Item = item;
            this.Priority = priority;
        }
    }
    public class MinHeap1<T>
    {
        private List<HeapItem<T>> _elements;

        public int Count { get { return _elements.Count; } }

        public MinHeap1(int capacity)
        {
            _elements = new List<HeapItem<T>>(capacity);
        }

        public void Push(HeapItem<T> heapItem)
        {
            if (_elements.Count == _elements.Capacity)
                throw new IndexOutOfRangeException();

            // insert the new node at the end of the heap array
            var lastIndex = _elements.Count;
            _elements.Insert(lastIndex, heapItem);
            BubbleUp();
        }

        public bool IsEmpty()
        {
            return _elements.Count == 0;
        }

        public T Pop()
        {
            if(_elements.Count == 0)
                throw new IndexOutOfRangeException();

            //1,2,3,4--remove 1
            var value = this._elements[0];
            _elements.RemoveAt(0);

            //put the last element of the array if has children to the top and shrink the array
            if (_elements.Count > 1)
            {
                var lastElIndex = _elements.Count - 1;
                _elements.Insert(0, _elements[lastElIndex]);

                _elements.RemoveAt(_elements.Count - 1);//shrink now

                //re-shuffle the heap now using bubble down
                BubbleDown();
            }

            return value.Item;
        }

        private void BubbleDown()
        {
            //start from root and stop once no left node exists (since valid heap is left to right always)
            var currentIndex = 0;//root node

            //check whether currentIndex has left index
            while((_elements.Count - 1) >= ((2 * currentIndex) + 1))
            {
                //check whether left or right is smaller
                //10, 9,3--here right is less and hence move to the right
                var smallerIndexOfChild = ((2 * currentIndex) + 1);//left node index by default

                var rightChildNodeIndex  = (2 * currentIndex) + 2;
                if ((_elements.Count > rightChildNodeIndex) && (_elements[rightChildNodeIndex].Priority < _elements[smallerIndexOfChild].Priority))//has right element
                {
                    smallerIndexOfChild = rightChildNodeIndex;
                }

                if (_elements[smallerIndexOfChild].Priority < _elements[currentIndex].Priority)
                {
                    //swap the elements
                    var temp = _elements[smallerIndexOfChild];
                    var currentVal = _elements[currentIndex];
                    _elements.RemoveAt(currentIndex);
                    _elements.Insert(currentIndex, temp);

                    _elements.RemoveAt(smallerIndexOfChild);
                    _elements.Insert(smallerIndexOfChild, currentVal);

                    currentIndex = smallerIndexOfChild;//move towards the lower value and check if it need to move further down
                }
                else
                {
                    break;
                }

            }
        }

        private void BubbleUp()
        {
            var currentIndex = _elements.Count -1;

            while(currentIndex > 0)
            {
                var element = _elements[currentIndex];
                var parentIndex = (currentIndex - 1) / 2;

                if(_elements[parentIndex].Priority > element.Priority)
                {
                    //Swap the parent and this element
                    var temp = _elements[parentIndex];
                    var currentVal = _elements[currentIndex];
                    _elements.RemoveAt(currentIndex);
                    _elements.Insert(currentIndex, temp);

                    _elements.RemoveAt(parentIndex);
                    _elements.Insert(parentIndex, currentVal);

                    //move the index to the parent and let it check its parent value
                    currentIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
