using System.Collections;
using System.Extract;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Uniques
{
    public unsafe static class UniqueKeyExtensions64
    {
        public static Byte[] UniqueBytes64(this IntPtr bytes, int length, uint seed = 0)
        {
            return UniqueCoder64.ComputeUniqueBytes((byte*)bytes.ToPointer(), length, seed);
        }
        public static Byte[] UniqueBytes64(this Byte[] bytes, uint seed = 0)
        {
            return UniqueCoder64.ComputeUniqueBytes(bytes, seed);
        }
        public static Byte[] UniqueBytes64(this Object obj, uint seed = 0)
        {
            if (obj is IUnique)
                return ((IUnique)obj).GetUniqueBytes();
            if (obj is ValueType)
                return GetValueTypeHashBytes64(obj);
            if (obj is string)
                return (((string)obj)).UniqueBytes64();
            if (obj is IList)
                return UniqueBytes64((IList)obj);
            return UniqueCoder64.ComputeUniqueBytes(obj.GetBytes(true), seed);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] UniqueBytes64(this IList obj, uint seed = 0)
        {
            if (obj is Byte[])
                return UniqueCoder64.ComputeUniqueBytes((Byte[])obj, seed);

            int length = 256, offset = 0, postoffset = 0, count = obj.Count, charsize = sizeof(char), s = 0;
            byte* buffer = stackalloc byte[length];
            bool toResize = false;

            for (int i = 0; i < count; i++)
            {
                object o = obj[i];
                if (o is string)
                {
                    string str = ((string)o);
                    s = str.Length * charsize;
                    postoffset = (s + offset);

                    if (postoffset > length)
                        toResize = true;
                    else
                        fixed (char* c = str)
                            Extractor.CopyBlock(buffer, (byte*)c, offset, s);
                }
                else
                {
                    if (o is IUnique)
                    {
                        s = 8;
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            *((long*)(buffer + offset)) = ((IUnique)o).UniqueKey;
                    }
                    else
                    {
                        s = o.GetSize();
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            Extractor.StructureToPointer(o, new IntPtr(buffer + offset));
                    }
                }

                if (toResize)
                {
                    i--;
                    toResize = false;
                    byte* _buffer = stackalloc byte[postoffset];
                    Extractor.CopyBlock(_buffer, buffer, offset);
                    buffer = _buffer;
                    length = postoffset;
                }
                else
                    offset = postoffset;
            }

            return UniqueCoder64.ComputeUniqueBytes(buffer, offset, seed);
        }
        public static Byte[] UniqueBytes64(this String obj, uint seed = 0)
        {
            fixed (char* c = obj)
                return UniqueCoder64.ComputeUniqueBytes((byte*)c, obj.Length * sizeof(char), seed);
        }
        public static Byte[] UniqueBytes64(this IUnique obj)
        {
            return obj.GetUniqueBytes();
        }

        public static unsafe 
                       Int64 UniqueKey64(this IntPtr ptr, int length, uint seed = 0)
        {
            return (long)UniqueCoder64.ComputeUniqueKey((byte*)ptr.ToPointer(), length, seed);
        }
        public static  Int64 UniqueKey64(this Byte[] bytes, uint seed = 0)
        {
            return (long)UniqueCoder64.ComputeUniqueKey(bytes, seed);
        }
        public static  Int64 UniqueKey64(this Object obj, uint seed = 0)
        {
            if (obj is IUnique)
                return ((IUnique)obj).UniqueKey;
            if (obj is IList)
                return UniqueKey64((IList)obj);
            if (obj is string)
                return (((string)obj)).UniqueKey64();        
            if (obj is ValueType)
                return getValueTypeUniqueKey64(obj);                    
            return (long)UniqueCoder64.ComputeUniqueKey(obj.GetBytes(true), seed);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe 
                       Int64 UniqueKey64(this IList obj, uint seed = 0)
        {
            if (obj is Byte[])
                return (long)UniqueCoder64.ComputeUniqueKey((Byte[])obj, seed);
          
            int length = 256, offset = 0, postoffset = 0, count = obj.Count, charsize = sizeof(char), s = 0;
            byte* buffer = stackalloc byte[length];
            bool toResize = false;

            for (int i = 0; i < count; i++)
            {
                object o = obj[i];
                if (o is string)
                {
                    string str = ((string)o);
                    s = str.Length * charsize;
                    postoffset = (s + offset);

                    if (postoffset > length)
                        toResize = true;
                    else
                        fixed (char* c = str)
                            Extractor.CopyBlock(buffer, (byte*)c, offset, s);
                }
                else
                {
                    if (o is IUnique)
                    {
                        s = 8;
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            *((long*)(buffer + offset)) = ((IUnique)o).UniqueKey;
                    }
                    else
                    {
                        s = o.GetSize();
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            Extractor.StructureToPointer(o, new IntPtr(buffer + offset));
                    }
                }

                if(toResize)
                {
                    i--;
                    toResize = false;
                    byte* _buffer = stackalloc byte[postoffset];
                    Extractor.CopyBlock(_buffer, buffer, offset);
                    buffer = _buffer;
                    length = postoffset;
                }
                else
                    offset = postoffset;
            }

            return (long)UniqueCoder64.ComputeUniqueKey(buffer, offset, seed);
        }      
        public static  Int64 UniqueKey64(this String obj, uint seed = 0)
        {
            fixed (char* c = obj)
            {
                return (long)UniqueCoder64.ComputeUniqueKey((byte*)c, obj.Length * sizeof(char), seed);
            }
        }
        public static  Int64 UniqueKey64<V>(this IUnique<V> obj, uint seed)
        {
            return UniqueKey64(obj.UniqueValues(), seed);
        }
        public static Int64  UniqueKey64(this IUnique obj, uint seed)
        {
            return (long)UniqueCoder64.ComputeUniqueKey(obj.GetUniqueBytes(), seed);
        }
        public static  Int64 UniqueKey64(this IUnique obj)
        {
            return obj.UniqueKey;
        }

        public static Int32 GetHashCode(this IntPtr ptr, int length, uint seed = 0)
        {
            return ptr.UniqueKey32(length, seed);
        }
        public static Int32 GetHashCode<T>(this IEquatable<T> obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this Byte[] obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this Object obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this IList obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this string obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this IUnique obj)
        {
            return obj.UniqueBytes32().ToInt32();
        }

        public static Int64 UniqueKey(this IntPtr ptr, int length, uint seed = 0)
        {
            return ptr.UniqueKey64(length, seed);
        }
        public static Int64 UniqueKey<T>(this IEquatable<T> obj, uint seed = 0)
        {
            return obj.UniqueKey64(seed);
        }
        public static Int64 UniqueKey(this Byte[] bytes, uint seed = 0)
        {
            return bytes.UniqueKey64(seed);
        }
        public static Int64 UniqueKey(this Object obj, uint seed = 0)
        {
            return obj.UniqueKey64(seed);
        }
        public static Int64 UniqueKey(this IList obj, uint seed = 0)
        {
            return UniqueKey64(seed);
        }
        public static Int64 UniqueKey(this String obj, uint seed = 0)
        {
            return UniqueKey64(seed);
        }
        public static Int64 UniqueKey<V>(this IUnique<V> obj, uint seed)
        {
            return UniqueKey64(obj.UniqueValues(), seed);
        }
        public static Int64 UniqueKey(this IUnique obj, uint seed)
        {
            return UniqueKey64(obj.GetUniqueBytes(), seed);
        }
        public static Int64 UniqueKey(this IUnique obj)
        {
            return obj.UniqueKey;
        }

        public static bool   IsSameOrNull(this Object obj, Object value)
        {
            if (obj != null)
                return obj.Equals(value);
            return (obj == null && value == null);
        }

        public static Int64  ComparableInt64(this Object obj, Type type = null)
        {
            if (type == null)
                type = obj.GetType();

            if (obj is string)
            {
                if (type != typeof(string))
                {
                    if (type == typeof(IUnique))
                        return new Usid((string)obj).UniqueKey();
                    if (type == typeof(DateTime))
                        return ((DateTime)Convert.ChangeType(obj, type)).ToBinary();
                    return Convert.ToInt64(Convert.ChangeType(obj, type));
                }
                return ((string)obj).UniqueKey64();
            }

            if (type == typeof(IUnique))
                return ((IUnique)obj).UniqueKey();
            if (type == typeof(DateTime))
                return ((DateTime)obj).Ticks;
            if (obj is ValueType)
                return Convert.ToInt64(obj);
            return obj.UniqueKey64();
        }

        public static Double ComparableDouble(this Object obj, Type type = null)
        {
            if (type == null)
                type = obj.GetType();

            if (obj is string)
            {
                if (type != typeof(string))
                {
                    if (type == typeof(Usid))
                        return new Usid((string)obj).UniqueKey();
                    if (type == typeof(DateTime))
                        return ((DateTime)Convert.ChangeType(obj, type)).ToOADate();
                    return Convert.ToDouble(Convert.ChangeType(obj, type));
                }
                return (((string)obj).UniqueKey64());
            }

            if (type == typeof(IUnique))
                return (((IUnique)obj).UniqueKey());
            if (type == typeof(DateTime))
                return ((DateTime)obj).ToOADate();
            if (obj is ValueType)
                return Convert.ToDouble(obj);
            return (obj.UniqueKey64());
        }

        private static Int64  getValueTypeUniqueKey64(object obj, uint seed = 0)
        {
            Type t = obj.GetType();

            if (t.IsPrimitive || t.IsLayoutSequential)
            {
                byte* ps = stackalloc byte[8];               
                Extraction.ValueStructureToPointer(obj, ps, 0);
                return *(long*)ps;
            }

            if (t == typeof(DateTime))
                return ((DateTime)obj).ToBinary();

            if (t == typeof(Enum))
                return Convert.ToInt32(obj);

            return (long)UniqueCoder64.ComputeUniqueKey(obj.GetBytes(true), seed);
        }

        private static Byte[] GetValueTypeHashBytes64(object obj, uint seed = 0)
        {
            Type t = obj.GetType();

            if (t.IsPrimitive || t.IsLayoutSequential)
            {
                byte[] s = new byte[8];
                fixed (byte* ps = s)
                    Extraction.ValueStructureToPointer(obj, ps, 0);
                return s;
            }          

            if (t == typeof(DateTime))
                return ((DateTime)obj).ToBinary().GetBytes();

            if (t == typeof(Enum))
                return Convert.ToInt32(obj).GetBytes();

            return ((long)UniqueCoder64.ComputeUniqueKey(obj.GetBytes(true), seed)).GetBytes(true);
        }
    }

    public unsafe static class UniqueKeyExtensions32
    {
        public static Int32 BitAggregate64to32(byte* bytes)
        {
            return new int[] { *((int*)&bytes), *((int*)&bytes[4]) }
                                       .Aggregate(7, (a, b) => (a + b) * 23);

        }
        public static Byte[] BitAggregate64to32(this Byte[] bytes)
        {
            byte[] bytes32 = new byte[4];
            fixed (byte* h32 = bytes32)
            fixed (byte* h64 = bytes)
            {
                *((int*)h32) = new int[] { *((int*)&h64), *((int*)&h64[4]) }
                                           .Aggregate(7, (a, b) => (a + b) * 23);
                return bytes32;
            }
        }
        public static Byte[] BitAggregate32to16(this Byte[] bytes)
        {
            byte[] bytes16 = new byte[2];
            fixed (byte* h16 = bytes16)
            fixed (byte* h32 = bytes)
            {
                *((short*)h16) = new short[] { *((short*)&h32), *((short*)&h32[2]) }
                                               .Aggregate((short)7, (a, b) => (short)((a + b) * 7));
                return bytes16;
            }
        }
        public static Byte[] BitAggregate64to16(this Byte[] bytes)
        {
            byte[] bytes16 = new byte[2];
            fixed (byte* h16 = bytes16)
            fixed (byte* h64 = bytes)
            {
                *((short*)h16) = new short[] { *((short*)&h64), *((short*)&h64[2]),
                                               *((short*)&h64[4]), *((short*)&h64[6]) }
                                               .Aggregate((short)7, (a, b) => (short)((a + b) * 7));
                return bytes16;
            }
        }

        public static unsafe Byte[] UniqueBytes32(this IntPtr ptr, int length, uint seed = 0)
        {
            return UniqueCoder32.ComputeUniqueBytes((byte*)ptr.ToPointer(), length, seed);
        }

        public static Byte[] UniqueBytes32(this Byte[] bytes, uint seed = 0)
        {
            return UniqueCoder32.ComputeUniqueBytes(bytes, seed);
        }
        public static Byte[] UniqueBytes32(this Object obj, uint seed = 0)
        {
            if (obj is IUnique)
                return ((IUnique)obj).UniqueBytes32();
            if (obj is ValueType)
                return getValueTypeUniqueBytes32(obj, seed);
            if (obj is string)
                return (((string)obj)).UniqueBytes32(seed);
            if (obj is IList)
                return UniqueBytes32((IList)obj, seed);
            return UniqueCoder32.ComputeUniqueBytes(obj.GetBytes(true), seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] UniqueBytes32(this IList obj, uint seed = 0)
        {
            if (obj is Byte[])
                return UniqueCoder32.ComputeUniqueBytes((Byte[])obj, seed);

            int length = 256, offset = 0, postoffset = 0, count = obj.Count, charsize = sizeof(char), s = 0;
            byte* buffer = stackalloc byte[length];
            bool toResize = false;

            for (int i = 0; i < count; i++)
            {
                object o = obj[i];
                if (o is string)
                {
                    string str = ((string)o);
                    s = str.Length * charsize;
                    postoffset = (s + offset);

                    if (postoffset > length)
                        toResize = true;
                    else
                        fixed (char* c = str)
                            Extractor.CopyBlock(buffer, (byte*)c, offset, s);
                }
                else
                {
                    if (o is IUnique)
                    {
                        s = 8;
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            *((long*)(buffer + offset)) = ((IUnique)o).UniqueKey;
                    }
                    else
                    {
                        s = o.GetSize();
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            Extractor.StructureToPointer(o, new IntPtr(buffer + offset));
                    }
                }

                if (toResize)
                {
                    i--;
                    toResize = false;
                    byte* _buffer = stackalloc byte[postoffset];
                    Extractor.CopyBlock(_buffer, buffer, offset);
                    buffer = _buffer;
                    length = postoffset;
                }
                else
                    offset = postoffset;
            }

            return UniqueCoder32.ComputeUniqueBytes(buffer, offset, seed);
        }

        public static Byte[] UniqueBytes32(this String obj, uint seed = 0)
        {
            fixed (char* c = obj)
                return UniqueCoder32.ComputeUniqueBytes((byte*)c, obj.Length * sizeof(char), seed);
        }
        public static Byte[] UniqueBytes32(this IUnique obj)
        {
            return obj.GetUniqueBytes().BitAggregate64to32();
        }

        public static unsafe Int32 UniqueKey32(this IntPtr ptr, int length, uint seed = 0)
        {
            return (int)UniqueCoder32.ComputeUniqueKey((byte*)ptr.ToPointer(), length, seed);
        }

        public static Int32 UniqueKey32(this Byte[] obj, uint seed = 0)
        {
            return (int)UniqueCoder32.ComputeUniqueKey(obj, seed);
        }
        public static Int32 UniqueKey32(this Object obj, uint seed = 0)
        {
            if (obj is IUnique)
                return ((IUnique)obj).UniqueBytes32().ToInt32();
            if (obj is ValueType)
                return getValueTypeUniqueKey32(obj, seed);           
            if (obj is string)
                return (((string)obj)).UniqueKey32(seed);
            if (obj is IList)
                return UniqueKey32((IList)obj, seed);
            return (int)UniqueCoder32.ComputeUniqueKey(obj.GetBytes(true), seed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 UniqueKey32(this IList obj, uint seed = 0)
        {
            if (obj is Byte[])
                return (int)UniqueCoder32.ComputeUniqueKey((Byte[])obj, seed);

            int length = 256, offset = 0, postoffset = 0, count = obj.Count, charsize = sizeof(char), s = 0;
            byte* buffer = stackalloc byte[length];
            bool toResize = false;

            for (int i = 0; i < count; i++)
            {
                object o = obj[i];
                if (o is string)
                {
                    string str = ((string)o);
                    s = str.Length * charsize;
                    postoffset = (s + offset);

                    if (postoffset > length)
                        toResize = true;
                    else
                        fixed (char* c = str)
                            Extractor.CopyBlock(buffer, (byte*)c, offset, s);
                }
                else
                {
                    if (o is IUnique)
                    {
                        s = 8;
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            *((long*)(buffer + offset)) = ((IUnique)o).UniqueKey;
                    }
                    else
                    {
                        s = o.GetSize();
                        postoffset = (s + offset);

                        if (postoffset > length)
                            toResize = true;
                        else
                            Extractor.StructureToPointer(o, new IntPtr(buffer + offset));
                    }
                }

                if (toResize)
                {
                    i--;
                    toResize = false;
                    byte* _buffer = stackalloc byte[postoffset];
                    Extractor.CopyBlock(_buffer, buffer, offset);
                    buffer = _buffer;
                    length = postoffset;
                }
                else
                    offset = postoffset;
            }

            return (int)UniqueCoder32.ComputeUniqueKey(buffer, offset, seed);
        }

        public static Int32 UniqueKey32(this string obj, uint seed = 0)
        {
            fixed (char* c = obj)
                return (int)UniqueCoder32.ComputeUniqueKey((byte*)c, obj.Length * sizeof(char), seed);
        }
        public static Int32 UniqueKey32<V>(this IUnique<V> obj, uint seed)
        {
            return UniqueKey32(obj.UniqueValues(), seed);
        }
        public static Int32 UniqueKey32(this IUnique obj, uint seed)
        {
            return (int)UniqueCoder32.ComputeUniqueKey(obj.GetUniqueBytes(), seed);
        }
        public static Int32 UniqueKey32(this IUnique obj)
        {
            return obj.UniqueBytes32().ToInt32();
        }

        public static unsafe 
                      Int32 GetHashCode(this IntPtr ptr, int length, uint seed = 0)
        {
            return ptr.UniqueKey32(length, seed);
        }

        public static Int32 GetHashCode<T>(this IEquatable<T> obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this Byte[] obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this Object obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this IList obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this string obj, uint seed = 0)
        {
            return obj.UniqueKey32(seed);
        }
        public static Int32 GetHashCode(this IUnique obj, uint seed = 0)
        {
            return obj.UniqueBytes32(seed).ToInt32();
        }

        private static Int32 getValueTypeUniqueKey32(object obj, uint seed = 0)
        {
            Type t = obj.GetType();

            if (t.IsPrimitive || t.IsLayoutSequential)
            {
                byte* ps = stackalloc byte[8];
                Extraction.ValueStructureToPointer(obj, ps, 0);
                return BitAggregate64to32(ps);
            }
          
            if (t == typeof(DateTime))
                return ((DateTime)obj).ToBinary().GetBytes().BitAggregate64to32().ToInt32();

            if (t == typeof(Enum))
                return Convert.ToInt32(obj);

            return (int)UniqueCoder32.ComputeUniqueKey(obj.GetBytes(true), seed);
        }

        private static Byte[] getValueTypeUniqueBytes32(object obj, uint seed = 0)
        {
            Type t = obj.GetType();

            if (t.IsPrimitive || t.IsLayoutSequential)
            {
                byte[] s = new byte[8];
                fixed (byte* ps = s)
                {
                    Extraction.ValueStructureToPointer(obj, ps, 0);
                    return s.BitAggregate64to32();
                }
            }

            if (t == typeof(DateTime))
                return ((DateTime)obj).ToBinary().GetBytes().BitAggregate64to32();

            if (t == typeof(Enum))
                return Convert.ToInt32(obj).GetBytes();

            return ((int)UniqueCoder32.ComputeUniqueKey(obj.GetBytes(true)), seed).GetBytes(true);
        }
    }

}
