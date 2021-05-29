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

    /// <summary>
    /// Defines the <see cref="Laborer" />.
    /// </summary>
    public class Laborer : IUnique
    {
        #region Fields

        /// <summary>
        /// Defines the input.
        /// </summary>
        private Board<object> input;
        /// <summary>
        /// Defines the output.
        /// </summary>
        private Board<object> output;
        /// <summary>
        /// Defines the SystemCode.
        /// </summary>
        private Ussc SystemCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Laborer"/> class.
        /// </summary>
        public Laborer()
        {
            input = new Board<object>();
            output = new Board<object>();
            EvokersIn = new NoteEvokers();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Laborer"/> class.
        /// </summary>
        /// <param name="Name">The Name<see cref="string"/>.</param>
        /// <param name="Method">The Method<see cref="IDeputy"/>.</param>
        public Laborer(string Name, IDeputy Method) : this()
        {
            Work = Method;
            LaborerName = Name;

            SystemCode = new Ussc(($"{Work.KeyBlock}.{DateTime.Now.ToBinary()}").GetHashKey());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Empty.
        /// </summary>
        public IUnique Empty => new Ussc();

        /// <summary>
        /// Gets or sets the EvokersIn.
        /// </summary>
        public NoteEvokers EvokersIn { get; set; }

        /// <summary>
        /// Gets or sets the Input.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the KeyBlock.
        /// </summary>
        public long KeyBlock { get => SystemCode.KeyBlock; set => SystemCode.KeyBlock = value; }

        /// <summary>
        /// Gets or sets the Labor.
        /// </summary>
        public Labor Labor { get; set; }

        /// <summary>
        /// Gets or sets the LaborerName.
        /// </summary>
        public string LaborerName { get; set; }

        /// <summary>
        /// Gets or sets the Output.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the SeedBlock.
        /// </summary>
        public uint SeedBlock { get => ((IUnique)SystemCode).SeedBlock; set => ((IUnique)SystemCode).SeedBlock = value; }

        /// <summary>
        /// Gets or sets the Work.
        /// </summary>
        public IDeputy Work { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="Recipient">The Recipient<see cref="Labor"/>.</param>
        public void AddEvoker(Labor Recipient)
        {
            EvokersIn.Add(new NoteEvoker(Labor, Recipient, new List<Labor>() { Labor }));
        }

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="Recipient">The Recipient<see cref="Labor"/>.</param>
        /// <param name="RelationLabors">The RelationLabors<see cref="List{Labor}"/>.</param>
        public void AddEvoker(Labor Recipient, List<Labor> RelationLabors)
        {
            EvokersIn.Add(new NoteEvoker(Labor, Recipient, RelationLabors));
        }

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="RecipientName">The RecipientName<see cref="string"/>.</param>
        public void AddEvoker(string RecipientName)
        {
            EvokersIn.Add(new NoteEvoker(Labor, RecipientName, new List<string>() { LaborerName }));
        }

        /// <summary>
        /// The AddEvoker.
        /// </summary>
        /// <param name="RecipientName">The RecipientName<see cref="string"/>.</param>
        /// <param name="RelationNames">The RelationNames<see cref="List{string}"/>.</param>
        public void AddEvoker(string RecipientName, List<string> RelationNames)
        {
            EvokersIn.Add(new NoteEvoker(Labor, RecipientName, RelationNames));
        }

        /// <summary>
        /// The CompareTo.
        /// </summary>
        /// <param name="other">The other<see cref="IUnique"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(IUnique other)
        {
            return SystemCode.CompareTo(other);
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="other">The other<see cref="IUnique"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(IUnique other)
        {
            return SystemCode.Equals(other);
        }

        /// <summary>
        /// The GetBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetBytes()
        {
            return SystemCode.GetBytes();
        }

        /// <summary>
        /// The GetHashKey.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        public long GetHashKey()
        {
            return SystemCode.GetHashKey();
        }

        /// <summary>
        /// The GetHashSeed.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetHashSeed()
        {
            return ((IUnique)SystemCode).GetHashSeed();
        }

        /// <summary>
        /// The GetKeyBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetKeyBytes()
        {
            return SystemCode.GetKeyBytes();
        }

        /// <summary>
        /// The SetHashKey.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public void SetHashKey(long value)
        {
            SystemCode.KeyBlock = value;
        }

        /// <summary>
        /// The SetHashSeed.
        /// </summary>
        /// <param name="seed">The seed<see cref="uint"/>.</param>
        public void SetHashSeed(uint seed)
        {
            ((IUnique)SystemCode).SetHashSeed(seed);
        }

        #endregion
    }
}
