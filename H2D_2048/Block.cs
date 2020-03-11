using System.Drawing;
using System.Windows.Forms;

namespace H2D_2048
{
    public class Block
    {
        public Label Blank = new Label
        {
            Size = new Size(80, 80),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = false,
            Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Text = "",
            BackColor = Color.WhiteSmoke
        };

        public void SetToBlank(Label label)
        {
            label.Text = "";
            label.BackColor = Color.WhiteSmoke;
        }

        public void SetTo2(Label label)
        {
            label.Text = "2";
            label.BackColor = Color.LightPink;
        }

        public void SetTo4(Label label)
        {
            label.Text = "4";
            label.BackColor = Color.Salmon;
        }

        public void SetTo8(Label label)
        {
            label.Text = "8";
            label.BackColor = Color.Bisque;
        }

        public void SetTo16(Label label)
        {
            label.Text = "16";
            label.BackColor = Color.FromArgb(255, 192, 128);
        }

        public void SetTo32(Label label)
        {
            label.Text = "32";
            label.BackColor = Color.FromArgb(255, 255, 192);
        }

        public void SetTo64(Label label)
        {
            label.Text = "64";
            label.BackColor = Color.FromArgb(255, 255, 128);
        }

        public void SetTo128(Label label)
        {
            label.Text = "128";
            label.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void SetTo256(Label label)
        {
            label.Text = "256";
            label.BackColor = Color.FromArgb(0, 192, 0);
        }

        public void SetTo512(Label label)
        {
            label.Text = "512";
            label.BackColor = Color.FromArgb(192, 192, 255);
        }

        public void SetTo1024(Label label)
        {
            label.Text = "1024";
            label.BackColor = Color.FromArgb(128, 128, 255);
        }

        public void SetTo2048(Label label)
        {
            label.Text = "2048";
            label.BackColor = Color.FromArgb(255, 128, 255);
        }

        public void SetTo4096(Label label)
        {
            label.Text = "4096";
            label.BackColor = Color.FromArgb(192, 192, 0);
        }

        public void SetTo8192(Label label)
        {
            label.Text = "8192";
            label.BackColor = Color.GreenYellow;
        }
    }
}
