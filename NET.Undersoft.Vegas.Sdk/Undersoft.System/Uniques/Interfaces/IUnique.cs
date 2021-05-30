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
    public interface IUnique<V> : IUnique
    {
        #region Properties

        V Value { get; set; }

        #endregion

        #region Methods

        long UniquesAsKey();

        int[] UniqueOrdinals();

        object[] UniqueValues();

        #endregion
    }
    public interface IUnique : IEquatable<IUnique>, IComparable<IUnique>
    {
        #region Properties

        IUnique Empty { get; }

        long UniqueKey { get; set; }

        uint UniqueSeed { get; set; }

        #endregion

        #region Methods

        byte[] GetBytes();

        byte[] GetUniqueBytes();

        //long GetUniqueKey();

        //uint GetUniqueSeed();



        //void SetUniqueKey(long value);

        //void SetUniqueSeed(uint seed);

        #endregion
    }
}
