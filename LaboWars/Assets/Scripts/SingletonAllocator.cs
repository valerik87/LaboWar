using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class SingletonAllocator<T> : IDisposable where T : IDisposable, new ()
    {
        private static SingletonAllocator<T> instance = null;
        //private static object locker = new object();        //for safe-thread

        //this is the Allocator templeted on T
        private static PoolAllocator<T> pool;

        //private costructor (singleton)
        private SingletonAllocator()
        {
            pool = new PoolAllocator<T>();        
        }       
        public void Dispose()
        {
            //lock (locker)
            //{
            pool.Dispose();
            //}
        }

        public static SingletonAllocator<T> GetInstance()
        {
            //get
            //{
                //lock (locker)
                //{
                    if (instance == null) instance = new SingletonAllocator<T>();
                    return instance;
                //}
            //}
        }

        public T GetFromPool()
        {
            return pool.Get();
        }
        public void Free(T item)
        {
            pool.Free(item);
        }
    }
}
