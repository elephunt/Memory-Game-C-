// -----------------------------------------------------------------------
// <copyright file="TheWindowsBoard.cs" company="">
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
    using System.Threading;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TheWindowsBoard : Form
    {
        private Color FirstPlayerColor = Color.LightBlue;
        private Color SecundPlayerColor = Color.LightCoral;
        private bool newGame = true;
        private int m_SizeOfColumns;
        private int m_SizeOfRows;
        private Label m_CurrentPlayer = new Label();
        private Label m_LabelFirstPlayer = new Label();
        private Label m_LabelSecundPlayer = new Label();
        private ExtraButtonClass[,] m_ArrayOfButtons = null;
        private GameLogic m_GameLogic = new GameLogic();

        public bool NewGame
        {
            get { return newGame; }
        }

        public TheWindowsBoard(int i_Coloms, int i_Rows, string i_NameOfFirstPlayer, string i_NameOFSecondPlayer, bool IfTheSecondPlayerIsPC)
        {
            m_GameLogic.PcPLayer = IfTheSecondPlayerIsPC;
            m_GameLogic.SetPlayers(i_NameOfFirstPlayer, 1);
            m_GameLogic.SetPlayers(i_NameOFSecondPlayer, 2);
            m_SizeOfColumns = i_Coloms;
            m_SizeOfRows = i_Rows;
            m_GameLogic.CreateTheBoard(m_SizeOfRows, m_SizeOfColumns);
            if (i_NameOFSecondPlayer == "Computer")
            {
                m_GameLogic.OpenCellsByComputer += OpenCellsByComputer;
            }

            m_GameLogic.CloseCell += closeTheButtonForNotMatch;
            m_ArrayOfButtons = new ExtraButtonClass[i_Rows, i_Coloms];
            this.Size = new Size(i_Coloms * 92, (i_Rows * 98) + 90);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        private void InitControls()
        {
            for (int CurrentRow = 0; CurrentRow < m_SizeOfRows; CurrentRow++)
            {
                for (int CurrentColumn = 0; CurrentColumn < m_SizeOfColumns; CurrentColumn++)
                {
                    m_ArrayOfButtons[CurrentRow, CurrentColumn] = new ExtraButtonClass(CurrentRow, CurrentColumn);
                    m_ArrayOfButtons[CurrentRow, CurrentColumn].Size = new Size(80, 80);
                    m_ArrayOfButtons[CurrentRow, CurrentColumn].Location = new Point(14 + (CurrentColumn * 85), 14 + (CurrentRow * 85));
                    m_ArrayOfButtons[CurrentRow, CurrentColumn].Click += new EventHandler(TheWindowsBoard_Click);
                    this.Controls.AddRange(new Control[] { m_ArrayOfButtons[CurrentRow, CurrentColumn] });
                }
            }

            m_CurrentPlayer.BackColor = CurentPlayerColor();
            m_CurrentPlayer.AutoSize = true;
            m_LabelFirstPlayer.AutoSize = true;
            m_LabelSecundPlayer.AutoSize = true;
            m_CurrentPlayer.Location = new Point(5, m_ArrayOfButtons[m_SizeOfRows - 1, 0].Bottom + 20);
            m_LabelFirstPlayer.BackColor = FirstPlayerColor;
            m_LabelSecundPlayer.BackColor = SecundPlayerColor;
            m_LabelFirstPlayer.Location = new Point(5, m_CurrentPlayer.Location.Y + 30);
            m_LabelSecundPlayer.Location = new Point(5, m_LabelFirstPlayer.Location.Y + 30);
            UpdateTheCurrentPlayerMessge();
        }

        private void TheWindowsBoard_Click(object sender, EventArgs e)
        {
            ExtraButtonClass TempButton = sender as ExtraButtonClass;
            if (TempButton != null)
            {
                TempButton.ShowTheCell(m_GameLogic.PlayerMove(TempButton.Rows, TempButton.Columns).ToString(), CurentPlayerColor());
                Thread.Sleep(150);
                if (m_GameLogic.MatchCells())
                {
                    screenBlink();
                }

                checkIfEndGameAndPrintMessage();
                UpdateTheCurrentPlayerMessge();
            }
        }

        private void checkIfEndGameAndPrintMessage()
        {
            if (m_GameLogic.BoardIsFull())
            {
                UpdateTheCurrentPlayerMessge();
                DialogResult dialogResult = MessageBox.Show(ResultOfGame().ToString() + "\n\nDo you want another game?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    newGame = true;
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    newGame = false;
                    this.Close();
                    MessageBox.Show("Good Bye");
                }
            }
        }

        private void closeTheButtonForNotMatch(int i_Row1, int i_col1, int i_Row2, int i_Col2)
        {
            MessageBox.Show("Wrong select");
            m_ArrayOfButtons[i_Row1, i_col1].DefualtButton();
            m_ArrayOfButtons[i_Row2, i_Col2].DefualtButton();
            Application.DoEvents();
            UpdateTheCurrentPlayerMessge();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "TheWindowsBoard";
            this.Load += new System.EventHandler(this.TheWindowsBoard_Load);
            this.ResumeLayout(false);
        }

        public Color CurentPlayerColor()
        {
            Color CurrentColor;
            if (m_GameLogic.CurrentPlayer == 1)
            {
                CurrentColor = FirstPlayerColor;
            }
            else
            {
                CurrentColor = SecundPlayerColor;
            }

            return CurrentColor;
        }

        public void UpdateTheCurrentPlayerMessge()
        {
            m_CurrentPlayer.BackColor = CurentPlayerColor();
            m_CurrentPlayer.Text = "Current Player: " + m_GameLogic.GetName();
            m_LabelFirstPlayer.Text = m_GameLogic.GetPLayer(0);
            m_LabelSecundPlayer.Text = m_GameLogic.GetPLayer(1);
            this.Controls.AddRange(new Control[] { m_CurrentPlayer, m_LabelSecundPlayer, m_LabelFirstPlayer });
            Application.DoEvents();
        }

        public StringBuilder ResultOfGame()
        {
            StringBuilder gameResults = new StringBuilder();
            gameResults.Append("The Score is: " + Environment.NewLine + m_GameLogic.GetPLayer(0));
            gameResults.Append(Environment.NewLine + m_GameLogic.GetPLayer(1));
            gameResults.Append(m_GameLogic.TheWinner());
            return gameResults;
        }

        private void TheWindowsBoard_Load(object sender, EventArgs e)
        {
        }

        ///////////////////////////////////JUST FOR FUN////////////////////////////////////
        public void screenBlink()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(8);
                m_CurrentPlayer.Focus();
                this.BackColor = SystemColors.Control;
                Application.DoEvents();
                Thread.Sleep(8);
                this.BackColor = Color.Green;
                Application.DoEvents();
                Thread.Sleep(8);

                this.BackColor = Color.Yellow;
                Application.DoEvents();
            }

            this.BackColor = SystemColors.Control;
            Application.DoEvents();
        }

        public void OpenCellsByComputer(int i_Row1, int i_Col1, int i_Row2, int i_Col2)
        {
            UpdateTheCurrentPlayerMessge();
            m_ArrayOfButtons[i_Row1, i_Col1].ShowTheCell(m_GameLogic.GetALetterInCell(i_Row1, i_Col1), CurentPlayerColor());
            m_ArrayOfButtons[i_Row2, i_Col2].ShowTheCell(m_GameLogic.GetALetterInCell(i_Row2, i_Col2), CurentPlayerColor());
            if (m_GameLogic.GetALetterInCell(i_Row2, i_Col2) == m_GameLogic.GetALetterInCell(i_Row1, i_Col1))
            {
                UpdateTheCurrentPlayerMessge();
                screenBlink();
            }

            Thread.Sleep(50);
        }
    }
}
