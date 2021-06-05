/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Mathset.MathsetSize.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Mathset
{
    using System;

    /// <summary>
    /// Defines the <see cref="MathsetSize" />.
    /// </summary>
    [Serializable]
    public class MathsetSize
    {
        #region Fields

        public static MathsetSize Scalar = new MathsetSize(1, 1);
        public int cols;
        public int rows;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MathsetSize"/> class.
        /// </summary>
        /// <param name="i">The i<see cref="int"/>.</param>
        /// <param name="j">The j<see cref="int"/>.</param>
        public MathsetSize(int i, int j)
        {
            rows = i;
            cols = j;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object o)
        {
            if (o is MathsetSize) return ((MathsetSize)o) == this;
            return false;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            return rows * cols;
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return "" + rows + " " + cols;
        }

        #endregion


        public static bool operator !=(MathsetSize o1, MathsetSize o2)
        {
            return o1.rows != o2.rows || o1.cols != o2.cols;
        }

        public static bool operator ==(MathsetSize o1, MathsetSize o2)
        {
            return o1.rows == o2.rows && o1.cols == o2.cols;
        }
    }
}
