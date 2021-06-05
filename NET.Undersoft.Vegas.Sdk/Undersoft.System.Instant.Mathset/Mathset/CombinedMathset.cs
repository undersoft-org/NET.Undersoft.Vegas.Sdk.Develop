/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Mathset.CombinedMathset.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Mathset
{
    /// Class for the Generated Code
    /// <summary>
    /// Defines the <see cref="CombinedMathset" />.
    /// </summary>
    public abstract class CombinedMathset
    {
        #region Fields

        public IFigures[] DataParameters = new IFigures[1];
        public int ParametersCount = 0;

        #endregion

        #region Methods

        /// <summary>
        /// The Compute.
        /// </summary>
        public abstract void Compute();

        /// <summary>
        /// The GetColumnCount.
        /// </summary>
        /// <param name="paramid">The paramid<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetColumnCount(int paramid)
        {
            return DataParameters[paramid].Rubrics.Count;
        }

        /// <summary>
        /// The GetIndexOf.
        /// </summary>
        /// <param name="v">The v<see cref="IFigures"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetIndexOf(IFigures v)
        {
            for (int i = 0; i < ParametersCount; i++)
                if (DataParameters[i] == v) return 1 + i;
            return -1;
        }

        /// <summary>
        /// The GetRowCount.
        /// </summary>
        /// <param name="paramid">The paramid<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetRowCount(int paramid)
        {
            return DataParameters[paramid].Count;
        }

        /// <summary>
        /// The Put.
        /// </summary>
        /// <param name="v">The v<see cref="IFigures"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Put(IFigures v)
        {
            int index = GetIndexOf(v);
            if (index < 0)
            {
                DataParameters[ParametersCount] = v;
                return 1 + ParametersCount++;
            }
            else
            {
                DataParameters[index] = v;
            }
            return index;
        }

        /// <summary>
        /// The SetParams.
        /// </summary>
        /// <param name="p">The p<see cref="IFigures"/>.</param>
        public void SetParams(IFigures p)
        {
            Put(p);
        }

        /// <summary>
        /// The SetParams.
        /// </summary>
        /// <param name="p">The p<see cref="IFigures"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool SetParams(IFigures p, int index)
        {
            if (index < ParametersCount)
            {
                if (ReferenceEquals(DataParameters[index], p))
                    return false;
                else
                    DataParameters[index] = p;
            }
            return false;
        }

        /// <summary>
        /// The SetParams.
        /// </summary>
        /// <param name="p">The p<see cref="IFigures[]"/>.</param>
        /// <param name="paramCount">The paramCount<see cref="int"/>.</param>
        public void SetParams(IFigures[] p, int paramCount)
        {
            DataParameters = p;
            ParametersCount = paramCount;
        }

        #endregion
    }
}
