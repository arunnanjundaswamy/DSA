//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using DSA_Prac2;
//using DSA_Prac2.Programming;
//using DSA_Proj1;

//namespace ConsoleApp1
//{
//    class BinarySearch
//    {
//        public const string Base = "/api";

//        //public const string apiUrl1 = $"{Base}/api1";//Possible in 10

//        private static void Main(string[] args)
//        {
            
//            Console.WriteLine("Binary Search");

//            //int[] inputArr = new int[] { 0, 1, 2, 3 };

//            int[] inputArr = new int[] { 3 };

//            int target = 4;

//           //LinkedList.Test();

//            //No need to test now
//            //var targetIndex = FindIndex(inputArr, target);

//            //Console.WriteLine("Target Index={0}", targetIndex != -1 ? targetIndex.ToString() : "Not found");

//            //Test Rotated binary search
//            //RotatedBinarySearch.Main(args);

//            //NodeTest.Test();

//            //LRUCacheTest.Test();

//            //RecursiveWork.Test();

//            //StackUsingArrayTest.Test();

//            //StackUsingLinkedListTest.Test();

//            //SymbolBalanceTest.Test();

//            //StackWithMinTest.Test();

//            // IsPalindromeUsingStackTest.Test();

//            //RecursiveReverseStackTest.Test();

//            //FindNearestRight.Test();

//            //QueueTest.Test();

//            //SlidingWindowsProblemsTest.Test();

//            BinaryTreeSample.Test();

//            //BinaryTreeSample.FindLongestSequenceTest();

//            //TryEqualityTest.Test();

//            //Tryouts.Test();

//            //Practice1.Test();

//            //ThreadingTryouts.Test();

//            //EncryptionAndHashing.Test();

//            //ArraySortTest.Test();

//            //ArraySort.MergeSortTest();

//            //DP.Test();

//            //BackTracking.BackTrackingTest();

//            //GraphProblems.Test();

//            //Greedy.GreedyTest();

//            //PatternsTest();

//            //BitProblems.Test();

//            Heap.Test();


//            LLSolution.AddTwoNumbersTest();


//            TestOOps.TestOOpsTest();


//            //Console.ReadLine();
//        }

//        private static int FindIndex(int[] inputArr, int target)
//        {
//            int left = 0, right = inputArr.Length - 1;

//            while (left <= right)
//            {
//                int mid = (left + right) / 2;
//                int midVal = inputArr[mid];

//                //Conditions:
//                //midVal = target
//                //midVal < target - check in the right
//                //midVal > target - check in the left

//                if (midVal == target) return mid;

//                //Look towards left
//                if (target < midVal)
//                {
//                    right = mid - 1;
//                }
//                else if (target > midVal)
//                {
//                    left = mid + 1;
//                }
//                //there cannot be any other condition
//            }

//            var x = new List<(int, int)> { };

//            return -1;
//        }

//        private static void PatternsTest()
//        {
//            //bool isJohnOrJohnny(string name) => name.ToUpper() is var upper && upper == "JOHN" || upper == "JOHNNY";

//            //Console.WriteLine($"isJohnOrJohnny ${isJohnOrJohnny("john")}");

//            ///*
//            // public bool function(int age)
//            //{
//            //    return age > 18;
//            //}
//            // */
//            //var pe = Expression.Parameter(typeof(int), "Age");
//            //MemberExpression me = Expression.Property(pe, "Age");
//            //var constant = Expression.Constant(12);
//            //var body = Expression.GreaterThanOrEqual(me, constant);
//            //var exprnTree = Expression.Lambda<Func<int, bool>>(body, new[] { pe });


//            ////var exprn = Expression.Lambda<Func<int, bool>>(Expression.Equals(Expression.Property(pe, "Age"), Expression.Constant(2, typeof(int)));
//        }
//    }

//    public interface I1
//    {
//        void M1();
//    }

//    public interface I2
//    {
//        void M1();
//    }


//    public class A
//    {
//        public void M1()
//        {
//            Console.WriteLine("A.M1");
//        }
//    }
//    public class B
//    {
//        public void M1()
//        {
//            Console.WriteLine("B.M1");
//        }
//        public virtual void M2()
//        {
//            Console.WriteLine("B.M2");
//        }
//    }
//    public class C: B, I1, I2
//    {
//        public new void  M1()
//        {
//            Console.WriteLine("C.M1");
//        }
//        public override void M2()
//        {
//            Console.WriteLine("C.M2");
//        }
//    }

//    public class TestOOps
//    {
//        static int val1 = 1, val2 = 1;

//        public static async void TestOOpsTest()
//        {
//            //Multiple inheitance is not supported due to diamond problem
//            /*
//             Diamond problem
//            A.M1() and B.M1()
//            C:A, B

//            var c = new C();
//            c.M1() -- Should call A.M1() or B.M1()?? -- Diamond problem
//             */
//            B b = new C();
//            b.M1();
//            b.M2();

//            B b1 = new B();
//            b1.M1();

//            using (D1 d1 = new D1())
//            {
//                Console.WriteLine("Inside d1");
//                //throw new Exception("ding");
//            }

//            //Deep clone


//            IEnumerable<int> lst = new List<int>() { 1, 2, 3 };

//            foreach (var item in lst)
//            {
//                Console.WriteLine($"items = {item}");
//            }


//            //Thread issue

//            ////uncomment later to check
//            Parallel.For(1, 100, (i, state) => Go());

//            Parallel.For(1, 100, (i, state) => GoThreadSafe());

//            var volatileTest = new VolatileTest();
//            Parallel.For(1, 100, (i, state) => volatileTest.GoVolatile(i));

//            Console.WriteLine("   ");
//            Parallel.For(1, 100, (i, state) => volatileTest.GoVolatile1(i));

//            //var t =  GetAsync();
//            //TraceThreadAndTask($"GetAsync call");

//            ////GetAsyncBlocking(); //uncomment later to check
//            //Console.WriteLine($"GetAsyncBlocking call");

//            //await t;

//            //uncomment later to check
//            //TaskAwaiter awaiter = GetAsync().GetAwaiter();
//            //awaiter.OnCompleted(() => TraceThreadAndTask("On Completion"));

//            //TraceThreadAndTask("before completion");

//            //TraceThreadAndTask("Started Main call on");
//            //await GetAsync();
//            //TraceThreadAndTask("Ended Main call on");

//            var context = SynchronizationContext.Current;
//            Console.WriteLine($"Current synchronization context= {context}");

//            await DoAsync();

//            GetAsync().ContinueWith(t =>
//            {
//                var errors = t.Exception as AggregateException;
//                if (errors == null)
//                {
//                    TraceThreadAndTask("Inside continue with exception");
//                }

//                context.Post(delegate 
//                {
//                    GetAsyncBlocking();
//                    TraceThreadAndTask("Inside sync context");

//                }, null);
//            });

//            Thread worker = new Thread(() => {
//                //Console.WriteLine("Inside new thread");
//                Console.ReadLine();
//            });
//            //worker.IsBackground = true;
//            worker.IsBackground = false;
//            worker.Start();


//            //Console.ReadLine();  

//        }

//        private static async Task DoAsync()
//        {
//            TraceThreadAndTask("Do Async inside Main");

//            TaskScheduler sch = null;
//            new TaskFactory().StartNew(() =>
//            {
//                var context = SynchronizationContext.Current;
//                sch = TaskScheduler.Current;
//                TraceThreadAndTask("Do Async inside T1");

//                var x = 10;

//                new TaskFactory().StartNew(() =>
//                {
//                    var context1 = SynchronizationContext.Current;
//                    TraceThreadAndTask("Do Async inside T2");
//                }).ContinueWith((t)=> {
//                    var context1 = SynchronizationContext.Current;
//                    TraceThreadAndTask("Do Async inside T3");
//                }, sch);
//            });
//        }

//        public class VolatileTest
//        {
//            static object loc = new object();
//            private volatile bool shouldStop = false;

//            private bool shouldStop1 = false;

//            public void GoVolatile(int i)
//            {
//                if (!shouldStop)
//                {
//                    if (i == 23)
//                        shouldStop = true;

//                    Console.WriteLine("Running " + i);
//                }
//                else
//                {
//                    Console.WriteLine("Stopped " + i);
//                    return;
//                }
//            }

//            public void GoVolatile1(int i)
//            {
//                lock (loc)
//                {
//                    if (!shouldStop1)
//                    {
//                        if (i == 23)
//                            shouldStop1 = true;

//                        Console.WriteLine("Running " + i);
//                    }
//                    else
//                    {
//                        Console.WriteLine("Stopped " + i);
//                        return;
//                    }
//                }
//            }

//        }
        

//        private static void Go()
//        {
//            if (val2 != 0)
//            {
//                Console.WriteLine($"value = {val1 / val2}");
//                Thread.Sleep(100);
//            }
//            val2 = 0;
//        }
//        private static object _locakable = new object();

//        private static void GoThreadSafe()
//        {
//            lock(_locakable)
//            {
//                if (val2 != 0)
//                {
//                    var x = val1 / val2;
//                    Console.WriteLine("value of x= " + x);
//                }
//            }

//            Monitor.Enter(_locakable);
//            if (val2 != 0)
//            {
//                var x = val1 / val2;
//                Console.WriteLine("value of x= " + x);
//            }
//            Monitor.Exit(_locakable);
//            val2 = 0;
//        }

//        public static void TraceThreadAndTask(string info)
//        {
//            string taskInfo = Task.CurrentId == null ? "no task" : "task " +
//            Task.CurrentId;
//            Console.WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} " +
//            $"and {taskInfo}");
//        }

//        private static Task GetAsync()
//        {
//            var task1 = Task.Factory.StartNew(() =>
//            {
//                TraceThreadAndTask("Get Async Inside threaad");
//                for (int i = 0; i < 100; i++)
//                {
//                    Thread.Sleep(10);
//                }
//            });

//            var task2 = Task.Factory.StartNew(() =>
//            {
//                for (int i = 0; i < 100; i++)
//                {
//                    Thread.Sleep(10);
//                }
//            });

//            var t = Task.WhenAll(task1, task2);//this is non-blocking
//            TraceThreadAndTask("Get Async");
//            return t;
//        }

//        private static void GetAsyncBlocking()
//        {
//            TraceThreadAndTask("Get AsyncBloacking Inside threaad");

//            var task1 = Task.Factory.StartNew(() =>
//            {
//                for (int i = 0; i < 10; i++)
//                {
//                    Thread.Sleep(10);
//                }
//            });

//            var task2 = Task.Factory.StartNew(() =>
//            {
//                for (int i = 0; i < 10; i++)
//                {
//                    Thread.Sleep(10);
//                }
//            });

//            Task.WaitAll(task1, task2);//this is non-blocking
//            TraceThreadAndTask("Get GetAsync Blocking");
//        }
//    }

//    public class D1 : IDisposable
//    {
//        private static object _lockable = new Object();
//        public void Dispose()
//        {
//            Console.WriteLine("Calling dispose method");
//            lock (_lockable)
//            {
//                Dispose(true);
//            }
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool isExplicit)
//        {
//            if (isExplicit)
//            {
//                //Clean up managed resources here
//                //-Not required to run in case of destructor as the managed resource might has already been released
//            }
//            //Cleanup unmanaged
//        }

//        ~D1()
//        {
//            Console.WriteLine("Called by the GC");
//            lock (_lockable)
//            {
//                Dispose(false);
//            }
//        }
//    }
//}


