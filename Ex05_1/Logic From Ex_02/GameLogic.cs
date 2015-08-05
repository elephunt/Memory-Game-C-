// -----------------------------------------------------------------------
// <copyright file="GameSettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace B14_Ex05_1
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public delegate void ButtonBoardEventHandler(int Row1, int Col1, int Row2, int Col2);

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// $G$ CSS-999 (0) Better class name would be GameLogic.
    public class GameLogic
    {
        private bool m_IfTheSecondPlayerIsPc = false;
        private int m_Row1 = 0, m_Row2 = 0, m_Col1 = 0, m_Col2 = 0;
        private int m_CounterOfMoves = 1;
        private int m_CurrentPlayer = 1;
        private GameBoard board = null;

        public event ButtonBoardEventHandler CloseCell;

        public event ButtonBoardEventHandler OpenCellsByComputer;

        private List<Player> m_Players = new List<Player>(2);

        /// $G$ NTT-005 (-3) You should use properties. Fields should be encapsulated.
        private List<MovesForRemberToPC> Moves = new List<MovesForRemberToPC>();

        public bool PcPLayer
        {
            set { m_IfTheSecondPlayerIsPc = value; }
        }

        public int CurrentPlayer
        {
            get { return m_CurrentPlayer; }
        }

        public void Quit(string i_CheckString)
        {
            if (i_CheckString.Length == 1)
            {
                if (i_CheckString[0] == 'Q')
                {
                    Environment.Exit(0);
                }
            }
        }

        public string GetPLayer(int i_NumberOfPLayer)
        {
            return m_Players[i_NumberOfPLayer].ToString();
        }

        private void changePlayer()
        {
            if (m_CurrentPlayer == 1)
            {
                m_CurrentPlayer = 2;
            }
            else
            {
                m_CurrentPlayer = 1;
            }
        }

        public void CreateTheBoard(int i_Rows, int i_Cols)
        {
            if (board == null)
            {
                board = new GameBoard();
                board.CreateABoard(i_Rows, i_Cols);
            }   
        }

        public void SetPlayers(string i_Name, int i_NumberOfPlayer)
        {
            Player temp = new Player(i_Name, i_NumberOfPlayer);
            if (m_Players == null)
            {
                m_Players = new List<Player>();
            }

            m_Players.Add(temp);
        }

        public string GetName()
        {
            string playerName;
            if (m_CurrentPlayer == 1)
            {
                playerName = m_Players[0].Name;
            }
            else
            {
                playerName = m_Players[1].Name;
            }

            return playerName;
        }

        public bool MatchCells()
        {
            bool matchCells = false;
            if (m_CounterOfMoves > 2)
            {
                m_CounterOfMoves = 1;
                matchCells = board.MatchCell(m_Row1, m_Row2, m_Col1, m_Col2);
                if (matchCells == false)
                {
                    changePlayer();
                    board.CloseCell(m_Row1, m_Row2, m_Col1, m_Col2);
                    CloseCell.Invoke(m_Row1, m_Col1, m_Row2, m_Col2);
                    if (m_IfTheSecondPlayerIsPc == true)
                    {
                        m_CounterOfMoves = 1;
                        PlayerVsPc();
                        changePlayer();
                    }
                }
                else
                {
                    RaiseScore();
                }
            }

            return matchCells;
        }

        public void RaiseScore()
        {
            if (m_CurrentPlayer == 1)
            {
                m_Players[0].RaiseScore();
            }
            else
            {
                m_Players[1].RaiseScore();
            }
        }

        public int GetScore()
        {
            int playerscore = 0;
            if (m_CurrentPlayer == 1)
            {
                playerscore = m_Players[0].Score;
            }
            else
            {
                playerscore = m_Players[1].Score;
            }

            return playerscore;
        }

        public bool BoardIsFull()
        {
            return board.BoardIsFull();
        }

        public StringBuilder TheWinner()
        {
            StringBuilder MessageOfTheWinner = new StringBuilder();
            if (m_Players[0].Score > m_Players[1].Score)
            {
                m_CurrentPlayer = 1;
                MessageOfTheWinner.Append(Environment.NewLine + "The Winner is " + m_Players[m_CurrentPlayer - 1].ToString());
            }
            else if (m_Players[0].Score < m_Players[1].Score)
            {
                m_CurrentPlayer = 2;
                MessageOfTheWinner.Append(Environment.NewLine + "The Winner is " + m_Players[m_CurrentPlayer - 1].ToString());
            }
            else
            {
                m_CurrentPlayer = 3;
                MessageOfTheWinner.Append(Environment.NewLine + "Its A tie");
            }

            return MessageOfTheWinner;
        }

        public char PlayerMove(int i_Row, int i_Column)
        {
            remeberTheRowsAndColsForMatching(i_Row, i_Column);
            board.FlopTheCell(i_Row, i_Column);
            if (m_IfTheSecondPlayerIsPc == true)
            {
                remeberUserChoiseForComputerPLayer(i_Row, i_Column);
            }

            m_CounterOfMoves++;
            return board.Board[i_Row, i_Column].Letter;
        }

        private void remeberTheRowsAndColsForMatching(int i_Row, int i_Cols)
        {
            if (m_CounterOfMoves == 1)
            {
                m_Row1 = i_Row;
                m_Col1 = i_Cols;
            }
            else
            {
                m_Row2 = i_Row;
                m_Col2 = i_Cols;
            }
        }

        private void computerTurnToGetIndexsByRandom(out int o_Rows, out int o_Cols)
        {
            o_Rows = 0;
            o_Cols = 0;
            int randomIndexOfRow;
            int randomindexOfCol;
            /// $G$ NTT-007 (-5) There's no need to re-instantiate the Random instance each time it is used.
            var randomChoise = new Random();
            do
            {
                randomindexOfCol = randomChoise.Next(0, board.Columns);
                randomIndexOfRow = randomChoise.Next(0, board.Rows);
            }
            while (board.Board[randomIndexOfRow, randomindexOfCol].Visibile == true);
            o_Rows = randomIndexOfRow;
            o_Cols = randomindexOfCol;
        }

        private bool ifLetterAlreadyInthereInPcMemory(int i_Row, int i_Col)
        {
            // $G$ CSS-001 (0) Bad local variable name (should be camelCased)
            bool Good = false;
            foreach (MovesForRemberToPC element in Moves)
            {
                if (element.Rows == i_Row && element.Columns == i_Col)
                {
                    Good = true;
                    break;
                }
            }

            return Good;
        }

        public void PlayerVsPc()
        {
            bool ifTheCellsAreMatch = false, boardIFull = false;
            do
            {
                mainComputerTurn();
                buttonWithLetter_Open();
                ifTheCellsAreMatch = board.MatchCell(m_Row1, m_Row2, m_Col1, m_Col2);
                if (ifTheCellsAreMatch == true)
                {
                    RaiseScore();
                }

                boardIFull = board.BoardIsFull();
            } 
            while (ifTheCellsAreMatch == true && boardIFull == false );
            if (boardIFull == false)
            {
                board.CloseCell(m_Row1, m_Row2, m_Col1, m_Col2);
                buttonDefault_Close();
            }
        }

        private void buttonDefault_Close()
        {
            if (CloseCell != null)
            {
                CloseCell.Invoke(m_Row1, m_Col1, m_Row2, m_Col2);
            }
        }

        private void buttonWithLetter_Open()
        {
            if (OpenCellsByComputer != null)
            {
                OpenCellsByComputer.Invoke(m_Row1, m_Col1, m_Row2, m_Col2);
            }
        }

        // $G$ CSS-010 (-5) Bad public/protected method name. Should be PascalCased.
        // $G$ DSN-003 (-3) This method is not used outside of this class. It should have been encapsulated.
        private bool returnRightLettersInMemoryPc(char i_Letter, int i_Row, int i_Col, out int o_Row, out int o_Col)
        {
            o_Row = 0;
            o_Col = 0;
            bool foundLetter = false;
            bool alreadyThere = false;
            alreadyThere = ifLetterAlreadyInthereInPcMemory(i_Row, i_Col);
            if (alreadyThere == false)
            {
                foreach (MovesForRemberToPC element in Moves)
                {
                    if (element.Letter == i_Letter)
                    {
                        o_Row = element.Rows;
                        o_Col = element.Columns;
                        foundLetter = true;
                        break;
                    }
                }
            }

            if (alreadyThere == false && foundLetter == false)
            {
                insertLetterForPcMemory(i_Row, i_Col, i_Letter);
            }

            if (foundLetter == true)
            {
                Moves.RemoveAll(item => item.Letter == i_Letter);
            }

            return foundLetter;
        }

        // $G$ CSS-010 (-5) Bad private method name. Should be lower Cased.
        private bool insertLetterForPcMemory(int i_Row, int i_Col, char i_Letter)
        {
            MovesForRemberToPC mov = new MovesForRemberToPC();
            bool good = true;
            if (Moves.Capacity < 8)
            {
                foreach (MovesForRemberToPC element in Moves)
                {
                    if (element.Letter == i_Letter && element.Rows == i_Row && element.Columns == i_Col)
                    {
                        good = false;
                        break;
                    }
                }
            }

            if (good == true)
            {
                mov.Columns = i_Col;
                mov.Rows = i_Row;
                mov.Letter = i_Letter;
                Moves.Add(mov);
            }

            return true;
        }

        private void checkIfLetterIsValiedForPcMemory()
        {
            Moves.RemoveAll(item => board.Board[item.Rows, item.Columns].Visibile == true);
        }

        // $G$ SFN-014 (+14) Bonus! use of AI to determine next computer move.
        private void mainComputerTurn()
        {
            if (Moves == null)
            {
                Moves = new List<MovesForRemberToPC>();
            }

                 m_Row1 = m_Col1 = m_Row2 = m_Col2 = 0;
              	if (checkPairInMemoryForComputerPLayer(out m_Row1, out m_Col1, out m_Row2, out m_Col2) == false)
	            {
	                computerTurnToGetIndexsByRandom(out m_Row1, out m_Col1);
	                board.FlopTheCell(m_Row1, m_Col1);
	
	                if (returnRightLettersInMemoryPc(board.Board[m_Row1, m_Col1].Letter, m_Row1, m_Col1, out m_Row2, out m_Col2) == false)
	                {
	                    computerTurnToGetIndexsByRandom(out m_Row2, out m_Col2);
	                }
	
	                board.FlopTheCell(m_Row2, m_Col2);
	            }
	
	            board.FlopTheCell(m_Row1, m_Col1);
	            board.FlopTheCell(m_Row2, m_Col2);
        }

        private bool checkPairInMemoryForComputerPLayer(out int o_Row1, out int o_Col1, out int i_Row2, out int i_Col2)
        {
            bool Pair = false;
            char LetterToRemove = 'A';
            o_Row1 = o_Col1 = i_Row2 = i_Col2 = 0;
            checkIfLetterIsValiedForPcMemory();
            foreach (MovesForRemberToPC element in Moves)
            {
                if (Pair == true)
                {
                    break;
                }

                foreach (MovesForRemberToPC otherelemnt in Moves)
                {
                    if (sameCellInMemoryOfPcAlready(element.Rows, element.Columns, otherelemnt.Rows, otherelemnt.Columns, element.Letter, otherelemnt.Letter) == true)
                    {
                        o_Row1 = element.Rows;
                        o_Col1 = element.Columns;
                        i_Row2 = otherelemnt.Rows;
                        i_Col2 = otherelemnt.Columns;
                        LetterToRemove = element.Letter;
                        Pair = true;
                        break;
                    }
                }
            }

            if (Pair == true)
            {
                Moves.RemoveAll(item => item.Letter == LetterToRemove);
            }

            return Pair;
        }

        public string GetALetterInCell(int i_Row1, int i_Col1)
        {
            return board.Board[i_Row1, i_Col1].Letter.ToString();
        }

        private void remeberUserChoiseForComputerPLayer(int i_Row, int i_Col)
        {
            if (ifLetterAlreadyInthereInPcMemory(i_Row, i_Col) == false)
            {
                insertLetterForPcMemory(i_Row, i_Col, board.Board[i_Row, i_Col].Letter);
            }
        }

        private bool sameCellInMemoryOfPcAlready(int i_Row1, int i_Col1, int i_Row2, int i_Col2, char i_letter1, char i_letter2)
        {
            bool sameCell = false;
            if (i_Row1 == i_Row2 && i_Col1 != i_Col2 && i_letter1 == i_letter2)
            {
                sameCell = true;
            }

            if (i_Row1 != i_Row2 && i_Col1 == i_Col2 && i_letter1 == i_letter2)
            {
                sameCell = true;
            }

            if (i_Row1 != i_Row2 && i_Col1 != i_Col2 && i_letter1 == i_letter2)
            {
                sameCell = true;
            }

            return sameCell;
        }
    }
}
