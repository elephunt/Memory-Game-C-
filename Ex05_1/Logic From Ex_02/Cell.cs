// -----------------------------------------------------------------------
// <copyright file="Cell.cs" company="">
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
    public struct Cell
    {
        private char m_Letter;
        private bool m_Visibile;
        private bool m_GetALetter;

        public char Letter
        {
            get
            {
                return m_Letter;
            }

            set
            {
                GetALetter = true;
                m_Letter = value;
            }
        }

        public bool Visibile
        {
            get { return m_Visibile; }
            set { m_Visibile = value; }
        }

        public bool GetALetter
        {
            get { return m_GetALetter; }
            set { m_GetALetter = value; }
        }
    }
}
