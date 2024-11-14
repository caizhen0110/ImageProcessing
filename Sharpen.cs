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
    public partial class Sharpen : Form
    {
        Bitmap originalImage;   //原始圖像

        public Sharpen()
        {
            InitializeComponent();
        }

        private void Sharpen_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\No_sharpen.png");
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
            NoSharpen_to_Sharpen();
        }

        //銳化
        private void NoSharpen_to_Sharpen()
        {
            // 初始化銳化圖像為原始圖像的副本
            Bitmap Sharpen_Image=new Bitmap(originalImage);

            // 拉普拉斯模板
            int[] Laplace = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            int Laplace_Index = 0;

            // 對中間區塊每個像素進行銳化
            for (int x = 1; x < originalImage.Width - 1; x++)
            {
                for (int y = 1; y < originalImage.Height - 1; y++)
                {
                    // 重設Laplace_Index
                    Laplace_Index = 0;

                    // 宣告每個通道的像素值
                    int Sharpen_R = 0;
                    int Sharpen_G = 0;
                    int Sharpen_B = 0;

                    // 使用拉普拉斯模板的 3x3 區域
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            // 累加每個通道的加權值
                            Sharpen_R += originalImage.GetPixel(x + i, y + j).R * Laplace[Laplace_Index];
                            Sharpen_G += originalImage.GetPixel(x + i, y + j).G * Laplace[Laplace_Index];
                            Sharpen_B += originalImage.GetPixel(x + i, y + j).B * Laplace[Laplace_Index];
                            Laplace_Index++;
                        }
                    }

                    // 處理顏色溢出
                    if (Sharpen_R > 255) { Sharpen_R = 255; }
                    else if (Sharpen_R < 0) { Sharpen_R = 0; }
                    if (Sharpen_G > 255) { Sharpen_G = 255; }
                    else if (Sharpen_G < 0) { Sharpen_G = 0; }
                    if (Sharpen_B > 255) { Sharpen_B = 255; }
                    else if (Sharpen_B < 0) { Sharpen_B = 0; }

                    // 將每個通道的像素設為銳化的對應像素
                    Sharpen_Image.SetPixel(x, y, Color.FromArgb(Sharpen_R, Sharpen_G, Sharpen_B));
                }
            }
            // 顯示銳化圖象
            pictureBox2.Image = Sharpen_Image;
        }
    }
}
