/*************************************************
   Copyright (c) 2021 Undersoft

   Laborer.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Labors
{
    using System.Collections.Generic;
    using System.Instant;
    using System.Multemic;
    using System.Uniques;

    public class Laborer : IUnique
    {
        #region Fields

        private Board<object> input;
        private Board<object> output;
        private Ussc SystemCode;

        #endregion

        #region Constructors

        public Laborer()
        {
            input = new Board<object>();
            output = new Board<object>();
            EvokersIn = new NoteEvokers();
        }
        public Laborer(string Name, IDeputy Method) : this()
        {
            Work = Method;
            LaborerName = Name;

            SystemCode = new Ussc(($"{Work.UniqueKey}.{DateTime.Now.ToBinary()}").UniqueKey());
        }

        #endregion

        #region Properties

        public IUnique Empty => new Ussc();

        public NoteEvokers EvokersIn { get; set; }

        public object Input
        {

            get
            {
                object _entry = null;
                input.TryDequeue(out _entry);
                return _entry;

            }
            set
            {
                input.Enqueue(value);
            }
        }

        public Labor Labor { get; set; }

        public string LaborerName { get; set; }

        public object Output
        {
            get
            {
                object _result = null;
                if (output.TryPick(0, out _result))
                    return _result;
                return null;
            }
            set
            {
                output.Enqueue(value);
            }
        }

        public long UniqueKey { get => SystemCode.UniqueKey; set => SystemCode.UniqueKey = value; }

        public uint UniqueSeed { get => ((IUnique)SystemCode).UniqueSeed; set => ((IUnique)SystemCode).UniqueSeed = value; }

        public IDeputy Work { get; set; }

        #endregion

        #region Methods

        public void AddEvoker(Labor Recipient)
        {
            EvokersIn.Add(new NoteEvoker(Labor, Recipient, new List<Labor>() { Labor }));
        }

        public void AddEvoker(Labor Recipient, List<Labor> RelationLabors)
        {
            EvokersIn.Add(new NoteEvoker(Labor, Recipient, RelationLabors));
        }

        public void AddEvoker(string RecipientName)
        {
            EvokersIn.Add(new NoteEvoker(Labor, RecipientName, new List<string>() { LaborerName }));
        }

        public void AddEvoker(string RecipientName, List<string> RelationNames)
        {
            EvokersIn.Add(new NoteEvoker(Labor, RecipientName, RelationNames));
        }

        public int CompareTo(IUnique other)
        {
            return SystemCode.CompareTo(other);
        }

        public bool Equals(IUnique other)
        {
            return SystemCode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return SystemCode.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return SystemCode.GetUniqueBytes();
        }

        //public uint GetUniqueSeed()
        //{
        //    return ((IUnique)SystemCode).UniqueSeed;
        //}

        //public void SetUniqueKey(long value)
        //{
        //    SystemCode.UniqueKey = value;
        //}

        //public void SetUniqueSeed(uint seed)
        //{
        //    ((IUnique)SystemCode).SetUniqueSeed(seed);
        //}

        //public long UniqueKey()
        //{
        //    return SystemCode.UniqueKey();
        //}

        #endregion
    }
}
