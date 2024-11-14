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
    public partial class ChannelConversion : Form
    {
        Bitmap originalImage;   //原始圖像
        public ChannelConversion()
        {
            InitializeComponent();
        }

        private void ChannelConversion_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\lena_RGB.png");
            // Zoom -> 維持原始外觀比例，對影像延展或縮小以符合 PictureBox
            // 調整圖片尺寸至符合pictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //在 pictureBox1 顯示原始圖像
            pictureBox1.Image = originalImage;
        }

        // 點擊[通道轉換]
        private void button1_Click(object sender, EventArgs e)
        {
            RGB_to_Channel();
        }

        // 原圖轉為通道色彩
        private void RGB_to_Channel()
        {
            // 初始化通道圖像為原始圖像的副本
            Bitmap R_Image = new Bitmap(originalImage);
            Bitmap G_Image = new Bitmap(originalImage);
            Bitmap B_Image = new Bitmap(originalImage);

            // 調整圖片尺寸至符合pictureBox
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;

            // 把每個像素轉為通道色彩
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    // 取得原圖像素色彩
                    Color O_color = originalImage.GetPixel(x, y);

                    // 取得通道色彩值
                    int R_value = (int)O_color.R;
                    int G_value = (int)O_color.G;
                    int B_value = (int)O_color.B;

                    // 用指定的 8 位元色彩值 (紅、綠和藍) 建立 Color 結構
                    // 建立通道色彩
                    Color R_color = Color.FromArgb(R_value, R_value, R_value);
                    Color G_color = Color.FromArgb(G_value, G_value, G_value);
                    Color B_color = Color.FromArgb(B_value, B_value, B_value);

                    // 將像素色彩設定為通道色彩
                    R_Image.SetPixel(x, y, R_color);
                    G_Image.SetPixel(x, y, G_color);
                    B_Image.SetPixel(x, y, B_color);
                }
            }
            // 在 pictureBox 顯示通道圖像
            pictureBox2.Image = R_Image;
            pictureBox3.Image = G_Image;
            pictureBox4.Image = B_Image;
        }
    }
}
