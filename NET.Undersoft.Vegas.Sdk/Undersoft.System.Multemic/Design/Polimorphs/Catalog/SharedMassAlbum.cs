﻿using System.Collections.Generic;
using System.Uniques;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

/*******************************************************************************
    Copyright (c) 2020 Undersoft

    System.Multemic.SharedMassAlbum
    
    abstract class for Safe-Thread MultiCardBook, 
        
    @author Darius Hanc      
    @project NETStandard.Undersoft.SDK                                   
    @version 0.8.D (Feb 7, 2020)                                            
    @licence MIT                                        
 
 ********************************************************************************/
namespace System.Multemic
{
    public abstract class SharedMassAlbum<V> : CardMassBook<V> where V : IUnique
    {
        #region Globals       

        protected static readonly int WAIT_READ_TIMEOUT = 5000;
        protected static readonly int WAIT_WRITE_TIMEOUT = 5000;
        protected static readonly int WAIT_REHASH_TIMEOUT = 5000;

        protected ManualResetEventSlim waitRead = new ManualResetEventSlim(true, 128);
        protected ManualResetEventSlim waitWrite = new ManualResetEventSlim(true, 128);
        protected ManualResetEventSlim waitRehash = new ManualResetEventSlim(true, 128);
        protected SemaphoreSlim writePass = new SemaphoreSlim(1);

        public int readers;

        protected void acquireRehash()
        {         
            if (!waitRehash.Wait(WAIT_REHASH_TIMEOUT))
                throw new TimeoutException("Wait write Timeout");
            waitRead.Reset();
        }
        protected void releaseRehash()
        {
            waitRead.Set();
        }
        protected void acquireReader()
        {
           Interlocked.Increment(ref readers);
            waitRehash.Reset();
            if (!waitRead.Wait(WAIT_READ_TIMEOUT))
                throw new TimeoutException("Wait write Timeout");
        }
        protected void releaseReader()
        {
            if (0 == Interlocked.Decrement(ref readers))
                waitRehash.Set();            
        }
        protected void acquireWriter()
        {
            do
            {
                if (!waitWrite.Wait(WAIT_WRITE_TIMEOUT))
                    throw new TimeoutException("Wait write Timeout");
                waitWrite.Reset();
            }
            while (!writePass.Wait(0));
        }
        protected void releaseWriter()
        {           
            writePass.Release();
            waitWrite.Set();
        }

        #endregion

        #region Constructor

        public SharedMassAlbum() : base(16, HashBits.bit64)
        {
        }
        public SharedMassAlbum(int capacity = 17, HashBits bits = HashBits.bit64) : base(capacity, bits)
        {      
        }
        public SharedMassAlbum(IList<V> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }
        public SharedMassAlbum(IList<IUnique<V>> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }
        public SharedMassAlbum(IEnumerable<V> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }
        public SharedMassAlbum(IEnumerable<IUnique<V>> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }
        #endregion

        #region Operations

        protected override                V InnerGet(long key)
        {
            acquireReader();
            var v = base.InnerGet(key);
            releaseReader();
            return v;
        }

        protected override             bool InnerTryGet(long key, out ICard<V> output)
        {
            acquireReader();
            var test = base.InnerTryGet(key, out output);
            releaseReader();
            return test;
        }

        protected override          ICard<V> InnerGetCard(long key)
        {
            acquireReader();
            var card = base.InnerGetCard(key);
            releaseReader();
            return card;
        }

        public override          ICard<V> GetCard(int index)
        {          
            if (index < count)
            {
                acquireReader();
                if (removed > 0)
                {
                    releaseReader();
                    acquireWriter();                   
                    Reindex();
                    releaseWriter();
                    acquireReader();
                }

                var temp = list[index];
                releaseReader();
                return temp;
            }
            throw new IndexOutOfRangeException("Index out of range");                     
        }

        protected override          ICard<V> InnerPut(long key, V value)
        {            
            acquireWriter();
            var temp = base.InnerPut(key, value);
            releaseWriter();
            return temp;
        }
        protected override          ICard<V> InnerPut(V value)
        {
            acquireWriter();
            var temp = base.InnerPut(value);
            releaseWriter();
            return temp;
        }
        protected override          ICard<V> InnerPut(ICard<V> value)
        {
            acquireWriter();
            var temp = base.InnerPut(value);
            releaseWriter();
            return temp;
        }

        protected override             bool InnerAdd(long key, V value)
        {
            acquireWriter();
            var temp = base.InnerAdd(key, value);
            releaseWriter();
            return temp;
        }
        protected override             bool InnerAdd(V value)
        {
            acquireWriter();
            var temp = base.InnerAdd(value);
            releaseWriter();
            return temp;
        }
        protected override             bool InnerAdd(ICard<V> value)
        {
            acquireWriter();
            var temp = base.InnerAdd(value);
            releaseWriter();
            return temp;
        }

        public override             void Insert(int index, ICard<V> item)
        {
            acquireWriter();
            base.InnerInsert(index, item);
            releaseWriter();            
        }

        protected override                V InnerRemove(long key)
        {
            acquireWriter();
            var temp = base.InnerRemove(key);
            releaseWriter();
            return temp;
        }      

        public override             bool TryDequeue(out V output)
        {
            acquireWriter();
            var temp = base.TryDequeue(out output);
            releaseWriter();
            return temp;
        }
        public override             bool TryDequeue(out ICard<V> output)
        {
            acquireWriter();
            var temp = base.TryDequeue(out output);
            releaseWriter();
            return temp;
        }

        public override              int IndexOf(ICard<V> item)
        {
            int id = 0;
            acquireReader();
            id = base.IndexOf(item);
            releaseReader();
            return id;
        }
        public override              int IndexOf(V item)
        {
            int id = 0;
            acquireReader();
            id = base.IndexOf(item);
            releaseReader();
            return id;
        }

        public override             void CopyTo(ICard<V>[] array, int index)
        {
            acquireReader();
            base.CopyTo(array, index);
            releaseReader();
        }
        public override             void CopyTo(Array array, int index)
        {
            acquireReader();
            base.CopyTo(array, index);
            releaseReader();
        }
        public override             void CopyTo(V[] array, int index)
        {
            acquireReader();
            base.CopyTo(array, index);
            releaseReader();
        }

        public override              V[] ToArray()
        {
            acquireReader();
            V[] array = base.ToArray();
            releaseReader();
            return array;
        }

        public override             void Clear()
        {
            acquireWriter();
            acquireRehash();

            base.Clear();

            releaseRehash();
            releaseWriter();
        }

        protected override             void Rehash(int newsize)
        {
            acquireRehash();           
            base.Rehash(newsize);
            releaseRehash();
        }

        protected override             void Reindex()
        {
           
            acquireRehash();
            base.Reindex();
            releaseRehash();
           
        }

        #endregion

    }

}