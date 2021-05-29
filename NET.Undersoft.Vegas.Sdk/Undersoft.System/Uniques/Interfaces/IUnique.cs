/*************************************************
   Copyright (c) 2021 Undersoft

   System.IUnique.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

namespace System
{
    /// <summary>
    /// Defines the <see cref="IUnique{V}" />.
    /// </summary>
    /// <typeparam name="V">.</typeparam>
    public interface IUnique<V> : IUnique
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        V Value { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The IdentitiesToKey.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        long IdentitiesToKey();

        /// <summary>
        /// The IdentityIndexes.
        /// </summary>
        /// <returns>The <see cref="int[]"/>.</returns>
        int[] IdentityIndexes();

        /// <summary>
        /// The IdentityValues.
        /// </summary>
        /// <returns>The <see cref="object[]"/>.</returns>
        object[] IdentityValues();

        #endregion
    }
    /// <summary>
    /// Defines the <see cref="IUnique" />.
    /// </summary>
    public interface IUnique : IEquatable<IUnique>, IComparable<IUnique>
    {
        #region Properties

        /// <summary>
        /// Gets the Empty.
        /// </summary>
        IUnique Empty { get; }

        /// <summary>
        /// Gets or sets the KeyBlock.
        /// </summary>
        long KeyBlock { get; set; }

        /// <summary>
        /// Gets or sets the SeedBlock.
        /// </summary>
        uint SeedBlock { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The GetBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        byte[] GetBytes();

        /// <summary>
        /// The GetHashKey.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        long GetHashKey();

        /// <summary>
        /// The GetHashSeed.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        uint GetHashSeed();

        /// <summary>
        /// The GetKeyBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        byte[] GetKeyBytes();

        /// <summary>
        /// The SetHashKey.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        void SetHashKey(long value);

        /// <summary>
        /// The SetHashSeed.
        /// </summary>
        /// <param name="seed">The seed<see cref="uint"/>.</param>
        void SetHashSeed(uint seed);

        #endregion
    }
}
