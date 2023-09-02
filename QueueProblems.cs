using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA_Prac2
{
    public class QueueProblemsTest
    {
        public void Test()
        {
        }
    }

    public class QueueUsingDoubleStackPushCostly
    {
        Stack<int> _stack = new Stack<int>();

        public void EnQueue(int val)
        {
            var tempStack = new Stack<int>();

            while (_stack.Count > 0)
            {
                tempStack.Push(_stack.Pop());
            }

            _stack.Push(val);

            while (tempStack.Count > 0)
            {
                _stack.Push(tempStack.Pop());
            }
        }

        public void EnQueueUsingCallStack(int val)
        {
            _stack.Push(val);
        }

        public int Dequeue()
        {
            if (_stack.Count == 0) throw new Exception("Queue is empty");

            return _stack.Pop();
        }

        public int DequeueUsingCallStack()
        {
            if (_stack.Count == 0) throw new Exception("Queue is empty");

            //Till you find the last element - start popping and
            //Basecase of recursion - where it should exit the  recursion
            if(_stack.Count == 1)
            {
                return _stack.Pop();
            }
            else //recursion case
            {
                int val = _stack.Pop();
                int retValue = DequeueUsingCallStack();
                _stack.Push(val);

                return retValue;
            }
        }

        public int Peek()
        {
            if (_stack.Count == 0) throw new Exception("Queue is empty");

            //Till you find the last element - start popping and
            //Basecase of recursion - where it should exit the  recursion
            if (_stack.Count == 1)
            {
                var val = _stack.Pop();
                _stack.Push(val);
                return val;
            }
            else //recursion case
            {
                int val = _stack.Pop();
                int retValue = DequeueUsingCallStack();
                _stack.Push(val);

                return retValue;
            }
        }
    }

}
