using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA_Proj1
{

    public class LinkedListProblems
    {
        public static void Test()
        {
            LinkedList<int> ll = new LinkedList<int>();
            ll.AddLast(10);

            MidNodesTest();

            LinkedList.FirstNonRepeatingCharInStreamTest();
        }

        private static void MidNodesTest()
        {
            LinkedList llT = new LinkedList();
            llT.Insert(1);
            llT.Insert(2);
            llT.Insert(3);
            llT.Insert(4);

            //Even Nodes: Ans: 3
            var midNode = llT.FindMidNode(llT.Head);
            Console.WriteLine($"Mid Node = {midNode}");

            //Odd nodes 1->2->3->4->5
            //Ans: 3
            llT.Insert(5);
            midNode = llT.FindMidNode(llT.Head);
            Console.WriteLine($"Mid Node = {midNode}");
        }
    }

    public class LinkedList
    {
        public LinkedListNode Head { get; set; }
        public LinkedListNode Tail { get; set; }

        // This is an input class. Do not edit.
        public class LinkedListNode
        {
            public int value;
            public LinkedListNode next;

            public LinkedListNode(int value)
            {
                this.value = value;
                this.next = null;
            }
        }

        public void Insert(int val)
        {
            var ll = new LinkedListNode(val);

            if (Head == null)
            {
                Head = ll;
                Tail = ll;
            }
            else
            {
                Tail.next = ll;
                Tail = ll;
            }

        }

        public LinkedListNode Search(int value)
        {
            var ll = Head;

            while(ll != null)
            {
                if (ll.value == value)
                    return ll;
                else
                {
                    ll = ll.next;
                }
            }

            return null;
        }

        public void Remove(int value)
        {
            var ll = Head;
            var nextPtr = ll.next;

            while(ll != null)
            {
                if (nextPtr == null) return;

                if(nextPtr.value == value)
                {
                    ll.next = nextPtr.next;
                    return;
                }
                ll = nextPtr;
                nextPtr = nextPtr.next;
            }
        }


        public void RemoveKthElement(int nodetoRemove)
        {
            if(nodetoRemove == 0)
            {
                Head = Head.next;
                return;
            }

            var currentNodePtr = Head;
            var nextNodePtr = Head.next;
            var index = 1;

            while(currentNodePtr != null)
            {
                if(nodetoRemove == index)
                {
                    currentNodePtr.next = nextNodePtr.next;
                    return;
                }
                currentNodePtr = nextNodePtr;
                nextNodePtr = nextNodePtr.next;
                index++;
            }
        }

        public void RemoveKthElementFromEnd(int k)
        {
            //1->2->3->4->5
            //k = 2 i.e. 4 to be removed
            var p1 = Head;
            var p2 = Head;

            //Move the p2 k nodes ahead
            var index = 0;
            while(index < k && p2 != null)
            {
                p2 = p2.next;//P2 = Head + k;
                index++;
            }

            if(p2 == null)
            {
                Head = Head.next;
                Head.next = Head.next.next;
            }

            //Then head need to be deleted
            if(p2.next == null)
            {
                Head = Head.next;
                return;
            }

            while(p2.next != null)
            {
                p1 = p1.next;//stops at 3
                p2 = p2.next;//stops at 5
            }

            //ignoring p2
            p1.next = p1.next.next;
        }

        public LinkedListNode Reverse(LinkedListNode linkedList)
        {
            //Input: 1->2->2->3->4
            //Output: 4->3->2->1

            LinkedListNode prevPtr = null;
            var currentPtr = linkedList ?? Head;
            var nextPtr = currentPtr.next;

            while(currentPtr != null)
            {
                currentPtr.next = prevPtr;
                prevPtr = currentPtr;
                currentPtr = nextPtr;
                nextPtr = nextPtr.next;
            }

            return prevPtr;
        }

        public LinkedListNode MergeTwoLinkedList(LinkedListNode first, LinkedListNode second)
        {
            if (first == null) return second;
            if (second == null) return first;

            var pathPtr = new LinkedListNode(-1);

            while(first != null && second != null)
            {
                if (first.value < second.value)
                {
                    pathPtr.next = first;
                    first = first.next;
                }
                else
                {
                    pathPtr.next = second;
                    second = second.next;
                }
            }

            while(first != null)
                pathPtr.next = first;

            while (second != null)
                pathPtr.next = second;

            return pathPtr.next;
        }

        public bool IsPalindrome(LinkedListNode linkedList)
        {
            //Input: 1->2->3-2->1 (odd)
            //Input: 1->2->2->1 (Even)

            //Find the mid node
            //Reverse from mid to end
            //Compare head to mid and mid to end

            var midPtr = FindMidNode(linkedList);
            midPtr = Reverse(midPtr);
            var headPtr = linkedList;

            while(midPtr != null)
            {
                if (headPtr.value != midPtr.value)
                    return false;

                headPtr = headPtr.next;
                midPtr = midPtr.next;
            }

            return true;
        }

        public LinkedListNode FindMidNode(LinkedListNode head)
        {
            //Input: 1->2->3->2->1 (odd)
            //mid = 3.next i.e 2

            //Input: 1->2->2->1 (Even)
            //mid = 2

            //slow and fast pointers
            var slow = head;
            var fast = head;

            //Incorrect
            /*
            while(fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            if (fast != null)// && fast.next == null)//odd
                slow = slow.next;

            return slow;
            */

            while(fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next != null ? fast.next.next : null;
            }

            return slow;
        }


        private LinkedListNode ReArrangeLinkedList()
        {
            //Input: 1->2->3->4->5->6 (Even)
            //Output: 1->6->2->5->3->4
            //Input: 1->2->3->4->5 (Odd)
            //output: 1->6->2->5->3
            //Approach:
            //FindMidNode
            //Spreate into two list l1 and l2 from mid node
            //reveres l2
            //Merge l1 and reversed l2
            //1->2->3
            //6->5->4

            var l2Head = FindMidNode(Head);
            var reversedL2 = Reverse(l2Head);

            var currentL1 = Head;
            var currentL2 = reversedL2;
            var l1Next = Head.next;
            var l2Next = currentL2.next;

            while(l1Next != null && l2Next != null)
            {
                currentL1.next = currentL2;
                currentL2.next = l1Next;

                currentL1 = l1Next;
                currentL2 = l2Next;

                l1Next = l1Next?.next;
                l2Next = l2Next?.next;
            }

            return Head;
        }

        public static void FirstNonRepeatingCharInStreamTest()
        {
            /*
             * https://www.geeksforgeeks.org/find-first-non-repeating-character-stream-characters/
             * 
             * Input:

                aabcbc
                Output:

                a -1 b b c -1 
                Explanation:

                When the input stream is "a,"  the first non-repeating character, "a," is appended.
                When the input stream is "aa,"  there is no first non-repeating character, so "-1" is appended.
                When the input stream is "aab,"  the first non-repeating character, "b," is appended.
                When the input stream is "aabc,"  the first non-repeating character, "b," is appended.
                When the input stream is "aabcb,"  the first non-repeating character, "c," is appended.
                When the input stream is "aabcbc," there is no first non-repeating character, so "-1" is appended.

             * 
             * Given a stream of characters, find the first non-repeating character from the stream. You need to tell the first non-repeating character in O(1) time at any moment.
             * 
             * Adding new element or remvoing the element in the LL takes 0(1) time and hence LL.
             Note: that appending a new node to DLL is O(1) operation if we maintain a tail pointer. Removing a node from DLL is also O(1). So both operations, addition of new character and finding first non-repeating character take O(1) time.
             */

            var str = "abcd";

            for (int index = 0; index < str.Length; index++)
            {
                FirstNonRepeatingCharInStreamHandler.FirstNonRepeatingCharInStream(str[index]);
            }

            //output: 'a'
            Console.WriteLine($"First non-repeating char= {FirstNonRepeatingCharInStreamHandler.GetFirstNonRepeatingCharInStream()}");

            FirstNonRepeatingCharInStreamHandler.FirstNonRepeatingCharInStream('a');
            //output: 'b' since a is removed now
            Console.WriteLine($"First non-repeating char= {FirstNonRepeatingCharInStreamHandler.GetFirstNonRepeatingCharInStream()}");

            FirstNonRepeatingCharInStreamHandler.FirstNonRepeatingCharInStream('c');
            //output: 'b'  - 'c' is removed now
            Console.WriteLine($"First non-repeating char= {FirstNonRepeatingCharInStreamHandler.GetFirstNonRepeatingCharInStream()}");

            //this should not add 'a' - since it was existing and removed
            FirstNonRepeatingCharInStreamHandler.FirstNonRepeatingCharInStream('a');
            //output: 'b' since a is removed now
            Console.WriteLine($"First non-repeating char= {FirstNonRepeatingCharInStreamHandler.GetFirstNonRepeatingCharInStream()}");
        }

        public class FirstNonRepeatingCharInStreamHandler
        {
            //LLNode - Doubly linked node that keeps each character 
            //lookup the holds the LLnode for each char
            private static Dictionary<char, LinkedListNode<char>> nodeLkp = new Dictionary<char, LinkedListNode<char>>();
            private static LinkedList<char> ll = new LinkedList<char>();

            public static void FirstNonRepeatingCharInStream(char c)
            {
                //if already added previously and removed (hence null value) then no need to add as it is a repeat
                if (nodeLkp.ContainsKey(c) && nodeLkp[c] == null)
                {
                    return;
                }

                if (!nodeLkp.ContainsKey(c))
                {
                    var newNode = new LinkedListNode<char>(c);

                    //Adding the very first char
                    if (ll.Count == 0)
                    {
                        ll.AddFirst(newNode);
                    }
                    else
                    {
                        //add other chars to the tail
                        //current tail
                        var currentlast = ll.Last;
                        ll.AddAfter(currentlast, newNode);
                    }

                    nodeLkp.Add(c, ll.Last);
                }
                else
                {
                    //if the char is coming again then delete that char already in the ll and also lkp should be made null(null is used to mark that the same char has come previously multiple time and hence removed and should not be considered again.
                    //e.g. a->c->c(deleted now)->b->c(should not be considered now also)
                    var repeatedNode = nodeLkp[c];
                    var prevNode = repeatedNode.Previous;

                    if (prevNode == null)//if head
                    {
                        ll.Remove(repeatedNode);
                    }
                    else
                    {
                        ll.Remove(repeatedNode);
                    }

                    //remove from lkp now
                    nodeLkp[c] = null;
                }
            }

            public static char GetFirstNonRepeatingCharInStream()
            {
                return ll.First.Value;
            }
        }
    }


    public class CircularLinkedList<T> : LinkedList<T>
    {
        public new IEnumerator GetEnumerator()
        {
            return new CircularLinkedListEnumerator<T>(this);
        }
    }

    public class CircularLinkedListEnumerator<T> : IEnumerator<T>
    {
        public T Current => _current.Value;

        object IEnumerator.Current => Current;

        private LinkedListNode<T> _current;

        public CircularLinkedListEnumerator(LinkedList<T> list)
        {
            _current = list.First;
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (_current == null) return false;

            _current = _current.Next ?? _current.List.First;

            return true;
        }

        public void Reset()
        {
            _current = _current.List.First;
        }
    }

    public class LinkedListRemoveDuplicates
    {
        // This is an input class. Do not edit.
        public class LinkedListNode
        {
            public int value;
            public LinkedListNode next;

            public LinkedListNode(int value)
            {
                this.value = value;
                this.next = null;
            }
        }

        public LinkedListNode RemoveDuplicatesFromLinkedList(LinkedListNode root)
        {
            // Write your code here.
            //Input: 1->1->2->3->4
            //Output: 1->2->3->4

            var currentNodePtr = root;

            while (currentNodePtr != null)
            {
                var nextNodePtr = currentNodePtr.next;

                while (nextNodePtr != null &&
                            currentNodePtr.value == nextNodePtr.value)
                {
                    //Change the next property of the next pointer to the
                    //next of the duplicate i.e. to value '2' in above input
                    currentNodePtr.next = nextNodePtr.next;
                    nextNodePtr = nextNodePtr.next;
                }
                currentNodePtr = nextNodePtr;
            }


            return root;
        }
    }


    #region AddTwoNumbersInLL

    /**
 * Definition for singly-linked list.
 * class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {this.val = x; this.next = null;}
 * }
 */
    public class LLSolution
    {
        /*
         A : [ 9 -> 9 -> 1 ]
        B : [ 1 ]

        The expected return value: 
        0 -> 0 -> 2 
         */

        public static void AddTwoNumbersTest()
        {
            var llA = new ListNode(9);
            llA.next = new ListNode(9);
            llA.next.next = new ListNode(1);

            var llB = new ListNode(1);

            var resNode = AddTwoNumbers(llA, llB);
        }
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { this.val = x; this.next = null; }
        }

        public static ListNode AddTwoNumbers(ListNode A, ListNode B)
        {

            var resultNode = new ListNode(-1);

            var head = resultNode;

            var prevRemainder = 0;
            while (A != null && B != null)
            {
                var sum = A.val + B.val;
                sum = sum + prevRemainder;
                prevRemainder = 0;

                if (sum >= 10)
                {
                    resultNode.next = new ListNode(sum % 10);
                    prevRemainder = sum / 10;
                }
                else
                {
                    resultNode.next = new ListNode(sum);
                }
                Console.WriteLine(resultNode.val);
                A = A.next;
                B = B.next;

                resultNode = resultNode.next;
            }

            while (A != null)
            {
                var sum = A.val;
                sum = sum + prevRemainder;
                prevRemainder = 0;

                if (sum >= 10)
                {
                    resultNode.next = new ListNode(sum % 10);
                    prevRemainder = sum / 10;
                }
                else
                {
                    resultNode.next = new ListNode(sum);
                }
                Console.WriteLine(resultNode.val);
                A = A.next;
                resultNode = resultNode.next;
            }

            while (B != null)
            {
                var sum = B.val;
                sum = sum + prevRemainder;
                prevRemainder = 0;

                if (sum >= 10)
                {
                    resultNode.next = new ListNode(sum % 10);
                    prevRemainder = sum / 10;
                }
                else
                {
                    resultNode.next = new ListNode(sum);
                }
                Console.WriteLine(resultNode.val);
                B = B.next;
                resultNode = resultNode.next;
            }

            return head.next;
        }
    }

    #endregion
}


public class LL<T>
{
    public LLNode<T> Head { get; set; }

    public LLNode<T> Tail { get; set; }

    public void Add(LLNode<T> node)
    {
        if(Tail == null)
        {
            Head = Tail = node;
            return;
        }

        var tail = Tail;
        tail.Next = node;
        Tail = node;
    }


    public class LLNode<T>
    {
        public LLNode<T> Next { get; set; }

        public LLNode<T> Previous { get; set; }

        public T Val { get; set; }

        public LLNode(T value)
        {
            Val = value;
        }
    }
}
