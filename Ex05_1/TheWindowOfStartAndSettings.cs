// -----------------------------------------------------------------------
// <copyright file="TheWindowOfStartAndSettings.cs" company="">
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
    public class TheWindowOfStartAndSettings : Form
    {
        private int Row = 4;
        private int Colom = 4;
        private string stringBoardSize;

        public int ROW
        {
            get { return Row; }
        }

        public int COLOM
        {
            get { return Colom; }
        }

        private TextBox m_FirstPlayerName = new TextBox();
        private TextBox m_SecondPlayerName = new TextBox();
        private Label m_LabelFirstPlayer = new Label();
        private Label m_LabelSecondPlayer = new Label();
        private Label m_LabelBoardSize = new Label();
        private Button m_Start = new Button();
        private Button m_BoardSize = new Button();
        private Button m_Against = new Button();

        public TheWindowOfStartAndSettings()
        {
            this.Size = new Size(350, 200);
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
            m_LabelFirstPlayer.Text = "First player:";
            m_LabelFirstPlayer.Location = new Point(10, 20);

            m_LabelSecondPlayer.Text = "Second player:";
            m_LabelSecondPlayer.Location = new Point(m_LabelFirstPlayer.Location.X, m_LabelFirstPlayer.Location.Y + 30);
            m_FirstPlayerName.Location = new Point(m_LabelFirstPlayer.Location.X + 100, m_LabelFirstPlayer.Location.Y);
            m_SecondPlayerName.Location = new Point(m_LabelFirstPlayer.Location.X + 100, m_LabelFirstPlayer.Location.Y + 30);
            m_SecondPlayerName.Text = "Computer";
            m_SecondPlayerName.Enabled = false;

            m_Against.Location = new Point(m_SecondPlayerName.Location.X + 120, m_SecondPlayerName.Location.Y);
            m_Against.Text = "Against Friend";
            m_Against.Size = new Size(100, 20);

            m_LabelBoardSize.Text = "Board Size";
            m_LabelBoardSize.Location = new Point(10, m_LabelSecondPlayer.Location.Y + 30);

            stringBoardSize = Colom.ToString() + "X" + Row.ToString();
            m_BoardSize.Text = stringBoardSize.ToString();
            m_BoardSize.Location = new Point(10, m_LabelBoardSize.Location.Y + 22);
            m_BoardSize.Size = new Size(80, 55);
            m_Start.Text = "Start";
            m_Start.Location = new Point(m_Against.Location.X, m_Against.Location.Y + 85);
            m_Start.BackColor = Color.Green;

            this.Controls.AddRange(new Control[] { m_LabelFirstPlayer, m_LabelSecondPlayer, m_FirstPlayerName, m_SecondPlayerName, m_Against, m_LabelBoardSize, m_BoardSize, m_Start });

            this.m_Against.Click += new EventHandler(m_Against_Click);
            this.m_BoardSize.Click += new EventHandler(m_BoardSize_Click);
            this.m_Start.Click += new EventHandler(m_Start_Click);
        }

        private void m_Start_Click(object sender, EventArgs e)
        {
            bool AgainstPc = m_Against.Text == "Against Friend";
            TheWindowsBoard GameBoard = new TheWindowsBoard(Colom, Row, m_FirstPlayerName.Text, m_SecondPlayerName.Text, AgainstPc);
            this.Hide(); ///////////// Check
            GameBoard.ShowDialog();
            if (GameBoard.NewGame == true)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void m_BoardSize_Click(object sender, EventArgs e)
        {
            int TempColumn = Colom;
            int TempRow = Row;
            if (TempRow == 4)
            {
                if (TempColumn < 6)
                {
                    TempColumn++;
                }
                else
                {
                    TempRow++;
                    TempColumn = 4;
                }
            }
            else
            {
                if (TempRow == 5)
                {
                    if (TempColumn < 6)
                    {
                        TempColumn++;
                        if (TempColumn == 5)
                        {
                            TempColumn++;
                        }
                    }
                    else
                    {
                        TempRow++;
                        TempColumn = 4;
                    }
                }
                else
                {
                    if (TempRow == 6)
                    {
                        if (TempColumn < 6)
                        {
                            TempColumn++;
                        }
                        else
                        {
                            TempRow = 4;
                            TempColumn = 4;
                        }
                    }
                }
            }

            Colom = TempColumn;
            Row = TempRow;
            stringBoardSize = ROW.ToString() + "X" + COLOM.ToString();
            (sender as Button).Text = stringBoardSize.ToString();
        }

        private void m_Against_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Against Friend")
            {
                (sender as Button).Text = "Against Computer";
                m_SecondPlayerName.Enabled = true;
                m_SecondPlayerName.Text = string.Empty;
            }
            else
            {
                (sender as Button).Text = "Against Friend";
                m_SecondPlayerName.Enabled = false;
                m_SecondPlayerName.Text = "Computer";
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "TheWindowOfStartAndSettings";
            this.Load += new System.EventHandler(this.TheWindowOfStartAndSettings_Load);
            this.ResumeLayout(false);
        }

        private void TheWindowOfStartAndSettings_Load(object sender, EventArgs e)
        {
        }
    }
}
