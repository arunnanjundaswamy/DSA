using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class LRUCacheTest
    {
        public static void Test()
        {
            //LRUCache cache = new LRUCache(3 /* capacity */ );
            //cache.Put("1", 1);
            //cache.Put("2", 2);
            //System.Console.WriteLine(cache.GetByKey("1"));       // returns 1
            //cache.Put("3", 3);    // evicts key 2
            //System.Console.WriteLine(cache.GetByKey("2"));       // returns -1 (not found)
            //cache.Put("4", 4);    // evicts key 1
            //System.Console.WriteLine(cache.GetByKey("1"));       // returns -1 (not found)
            //System.Console.WriteLine(cache.GetByKey("3"));       // returns 3
            //System.Console.WriteLine(cache.GetByKey("4"));       // returns 4

            LRUCacheWithLL cache1 = new LRUCacheWithLL(3 /* capacity */ );
            cache1.Put("1", 1);
            cache1.Put("2", 2);
            System.Console.WriteLine(cache1.Get("1"));       // returns 1
            cache1.Put("3", 3);    // evicts key 2
            System.Console.WriteLine(cache1.Get("2"));       // returns -1 (not found)
            cache1.Put("4", 4);    // evicts key 1
            System.Console.WriteLine(cache1.Get("1"));       // returns -1 (not found)
            System.Console.WriteLine(cache1.Get("3"));       // returns 3
            System.Console.WriteLine(cache1.Get("4"));       // returns 4


        }
    }

    //Good Approach
    public class LRUCacheWithLL
    {
        private int _size;
        private LinkedList<(string key, int val)> _ll = new LinkedList<(string key, int val)>();

        private Dictionary<string, LinkedListNode<(string key,int value)>> _dict = new Dictionary<string, LinkedListNode<(string key, int value)>>();
        
        public LRUCacheWithLL(int capacity)
        {
            _size = capacity;
        }

        public int Get(string key)
        {
            if (!_dict.ContainsKey(key)) return -1;

            var node = _dict[key];

            AddToHead(key, node);

            return node.Value.Item2;
        }

        private void AddToHead(string key, LinkedListNode<(string key, int val)> node)
        {
            
            //var chars = "a";// "ಕನ್ನ";// 'a'.ToString();
            //chars = Guid.NewGuid().ToString();
            //var x = Encoding.UTF32.GetByteCount(chars);
            //Console.WriteLine(x);

            //var prev = node.Previous;
            //var next = node.Next;

            //if (prev == null && next == null)//if both head and tail - only one node
            if(_ll.Count == 0) //no node exists
            {
                _ll.AddFirst(node);
                return;
            }


            //if (prev == null)//if it is already head
            if(_ll.First.Value.key == node.Value.key)
                return;

            //if(next == null)//if tail
            //{
            //    _ll.RemoveLast();
            //    _ll.AddFirst(val.Value);
            //    return;
            //}
            //else if in any other node

            _ll.Remove(node);
            _ll.AddFirst(node);
        }

        public void Put(string key, int val)
        {
            if (_dict.ContainsKey(key))
            {
                AddToHead(key, _dict[key]);
            }
            else
            {
                if(_dict.Count >= _size)
                {
                    var last = _ll.Last;
                    var lastItemKey = last.Value.Item1;
                    _ll.RemoveLast();
                    _dict.Remove(lastItemKey);
                }

                var node = new LinkedListNode<(string key, int val)>((key, val));
                _dict[key] = node;
                AddToHead(key, node);
            }
        }
    }

    public class LRUCache
    {
        private Dictionary<string, DoublyLinkedNode> _cache;
        private DoublyLinkedNode _head;
        private DoublyLinkedNode _tail;
        private int _size = 0;
        public int Capacity { get; set; }


        public LRUCache(int size)
        {
            this.Capacity = size;
            _cache = new Dictionary<string, DoublyLinkedNode>(capacity: Capacity);
        }

        public int GetByKey(string key)
        {
            if (!_cache.ContainsKey(key)) return -1;

            var val = _cache[key].Value;

            //Move this node to head since it was accessed
            AddNode(_cache[key]);
            return val;
        }

        public void Put(string key, int value)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key].Value = value;
                AddNode(_cache[key]);
                return;
            }

            if (_size >= Capacity)
            {
                var removedKey = RemoveTailNode();
                if (removedKey != null)
                {
                    _cache.Remove(key);
                    _size--;
                }
            }

            var newNode = new DoublyLinkedNode(key, value);
            AddNode(newNode);
            _cache[key] = newNode;

            _size++;
        }


        //Add the new node to the head
        private void AddNode(DoublyLinkedNode node)
        {
            if (node == _head) return;

            if (_head == null)
            {
                _head = node;
                _tail = node;
            }
            else if (_head == _tail)
            {
                node.Next = _tail; //or _head;
                _head = node;

                _tail.Prev = node;
            }
            else if (node == _tail)
            {
                var prevNode = _tail.Prev;
                prevNode.Next = null;
                _tail = prevNode;

                var currentHead = _head;

                node.Next = currentHead;
                node.Prev = null;

                currentHead.Prev = node;
                _head = node;
            }
            else
            {
                var currentHead = _head;
                currentHead.Prev = node;

                node.Next = currentHead;
                node.Prev = null;

                _head = node;
            }

        }

        //Remove the node from List
        private void RemoveNode(DoublyLinkedNode node)
        {
            //Get Previous and Next nodes of this node
            var prevNode = node.Prev;
            var nextNode = node.Next;

            if (prevNode != null)
                prevNode.Next = nextNode;
            if (nextNode != null)
                nextNode.Prev = prevNode;
        }

        private string RemoveTailNode()
        {
            //Get Previous and Next nodes of this node
            var keyRemoved = _tail?.Key;

            var prevNode = _tail.Prev;

            if (prevNode != null)
                prevNode.Next = null;

            _tail = prevNode;

            return keyRemoved;
        }

        private void MoveNodeToHead(DoublyLinkedNode node)
        {
            //Remove the node from existing i.e. De-link it first and then move to head
            RemoveNode(node);
            AddNode(node);
        }
    }

    public class DoublyLinkedNode
    {
        public DoublyLinkedNode Prev { get; set; }
        public DoublyLinkedNode Next { get; set; }
        public string Key { get; set; }

        public int Value { get; set; }

        public DoublyLinkedNode(string key, int val)
        {
            this.Key = key;
            this.Value = val;
        }
    }

}
