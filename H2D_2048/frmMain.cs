using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace H2D_2048
{
    public partial class frmMain : Form
    {
        private int[,] matrixNumber = new int[4, 4];
        private Label[,] matrixBoard = new Label[4, 4];
        private Block block = new Block();
        private Random rand = new Random();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                PrintBoard(pnGame);
                RandomBlock();
                RandomBlock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                        }
                    }
                }
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
            if (e.KeyCode == Keys.Up)
            {
                ActionUp();
            }
            if (e.KeyCode == Keys.Down)
            {
                ActionDown();
            }
            if (e.KeyCode == Keys.Right)
            {
                ActionRight();
            }
            if (e.KeyCode == Keys.Left)
            {
                ActionLeft();
            }
            RandomBlock();
            Refresh();
        }

        private void ActionRight()
        {
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
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x1, y])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x1, y] = 0;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void ActionLeft()
        {
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
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x1, y])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x1, y] = 0;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void ActionDown()
        {
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
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x, y1])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x, y1] = 0;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void ActionUp()
        {
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
                            }
                            else if (matrixNumber[x, y] == matrixNumber[x, y1])
                            {
                                matrixNumber[x, y] *= 2;
                                matrixNumber[x, y1] = 0;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
