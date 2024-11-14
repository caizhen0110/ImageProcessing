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
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GrayscaleConversion GrayscaleConversion = new GrayscaleConversion();
            GrayscaleConversion.ShowDialog();   // 開啟 GrayscaleConversion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChannelConversion ChannelConversion = new ChannelConversion();
            ChannelConversion.ShowDialog();   // 開啟 ChannelConversion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Binarization Binarization = new Binarization();
            Binarization.ShowDialog();   // 開啟 Binarization
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Erode_Dilate Erode_Dilate = new Erode_Dilate();
            Erode_Dilate.ShowDialog();   // 開啟 Erode_Dilate
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Denoise Denoise = new Denoise();
            Denoise.ShowDialog();   // 開啟 Denoise
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Sharpen Sharpen = new Sharpen();
            Sharpen.ShowDialog();   // 開啟 Sharpen
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Edge Edge = new Edge();
            Edge.ShowDialog();   // 開啟 Edge
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Histogram Histogram = new Histogram();
            Histogram.ShowDialog();   // 開啟 Histogram
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Rotation Rotation = new Rotation();
            Rotation.ShowDialog();   // 開啟 Rotation
        }
    }
}
