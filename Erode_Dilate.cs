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
    public partial class Erode_Dilate : Form
    {
        Bitmap originalImage;   //原始圖像

        public Erode_Dilate()
        {
            InitializeComponent();
        }

        private void Erosion_Expansion_Load(object sender, EventArgs e)
        {
            // Bitmap -> 用來處理像素資料所定義影像的物件
            // 取得原圖 -- 從指定檔案，初始化 Bitmap 類別的新執行個體
            originalImage = new Bitmap("C:\\Users\\11031\\OneDrive\\Desktop\\1131\\影像處理\\期中Project\\影像處理_期中Project\\影像處理_期中Project\\Input image files\\No_EG_DG.png");
            // Zoom -> 維持原始外觀比例，對影像延展或縮小以符合 PictureBox
            // 調整圖片尺寸至符合pictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            // 在 pictureBox1 顯示原始圖像
            pictureBox1.Image = originalImage;
        }

        // 點擊[侵蝕]
        private void button1_Click(object sender, EventArgs e)
        {
            Erode();
        }

        // 侵蝕
        private void Erode()
        {
            Bitmap Eroding_Image;
            // 初始化侵蝕圖像為原始圖像的副本
            Bitmap Erode_Image = new Bitmap(originalImage);

            // 以 numericUpDown 的值決定侵蝕次數
            for (int n = 0; n < numericUpDown1.Value; n++)
            {
                // 初始化侵蝕圖像為最新侵蝕後的版本
                Eroding_Image = new Bitmap(Erode_Image);

                // 寬、高的 第一個 ~ 倒數第三個
                for (int x = 0; x < originalImage.Width - 2; x++)
                {
                    for (int y = 0; y < originalImage.Height - 2; y++)
                    {
                        // 不全白 -> 把中心點設為黑
                        if (Check_Erode_33mask(Erode_Image, x, y) == true)
                        {
                            Eroding_Image.SetPixel(x + 1, y + 1, Color.FromArgb(0, 0, 0));
                        }
                    }
                }
                // 初始化侵蝕圖像為最新侵蝕後的版本
                Erode_Image = new Bitmap(Eroding_Image);
            }
            // 顯示最終侵蝕圖像
            pictureBox2.Image = Erode_Image;
        }

        // 檢視 侵蝕的 3x3 遮罩內的值
        private Boolean Check_Erode_33mask(Bitmap Erode_Image, int x,int y )
        {
            // 檢視 3x3 遮罩內的值
            for (int i = x; i <= x + 2; i++)
            {
                for (int j = y; j <= y + 2; j++)
                {
                    // 一個是黑點 ( 不全白 )
                    if (Erode_Image.GetPixel(i, j).R == 0) //黑
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // 點擊[擴張]
        private void button2_Click(object sender, EventArgs e)
        {
            Dilate();
        }

        // 擴張
        private void Dilate()
        {
            
            Bitmap Dilating_Image;
            // 初始化擴張圖像為原始圖像的副本
            Bitmap Dilate_Image = new Bitmap(originalImage);

            // 以 numericUpDown 的值決定擴張次數
            for (int n = 0; n < numericUpDown2.Value; n++)
            {
                // 初始化擴張圖像為最新擴張後的版本
                Dilating_Image = new Bitmap(Dilate_Image);

                // 寬、高的 第一個 ~ 倒數第三個
                for (int x = 0; x < originalImage.Width - 2; x++)
                {
                    for (int y = 0; y < originalImage.Height - 2; y++)
                    {
                        // 不全黑 -> 把中心點設為白
                        if (Check_Dilate_33mask(Dilate_Image, x, y) == true)
                        {
                            Dilating_Image.SetPixel(x + 1, y + 1, Color.FromArgb(255, 255, 255));
                        }
                    }
                }
                // 初始化擴張圖像為最新擴張後的版本
                Dilate_Image = new Bitmap(Dilating_Image);
            }
            // 顯示最終擴張圖像
            pictureBox3.Image = Dilate_Image;
        }

        // 檢視 擴張的 3x3 遮罩內的值
        private Boolean Check_Dilate_33mask(Bitmap Erode_Image, int x, int y)
        {
            // 檢視 3x3 遮罩內的值
            for (int i = x; i <= x + 2; i++)
            {
                for (int j = y; j <= y + 2; j++)
                {
                    // 一個是白點 ( 不全黑 )
                    if (Erode_Image.GetPixel(i, j).R == 255) //白
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
