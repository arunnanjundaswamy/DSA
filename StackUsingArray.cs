using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class StackUsingArrayTest
    {
        public static void Test()
        {
            //StackUsingArray stack = new StackUsingArray(3);
            //StackUsingDoubleQueuesPushCostly1 stack = new StackUsingDoubleQueuesPushCostly1();
            StackUsingDoubleQueuesPullCostly1 stack = new StackUsingDoubleQueuesPullCostly1();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.Pop());

            //Exceptions
            //Console.WriteLine(stack.Pop());

            stack.Push(8);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
        }
    }

    /// <summary>
    /// This may be the best approach
    /// </summary>
    public class StackUsingDoubleQueuesPushCostly1
    {
        Queue<int> _queue = new Queue<int>();

        public void Push(int val)
        {
            //Put all the data in the queue to other temp queue
            var tempQueue = new Queue<int>();

            while(_queue.Count > 0)
            {
                tempQueue.Enqueue(_queue.Dequeue());
            }

            _queue.Enqueue(val);

            //Put back from the temp queue
            while (tempQueue.Count > 0)
            {
                _queue.Enqueue(tempQueue.Dequeue());
            }
        }

        //Ignore this approach
        public void PushUsingCallStackApproach(int val)
        {
            _queue.Enqueue(val);
        }

        public int PopUsingCallStackApproach()
        {
            if (_queue.Count == 0) throw new Exception("Stack is empty");

            //Base case of recursion
            if(_queue.Count == 0)
            {
                var val = _queue.Dequeue();
                return val;
            }
            else
            {
                //Dequeue the next element and put to call stack
                var val = _queue.Dequeue();
                var returnedVal = PopUsingCallStackApproach(); //Keeps iterating till the based case is satisfied
                //Putting it back to the original queue in the same sequence

                return returnedVal;
            }
        }

        public int Pop()
        {
            if (IsEmpty()) throw new Exception("Stack is empty");

            return _queue.Dequeue();
        }

        private bool IsEmpty()
        {
            return _queue.Count == 0;
        }

        public int Top()
        {
            if (IsEmpty()) throw new Exception("Stack is empty");

            return _queue.Peek();
        }
    }

    
    /// <summary>
    /// This is to be referred
    /// </summary>
    public class StackUsingDoubleQueuesPullCostly1
    {
        Queue<int> _queue = new Queue<int>();

        public void Push(int val)
        {
            _queue.Enqueue(val);
        }

        public int Pop()
        {
            if (_queue.Count == 0) throw new Exception("Stack is empty");

            var tempQueue = new Queue<int>();

            //Take all the elements till the last one in the read queue
            //The last element needs to be retuned back
            while (_queue.Count > 1)
            {
                tempQueue.Enqueue(_queue.Dequeue());
            }

            var valToReturn = _queue.Dequeue();

            //Put back from the temp queue
            while (tempQueue.Count > 0)
            {
                _queue.Enqueue(tempQueue.Dequeue());
            }

            return valToReturn;
        }

        

        public int Top()
        {
            if (_queue.Count == 0) throw new Exception("Stack is empty");

            var tempQueue = new Queue<int>();

            //Take all the elements till the last one in the read queue
            //The last element needs to be retuned back
            while (_queue.Count > 1)
            {
                tempQueue.Enqueue(_queue.Dequeue());
            }

            var valToReturn = _queue.Dequeue();

            //Put back from the temp queue
            while (tempQueue.Count > 0)
            {
                _queue.Enqueue(tempQueue.Dequeue());
            }
            _queue.Enqueue(valToReturn);

            return valToReturn;
        }
    }

    public class StackUsingDoubleQueuesPushCostly
    {
        Queue<int> _writeQueue = new Queue<int>();
        Queue<int> _readQueue = new Queue<int>();

        public void Push(int val)
        {
            _writeQueue.Enqueue(val);

            //Re-arranging write queue
            while (_readQueue.Count > 0)
            {
                _writeQueue.Enqueue(_readQueue.Dequeue());
            }

            //Re-arrange is done above - Now populate the 'read' queue

            var qTemp = _readQueue;
            _readQueue = _writeQueue;
            _writeQueue = qTemp;
        }

        public int Pop()
        {
            if (IsEmpty()) throw new Exception("Stack is empty");

            return _readQueue.Dequeue();
        }

        private bool IsEmpty()
        {
            return _readQueue.Count == 0;
        }

        public int Top()
        {
            if (IsEmpty()) throw new Exception("Stack is empty");

            return _readQueue.Peek();
        }
    }

    public class StackUsingDoubleQueuesPullCostly
    {
        Queue<int> _readQueue = new Queue<int>();
        Queue<int> _writeQueue = new Queue<int>();

        public void Push(int val)
        {
            _writeQueue.Enqueue(val);
        }

        public int Pull()
        {
            if(_writeQueue.Count == 0) throw new Exception("Stack is empty");

            //Take all the elements till the last one in the _writeQueue queue
            //The last element needs to be returned back
            while (_writeQueue.Count > 1)
            {
                _readQueue.Enqueue(_writeQueue.Dequeue());
            }

            var valToReturn = _writeQueue.Dequeue();

            var tempQueue = _writeQueue;
            _writeQueue = _readQueue;
            _readQueue = tempQueue;

            return valToReturn;
        }

        public int Top()
        {
            if (_writeQueue.Count == 0) throw new Exception("Stack is empty");

            //Take all the elements till the last one in the read queue
            //The last element needs to be retuned back
            while (_writeQueue.Count > 1)
            {
                _readQueue.Enqueue(_writeQueue.Dequeue());
            }

            var valToReturn = _writeQueue.Dequeue();
            _readQueue.Enqueue(valToReturn);

            var tempQueue = _writeQueue;
            _writeQueue = _readQueue;
            _readQueue = tempQueue;

            return valToReturn;
        }
    }

    public class StackUsingSingleQueue
    {
        Queue<int> _queue = new Queue<int>();

        public void Push(int val)
        {
            _queue.Enqueue(val);

            var cnt = _queue.Count;

            //Re-arrange to the same queue and till the just added element
            while(cnt > 1)
            {
                var qVal = _queue.Dequeue();
                _queue.Enqueue(qVal);
                cnt--;
            }
        }

        public int Pop()
        {
            if (_queue.Count == 0) throw new Exception("Stack is empty");
            return _queue.Dequeue();
        }

        public int Top()
        {
            if (_queue.Count == 0) throw new Exception("Stack is empty");
            return _queue.Peek();
        }
    }

    public class StackUsingArray
    {
        int[] arr = null;
        int topIndex = -1;

        public StackUsingArray(int capacity)
        {
            arr = new int[capacity];
        }

        public void Push(int val)
        {
            if (IsFull()) throw new Exception("Stack is full"); //overflow condition

            topIndex = topIndex + 1;
            arr[topIndex] = val;

        }

        public int Pop()
        {
            if (IsEmpty()) throw new Exception("Stack is empty");//underflow condition

            int val = arr[topIndex];
            topIndex = topIndex - 1;
            return val;
        }

        public int Top()
        {
            return topIndex;
        }

        public bool IsEmpty()
        {
            return topIndex == -1;
        }

        public bool IsFull()
        {
            return topIndex == (arr.Length - 1);
        }
    }

    public class StackUsingLinkedListTest
    {
        public static void Test()
        {
            StackUsingLinkedList stack = new StackUsingLinkedList();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());

            //Exceptions
            //Console.WriteLine(stack.Pop());

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
        }
    }

    public class StackUsingLinkedList
    {
        public class SinglyNode
        {
            public SinglyNode Next { get; set; }

            public int Val { get; set; }

            public SinglyNode(int val)
            {
                Val = val;
            }
        }

        public SinglyNode Head { get; set; } = null;

        public void Push(int val)
        {
            var existingHead = Head;

            var newNode = new SinglyNode(val)
            {
                Next = existingHead
            };

            Head = newNode;
        }

        public int Pop()
        {
            if (Head == null) throw new Exception("Stack is empty");

            var val = Head.Val;

            var nextNode = Head.Next;

            Head = nextNode;

            return val;
        }

        public int Top()
        {
            if (Head == null) throw new Exception("Stack is empty");

            var val = Head.Val;

            return val;
        }

    }



    public class SymbolBalanceTest
    {
        public static void Test()
        {
            var input = "((()){()}";//valid

            Console.WriteLine("Result is {0}", IsBalanced(input));
        }

        private static bool IsBalanced(string input)
        {
            var pairs = new Dictionary<char, char> { { '{', '}' }, { '(', ')' }, { '[', ']' } };

            var inputChars = input.ToCharArray();

            var vs = new Stack<char>();

            var count = 0;

            while (count < inputChars.Length)
            {
                var currentChar = input[count];

                if (pairs.ContainsKey(currentChar))
                {
                    vs.Push(currentChar);
                }
                else
                {
                    if (vs == null) return false;

                    var lastEl = vs.Peek();

                    if (pairs[lastEl] == currentChar)
                    {
                        vs.Pop();
                    }
                }

                count++;
            }

            return vs.Count > 0 ? false : true;
        }
    }

    public class StackWithMinTest
    {
        public static void Test()
        {
            //var stack = new StackWithMin(10);
            //stack.Push(6);
            //stack.Push(11);
            //stack.Push(-2);
            //stack.Push(-1);
            //stack.Push(1);
            //stack.Push(2);
            //stack.Push(3);
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.GetMin());
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.GetMin());
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.GetMin());
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.GetMin());
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.GetMin());

            //Console.WriteLine("Second test");
            //var stack2 = new StackWithMin(10);
            //stack2.Push(2);
            //stack2.Push(6);
            //stack2.Push(4);
            //stack2.Push(1);
            //stack2.Push(5);
            //Console.WriteLine(stack2.Pop());
            //Console.WriteLine(stack2.GetMin());
            //Console.WriteLine(stack2.Pop());
            //Console.WriteLine(stack2.GetMin());
            //Console.WriteLine(stack2.Pop());
            //Console.WriteLine(stack2.GetMin());
            //Console.WriteLine(stack2.Pop());
            //Console.WriteLine(stack2.GetMin());
            //Console.WriteLine(stack2.Pop());//last one

            var stack3 = new StackWithMinWOAuxMem(10);
            stack3.Push(3);
            stack3.Push(5);
            stack3.Push(1);
            stack3.Push(-4);
            stack3.Push(6);
            //stack3.Push(2);
            //stack3.Push(3);
            Console.WriteLine(stack3.Pop());
            Console.WriteLine(stack3.GetMin());
            Console.WriteLine(stack3.Pop());
            Console.WriteLine(stack3.GetMin());
            Console.WriteLine(stack3.Pop());
            Console.WriteLine(stack3.GetMin());
            Console.WriteLine(stack3.Pop());
            Console.WriteLine(stack3.GetMin());
            Console.WriteLine(stack3.Pop());
            Console.WriteLine(stack3.GetMin());
            Console.WriteLine(stack3.Pop());
            //Console.WriteLine(stack3.GetMin());
        }
    }


    /*
     Consider the following SpecialStack
        16  --> TOP
        15
        29
        19
        18

        When getMin() is called it should return 15, 
        which is the minimum element in the current stack. 

        If we do pop two times on stack, the stack becomes
        29  --> TOP
        19
        18

        When getMin() is called, it should return 18 
        which is the minimum in the current stack.
     */
    public class StackWithMin
    {
        int _capacity = 0;
        int[] _arr = null;
        int _currentIndex = -1;

        //Takes auxilarry memory
        Stack<int> minStack = new Stack<int>();
        public StackWithMin(int capacity)
        {
            _capacity = capacity;
            _arr = new int[capacity];
        }

        public bool IsFull()
        {
            return (_currentIndex >= _arr.Length - 1);
        }
        public bool IsEmpty()
        {
            return _currentIndex == -1;
        }

        public void Push(int x)
        {
            if (IsFull()) return;

            if (_currentIndex++ == -1)
            {
                minStack.Push(x);

                _arr[_currentIndex] = x;
            }
            else
            {
                if (x <= minStack.Peek())
                {
                    minStack.Push(x);
                }
                //else
                //{
                //    minStack.Push(minStack.Peek());
                //}
                _arr[_currentIndex] = x;
            }

        }

        public int Pop()
        {
            if (IsEmpty()) return -1;

            var minNo = minStack.Peek();
            var val = _arr[_currentIndex];

            if (val <= minNo) minStack.Pop();

            _currentIndex--;

            return val;
        }

        public int Top()
        {
            if (IsEmpty()) return -1;

            return _arr[_currentIndex];
        }

        public int GetMin()
        {
            if (minStack.Count == 0) throw new InvalidOperationException();

            return minStack.Peek();
        }
    }


    public class StackWithMinWOAuxMem
    {
        int _capacity = 0;
        int[] _arr = null;
        int _currentIndex = -1;

        //Takes no auxilary memory
        //Establish a relation between new Min and Previous min
        //Apply this relation only when the min changes - that's it.
        //CurrentMin-PreviousMin = Value Stored in Stack
        //At any point of time two variables will be known
        //During pop previousMin = CurrentMin - Value Stored in stack
        int minStackVal = -1000;
        public StackWithMinWOAuxMem(int capacity)
        {
            _capacity = capacity;
            _arr = new int[capacity];
        }

        public bool IsFull()
        {
            return (_currentIndex >= _arr.Length - 1);
        }
        public bool IsEmpty()
        {
            return _currentIndex == -1;
        }

        public void Push(int x)
        {
            if (IsFull()) return;

            if (_currentIndex++ == -1)
            {
                minStackVal = x;
                _arr[_currentIndex] = x;
            }
            else
            {
                if (x <= minStackVal)
                {
                    var val = x - minStackVal;//(Current val - Previous Min);
                    _arr[_currentIndex] = val;

                    minStackVal = x;
                }
                else
                {
                    _arr[_currentIndex] = x;
                }
            }
        }

        //3,5,1,-4
        //3-3
        //5-3
        //(-2)-1
        //(-5)-(-4)

        //3,5,1,-4,6

        //3-3
        //2-3
        //-2-1
        //-5-4
        //10,-4

        //-4-p=10
        //p=14
        //x=

        //-4-p=-5
        //p=1
        //

        //1-p=-2
        //p=3
        //x-3=-2
        //x=-2+3=1

        //3-p=2
        //p=1//not possible--take current as p
        //x-3=2
        //x=5

        //3-p=3
        //p=0
        //x-3=3


        public int Pop()
        {
            if (IsEmpty()) return -1;

            //currMin - prevmin  = val
            var val = _arr[_currentIndex];

            if (val > minStackVal)
            {
                _currentIndex--;
                return val;
            }

            var currentMin = minStackVal;
            var previousMin = currentMin - val;

            if (previousMin <= currentMin)//not possible since we are storing only if the min is less up the stack and higher down the stack
            {
                previousMin = currentMin;
            }
            _currentIndex--;

            var derVal = val + previousMin;
            minStackVal = previousMin;
            return derVal;
        }

        public int Top()
        {
            if (IsEmpty()) return -1;

            return _arr[_currentIndex];
        }

        public int GetMin()
        {
            if (minStackVal == -1000) throw new InvalidOperationException();

            return minStackVal;
        }
    }

    public class IsPalindromeUsingStackTest
    {
        public static void Test()
        {
            var pali = new IsPalindromeUsingStack(new char[] { 'a', 'b', 'x', 'b', 'a' });

            pali.IsPalindrome();
        }
    }
    public class IsPalindromeUsingStack
    {
        char[] arr = null;
        public IsPalindromeUsingStack(char[] arr)
        {
            this.arr = arr;
        }

        public bool IsPalindrome()
        {
            var cntr = 0;
            var firstStack = new Stack<char>();

            while (arr[cntr] != 'x')
            {
                firstStack.Push(arr[cntr]);
                cntr++;
            }

            cntr++;

            while (cntr < arr.Length)
            {
                if (arr[cntr] != firstStack.Pop())
                    return false;

                cntr++;
            }

            return true;
        }
    }

    public class RecursiveReverseStackTest
    {
        public static void Test()
        {
            var s = new Stack<int>();
            s.Push(3);
            s.Push(2);
            s.Push(1);
            var t = new RecursiveReverseStack();
            t.Reverse(s);
        }
    }

    public class RecursiveReverseStack
    {
        public void Reverse(Stack<int> s)
        {
            if (s.Count == 0) return;

            var data = s.Pop();
            Reverse(s);
            AddToStack(s, data);
        }

        private void AddToStack(Stack<int> s, int data)
        {
            s.Push(data);
        }
    }


    public class FindNearestRight
    {
        public static void Test()
        {
            int[] arr = new int[] { 1, 4, 0, 2, 9 };
            var cl = new FindNearestRight();

            var res = cl.GetNearestHighest(arr);
            var res1 = cl.GetNearestHighestRightUsingStack(arr);
            var res2 = cl.GetNearestLeftHighestUsingStack(arr);

            arr = new int[] { 100, 80, 60, 70, 60, 75, 85 };
            var res3 = cl.FindSpan(arr);

            arr = new int[] { 3, 8, 6, 2, 7 };

            var highest = cl.FindHighestAreaInHistogram(arr);
            Console.WriteLine(highest);


            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

            foreach (var item in res1)
            {
                Console.WriteLine(item);
            }

            foreach (var item in res2)
            {
                Console.WriteLine(item);
            }

            foreach (var item in res3)
            {
                Console.WriteLine(item);
            }
        }

        public int[] GetNearestHighest(int[] arr)
        {
            /*
             Input: 2,4,7,8,1
            Output: 4,7,8,-1,-1
             */
            int[] res = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                {
                    res[i] = -1;
                }
                else
                {
                    res[i] = -1;

                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (arr[j] > arr[i])
                        {
                            res[i] = arr[j];
                            break;
                        }
                    }

                }
            }

            return res;
        }

        public int[] GetNearestLowestRightUsingStack(int[] arr)
        {
            /*
             Input:  arr[] = {1, 6, 4, 10, 2, 5}
            Output:         {-1,4,2,2,-1,-1}
             */
            int[] res = new int[arr.Length];

            var st = new Stack<int>();

            res[arr.Length - 1] = -1;
            st.Push(arr[arr.Length - 1]);

            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (st.Peek() < arr[i])
                {
                    res[i] = st.Peek();
                    st.Push(arr[i]);
                }
                else
                {
                    while (st.Count > 0 && arr[i] < st.Peek())
                    {
                        st.Pop();
                    }

                    if (st.Count > 0 && st.Peek() < arr[i])
                        res[i] = st.Peek();
                    else
                        res[i] = -1;

                    st.Push(arr[i]);
                }
            }

            return res;
        }

        public int FindHighestAreaInHistogram(int[] arr)
        {
            /*
            
            arr =   [6, 2, 5, 4, 5, 1, 6]
    NSR Index        1, 5, 3, 5, 2, 7, 7
    NSL Index       -1, -1, 1, 1, 3, -1, 5
            
             */
            //4,2,3,1
            //Area: arr[i] * (Right span + Left span) - 1;

            //Find nearer smallest right with index
            int[] rightNRS = new int[arr.Length];
            //FindNearerRightSmallest(arr, rightNRS);
            NearestSmallestRightIndex(arr, rightNRS);

            //Find left Span
            int[] leftSpan = new int[arr.Length];

            //FindLeftSpan(arr, leftSpan);
            NearestSmallestLeftIndex(arr, leftSpan);

            var highestArea = -1;

            for (int i = 0; i < arr.Length; i++)
            {
                var area = arr[i] * ((rightNRS[i] + leftSpan[i]) - 1);

                if (area > highestArea)
                    highestArea = area;
            }

            return highestArea;
        }

        private void FindNearerRightSmallest(int[] arr, int[] rightNRS)
        {
            //*Approach; in the stack -store in decreasing order from right to left

            /*

           arr =   [6, 2, 5, 4, 5, 1, 6]
   NSR Index        1, 5, 3, 5, 2, 7, 7

            */

            rightNRS[arr.Length - 1] = 1;
            var stack = new Stack<Tuple<int, int>>();
            stack.Push(Tuple.Create(arr[arr.Length - 1], arr.Length));

            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] > stack.Peek().Item1)
                {
                    rightNRS[i] = stack.Peek().Item2 - i;
                    stack.Push(Tuple.Create(arr[i], i));
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek().Item1 > arr[i])
                    {
                        stack.Pop();
                    }

                    if (stack.Count == 0)
                    {
                        rightNRS[i] = (arr.Length);
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                    else
                    {
                        rightNRS[i] = (stack.Peek().Item2 - i) + 1;
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                }
            }
        }

        private void FindNearerLeftSmallest(int[] arr, int[] rightNRS)
        {
            /*

           arr =   [6, 2, 5, 4, 5, 1, 6]
   NSL Index        -1, -1, 1, 1, 3, -1, 5

            */

            rightNRS[arr.Length - 1] = 1;
            var stack = new Stack<Tuple<int, int>>();
            stack.Push(Tuple.Create(arr[arr.Length - 1], arr.Length));

            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] > stack.Peek().Item1)
                {
                    rightNRS[i] = stack.Peek().Item2 - i;
                    stack.Push(Tuple.Create(arr[i], i));
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek().Item1 > arr[i])
                    {
                        stack.Pop();
                    }

                    if (stack.Count == 0)
                    {
                        rightNRS[i] = (arr.Length);
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                    else
                    {
                        rightNRS[i] = (stack.Peek().Item2 - i) + 1;
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                }
            }
        }


        //    public int FindHighestAreaInHistogram(int[] arr)
        //    {
        //        /*
        //         arr: {100, 80, 60, 70, 60, 75, 85},
        //         LS:   {1, 1, 1, 2, 1, 4, 6}
        //         RS:   {7, 5, 1, 2, 1, 1, 1}

        //        arr =   [6, 2, 5, 4, 5, 1, 6]
        //NSR INdex        1, 5, 3, 5, 2, 7, 7
        //NSL Index        1, 1, 2, 1, 4, 1, 2

        //         */
        //        //4,2,3,1
        //        //Area: arr[i] * (Right span + Left span) - 1;

        //        //Find Right Span
        //        int[] rightSpan = new int[arr.Length];
        //        FindRightSpan(arr, rightSpan);

        //        //Find left Span
        //        int[] leftSpan = new int[arr.Length];

        //        FindLeftSpan(arr, leftSpan);

        //        var highestArea = -1;

        //        for (int i = -0; i < arr.Length; i++)
        //        {
        //            var area = arr[i] * ((rightSpan[i] + leftSpan[i]) - 1);

        //            if (area > highestArea)
        //                highestArea = area;
        //        }

        //        return highestArea;
        //    }

        private void NearestSmallestLeftIndex(int[] arr, int[] leftSpan)
        {
            //Span = index of the current element - index of the previous (on left side) lowest
            //In stack, store in increasing order from left to right
            /*
             *  arr = [6, 2, 5, 4, 5, 1, 6]
                       1, 1, 2, 3, 2, 1, 2
               
             */
            leftSpan[0] = 1;

            var stack = new Stack<(int val, int index)>();
            stack.Push((arr[0], 0));

            for (int index = 0; index < arr.Length; index++)
            {
                if (arr[index] > stack.Peek().Item1)
                {
                    leftSpan[index] = 1;
                    stack.Push((arr[index], index));
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek().Item1 > arr[index])
                    {
                        stack.Pop();
                    }

                    var currentVal = arr[index];
                    leftSpan[index] = stack.Count > 0 && stack.Peek().Item1 < arr[index] ? index - stack.Peek().Item2 : 1;
                    stack.Push((arr[index], index));
                }
            }

        }

        private void NearestSmallestRightIndex(int[] arr, int[] rightSpan)
        {
            //Span = index of the current element - index of the lowest previous (on right side) 
            //In stack, store in increasing order from  right to left
            /*
             *  arr = [6, 2, 5, 4, 5, 1, 6]
                       2, 5, 2, 3, 2, 1, 1
               
             */
            rightSpan[arr.Length -1] = 1;

            var stack = new Stack<Tuple<int, int>>();
            stack.Push(Tuple.Create(arr[arr.Length - 1], arr.Length -1));

            for (int index = arr.Length -2 ; index >= 0; index--)
            {
                if (arr[index] > stack.Peek().Item1)
                {
                    rightSpan[index] = 1;
                    stack.Push(new Tuple<int, int>(arr[index], index));
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek().Item1 > arr[index])
                    {
                        stack.Pop();
                    }

                    var currentVal = arr[index];
                    rightSpan[index] = stack.Count > 0 && stack.Peek().Item1 < currentVal ? stack.Peek().Item2 - index : 1;
                    stack.Push(new Tuple<int, int>(currentVal, index));
                }
            }

        }


        private void FindLeftSpan(int[] arr, int[] leftSpan)
        {
            //Span = index of the current element - index of the previous (on left side) highest

            /*
                {100, 80, 60, 70, 60, 75, 85},
                {1, 1, 1, 2, 1, 4, 6}

                2nd:
                //1,4,0,2,9
                //1,2,1,2,3

                arr = [6, 2, 5, 4, 5, 1, 6]
                       1, 1, 2, 1, 4, 1, 6 
             */

            leftSpan[0] = 1;

            var stack = new Stack<Tuple<int, int>>();
            stack.Push(Tuple.Create(arr[0], 0));

            for (int i = 1; i < arr.Length; i++)
            {
                if (stack.Peek().Item1 < arr[i])
                {
                    leftSpan[i] = i - stack.Peek().Item2;
                    stack.Push(Tuple.Create(arr[i], i));
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek().Item1 > arr[i])
                    {
                        stack.Pop();
                    }

                    if (stack.Count == 0)
                    {
                        leftSpan[i] = i + 1;
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                    else
                    {
                        leftSpan[i] = i - stack.Peek().Item2;
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                }
            }
        }

        private void FindRightSpan(int[] arr, int[] rightSpan)
        {
            //Span = index of the current element - index of the previous (on right side) highest

            /*
                {100, 80, 60, 70, 60, 75, 85},
                {7, 6, 1, 2, 1, 1, 1}

                2nd:
                //1,4,0,2,9
                //1,3,1,1,1

                arr = [6, 2, 5, 4, 5, 1, 6]
                       6, 2, 5, 1, 2, 1, 1                
             */

            rightSpan[arr.Length - 1] = 1;
            var stack = new Stack<Tuple<int, int>>();
            stack.Push(Tuple.Create(arr[arr.Length - 1], arr.Length - 1));

            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] > stack.Peek().Item1)
                {
                    rightSpan[i] = stack.Peek().Item2 - i;
                    stack.Push(Tuple.Create(arr[i], i));
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek().Item1 > arr[i])
                    {
                        stack.Pop();
                    }

                    if (stack.Count == 0)
                    {
                        rightSpan[i] = (arr.Length) - i;
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                    else
                    {
                        rightSpan[i] = stack.Peek().Item2 - i;
                        stack.Push(Tuple.Create(arr[i], i));
                    }
                }
            }
        }

        public int[] FindSpan(int[] arr)
        {
            /*
                {100, 80, 60, 70, 60, 75, 85},
                {1, 1, 1, 2, 1, 4, 6}

                2nd:
                //1,4,0,2,9
                //1,2,1,2,3
             */

            //To store span
            //Span = index of the current element - index of the previous (on left side) highest
            int[] res = new int[arr.Length];

            //to store the value and the index of that array element
            var st = new Stack<Tuple<int, int>>();
            res[0] = 1;//to store span
            st.Push(Tuple.Create(arr[0], 0));

            for (int i = 1; i < arr.Length; i++)
            {
                if (st.Peek().Item1 > arr[i])
                {
                    res[i] = i - st.Peek().Item2;
                    st.Push(Tuple.Create(arr[i], i));
                }
                else
                {
                    var cnt = 1;

                    while (st.Count > 0 && st.Peek().Item1 < arr[i])
                    {
                        st.Pop();
                        cnt++;
                    }

                    if (st.Count == 0)
                    {
                        res[i] = i - (-1);
                        st.Push(Tuple.Create(arr[i], i));
                    }
                    else
                    {
                        res[i] = i - st.Peek().Item2;
                        st.Push(Tuple.Create(arr[i], i));
                    }
                }
            }

            return res;
        }

        public int[] GetNearestHighestRightUsingStack(int[] arr)
        {
            /*
             * Approach; in the stack - store in increasing order from right to left
                Input : arr[] = {10, 5, 11, 6, 20, 12} 
                Output : 11 6 12 20 -1 -1
                Input : arr[] = {10, 5, 11, 10, 20, 12} 
                Output : 11,11,20,20,-1,-1
                Input:  4, 5, 2, 25
                Output: 5,25,25,-1
             */
            int[] res = new int[arr.Length];

            var st = new Stack<int>();

            res[arr.Length - 1] = -1;
            st.Push(arr[arr.Length - 1]);

            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (st.Peek() > arr[i])
                {
                    res[i] = st.Peek();
                    st.Push(arr[i]);
                }
                else
                {
                    while (st.Count > 0 && arr[i] > st.Peek())
                    {
                        st.Pop();
                    }

                    if (st.Count > 0 && st.Peek() > arr[i])
                        res[i] = st.Peek();
                    else
                        res[i] = -1;

                    st.Push(arr[i]);
                }
            }

            return res;
        }

        //Refer this
        public int[] GetNearestLeftHighestUsingStack(int[] arr)
        {
            /*
             Input : arr[] =    {10, 5, 11, 6, 20, 12} 
            Output :            -1, 10, -1, 11, -1, 20
            Approach: in the stack - store in increasing order from right to left
             */
            int[] res = new int[arr.Length];

            var st = new Stack<int>();

            res[0] = -1;
            st.Push(arr[0]);

            for (int i = 1; i <= arr.Length - 1; i++)
            {
                if (st.Peek() > arr[i])
                {
                    res[i] = st.Peek();
                    st.Push(arr[i]);
                }
                else
                {
                    //If the current number is greater than the previous, no point in having it
                    while (st.Count > 0 && arr[i] > st.Peek())
                    {
                        st.Pop();
                    }

                    //Store the results
                    if (st.Count > 0 && st.Peek() > arr[i])
                    {
                        res[i] = st.Peek();
                    }   
                    else
                    {
                        res[i] = -1;
                    }   

                    //Add this new item
                    st.Push(arr[i]);
                }
            }

            return res;
        }
    }

    public class DoubleEndedQueue
    {
    }
}
