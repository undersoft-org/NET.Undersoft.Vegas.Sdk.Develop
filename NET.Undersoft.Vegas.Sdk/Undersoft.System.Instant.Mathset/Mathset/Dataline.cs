/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Mathset.Dataline.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Mathset
{
    using System;

    public class Dataline
    {
        public int RowCount;
        public int RowOffset;

        public Dataline()
        {
        }
        public Dataline(IFigures table)
        {
            Data = table;
        }
        public Dataline(IFigures table, int rowOffset, int rowCount)
        {
            RowCount = rowCount;
            RowOffset = rowOffset;
            Data = table;
        }

        public double this[int rowid, int cellid]
        {
            get
            {
                return Convert.ToDouble(Data[rowid, cellid]);
            }
            set
            {
                Data[rowid, cellid] = value;
            }
        }

        public IFigures Data;

        public bool Enabled = false;
        public int Count
        { get; set; }
    }
}
