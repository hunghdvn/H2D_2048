using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace H2D_2048
{
    public partial class frmMain : Form
    {
        private int[,] matrixNumber = new int[4, 4];
        private Label[,] matrixBoard = new Label[4, 4];
        private Block block = new Block();
        private Random rand = new Random();
        private int Score;
        private bool GameOver = false;
        private string directory = @"C:\ProgramData\H2D_2048\";
        private string file = "Hiscore.txt";
        private string fullPath = "";

        public frmMain()
        {
            InitializeComponent();
            fullPath = directory + file;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                NewGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewGame()
        {
            LoadHiscore();
            Score = 0;
            lbScore.Text = Score.ToString();
            PrintBoard(pnGame);
            RandomBlock();
            RandomBlock();
        }

        private void LoadHiscore()
        {
            if (File.Exists(fullPath))
            {
                string oldHiscore = File.ReadAllText(fullPath);
                lbHiscore.Text = oldHiscore;
            }
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        switch (matrixNumber[i, j])
                        {
                            case 0:
                                block.SetToBlank(matrixBoard[i, j]);
                                break;
                            case 2:
                                block.SetTo2(matrixBoard[i, j]);
                                break;
                            case 4:
                                block.SetTo4(matrixBoard[i, j]);
                                break;
                            case 8:
                                block.SetTo8(matrixBoard[i, j]);
                                break;
                            case 16:
                                block.SetTo16(matrixBoard[i, j]);
                                break;
                            case 32:
                                block.SetTo32(matrixBoard[i, j]);
                                break;
                            case 64:
                                block.SetTo64(matrixBoard[i, j]);
                                break;
                            case 128:
                                block.SetTo128(matrixBoard[i, j]);
                                break;
                            case 256:
                                block.SetTo256(matrixBoard[i, j]);
                                break;
                            case 512:
                                block.SetTo512(matrixBoard[i, j]);
                                break;
                            case 1024:
                                block.SetTo1024(matrixBoard[i, j]);
                                break;
                            case 2048:
                                block.SetTo2048(matrixBoard[i, j]);
                                break;
                            case 4096:
                                block.SetTo4096(matrixBoard[i, j]);
                                break;
                            case 8192:
                                block.SetTo8192(matrixBoard[i, j]);
                                break;
                        }
                    }
                }
                lbScore.Text = Score.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RandomBlock()
        {
            var lstBlank = new List<int>();
            //Get block value blank
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrixNumber[i, j] == 0)
                    {
                        lstBlank.Add(i * 4 + j);
                    }
                }
            }
            if (lstBlank.Count > 0)
            {
                int set = lstBlank[rand.Next(0, lstBlank.Count - 1)];
                while (matrixNumber[set / 4, set % 4] != 0 && lstBlank.Count > 1)
                {
                    lstBlank.Remove(set);
                    set = lstBlank[rand.Next(0, lstBlank.Count - 1)];
                }
                matrixNumber[set / 4, set % 4] = rand.Next(1, 100) > 85 ? 4 : 2;
                Score += matrixNumber[set / 4, set % 4];
            }
        }

        private void PrintBoard(Panel mainPanel)
        {
            mainPanel.Controls.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var newLabel = new Block().Blank;
                    newLabel.Location = new Point(i + 4 + i * 80 + 4 * i, j + 4 + j * 80 + 4 * j);
                    mainPanel.Controls.Add(newLabel);
                    matrixNumber[i, j] = 0;
                    matrixBoard[i, j] = newLabel;
                }
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                bool bCanMove = false;
                if (GameOver)
                {
                    return;
                }
                if (e.KeyCode == Keys.Up)
                {
                    bCanMove = ActionUp();
                }
                if (e.KeyCode == Keys.Down)
                {
                    bCanMove = ActionDown();
                }
                if (e.KeyCode == Keys.Right)
                {
                    bCanMove = ActionRight();
                }
                if (e.KeyCode == Keys.Left)
                {
                    bCanMove = ActionLeft();
                }
                if (bCanMove)
                {
                    RandomBlock();
                }
                Refresh();
                if (CheckGameOver())
                {
                    GameOver = true;
                    MessageBox.Show("GameOver", "2048", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveHiscore(Score);
                    Score = 0;
                    lbScore.Text = Score.ToString();
                    LoadHiscore();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveHiscore(int score)
        {
            int hiscore = 0;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (File.Exists(fullPath))
            {
                string oldHiscore = File.ReadAllText(fullPath);
                if (!string.IsNullOrWhiteSpace(oldHiscore))
                    hiscore = int.Parse(oldHiscore);
                File.Delete(fullPath);
            }
            if (score > hiscore)
            {
                hiscore = score;
            }
            File.WriteAllText(fullPath, hiscore.ToString());
        }

        private bool ActionRight()
        {
            bool output = false;
            for (int x = 3; x > 0; x--)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x1 = x - 1; x1 >= 0; x1--)
                    {
                        if (matrixNumber[x1, y] > 0)
                        {
                            if (matrixNumber[x, y] == 0)
                            {
                                matrixNumber[x, y] = matrixNumber[x1, y];
                                matrixNumber[x1, y] = 0;
                                output = true;
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x1, y])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x1, y] = 0;
                                output = true;
                            }
                            break;
                        }
                    }
                }
            }
            return output;
        }

        private bool ActionLeft()
        {
            bool output = false;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x1 = x + 1; x1 < 4; x1++)
                    {
                        if (matrixNumber[x1, y] > 0)
                        {
                            if (matrixNumber[x, y] == 0)
                            {
                                matrixNumber[x, y] = matrixNumber[x1, y];
                                matrixNumber[x1, y] = 0;
                                output = true;
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x1, y])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x1, y] = 0;
                                output = true;
                            }
                            break;
                        }
                    }
                }
            }
            return output;
        }

        private bool ActionDown()
        {
            bool output = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 3; y > 0; y--)
                {
                    for (int y1 = y - 1; y1 >= 0; y1--)
                    {
                        if (matrixNumber[x, y1] > 0)
                        {
                            if (matrixNumber[x, y] == 0)
                            {
                                matrixNumber[x, y] = matrixNumber[x, y1];
                                matrixNumber[x, y1] = 0;
                                y++;
                                output = true;
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x, y1])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x, y1] = 0;
                                output = true;
                            }
                            break;
                        }
                    }
                }
            }
            return output;
        }

        private bool ActionUp()
        {
            bool output = false;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int y1 = y + 1; y1 < 4; y1++)
                    {
                        if (matrixNumber[x, y1] > 0)
                        {
                            if (matrixNumber[x, y] == 0)
                            {
                                matrixNumber[x, y] = matrixNumber[x, y1];
                                matrixNumber[x, y1] = 0;
                                y--;
                                output = true;
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x, y1])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x, y1] = 0;
                                output = true;
                            }
                            break;
                        }
                    }
                }
            }
            return output;
        }

        private bool CheckGameOver()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (matrixNumber[x, y] == 0 || (y < 3 && matrixNumber[x, y] == matrixNumber[x, y + 1]) || (x < 3 && matrixNumber[x, y] == matrixNumber[x + 1, y]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            frmMain_Paint(null, null);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to exit game?", "2048", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new frmAbout();
            about.ShowDialog();
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string howToPlay = "Arow Up to swipe Up\r\n" +
                "Arow Down to swipe Down\r\n" +
                "Arow Left to swipe Left\r\n" +
                "Arow Right to swipe Right";
            MessageBox.Show(howToPlay, "How to play");
        }
    }
}
