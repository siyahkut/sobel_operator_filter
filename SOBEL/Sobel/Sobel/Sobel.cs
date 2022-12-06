using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Sobel
{
    class Sobel
    {
        Bitmap grayBitmap;
        public Bitmap output;
        public Sobel(Bitmap input)
        {
           this.grayBitmap = ConvertToGray(input);
        }

        private Bitmap ConvertToGray(Bitmap input)
        {
            Bitmap bt = (Bitmap)input.Clone();
            for (int y = 0; y < bt.Height; y++)
            {
                for (int x = 0; x < bt.Width; x++)
                {
                    Color c = bt.GetPixel(x, y);

                    var avg = getColorAvgValue(c);



                    bt.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            return bt;
        }
        private int getColorAvgValue(Color c)
        {
            int r = c.R;
            int g = c.G;
            int b = c.B;
            int result = (r + g + b) / 3;
            //aslında bu grileştirme işlemini insan gözü için yapsaydık R .3, G .59, B  .11 oranları daha anlamlı olabilirdi; tercihen istenilen yöntem kullanılabilir
            // result = (int) (0.3 * r + 0.59 * g + 0.11 * b);

            result = result >= 255 ? 255 : result <= 0 ? 0 : result;

            return result;
        }

        private Bitmap ApplySobel()
        {
            Bitmap b = this.grayBitmap;
            Bitmap output = (Bitmap)this.grayBitmap.Clone();
            int width = b.Width;
            int height = b.Height;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            int[,] pixelArray = new int[width, height];         

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixelArray[i, j] = getColorAvgValue(b.GetPixel(i, j));
                }
            }  
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {

                    int resultPixel = 0;
                    int pixelValue;
                    int totalGx = 0;
                    int totalGy = 0;
                    for (int wi = -1; wi <= 1; wi++)
                    {
                        for (int hw = -1; hw <= 1; hw++)
                        {

                            pixelValue = pixelArray[i + wi, j + hw];
                            totalGx += gx[wi + 1, hw + 1] * pixelValue;
                            totalGy += gy[wi + 1, hw + 1] * pixelValue;

                        }
                    }
                    resultPixel = Convert.ToInt32(Math.Sqrt(totalGx * totalGx + totalGy * totalGy)) ; 
                    resultPixel = resultPixel > 255 ? 255 : resultPixel;

                    output.SetPixel(i, j, Color.FromArgb(resultPixel, resultPixel, resultPixel));
                   
                }
            }
            return output;

        }


        public void Run()
        {
            this.output = this.ApplySobel();
        }

    }
}

