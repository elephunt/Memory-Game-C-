// -----------------------------------------------------------------------
// <copyright file="Player.cs" company="">
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
    public class Player
    {
        /// $G$ CSS-002 (0) Bad member variable name (should be in the form of m_PascalCased)
        private string m_name;

        private int m_score;

        private int m_numberOfPlayer;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int Score
        {
            get { return m_score; }
        }

        public void RaiseScore()
        {
            m_score++;
        }

        public int NumberOfPlayer
        {
            get { return m_numberOfPlayer; }
            set { m_numberOfPlayer = value; }
        }

        public Player(string i_name, int i_NumberOfPlayer)
        {
            m_name = i_name;
            m_score = 0;
            m_numberOfPlayer = i_NumberOfPlayer;
        }

        public void ResetScore()
        {
            m_score = 0;
        }

        public override string ToString()
        {
            return m_name + ": " + m_score;
        }
    }
}
