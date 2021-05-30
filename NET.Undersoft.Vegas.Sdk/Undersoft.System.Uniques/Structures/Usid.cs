﻿using System.Extract;
using System.Runtime.InteropServices;
using System;

namespace System.Uniques
{
    [Serializable]
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential, Size = 8)]
    public unsafe struct Usid : IFormattable, IComparable
        , IComparable<IUnique>, IEquatable<IUnique>, IUnique
    {
        private fixed byte bytes[8];       

        private long _KeyBlock
        {
            get
            {
                long block = UniqueKey;
                return (block << 32) | ((block >> 16) & 0xffff0000) | (block >> 48);
            }
            set
            {
                UniqueKey = (value >> 32) | (((value & 0x0ffff0000) << 16)) | (value << 48);
            }
        }

        public Usid(long l)
        {
            UniqueKey = l;
        }
        public Usid(string ca)
        {
            this.FromHexTetraChars(ca.ToCharArray());
        }
        public Usid(byte[] b)
        {
            if (b != null)
            {
                int l = b.Length;
                if (l > 8)
                    l = 8;
                fixed (byte* dbp = bytes)
                fixed (byte* sbp = b)
                {
                    Extractor.CopyBlock(dbp, sbp, l);
                }
            }
        }
        public Usid(ushort z, ushort y, uint x)
        {
            fixed(byte* pbytes = bytes)
            {
                *((uint*)pbytes) = x;
                *((uint*)(pbytes + 4)) = y;
                *((uint*)(pbytes + 6)) = z;
            }
        }
        public Usid(object key)
        {
            fixed (byte* n = bytes)
                *((long*)n) = key.UniqueKey64();            
        }

        public byte[] this[int offset]
        {
            get
            {
                if (offset > 0 && offset < 8)
                {                   
                    int l = (8 - offset);
                    byte[] r = new byte[l];
                    fixed (byte* pbyte = bytes)
                    fixed (byte* rbyte = r)
                        Extractor.CopyBlock(rbyte, pbyte + offset, l);
                    return r;
                }
                return GetBytes();
            }
            set
            {
                int l = value.Length;
                if (offset > 0 || l < 8)
                {
                    int count = 8 - offset;
                    if (l < count)
                        count = l;
                    fixed (byte* pbyte = bytes)
                    fixed (byte* rbyte = value)
                    {
                        Extractor.CopyBlock(pbyte, rbyte, offset, l);
                    }
                }
                else
                {
                    fixed (byte* v = value)
                    fixed (byte* b = bytes)
                        *(long*)b = *(long*)v;
                }
            }
        }

        public byte[] GetBytes()
        {
            byte[] r = new byte[8];
            fixed (byte* rbyte = r)
            fixed (byte* pbyte = bytes)
            {
                *((long*)rbyte) = *((long*)pbyte);
            }
            return r;
        }

        public byte[] GetUniqueBytes()
        {          
            return GetBytes();
        }

        public long UniqueKey
        {
            get
            {
                fixed (byte* pbyte = bytes)
                    return *((long*)pbyte);
            }
            set
            {

                fixed (byte* b = bytes)
                    *((long*)b) = value;
            }
        }

        public ushort BlockZ
        {
            get
            {
                fixed (byte* pbyte = bytes)
                    return *((ushort*)(pbyte + 6));
            }
            set
            {
                fixed (byte* pbyte = bytes)
                    *((ushort*)(pbyte + 6)) = value;
            }
        }

        public ushort BlockY
        {
            get
            {
             
                fixed (byte* pbyte = bytes)
                    return *((ushort*)(pbyte + 4));
            }
            set
            {
                fixed (byte* pbyte = bytes)
                    *((ushort*)(pbyte + 4)) = value;
            }
        }

        public ushort BlockX
        {
            get
            {
                fixed (byte* pbyte = bytes)
                    return *((ushort*)pbyte);
            }
            set
            {
                fixed (byte* pbyte = bytes)
                    *((ushort*)pbyte) = value;
            }
        }

        public bool IsNotEmpty
        {
            get { return (UniqueKey > 0); }
        }

        public bool IsEmpty
        {
            get { return (UniqueKey == 0); }
        }             

        public override int GetHashCode()
        {
            fixed (byte* pbyte = bytes)
            {
                return (int)UniqueCoder32.ComputeUniqueKey(pbyte, 8);
            }
        }

        public void SetUniqueKey(long value)
        {
            UniqueKey = value;
        }

        public long GetUniqueKey()
        {
            return UniqueKey;
        }

        public int CompareTo(object value)
        {
            if (value == null)
                return -1;
            if (!(value is Usid))
                throw new Exception();

            return (int)(UniqueKey - value.UniqueKey64());
        }

        public int CompareTo(Usid g)
        {
            return (int)(UniqueKey - g.UniqueKey);
        }

        public int CompareTo(IUnique g)
        {
            return (int)(UniqueKey - g.UniqueKey());
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;
            if ((value is string))
                return new Usid(value.ToString()).UniqueKey == UniqueKey;

            return (UniqueKey == ((Usid)value).UniqueKey);
        }

        public bool Equals(long g)
        {
            return (UniqueKey == g);
        }
        public bool Equals(Usid g)
        {
            return (UniqueKey == g.UniqueKey);
        }
        public bool Equals(IUnique g)
        {
            return (UniqueKey == g.UniqueKey);
        }
        public bool Equals(string g)
        {
            return (UniqueKey == new Usid(g).UniqueKey);
        }

        public override String ToString()
        {
            return new string(this.ToHexTetraChars());
        }

        public String ToString(String format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return new string(this.ToHexTetraChars());
        }

        public static bool operator ==(Usid a, Usid b)
        {
            return (a.UniqueKey == b.UniqueKey);
        }
        public static bool operator !=(Usid a, Usid b)
        {
            return (a.UniqueKey != b.UniqueKey);
        }

        public static explicit operator Usid(String s)
        {
            return new Usid(s);
        }
        public static implicit operator String(Usid s)
        {
            return s.ToString();
        }

        public static explicit operator Usid(byte[] l)
        {
            return new Usid(l);
        }
        public static implicit operator byte[] (Usid s)
        {
            return s.GetBytes();
        }

        public static Usid Empty
        {
            get { return new Usid(); }
        }

        IUnique IUnique.Empty
        {
            get
            {
                return new Usid();
            }
        }        

        public char[] ToHexTetraChars()
        {
            char[] pchchar = new char[10];
            long pchlong;
            byte pchbyte;
            int pchlength = 0;
            long _longValue = _KeyBlock;
            //56-bit representation: //A [max 2^30] //B [max 2^16] //C [max 2^10]
            //i.e., bits: [A: 55..26][B: 25..10][C: 9..0]
            pchlong = ((_longValue & 0x3fffffff00000000L) >> 6) | ((_longValue & 0xffff0000L) >> 6) | (_longValue & 0x03ffL);
            for (int i = 0; i < 5; i++)
            {
                pchbyte = (byte)(pchlong & 0x003fL);
                pchchar[i] = (pchbyte).ToHexTetraChar();
                pchlong = pchlong >> 6;
            }

            pchlength = 5;

            //Trim PrimeId
            for (int i = 5; i < 10; i++)
            {
                pchbyte = (byte)(pchlong & 0x003fL);
                if (pchbyte != 0x00) pchlength = i + 1;
                pchchar[i] = (pchbyte).ToHexTetraChar();
                pchlong = pchlong >> 6;
            }

            char[] pchchartrim = new char[pchlength];
            Array.Copy(pchchar, 0, pchchartrim, 0, pchlength);

            return pchchartrim;
        }

        public void FromHexTetraChars(char[] pchchar)
        {
            long pchlong = 0;
            byte pchbyte;
            int pchlength = 0;

            //bits: [A: 55..26][B: 25..10][C: 9..0]
            pchlength = pchchar.Length;
            pchbyte = (pchchar[pchlength - 1]).ToHexTetraByte();
            pchlong = pchbyte & 0x3fL;
            for (int i = pchlength - 2; i >= 0; i--)
            {
                pchbyte = (pchchar[i]).ToHexTetraByte();
                pchlong = pchlong << 6;
                pchlong = pchlong | (pchbyte & 0x3fL);
            }
            _KeyBlock = ((pchlong << 6) & 0x3fffffff00000000L) | ((pchlong << 6) & 0xffff0000L) | (pchlong & 0x03ffL);
        }

        public uint UniqueSeed
        {
            get => 0;
            set => throw new NotImplementedException();
        }
        public void SetUniqueSeed(uint seed)
        {
            throw new NotImplementedException();
        }
        public uint GetUniqueSeed()
        {
            return 0;
        }
    }
}