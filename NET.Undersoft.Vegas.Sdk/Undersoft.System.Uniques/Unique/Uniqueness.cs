using System.Collections;
using System.Extract;
using System.Runtime.CompilerServices;
using System.Uniques;

namespace System.Uniques
{
    public class Unique32 : Uniqueness
    {
        public Unique32() : base(HashBits.bit32)
        {            
        }

        public override unsafe Byte[] ComputeUniqueBytes(byte* bytes, int length, uint seed = 0)
        {
            return UniqueCode32.ComputeUniqueBytes(bytes, length, seed);
        }
        public override unsafe Byte[] ComputeUniqueBytes(byte[] bytes, uint seed = 0)
        {
            return UniqueCode32.ComputeUniqueBytes(bytes, seed);
        }
        public override unsafe UInt64 ComputeUniqueKey(byte* bytes, int length, uint seed = 0)
        {
            return UniqueCode32.ComputeUniqueKey(bytes, length, seed);
        }
        public override unsafe UInt64 ComputeUniqueKey(byte[] bytes, uint seed = 0)
        {
            return UniqueCode32.ComputeUniqueKey(bytes, seed);
        }

        protected override unsafe Byte[] UniqueBytes(byte* obj, int length, uint seed = 0)
        {
            return ComputeUniqueBytes(obj, length, seed);
        }
        public override unsafe Byte[] UniqueBytes(IntPtr obj, int length, uint seed = 0)
        {
            return ComputeUniqueBytes((byte*)obj.ToPointer(), length, seed);
        }

        public override Byte[] UniqueBytes(Byte[] obj, uint seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }
        public override Byte[] UniqueBytes(Object obj, uint seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }
        public override Byte[] UniqueBytes(IList obj, uint seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }
        public unsafe override 
                        Byte[] UniqueBytes(string obj, uint seed = 0)
        {
            return obj.UniqueBytes32(seed);
        }
        public override Byte[] UniqueBytes(IUnique obj)
        {
            return obj.GetUniqueBytes();
        }

        protected override unsafe Int64 UniqueKey(byte* obj, int length, uint seed = 0)
        {
            return (long)ComputeUniqueKey(obj, length, seed);
        }
        public override unsafe Int64 UniqueKey(IntPtr obj, int length, uint seed = 0)
        {
            return (long)ComputeUniqueKey((byte*)obj.ToPointer(), length, seed);
        }

        public override Int64 UniqueKey(Byte[] obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public override Int64 UniqueKey(Object obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public override Int64 UniqueKey(IList obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public override Int64 UniqueKey(string obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public override Int64 UniqueKey(IUnique obj, uint seed)
        {
            return UniqueKey(obj.GetUniqueBytes(), seed);
        }
        public override Int64 UniqueKey(IUnique obj)
        {
            return obj.UniqueKey();
        }
        public override Int64 UniqueKey<V>(IUnique<V> obj, uint seed)
        {
            return UniqueKey(obj.UniqueValues(), seed);
        }
        public override Int64 UniqueKey<V>(IUnique<V> obj)
        {
            return obj.UniquesAsKey();
        }
    }

    public class Unique64 : Uniqueness
    {
        public Unique64() : base(HashBits.bit64)
        {
        }

        public override unsafe Byte[] ComputeUniqueBytes(byte* bytes, int length, uint seed = 0)
        {
            return UniqueCode64.ComputeUniqueBytes(bytes, length, seed);
        }
        public override unsafe Byte[] ComputeUniqueBytes(byte[] bytes, uint seed = 0)
        {
            return UniqueCode64.ComputeUniqueBytes(bytes, seed);
        }
        public override unsafe UInt64 ComputeUniqueKey(byte* bytes, int length, uint seed = 0)
        {
            return UniqueCode64.ComputeUniqueKey(bytes, length, seed);
        }
        public override unsafe UInt64 ComputeUniqueKey(byte[] bytes, uint seed = 0)
        {
            return UniqueCode64.ComputeUniqueKey(bytes, seed);
        }

        protected override unsafe Byte[] UniqueBytes(byte* obj, int length, uint seed = 0)
        {
            return ComputeUniqueBytes(obj, length, seed);
        }
        public override unsafe Byte[] UniqueBytes(IntPtr obj, int length, uint seed = 0)
        {
            return ComputeUniqueBytes((byte*)obj.ToPointer(), length, seed);
        }

        public override Byte[] UniqueBytes(Byte[] obj, uint seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }
        public override Byte[] UniqueBytes(Object obj, uint seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }
        public override Byte[] UniqueBytes(IList obj, uint seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }
        public override Byte[] UniqueBytes(string obj, uint seed = 0)
        {
            return obj.UniqueBytes64(seed);
        }
        public override Byte[] UniqueBytes(IUnique obj)
        {
            return obj.GetUniqueBytes();
        }

        protected override unsafe Int64 UniqueKey(byte* obj, int length, uint seed = 0)
        {
            return (long)ComputeUniqueKey(obj, length, seed);
        }
        public override unsafe Int64 UniqueKey(IntPtr obj, int length, uint seed = 0)
        {
            return (long)ComputeUniqueKey((byte*)obj.ToPointer(), length, seed);
        }

        public override Int64 UniqueKey(Byte[] obj, uint seed = 0)
        {
            return obj.UniqueKey64(seed);
        }
        public override Int64 UniqueKey(Object obj, uint seed = 0)
        {
            return obj.UniqueKey64(seed);
        }
        public override Int64 UniqueKey(IList obj, uint seed = 0)
        {
            return obj.UniqueKey64(seed);
        }
        public override Int64 UniqueKey(string obj, uint seed = 0)
        {
            return obj.UniqueKey64(seed);
        }
        public override Int64 UniqueKey(IUnique obj, uint seed)
        {
            return UniqueKey(obj.GetUniqueBytes(), seed);
        }
        public override Int64 UniqueKey(IUnique obj)
        {
            return obj.UniqueKey();
        }
        public override Int64 UniqueKey<V>(IUnique<V> obj, uint seed)
        {
            return UniqueKey(obj.UniqueValues(), seed);
        }
        public override Int64 UniqueKey<V>(IUnique<V> obj)
        {
            return obj.UniquesAsKey();
        }
    }

    public abstract class Uniqueness : IUniqueness
    {
        protected Uniqueness __base_;

        public Uniqueness()
        {
            __base_ = Unique.Key64;
        }
        public Uniqueness(HashBits hashBits)
        {
            if (hashBits == HashBits.bit32)
                __base_ = Unique.Key32;
            else
                __base_ = Unique.Key64;
        }

        public virtual unsafe Byte[] ComputeUniqueBytes(byte* bytes, int length, uint seed = 0)
        {
            return __base_.ComputeUniqueBytes(bytes, length, seed);
        }
        public virtual unsafe Byte[] ComputeUniqueBytes(byte[] bytes, uint seed = 0)
         {
            return __base_.ComputeUniqueBytes(bytes, seed);
        }
        public virtual unsafe UInt64 ComputeUniqueKey(byte* bytes, int length, uint seed = 0)
        {
            return __base_.ComputeUniqueKey(bytes, length, seed);
        }
        public virtual unsafe UInt64 ComputeUniqueKey(byte[] bytes, uint seed = 0)
        {
            return __base_.ComputeUniqueKey(bytes, seed);
        }

        protected virtual unsafe 
                       Byte[] UniqueBytes(byte* obj, int length, uint seed = 0)
        {
            return __base_.UniqueBytes(obj, length, seed);
        }
        public virtual Byte[] UniqueBytes(IntPtr obj, int length, uint seed = 0)
        {
            return __base_.UniqueBytes(obj, length, seed);
        }

        public virtual Byte[] UniqueBytes(Byte[] obj, uint seed = 0)
        {
            return __base_.UniqueBytes(obj, seed);
        }
        public virtual Byte[] UniqueBytes(Object obj, uint seed = 0)
        {
            return __base_.UniqueBytes(obj, seed);
        }
        public virtual Byte[] UniqueBytes(IList obj, uint seed = 0)
        {
            return __base_.UniqueBytes(obj, seed);
        }
        public virtual Byte[] UniqueBytes(string obj, uint seed = 0)
        {
            return __base_.UniqueBytes(obj, seed);
        }
        public virtual Byte[] UniqueBytes(IUnique obj)
        {
            return obj.GetUniqueBytes();
        }

        protected virtual unsafe
                       Int64 UniqueKey(byte* obj, int length, uint seed = 0)
        {
            return __base_.UniqueKey(obj, length, seed);
        }

        public virtual Int64 UniqueKey(IntPtr obj, int length, uint seed = 0)
        {
            return __base_.UniqueKey(obj, length, seed);
        }
        public virtual Int64 UniqueKey(Byte[] obj, uint seed = 0)
        {
            return __base_.UniqueKey(obj, seed);
        }
        public virtual Int64 UniqueKey(Object obj, uint seed = 0)
        {
            return __base_.UniqueKey(obj, seed);
        }
        public virtual Int64 UniqueKey(IList obj, uint seed = 0)
        {
            return __base_.UniqueKey(obj, seed);
        }
        public virtual Int64 UniqueKey(string obj, uint seed = 0)
        {
            return __base_.UniqueKey(obj, seed);
        }
        public virtual Int64 UniqueKey(IUnique obj, uint seed)
        {
            return __base_.UniqueKey(obj.UniqueKey(), seed);
        }
        public virtual Int64 UniqueKey(IUnique obj)
        {
            return obj.UniqueKey();
        }
        public virtual Int64 UniqueKey<V>(IUnique<V> obj, uint seed)
        {
            return __base_.UniqueKey(obj.UniqueValues(), seed);
        }
        public virtual Int64 UniqueKey<V>(IUnique<V> obj)
        {
            return obj.UniquesAsKey();
        }
    }

    public interface IUniqueness
    {
        Byte[] UniqueBytes(Byte[] obj, uint seed = 0);
        Byte[] UniqueBytes(Object obj, uint seed = 0);
        Byte[] UniqueBytes(IList obj, uint seed = 0);
        Byte[] UniqueBytes(string obj, uint seed = 0);
        Byte[] UniqueBytes(IUnique obj);

        Int64 UniqueKey(Byte[] obj, uint seed = 0);
        Int64 UniqueKey(Object obj, uint seed = 0);
        Int64 UniqueKey(IList obj, uint seed = 0);
        Int64 UniqueKey(string obj, uint seed = 0);
        Int64 UniqueKey(IUnique obj, uint seed);
        Int64 UniqueKey(IUnique obj);
        Int64 UniqueKey<V>(IUnique<V> obj);
    }

    public enum HashBits
    {
        bit64,
        bit32
    }
}

