using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ColorFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string dosyaYolu = string.Empty;
        Bitmap bmp = null;

        bool IsChoosenPixel(Color color, int tolerance) => 
            color.R < button3.BackColor.R + tolerance && color.R > button3.BackColor.R - tolerance &&
            color.G < button3.BackColor.G + tolerance && color.G > button3.BackColor.G - tolerance &&
            color.B < button3.BackColor.B + tolerance && color.B > button3.BackColor.B - tolerance;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dosyaYolu = openFileDialog1.FileName;

                //bir bitmap nesnesi oluşturulur ve seçilen resim bu nesneye yüklenir.  

                bmp = new Bitmap(dosyaYolu);

                pictureBox1.Image = bmp;

                int width = bmp.Width / 700;
                int height = bmp.Height / 380;


                if (width > 1 || height >1)
                {
                    if (width < height)
                    {
                        pictureBox1.Width = bmp.Width /height;
                        pictureBox1.Height = bmp.Height /height;
                    }
                    else if (width > height)
                    {
                        pictureBox1.Width = bmp.Width /width;
                        pictureBox1.Height = bmp.Height /width;
                    }
                }

                //picturebox nesnesinin sizemode özelliği strech olarak ayarlanır.Bunun
                //sebebi ise seçilen resmin picturebox nesnesinde tam olarak gözükmesini sağlamaktır.
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < bmp.Height; y++) //Pixelleri boyuna olarak tarar.
            {
                for (int x = 0; x < bmp.Width; x++)//Pixelleri yatay olarak tarar.
                {
                    Color eski = bmp.GetPixel(x, y); //Sıradaki pixeli alır.
                    if(!IsChoosenPixel(eski, Convert.ToInt32(numericUpDown1.Value)))
                    {
                    int siyah = (eski.R + eski.G + eski.B) / 255; //ele alınan pixelin RGB kodlarını siayaha sıfırlar.

                    Color yeni = Color.FromArgb(eski.A, siyah, siyah, siyah);//Bulunan ortalamanın RGB olarak renk değerini alır.

                    bmp.SetPixel(x, y, yeni);//Pixele yeni RGB kodlarını atar ve pixeli eski yerine koyar.

                    }
                }
            }

            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDialog1.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }
    }
}
