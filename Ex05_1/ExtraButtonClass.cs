// -----------------------------------------------------------------------
// <copyright file="ExtraButtonClass.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace B14_Ex05_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ExtraButtonClass : Button
    {
        private readonly int m_Columns;
        private readonly int m_Rows;

        public int Columns
        {
            get { return m_Columns; }
        }

        public int Rows
        {
            get { return m_Rows; }
        }

        public ExtraButtonClass(int i_NumberOfRow, int i_NumberOfCol)
        {
            m_Rows = i_NumberOfRow;
            m_Columns = i_NumberOfCol;
        }

        public void ShowTheCell(string i_Letter, Color i_ColorOfPlayer)
        {
            this.BackColor = i_ColorOfPlayer;
            this.Text = i_Letter.ToString();
            this.Enabled = false;
        }

        public void DefualtButton()
        {
            this.Text = string.Empty;
            this.Enabled = true;
            this.UseVisualStyleBackColor = true;
        }
    }
}
