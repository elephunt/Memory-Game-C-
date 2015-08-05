// -----------------------------------------------------------------------
// <copyright file="Programm.cs" company="">
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
    public class Programm
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new TheWindowOfStartAndSettings());
        }
    }
}
