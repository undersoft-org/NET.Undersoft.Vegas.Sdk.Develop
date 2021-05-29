/*************************************************
   Copyright (c) 2021 Undersoft

   Note.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Labors
{
    using System.Uniques;

    /// <summary>
    /// Defines the <see cref="Note" />.
    /// </summary>
    public class Note : IUnique
    {
        #region Fields

        /// <summary>
        /// Defines the Parameters.
        /// </summary>
        public object[] Parameters;
        /// <summary>
        /// Defines the SenderBox.
        /// </summary>
        public NoteBox SenderBox;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipient">The recipient<see cref="Labor"/>.</param>
        /// <param name="Out">The Out<see cref="NoteEvoker"/>.</param>
        /// <param name="In">The In<see cref="NoteEvokers"/>.</param>
        /// <param name="Params">The Params<see cref="object[]"/>.</param>
        public Note(Labor sender, Labor recipient, NoteEvoker Out, NoteEvokers In, params object[] Params)
        {
            Parameters = Params;

            if (recipient != null)
            {
                Recipient = recipient;
                RecipientName = Recipient.Laborer.LaborerName;
            }

            Sender = sender;
            SenderName = Sender.Laborer.LaborerName;

            if (Out != null)
                EvokerOut = Out;

            if (In != null)
                EvokersIn = In;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="string"/>.</param>
        /// <param name="Params">The Params<see cref="object[]"/>.</param>
        public Note(string sender, params object[] Params) : this(sender, null, null, null, Params)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="string"/>.</param>
        /// <param name="recipient">The recipient<see cref="string"/>.</param>
        /// <param name="Out">The Out<see cref="NoteEvoker"/>.</param>
        /// <param name="In">The In<see cref="NoteEvokers"/>.</param>
        /// <param name="Params">The Params<see cref="object[]"/>.</param>
        public Note(string sender, string recipient, NoteEvoker Out, NoteEvokers In, params object[] Params)
        {
            SenderName = sender;
            Parameters = Params;

            if (recipient != null)
                RecipientName = recipient;

            if (Out != null)
                EvokerOut = Out;

            if (In != null)
                EvokersIn = In;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="string"/>.</param>
        /// <param name="recipient">The recipient<see cref="string"/>.</param>
        /// <param name="Out">The Out<see cref="NoteEvoker"/>.</param>
        /// <param name="Params">The Params<see cref="object[]"/>.</param>
        public Note(string sender, string recipient, NoteEvoker Out, params object[] Params) : this(sender, recipient, Out, null, Params)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="string"/>.</param>
        /// <param name="recipient">The recipient<see cref="string"/>.</param>
        /// <param name="Params">The Params<see cref="object[]"/>.</param>
        public Note(string sender, string recipient, params object[] Params) : this(sender, recipient, null, null, Params)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Empty.
        /// </summary>
        public IUnique Empty => new Ussc();

        /// <summary>
        /// Gets or sets the EvokerOut.
        /// </summary>
        public NoteEvoker EvokerOut { get; set; }

        /// <summary>
        /// Gets or sets the EvokersIn.
        /// </summary>
        public NoteEvokers EvokersIn { get; set; }

        /// <summary>
        /// Gets or sets the KeyBlock.
        /// </summary>
        public long KeyBlock { get => Sender.KeyBlock; set => Sender.KeyBlock = value; }

        /// <summary>
        /// Gets or sets the Recipient.
        /// </summary>
        public Labor Recipient { get; set; }

        /// <summary>
        /// Gets or sets the RecipientName.
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Gets or sets the SeedBlock.
        /// </summary>
        public uint SeedBlock { get => ((IUnique)Sender).SeedBlock; set => ((IUnique)Sender).SeedBlock = value; }

        /// <summary>
        /// Gets or sets the Sender.
        /// </summary>
        public Labor Sender { get; set; }

        /// <summary>
        /// Gets or sets the SenderName.
        /// </summary>
        public string SenderName { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The CompareTo.
        /// </summary>
        /// <param name="other">The other<see cref="IUnique"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CompareTo(IUnique other)
        {
            return Sender.CompareTo(other);
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="other">The other<see cref="IUnique"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(IUnique other)
        {
            return Sender.Equals(other);
        }

        /// <summary>
        /// The GetBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetBytes()
        {
            return Sender.GetBytes();
        }

        /// <summary>
        /// The GetHashKey.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        public long GetHashKey()
        {
            return Sender.GetHashKey();
        }

        /// <summary>
        /// The GetHashSeed.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetHashSeed()
        {
            return ((IUnique)Sender).GetHashSeed();
        }

        /// <summary>
        /// The GetKeyBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetKeyBytes()
        {
            return Sender.GetKeyBytes();
        }

        /// <summary>
        /// The SetHashKey.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public void SetHashKey(long value)
        {
            Sender.KeyBlock = value;
        }

        /// <summary>
        /// The SetHashSeed.
        /// </summary>
        /// <param name="seed">The seed<see cref="uint"/>.</param>
        public void SetHashSeed(uint seed)
        {
            ((IUnique)Sender).SetHashSeed(seed);
        }

        #endregion
    }
}
