using System;
using System.Drawing;
using System.Windows.Forms;


namespace 影像處理_期中Project
{

    public partial class GrayscaleConversion : Form
    {
        Bitmap originalImage;   //原始圖像

        public GrayscaleConversion()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        // 點擊[灰階轉換]
        private void button1_Click(object sender, EventArgs e)
        {
            RGB_to_Gray();
        }

        // 原圖轉為灰階
        private void RGB_to_Gray()
        {
            // 初始化灰階圖像為原始圖像的副本
            Bitmap Gray_Image = new Bitmap(originalImage);

            // 調整圖片尺寸至符合pictureBox
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            // 把每個像素轉為灰階
            for (int x = 0; x < originalImage.Width; x++)
            {
                for(int y = 0; y < originalImage.Height; y++)
                {
                    // 取得原圖像素色彩
                    Color O_color = originalImage.GetPixel(x, y);

                    // 計算灰階值 -- 加權平均法 根據人眼對紅、綠、藍三種顏色的敏感度不同
                    int Gray_value = (int)(O_color.R * 0.3 + O_color.G * 0.59 + O_color.B * 0.11);
                    //int Gray_value = (int)(O_color.R + O_color.G + O_color.B)/3;  //平均法

                    // 用指定的 8 位元色彩值 (紅、綠和藍) 建立 Color 結構
                    // 建立灰階色彩
                    Color Gray_color = Color.FromArgb(Gray_value,Gray_value,Gray_value);

                    // 將像素色彩設定為灰階色彩
                    Gray_Image.SetPixel(x, y, Gray_color);
                }
            }
            // 在 pictureBox2 顯示灰階圖像
            pictureBox2.Image = Gray_Image;
        }
    }
}
