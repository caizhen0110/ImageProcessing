using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 影像處理_期中Project
{
    public partial class Histogram : Form
    {
        Bitmap originalImage;   //原始圖像

        public Histogram()
        {
            InitializeComponent();
        }

        private void Histogram_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\before_histogram.png");
            // Zoom -> 維持原始外觀比例，對影像延展或縮小以符合 PictureBox
            // 調整圖片尺寸至符合pictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            // 在 pictureBox1 顯示原始圖像
            pictureBox1.Image = originalImage;
        }

        // 點擊[銳化]
        private void button1_Click(object sender, EventArgs e)
        {
            To_Histigram();
        }

        //直方圖等化
        private void To_Histigram()
        {
            // 初始化直方圖圖像為原始圖像的副本
            Bitmap Histigram_Image = new Bitmap(originalImage);

            // 宣告一陣列，儲存 R.G.B 中的像素出現次數
            int[,] Pixel_Num = new int[3, 256];

            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    // 紀錄對應的像素出現次數
                    Pixel_Num[0, originalImage.GetPixel(x, y).R]++;
                    Pixel_Num[1, originalImage.GetPixel(x, y).G]++;
                    Pixel_Num[2, originalImage.GetPixel(x, y).B]++;
                }
            }

            // 累積亮度
            int[,] cdf = new int[3, 256];
            for (int i = 0; i < 256; i++)
            {
                if (i == 0)
                {
                    cdf[0, i] = Pixel_Num[0, i];
                    cdf[1, i] = Pixel_Num[1, i];
                    cdf[2, i] = Pixel_Num[2, i];
                }
                else
                {
                    cdf[0, i] = cdf[0, i-1] + Pixel_Num[0, i];
                    cdf[1, i] = cdf[1, i - 1] + Pixel_Num[1, i];
                    cdf[2, i] = cdf[2, i - 1] + Pixel_Num[2, i];
                }

            }

            // // 宣告一陣列，儲存直方圖色彩
            int[,] Histigram_value = new int[3, 256];

            // 計算直方圖色彩 -- (cdf[0, i] * 255) / totalPixels
            // CDF 值映射到 0 - 255 的範圍內，這樣可以得到新的灰階值，使亮度分佈更均勻
            for (int i = 0; i < 256; i++)
            {
                Histigram_value[0, i] = (cdf[0, i] * 255) / (originalImage.Width * originalImage.Height);   // R
                Histigram_value[1, i] = (cdf[1, i] * 255) / (originalImage.Width * originalImage.Height);   // G
                Histigram_value[2, i] = (cdf[2, i] * 255) / (originalImage.Width * originalImage.Height);   // B
            }

            // 將直方圖色彩設定給區域內所有像素
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    int FromArgb_R = Histigram_value[0, originalImage.GetPixel(x, y).R];
                    int FromArgb_G = Histigram_value[1, originalImage.GetPixel(x, y).G];
                    int FromArgb_B = Histigram_value[2, originalImage.GetPixel(x, y).B];

                    Histigram_Image.SetPixel(x, y, Color.FromArgb(FromArgb_R, FromArgb_G, FromArgb_B));
                }
            }

            //顯示直方圖圖像
            pictureBox2.Image = Histigram_Image;
        }
    }
}
