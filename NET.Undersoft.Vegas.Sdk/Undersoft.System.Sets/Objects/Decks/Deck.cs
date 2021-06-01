using System.Collections.Generic;

/*************************************************************************************
    Copyright (c) 2020 Undersoft

    System.Sets.Deck      

    Default Implementation of Deck class
    using 64 bit hash code and long representation;  
        
    @author Darius Hanc                                                  
    @project NETStandard.Undersoft.SDK                                    
    @version 0.8.D (Feb 7, 2020)                                            
    @licence MIT                                       
 *********************************************************************************/
namespace System.Sets
{  
    public class Deck<V> : BaseDeck<V>                                                     
    {
        public Deck(int capacity = 9) : base(capacity) { }
        public Deck(IList<ICard<V>> collection, int capacity = 9) : base(collection, capacity) { }
        public Deck(IList<IUnique<V>> collection, int capacity = 9) : base(collection, capacity) { }   
        public Deck(IEnumerable<ICard<V>> collection, int capacity = 9) : base(collection, capacity) { }
        public Deck(IEnumerable<IUnique<V>> collection, int capacity = 9) : base(collection, capacity) { }

        public override ICard<V> EmptyCard()
        {
            return new Card<V>();
        }

        public override ICard<V> NewCard(ulong key, V value)
        {
            return new Card<V>(key, value);
        }
        public override ICard<V> NewCard(object key, V value)
        {
            return new Card<V>(key, value);
        }
        public override ICard<V> NewCard(V value)
        {
            return new Card<V>(value);
        }
        public override ICard<V> NewCard(ICard<V> card)
        {
            return new Card<V>(card);
        }

        public override ICard<V>[] EmptyCardTable(int size)
        {
            return new Card<V>[size];
        }
    }

}
