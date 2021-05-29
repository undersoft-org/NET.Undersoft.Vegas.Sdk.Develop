using System.Uniques;
using System.Multemic;
using System.Instant.Linking;
using System.Instant.Treatments;
using System.IO;

namespace System.Instant
{
    public abstract class FigureAlbum : CardBook<IFigure>, IFigures
    {
        public IInstant Instant { get; set; }

        public abstract bool Prime { get; set; }

        public abstract object this[int index, string propertyName] { get; set; }

        public abstract object this[int index, int fieldId] { get; set; }

        public abstract IRubrics Rubrics { get; set; }

        public abstract IRubrics KeyRubrics { get; set; }

        public abstract IFigure NewFigure();

        public abstract  Type FigureType { get; set; }

        public abstract int FigureSize { get; set; }

        public abstract Ussn SystemSerialCode { get; set; }

        public int Length => Cards.Length;

        public override ICard<IFigure> EmptyCard()
        {
            return new FigureCard(this);
        }

        public override ICard<IFigure> NewCard(long key, IFigure value)
        {
            return new FigureCard(key, value, this);
        }
        public override ICard<IFigure> NewCard(object key, IFigure value)
        {
            return new FigureCard(key, value, this);
        }
        public override ICard<IFigure> NewCard(IFigure value)
        {
            return new FigureCard(value, this);
        }
        public override ICard<IFigure> NewCard(ICard<IFigure> value)
        {
            return new FigureCard(value, this);
        }

        public override ICard<IFigure>[] EmptyCardTable(int size)
        {
            return new FigureCard[size];
        }

        public override ICard<IFigure>[] EmptyCardList(int size)
        {
            cards = null;
            cards = new FigureCard[size];
            return cards;
        }

        protected override bool InnerAdd(IFigure value)
        {
            return InnerAdd(NewCard(value));
        }

        protected override ICard<IFigure> InnerPut(IFigure value)
        {
            return InnerPut(NewCard(value));
        }

        public override ICard<IFigure> AddNew()
        {
            ICard<IFigure> newCard = NewCard(Unique.NewKey, NewFigure());
            if (InnerAdd(newCard))
                return newCard;
            return null;
        }
        public override ICard<IFigure> AddNew(long key)
        {
            ICard<IFigure> newCard = NewCard(key, NewFigure());
            if (InnerAdd(newCard))
                return newCard;
            return null;
        }
        public override ICard<IFigure> AddNew(object key)
        {
            ICard<IFigure> newCard = NewCard(base.GetHashKey(key), NewFigure());
            if (InnerAdd(newCard))
                return newCard;
            return null;
        }

        private ICard<IFigure>[] cards;
        public ICard<IFigure>[] Cards { get => cards; }

        public object[] ValueArray { get => ToObjectArray(); set => Put(value); }

        public Type Type { get; set; }

        public IFigures Picked { get; set; }

        public IFigure Summary { get; set; }

        public FigureFilter Filter { get; set; }

        public FigureSort Sort { get; set; }

        public Func<IFigure, bool> Picker { get; set; }

        public IUnique Empty => Ussn.Empty;

        public long KeyBlock
        {
            get => SystemSerialCode.KeyBlock;
            set => SystemSerialCode.SetHashKey(value);
        }

        object IFigure.this[int fieldId]
        {
            get => this[fieldId];
            set => this[fieldId] = (IFigure)value;
        }
        public object this[string propertyName]
        {
            get => this[propertyName];
            set => this[propertyName] = (IFigure)value;
        }

        public byte[] GetBytes()
        {
            return SystemSerialCode.GetBytes();
        }
        public byte[] GetKeyBytes()
        {
            return SystemSerialCode.GetKeyBytes();
        }

        public void   SetHashKey(long value)
        {
            SystemSerialCode.SetHashKey(value);
        }
        public long   GetHashKey()
        {
            return SystemSerialCode.GetHashKey();
        }

        public uint SeedBlock
        {
            get => SystemSerialCode.SeedBlock;
            set => SystemSerialCode.SetHashSeed(value);
        }

        public void SetHashSeed(uint seed)
        {
            SystemSerialCode.SetHashSeed(seed);
        }
        public uint GetHashSeed()
        {
            return SystemSerialCode.GetHashSeed();
        }

        public bool Equals(IUnique other)
        {
            return SystemSerialCode.Equals(other);
        }

        public int CompareTo(IUnique other)
        {
            return SystemSerialCode.CompareTo(other);
        }       

        #region Formatter

        public int SerialCount { get; set; }
        public int DeserialCount { get; set; }
        public int ProgressCount { get; set; }

        public int Serialize(Stream stream, int offset, int batchSize, FigureFormat serialFormat = FigureFormat.Binary)
        {
            throw new NotImplementedException();
        }
        public int Serialize(IFigurePacket buffor, int offset, int batchSize, FigureFormat serialFormat = FigureFormat.Binary)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(Stream stream, FigureFormat serialFormat = FigureFormat.Binary)
        {
            throw new NotImplementedException();
        }
        public object Deserialize(ref object block, FigureFormat serialFormat = FigureFormat.Binary)
        {
            throw new NotImplementedException();
        }

        public object[] GetMessage()
        {
            return new[] { (IFigures)this };
        }

        public object GetHeader()
        {
            return this;
        }

        public int ItemsCount => throw new NotImplementedException();

        #endregion

        public Links Links { get; set; } = new Links();

        private Treatment treatment;
        public  Treatment Treatment
        {
            get => treatment == null ? treatment = new Treatment(this) : treatment;
            set => treatment = value;
        }

        public IDeck<IComputation> Computations { get; set; }

    }
}