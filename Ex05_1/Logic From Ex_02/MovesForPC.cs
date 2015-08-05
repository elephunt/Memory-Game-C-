// -----------------------------------------------------------------------
// <copyright file="MovesForPC.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace B14_Ex05_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public struct MovesForRemberToPC
    {
        private int m_Columns;
        private int m_Rows;
        private char m_Letter;

        public int Columns
        {
            get { return m_Columns; }
            set { m_Columns = value; }
        }

        public int Rows
        {
            get { return m_Rows; }
            set { m_Rows = value; }
        }

        public char Letter
        {
            get { return m_Letter; }
            set { m_Letter = value; }
        }
    }
}
