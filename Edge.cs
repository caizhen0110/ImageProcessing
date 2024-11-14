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
    public partial class Edge : Form
    {
        Bitmap originalImage;   //原始圖像

        public Edge()
        {
            InitializeComponent();
        }

        private void Edge_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\gray.png");
            // Zoom -> 維持原始外觀比例，對影像延展或縮小以符合 PictureBox
            // 調整圖片尺寸至符合pictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            // 在 pictureBox1 顯示原始圖像
            pictureBox1.Image = originalImage;
        }

        // 點擊[Edge]
        private void button1_Click(object sender, EventArgs e)
        {
            Gray_to_Edge();
        }

        // Edge
        private void Gray_to_Edge()
        {
            // 初始化邊緣圖像為原始圖像的副本
            Bitmap Edge_Image = new Bitmap(originalImage);

            // 水平梯度 -- 索貝爾運算子
            int[] GX = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
            // 垂直梯度 -- 索貝爾運算子
            int[] GY = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };

            for(int x=1;x<originalImage.Width-1; x++)
            {
                for(int y=1;y< originalImage.Height-1; y++) {
                    
                    // GX 的相應位置相乘累加值
                    int SX = 0;
                    // GY 的相應位置相乘累加值
                    int SY = 0;

                    int Edge_Index = 0;

                    // 在 3X3 遮罩內用 索貝爾運算子 計算梯度值
                    for (int i=x-1;i<=x+1;i++)
                    {
                        for(int j=y-1;j<=y+1;j++)
                        {
                            SX += originalImage.GetPixel(i, j).R * GX[Edge_Index];
                            SY += originalImage.GetPixel(i, j).R * GY[Edge_Index];
                            Edge_Index++;
                        }
                    }

                    // 計算邊緣強度
                    int Edge_Strength = (int)Math.Pow(SX * SX + SY * SY, 0.5);

                    // 處理顏色溢出
                    if (Edge_Strength > 255) { Edge_Strength = 255; }
                    else if (Edge_Strength < 0) { Edge_Strength = 0; }

                    // 應用閾值，增強邊緣對比度
                    if (Edge_Strength > numericUpDown1.Value) 
                    {
                        Edge_Strength = 0; // 設為黑色
                    }
                    else
                    {
                        Edge_Strength = 255; // 設為白色
                    }

                    // 以邊緣強度的建立邊緣圖象
                    Edge_Image.SetPixel(x, y, Color.FromArgb(Edge_Strength, Edge_Strength, Edge_Strength));
                }
            }
            //顯示邊緣圖像
            pictureBox2.Image = Edge_Image;
        }
    }
}
