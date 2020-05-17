using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ShadySideCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Замер времени
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            //Фоновый цвет
            Color bk = Color.FromArgb(255, 0, 0, 0);

            //Цвет заливки
            Color cl = Color.FromArgb(255, 0, 0);

            //Процент alpha
            double percent;

            //Процент alpha в 256
            double a;

            //Конечное значение alpha
            int alpha;

            //Строка для выгрузки в файл
            string str = "\'{\"255,0,0\":[";

            //Создаём Bitmap для работы с pixel
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);

            //Цикл alpha
            for (double i = 1.00; i < 100.0; i++)
            {
                percent = i;
                a = 255 * percent / 100;
                if (i == 30.0 || i == 70.0)
                {
                    alpha = Convert.ToInt32(Math.Ceiling(a));
                }
                else
                {
                    alpha = Convert.ToInt32(Math.Round(a));
                }

                //Заливаем фон
                graphics.Clear(bk);

                //System.ComponentModel.TypeConverter rectConverter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(Color));
                //Color color = (Color)rectConverter.ConvertFromString("#b310cf46");

                //Создаём кисть и заполняем Rectangle
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, cl));
                graphics.FillRectangle(solidBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);

                //Создаём Bitmap для работы с pixel
                Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                pictureBox1.DrawToBitmap(bmp, new Rectangle(pictureBox1.ClientRectangle.Location, pictureBox1.ClientSize));

                str += $"\"{bmp.GetPixel(pictureBox1.Width / 2, 0).R.ToString()},{bmp.GetPixel(pictureBox1.Width / 2, 0).G.ToString()},{bmp.GetPixel(pictureBox1.Width / 2, 0).B.ToString()}\",";
            }

            str = str.Remove(str.Length - 1);

            str += "]}\'";

            string pa = $"0,0,0.txt";

            using (StreamWriter sw = new StreamWriter(pa, false, System.Text.Encoding.Default))
            {
                sw.Write(str);
            }

            //sw.Stop();
            //MessageBox.Show((sw.Elapsed.TotalSeconds ).ToString());
        }
    }
}
