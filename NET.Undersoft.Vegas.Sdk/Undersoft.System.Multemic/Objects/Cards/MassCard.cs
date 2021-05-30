using System.Runtime.InteropServices;
using System.Uniques;

/******************************************************************
    Copyright (c) 2020 Undersoft

    System.Multemic.Card64
    
    Implementation of Card abstract class
    using 64 bit hash code and long representation;  
        
    @author Darius Hanc                                                  
    @project NETStandard.Undersoft.SDK                                    
    @version 0.8.D (Feb 7, 2020)                                            
    @licence MIT                                       
 
 ******************************************************************/
namespace System.Multemic
{     
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class MassCard<V> : Card<V> where V : IUnique
    {
        private long _key;

        public MassCard()
        { }
        public MassCard(object key, V value) : base(key, value)
        {
        }
        public MassCard(long key, V value) : base(key, value)
        {
        }
        public MassCard(V value) : base(value)
        {
        }
        public MassCard(ICard<V> value) : base(value)
        {
        }

        public override void Set(object key, V value)
        {
            this.value = value;
            _key = key.UniqueKey64(value.UniqueSeed);
        }
        public override void Set(V value)
        {
            this.value = value;
            _key = value.UniqueKey64(value.UniqueSeed);
        }
        public override void Set(ICard<V> card)
        {
            this.value = card.Value;
            _key = card.Key;
        }

        public override bool Equals(long key)
        {
            return Key == key;
        }
        public override bool Equals(object y)
        {
            return Key.Equals(y.UniqueKey64(UniqueSeed));
        }

        public override int GetHashCode()
        {
            return Key.UniqueKey32();
        }

        public override int CompareTo(object other)
        {
            return (int)(Key - other.UniqueKey64(UniqueSeed));
        }
        public override int CompareTo(long key)
        {
            return (int)(Key - key);
        }
        public override int CompareTo(ICard<V> other)
        {
            return (int)(Key - other.Key);
        }

        public override byte[] GetBytes()
        {
            return this.value.GetBytes();
        }

        public unsafe override byte[] GetUniqueBytes()
        {
            byte[] b = new byte[8];
            fixed (byte* s = b)
                *(long*)s = _key;
            return b;
        }

        public override long Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public override uint UniqueSeed
        {
            get => this.value.UniqueSeed;
            set => this.value.UniqueSeed = value;
        }

    }
}
