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
    public partial class Binarization : Form
    {
        Bitmap originalImage;   //原始圖像
        public Binarization()
        {
            InitializeComponent();
        }

        private void Binarization_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\gray.png");
            // Zoom -> 維持原始外觀比例，對影像延展或縮小以符合 PictureBox
            // 調整圖片尺寸至符合pictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            // 在 pictureBox1 顯示原始圖像
            pictureBox1.Image = originalImage;
        }

        // 捲動閾值
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // 同步顯示閾值
            label1.Text = "閾值：" + trackBar1.Value.ToString(); 

            Gray_to_Binarization();
            //Gray_to_Binarization_by_Gray(); // 先轉灰階
        }

        // 二值化 -- by 原圖
        private void Gray_to_Binarization()
        {
            // 初始化二值化圖像為原始圖像的副本
            Bitmap Binarization_Image = new Bitmap(originalImage);

            // 對每個像素做二值化
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    // 取得原圖像素色彩
                    Color O_color = originalImage.GetPixel(x, y);

                    // 灰階圖 RGB 會相同
                    // 二值化
                    if (Binarization_Image.GetPixel(x,y).R > trackBar1.Value)
                    {
                        // 大於閾值 -> 白
                        Binarization_Image.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        // 小於閾值 -> 黑
                        Binarization_Image.SetPixel(x, y, Color.FromArgb(0,0,0));
                    }
                }
            }
            // 在 pictureBox 顯示二值化圖像
            pictureBox1.Image = Binarization_Image;
        }

        // 點擊[原圖]
        private void button1_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            label1.Text = "閾值：";

            // 顯示原圖
            pictureBox1.Image = originalImage;
        }




        // --- 其他做法 ---

        // [未使用]
        // 二值化 -- by 灰階
        private void Gray_to_Binarization_by_Gray()
        {
            // 初始化灰階圖像為原始圖像的副本
            Bitmap Gray_Image = new Bitmap(originalImage);
            // 轉換灰階圖像
            RGB_to_Gray(Gray_Image);

            // 初始化二值化圖像為灰階圖像的副本
            Bitmap Binarization_Image = new Bitmap(Gray_Image);

            // 對每個像素做二值化
            for (int x = 0; x < Gray_Image.Width; x++)
            {
                for (int y = 0; y < Gray_Image.Height; y++)
                {
                    // 取得原圖像素色彩
                    Color O_color = Gray_Image.GetPixel(x, y);

                    // 灰階圖 RGB 會相同
                    // 二值化
                    if (Binarization_Image.GetPixel(x, y).R > trackBar1.Value)
                    {
                        // 大於閾值 -> 白
                        Binarization_Image.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        // 小於閾值 -> 黑
                        Binarization_Image.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            // 在 pictureBox 顯示二值化圖像
            pictureBox1.Image = Binarization_Image;
        }

        // [未使用]
        // 原圖轉為灰階
        private void RGB_to_Gray(Bitmap Gray_Image)
        {
            // 初始化灰階圖像為原始圖像的副本
            Gray_Image = new Bitmap(originalImage);

            // 把每個像素轉為灰階
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    // 取得原圖像素色彩
                    Color O_color = originalImage.GetPixel(x, y);

                    // 計算灰階值 -- 加權平均法 根據人眼對紅、綠、藍三種顏色的敏感度不同
                    int Gray_value = (int)(O_color.R * 0.3 + O_color.G * 0.59 + O_color.B * 0.11);
                    //int Gray_value = (int)(O_color.R + O_color.G + O_color.B)/3;  //平均法

                    // 用指定的 8 位元色彩值 (紅、綠和藍) 建立 Color 結構
                    // 建立灰階色彩
                    Color Gray_color = Color.FromArgb(Gray_value, Gray_value, Gray_value);

                    // 將像素色彩設定為灰階色彩
                    Gray_Image.SetPixel(x, y, Gray_color);
                }
            }
        }
    }
}
