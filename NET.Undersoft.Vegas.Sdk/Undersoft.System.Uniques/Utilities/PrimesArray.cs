/*************************************************************************************
    Copyright (c) 2020 Undersoft

    System.Sets.Uniques.SizePrimes
              
    @author Darius Hanc                                                  
    @project NETStandard.Undersoft.SDK                 
    @version 0.7.1.r.d (Feb 7, 2020)                                            
    @licence MIT                                       
 *********************************************************************************/

namespace System.Uniques
{
    /// <summary>
    /// Defines the <see cref="PRIMES_ARRAY" />.
    /// </summary>
    public static class PRIMES_ARRAY
    {
        #region Fields

        private static readonly int[] primes = {  53, 97, 193, 389, 769, 1543, 3079, 6151, 12289, 17519, 24593, 49157, 75431, 98317,
                                                156437, 196613, 270371, 393241, 560689, 786433, 1162687, 1572869, 2009191, 3145739,
                                                4166287, 6291469, 7199369, 10351711, 13289233, 15517591, 17987791, 20081053, 22983811,
                                                25165843, 34545523, 50331653, 76724731, 100663319, 154205533, 201326611, 309946381,
                                                402653189, 622991143, 805306457, 1256055793, 1610612741 };

        #endregion

        #region Methods

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int Get(int id)
        {
            return primes[id];
        }

        #endregion
    }
}
