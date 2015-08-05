// -----------------------------------------------------------------------
// <copyright file="TheBoard.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace B14_Ex05_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameBoard
    {
        private int m_Columns = 0;
        private int m_Rows = 0;

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

        private Cell[,] m_Board = null;

        public Cell[,] Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public void CreateABoard(int i_Rows, int i_Column)
        {
            Rows = i_Rows;
            Columns = i_Column;
            randomStringCreator();
        }

        private void randomStringCreator()
        {
            int sizeOfBoard = m_Columns * m_Rows;
            int randomIndex = 0;
            var randomChoice = new Random();
            Cell[] board = new Cell[sizeOfBoard];
            char currentLetter = 'A';
            for (int i = 0; i < sizeOfBoard; i++)
            {
                randomIndex = randomChoice.Next(0, sizeOfBoard);

                while (board[randomIndex].GetALetter == true)
                {
                    if (randomIndex == (sizeOfBoard - 1))
                    {
                        randomIndex = -1;  //// Start from the beginning of the Array
                    }

                    randomIndex++; //// Moves to the next index
                }

                board[randomIndex].Letter = currentLetter;

                if (i % 2 != 0)
                {
                    currentLetter++; ////Move to the next char
                }

                from1dTo2d(board);
            }
        }

        private void from1dTo2d(Cell[] i_Board)
        {
            m_Board = new Cell[m_Rows, m_Columns];
            int k = 0;
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Columns; j++, k++)
                {
                    m_Board[i, j] = i_Board[k];
                }
            }
        }

        public bool BoardIsFull() ////check if the game is over
        {
            bool isFull = true;
            foreach (Cell element in m_Board)
            {
                if (element.Visibile == false)
                {
                    isFull = false;
                }
            }

            return isFull;
        }

        public bool IfTheCellIsOpen(int i_Row, int i_Cols)
        {
            return m_Board[i_Row, i_Cols].Visibile == true;
        }

        // $G$ CSS-014 (-3) Bad third output variable name (should be in the form of o_PascalCased)
        public void CloseCell(int i_Row1, int i_Row2, int i_Col1, int i_Col2)
        {
            m_Board[i_Row1, i_Col1].Visibile = false;
            m_Board[i_Row2, i_Col2].Visibile = false;
        }

        public bool MatchCell(int i_Row1, int i_Row2, int i_Col1, int i_Col2)
        {
            return m_Board[i_Row1, i_Col1].Letter == m_Board[i_Row2, i_Col2].Letter;
        }

        public void FlopTheCell(int i_Row, int i_Cols)
        {
            m_Board[i_Row, i_Cols].Visibile = true;
        }

        public void ResetTable()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Columns; j++)
                {
                    m_Board[i, j].GetALetter = false;
                    m_Board[i, j].Visibile = false;
                }
            }

            randomStringCreator();
        }
    }  
}