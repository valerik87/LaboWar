﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class PoolAllocator<T> : IDisposable where T : IDisposable, new()
    {
        private Stack<T> _items = new Stack<T>();
        private object _sync = new object(); 
          
        public T Get()
        {
            lock (_sync)
            {
                if (_items.Count == 0)
                {
                    return new T();
                }
                else
                {
                    return _items.Pop();
                }
            }
        }
          
        public void Free(T item)
        {
            lock (_sync)
            {
                _items.Push(item);
            }
        }

        public void Dispose()
        {
            lock (_sync)
            {
                while( _items.Count > 0)
                {
                    _items.Pop().Dispose();
                }                
            }
        }
    }
}
