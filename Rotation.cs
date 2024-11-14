using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace 影像處理_期中Project
{
    public partial class Rotation : Form
    {
        Bitmap originalImage;   //原始圖像

        public Rotation()
        {
            InitializeComponent();
        }

        private void Rotation_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\lena_RGB.png");
            // Zoom -> 維持原始外觀比例，對影像延展或縮小以符合 PictureBox
            // 調整圖片尺寸至符合pictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            // 在 pictureBox1 顯示原始圖像
            pictureBox1.Image = originalImage;
        }

        // 捲動Rotate
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // 同步顯示閾值
            label1.Text = "Rotate：" + trackBar1.Value.ToString();
            
            // 從輸入框讀取角度
            int angle = trackBar1.Value; 
            RotateImage(angle);
        }

        // 旋轉
        private void RotateImage(float angle)
        {
            // 初始化旋轉圖像為原始圖像的副本
            Bitmap Rotation_Image = new Bitmap(originalImage);

            // 紀錄旋轉圖像的寬、高
            int width = Rotation_Image.Width;
            int height = Rotation_Image.Height;

            // 計算弧度
            double radians = angle * Math.PI / 180;
            // 正弦 -- 用於座標變換
            double cos = Math.Cos(radians);
            // 餘弦 -- 用於座標變換
            double sin = Math.Sin(radians);

            // 計算旋轉後寬、高
            int newWidth = (int)(Math.Abs(width * cos) + Math.Abs(height * sin));
            int newHeight = (int)(Math.Abs(width * sin) + Math.Abs(height * cos));

            Bitmap New_Rotation_Image = new Bitmap(newWidth, newHeight);

            // 計算原始圖片的中心點
            int x0 = width / 2;
            int y0 = height / 2;

            // 計算旋轉後新圖片的中心點
            int x1 = newWidth / 2;
            int y1 = newHeight / 2;

            // 計算原圖在範圍內每個像素旋轉後的新位置
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // 計算旋轉後的新座標
                    int newX = (int)((x - x0) * cos - (y - y0) * sin) + x1;
                    int newY = (int)((x - x0) * sin + (y - y0) * cos) + y1;

                    // 檢查新座標是在新圖像的範圍內，避免越界
                    if (newX >= 0 && newX < newWidth && newY >= 0 && newY < newHeight)
                    {
                        // 將原圖像素設置到新座標位置
                        New_Rotation_Image.SetPixel(newX, newY, Rotation_Image.GetPixel(x, y));
                    }
                }
            }

            // 處理雜訊
            // 寬、高的 第一個 ~ 倒數第三個
            for (int x = 0; x < New_Rotation_Image.Width - 2; x++)
            {
                for (int y = 0; y < New_Rotation_Image.Height - 2; y++)
                {
                    // 中值濾波
                    int[] Middle_Pixel = MedianFilter_33_by_3Channel(New_Rotation_Image, x, y);
                    New_Rotation_Image.SetPixel(x + 1, y + 1, Color.FromArgb(Middle_Pixel[0], Middle_Pixel[1], Middle_Pixel[2]));
                }
            }

            // 顯示旋轉後的圖片
            pictureBox1.Image = New_Rotation_Image;
        }

        // 點擊[原圖]
        private void button1_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            label1.Text = "Rotate：";

            // 顯示原圖
            pictureBox1.Image = originalImage;
        }

        // 3x3中值濾波器 -- 三個通道
        private int[] MedianFilter_33_by_3Channel(Bitmap Denoise_Image, int x, int y)
        {
            // 宣告 3 個通道的陣列，紀錄 3x3 遮罩中的每個像素值
            int[] MaskPixel_R = new int[9];
            int[] MaskPixel_G = new int[9];
            int[] MaskPixel_B = new int[9];
            // 宣告一個陣列，紀錄 3 個通道的中值
            int[] Median = new int[3];

            // 3x3 遮罩
            for (int i = x, n = 0; i <= x + 2; i++)
            {
                for (int j = y; j <= y + 2; j++)
                {
                    //紀錄像素值
                    MaskPixel_R[n] = Denoise_Image.GetPixel(i, j).R;
                    MaskPixel_G[n] = Denoise_Image.GetPixel(i, j).G;
                    MaskPixel_B[n] = Denoise_Image.GetPixel(i, j).B;
                    n++;
                }
            }

            // 分別對三個通道排序
            BubbleSort(MaskPixel_R);
            BubbleSort(MaskPixel_G);
            BubbleSort(MaskPixel_B);

            // 分別紀錄中值 -- 第5個
            Median[0] = MaskPixel_R[4];
            Median[1] = MaskPixel_G[4];
            Median[2] = MaskPixel_B[4];

            // 回傳中值
            return Median;
        }

        // 氣泡排序法
        private void BubbleSort(int[] MaskPixel)
        {
            // MaskPixel 排序
            for (int i = MaskPixel.Length - 1; i > 0; i--)
            {
                for (int j = 0, box = 0; j < i; j++)
                {
                    // 前者較大
                    if (MaskPixel[j] > MaskPixel[j + 1])
                    {
                        // 交換
                        box = MaskPixel[j];
                        MaskPixel[j] = MaskPixel[j + 1];
                        MaskPixel[j + 1] = box;
                    }
                }
            }
        }
    }
}
