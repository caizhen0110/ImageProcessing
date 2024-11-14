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
    public partial class Denoise : Form
    {
        Bitmap originalImage;   //原始圖像
        Bitmap Gray_Image;

        public Denoise()
        {
            InitializeComponent();
        }

        private void Denoise_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\circuit board_pepper.png");
            // Zoom -> 維持原始外觀比例，對影像延展或縮小以符合 PictureBox
            // 調整圖片尺寸至符合pictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            // 在 pictureBox1 顯示原始圖像
            pictureBox1.Image = originalImage;
        }

        // 點擊[去雜訊]
        private void button1_Click(object sender, EventArgs e)
        {
            //NoisePepper_to_Denoise(); // 灰階+中值濾波
            NoisePepper_to_Denoise_by_3Channel();   //三個通道+中值濾波
        }

        // 去雜訊 -- 三個通道
        private void NoisePepper_to_Denoise_by_3Channel()
        {
            // 初始化去雜訊圖像為原始圖像的副本
            Bitmap Denoise_Image = new Bitmap(originalImage);

            // 調整圖片尺寸至符合pictureBox
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            // 寬、高的 第一個 ~ 倒數第三個
            for (int x = 0; x < originalImage.Width - 2; x++)
            {
                for (int y = 0; y < originalImage.Height - 2; y++)
                {
                    // 中值
                    int[] Middle_Pixel = MedianFilter_33_by_3Channel(Denoise_Image, x, y);
                    Denoise_Image.SetPixel(x + 1, y + 1, Color.FromArgb(Middle_Pixel[0], Middle_Pixel[1], Middle_Pixel[2]));
                }
            }

            // 顯示去雜訊圖像
            pictureBox2.Image = Denoise_Image;
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








        // --- 其他做法 ---


        // [未使用]
        // 去雜訊 -- 先轉灰階
        private void NoisePepper_to_Denoise()
        {
            //轉灰階 -- 可降低計算複雜度&去除彩色雜訊引入的色偏
            RGB_to_Gray();
            // 初始化去雜訊圖像為灰階圖像的副本
            Bitmap Denoise_Image = new Bitmap(Gray_Image);

            // 調整圖片尺寸至符合pictureBox
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            // 寬、高的 第一個 ~ 倒數第三個
            for (int x = 0; x < originalImage.Width - 2; x++)
            {
                for (int y = 0; y < originalImage.Height - 2; y++)
                {
                    // 取得中值 -- 中值濾波器
                    int Middle_Pixel = MedianFilter_33(x, y);
                    // 將中心點設為中值
                    Denoise_Image.SetPixel(x + 1, y + 1, Color.FromArgb(Middle_Pixel, Middle_Pixel, Middle_Pixel));
                }
            }

            // 顯示去雜訊圖像
            pictureBox2.Image = Denoise_Image;
        }

        // [未使用]
        // 3x3中值濾波器
        private int MedianFilter_33(int x, int y)
        {
            // 宣告一個陣列，紀錄 3x3 遮罩中的每個像素值
            int[] MaskPixel = new int[9];

            // 3x3 遮罩
            for (int i = x, n = 0; i <= x + 2; i++)
            {
                for (int j = y; j <= y + 2; j++)
                {
                    //紀錄像素值
                    MaskPixel[n] = Gray_Image.GetPixel(i, j).R;
                    n++;
                }
            }

            // MaskPixel 排序 -- 氣泡排序法 (Bubble Sort)
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

            // 回傳中值 -- 第5個
            return MaskPixel[4];
        }

        // [未使用]
        // 3x3中值加權濾波器
        private int MedianWeightFilter_33(int x, int y)
        {
            // 宣告一個陣列，紀錄 3x3 遮罩中的每個像素值
            int[] MaskPixel = new int[9 + 4];

            // 3x3 遮罩
            for (int i = x, n = 0; i <= x + 2; i++)
            {
                for (int j = y; j <= y + 2; j++)
                {
                    // 取得像素值
                    int pixel = Gray_Image.GetPixel(i, j).R;
                    // 該像素值數量+1
                    //Pixel_Matrix[pixel]++;

                    //紀錄像素值
                    MaskPixel[n] = pixel;
                    n++;
                }
            }

            // 多複製4次中心像素值
            for (int i = MaskPixel.Length - 4 - 1; i < MaskPixel.Length; i++)
            {
                MaskPixel[i] = Gray_Image.GetPixel(x + 1, y + 1).R;
            }

            // MaskPixel 排序 -- 氣泡排序法 (Bubble Sort)
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

            // 回傳中值 -- 第7個
            return MaskPixel[6];
        }

        // [未使用]
        // 原圖轉為灰階
        private void RGB_to_Gray()
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
