using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Sobel
{
    public partial class MainForm : Form
    {
        private Bitmap selected;
        public MainForm()
        {
            InitializeComponent();


        }

        private void loadImg_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                selected = (Bitmap)pictureBox1.Image.Clone();
                selected = ConvertToNonIndexed(selected, PixelFormat.Format24bppRgb);
            }
        }
        private Bitmap ConvertToNonIndexed(Image img, PixelFormat fmt)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height, fmt);
            Graphics gr = Graphics.FromImage(bmp);
            gr.DrawImage(img, 0, 0);
            gr.Dispose();
            return bmp;
        }
        private void RunSobel_Click(object sender, EventArgs e)
        {
            Sobel sobel = new Sobel(selected);
            sobel.Run();
            pictureBox2.Image = sobel.output;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
