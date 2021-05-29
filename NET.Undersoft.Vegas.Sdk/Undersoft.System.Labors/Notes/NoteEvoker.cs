/*************************************************
   Copyright (c) 2021 Undersoft

   NoteEvoker.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Labors
{
    using System.Collections.Generic;
    using System.Extract;
    using System.Linq;
    using System.Multemic;
    using System.Uniques;

    /// <summary>
    /// Defines the <see cref="NoteEvoker" />.
    /// </summary>
    public class NoteEvoker : Board<Labor>, IUnique
    {
        #region Fields

        /// <summary>
        /// Defines the RelationLabors.
        /// </summary>
        public List<Labor> RelationLabors = new List<Labor>();
        /// <summary>
        /// Defines the RelationNames.
        /// </summary>
        public List<string> RelationNames = new List<string>();
        /// <summary>
        /// Defines the SystemCode.
        /// </summary>
        private Usid SystemCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipient">The recipient<see cref="Labor"/>.</param>
        /// <param name="relayLabors">The relayLabors<see cref="List{Labor}"/>.</param>
        public NoteEvoker(Labor sender, Labor recipient, List<Labor> relayLabors)
        {
            Sender = sender;
            SenderName = sender.Laborer.LaborerName;
            Recipient = recipient;
            RecipientName = recipient.Laborer.LaborerName;
            SystemCode = new Usid(($"{SenderName}.{RecipientName}").GetHashKey());
            RelationLabors = relayLabors;
            RelationNames.AddRange(RelationLabors.Select(rn => rn.Laborer.LaborerName));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipient">The recipient<see cref="Labor"/>.</param>
        /// <param name="relayNames">The relayNames<see cref="List{string}"/>.</param>
        public NoteEvoker(Labor sender, Labor recipient, List<string> relayNames)
        {
            Sender = sender;
            SenderName = sender.Laborer.LaborerName;
            Recipient = recipient;
            RecipientName = recipient.Laborer.LaborerName;
            SystemCode = new Usid(($"{SenderName}.{RecipientName}").GetHashKey());
            RelationNames = relayNames;
            RelationLabors = Sender.Scope.Subjects.AsCards()
                                    .Where(m => m.Value.Labors.AsIdentifiers()
                                        .Where(k => relayNames
                                            .Select(rn => rn.GetHashKey()).Contains(k.KeyBlock)).Any())
                                                .SelectMany(os => os.Value.Labors.AsCards()
                                                    .Select(o => o.Value)).ToList();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipientName">The recipientName<see cref="string"/>.</param>
        /// <param name="relayLabors">The relayLabors<see cref="IList{Labor}"/>.</param>
        public NoteEvoker(Labor sender, string recipientName, IList<Labor> relayLabors)
        {
            Sender = sender;
            SenderName = sender.Laborer.LaborerName;
            RecipientName = recipientName;
            SystemCode = new Usid(($"{SenderName}.{RecipientName}").GetHashKey());
            List<Labor> objvl = Sender.Scope.Subjects.AsCards()
                                        .Where(m => m.Value.Labors.ContainsKey(recipientName))
                                            .SelectMany(os => os.Value.Labors.AsCards().Select(o => o.Value)).ToList();
            if (objvl.Any())
                Recipient = objvl.First();
            RelationLabors = new List<Labor>(relayLabors);
            RelationNames.AddRange(RelationLabors.Select(rn => rn.Laborer.LaborerName));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteEvoker"/> class.
        /// </summary>
        /// <param name="sender">The sender<see cref="Labor"/>.</param>
        /// <param name="recipientName">The recipientName<see cref="string"/>.</param>
        /// <param name="relayNames">The relayNames<see cref="IList{string}"/>.</param>
        public NoteEvoker(Labor sender, string recipientName, IList<string> relayNames)
        {
            Sender = sender;
            SenderName = sender.Laborer.LaborerName;
            List<Labor> objvl = Sender.Scope.Subjects.AsCards().Where(m => m.Value.Labors.ContainsKey(recipientName)).SelectMany(os => os.Value.Labors.AsCards().Select(o => o.Value)).ToList();
            if (objvl.Any())
                Recipient = objvl.First();
            RecipientName = recipientName;
            SystemCode = new Usid(($"{SenderName}.{RecipientName}").GetHashKey());
            RelationNames = new List<string>(relayNames);
            RelationLabors = Sender.Scope.Subjects.AsCards().Where(m => m.Value.Labors.AsIdentifiers().Where(k => relayNames.Select(rn => rn.GetHashKey()).Contains(k.KeyBlock)).Any()).SelectMany(os => os.Value.Labors.AsCards().Select(o => o.Value)).ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Empty.
        /// </summary>
        public IUnique Empty => new Usid();

        /// <summary>
        /// Gets or sets the EvokerName.
        /// </summary>
        public string EvokerName { get; set; }

        /// <summary>
        /// Gets or sets the EvokerType.
        /// </summary>
        public EvokerType EvokerType { get; set; }

        /// <summary>
        /// Gets or sets the KeyBlock.
        /// </summary>
        public long KeyBlock { get => SystemCode.KeyBlock; set => SystemCode.KeyBlock = value; }

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
        public uint SeedBlock { get => SystemCode.SeedBlock; set => SystemCode.SeedBlock = value; }

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
            return ($"{SenderName}.{RecipientName}").GetBytes();
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
            return SystemCode.GetHashSeed();
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
            SystemCode.SetHashSeed(seed);
        }

        #endregion
    }
}
