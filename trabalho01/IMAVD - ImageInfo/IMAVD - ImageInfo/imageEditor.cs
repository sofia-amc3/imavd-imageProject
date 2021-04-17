using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;

namespace IMAVD___ImageInfo
{
    public partial class imageEditor : Form
    {
        public imageEditor()
        {
            InitializeComponent();
            chckClrBtn.Enabled = false;
        }

        FileInfo OriginalImage_info;
        Bitmap OriginalImage;
        Bitmap CheckColorImage;
        bool IsColorFound;
        int countPixels;


        private void ldBtn_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                OriginalImage_info = new FileInfo(open.FileName);
                OriginalImage = new Bitmap(OriginalImage_info.FullName);
                pictureBox1.Image = OriginalImage;

                imgName.Text = open.SafeFileName;
                imgExt.Text = open.SafeFileName.Split('.')[open.SafeFileName.Split('.').Length-1];
                imgLoc.Text = open.FileName;
                imgDim.Text = OriginalImage.Width + " x " + OriginalImage.Height;
                imgSize.Text = OriginalImage_info.Length + " bytes";
                imgCrtOn.Text = OriginalImage_info.CreationTime.ToString();

                tabControl1.SelectedTab = tabPage2;
            }

            chckClrBtn.Enabled = true;
        }


        private void colorPicked(Color clr)
        {
            CheckColorImage = (Bitmap)OriginalImage.Clone();

            countPixels = 0;

            for (int i = 0; i < pictureBox1.Image.Height; i++)
            {
                for (int j = 0; j < pictureBox1.Image.Width; j++)
                {
                    //Get the color at each pixel
                    Color now_color = OriginalImage.GetPixel(j, i);

                    //Compare Pixel's Color ARGB property with the picked color's ARGB property 
                    if (now_color.ToArgb() == clr.ToArgb())
                    {
                        CheckColorImage.SetPixel(j, i, Color.FromArgb(255, 255, 0, 0));
                        countPixels++;
                    }
                }
            }

            pictureBox2.Image = CheckColorImage;
            MessageBox.Show("There are " + countPixels + " " + clr.Name + " pixels.");
        }

        private void chckClrBtn_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            countPixels = 0;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                colorPicked(colorDlg.Color);
            }
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Color colorPicked_ = new Bitmap(pictureBox1.Image, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height).GetPixel(e.X, e.Y);

                //MessageBox.Show(e.X + "," + e.Y);

                colorPicked(colorPicked_);

                CheckColorImage.SetPixel(e.X, e.Y, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X - 1, e.Y, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X, e.Y - 1, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X + 1, e.Y, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X, e.Y + 1, Color.FromArgb(255, 0, 0, 0));
            }
        }

        private void svImagebtn_Click(object sender, EventArgs e) // -------------------------------------------  MUDAR PARA PICTUREBOX2
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "Images|*.png;*.bmp;*.jpg"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //Bitmap bmp = new Bitmap(pictureBox2.Image, pictureBox2.ClientSize.Width, pictureBox2.ClientSize.Height);
                    //drawImage.DrawToBitmap(bmp, new Rectangle(0, 0, width, height);
                    pictureBox1.Image.Save(dialog.FileName, ImageFormat.Jpeg);
                }
            }
        }
        
        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            if (zoomValue.Value < 50)
            {
                zoomValue.Value = 50;
            }
            if (zoomValue.Value > 500)
            {
                zoomValue.Value = 500;
            }

            Bitmap tempBitmap = new Bitmap(pictureBox1.Image, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);

            // Set the resolution of the bitmap to match the original resolution.

            tempBitmap.SetResolution(OriginalImage.HorizontalResolution,
                                     OriginalImage.VerticalResolution);

            // Create a Graphics object to further edit the temporary bitmap

            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            // First clear the image with the current backcolor

            //bmGraphics.Clear(_BackColor);

            // Set the interpolationmode since we are resizing an image here

            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the original image on the temporary bitmap, resizing it using

            // the calculated values of targetWidth and targetHeight.

            int HorizLeft = (int)(OriginalImage.Width / 2) - ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));
            int VertiTop = (int)(OriginalImage.Height / 2) - ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));


            int HorizRight = (int)(OriginalImage.Width / 2) + ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));
            int VertiBottom = (int)(OriginalImage.Height / 2) + ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));


            bmGraphics.DrawImage(OriginalImage,
                                 new Rectangle(HorizLeft, VertiTop, HorizRight, VertiBottom),
                                 new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                                 GraphicsUnit.Pixel);

            pictureBox2.Refresh();
        }

        private void zoomValue_ValueChanged(object sender, EventArgs e)
        {
            if (zoomValue.Value < 50)
            {
                zoomValue.Value = 50;
            }
            if (zoomValue.Value > 500)
            {
                zoomValue.Value = 500;
            }

            Bitmap tempBitmap = new Bitmap(pictureBox1.Image, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);

            // Set the resolution of the bitmap to match the original resolution.

            tempBitmap.SetResolution(OriginalImage.HorizontalResolution,
                                     OriginalImage.VerticalResolution);

            // Create a Graphics object to further edit the temporary bitmap

            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            // First clear the image with the current backcolor

            

            // Set the interpolationmode since we are resizing an image here

            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draws the original image on the temporary bitmap, resizing it using
            // the calculated values of targetWidth and targetHeight.

            int HorizLeft = (int)(OriginalImage.Width / 2) - (int)((OriginalImage.Width / 2) / (zoomValue.Value / 100));
            int VertiTop = (int)(OriginalImage.Height / 2) - (int)((OriginalImage.Height / 2) / (zoomValue.Value / 100));


            int HorizRight = (int)(OriginalImage.Width / 2) + (int)((OriginalImage.Width / 2) / (zoomValue.Value / 100));
            int VertiBottom = (int)(OriginalImage.Height / 2) + (int)((OriginalImage.Height / 2) / (zoomValue.Value / 100));


            bmGraphics.DrawImage(OriginalImage,
                                 new Rectangle(0, 0, (int)(pictureBox1.ClientSize.Width* (zoomValue.Value / 100)), (int)(pictureBox1.ClientSize.Height * (zoomValue.Value / 100))),
                                 new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                                 GraphicsUnit.Pixel);

            pictureBox2.BackColor = SystemColors.Control;
            pictureBox2.Image = tempBitmap;
            pictureBox2.Refresh();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)  
        {
            int index = e.Item.Index;

            // when clicking a menu icon shows side tab
            tabControl2.Visible = true;

            // selects preferred tab according to the clicked icon 
            tabControl2.SelectedTab = tabControl2.TabPages[index];
            e.Item.Selected = false;
        }


        /*private void zoomValue_Enter(object sender, EventArgs e)
        {
            if (zoomValue.Value < 50)
            {
                zoomValue.Value = 50;
            }
            if (zoomValue.Value > 500)
            {
                zoomValue.Value = 500;
            }

            Bitmap tempBitmap = new Bitmap(pictureBox1.Image, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);

            // Set the resolution of the bitmap to match the original resolution.

            tempBitmap.SetResolution(OriginalImage.HorizontalResolution,
                                     OriginalImage.VerticalResolution);

            // Create a Graphics object to further edit the temporary bitmap

            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            // First clear the image with the current backcolor

            //bmGraphics.Clear(_BackColor);

            // Set the interpolationmode since we are resizing an image here

            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the original image on the temporary bitmap, resizing it using

            // the calculated values of targetWidth and targetHeight.

            int HorizLeft = (int)(OriginalImage.Width / 2) - ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));
            int VertiTop = (int)(OriginalImage.Height / 2) - ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));


            int HorizRight = (int)(OriginalImage.Width / 2) + ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));
            int VertiBottom = (int)(OriginalImage.Height / 2) + ((int)(OriginalImage.Width / 2) / (int)(zoomValue.Value / 100));


            bmGraphics.DrawImage(OriginalImage,
                                 new Rectangle(HorizLeft, VertiTop, HorizRight, VertiBottom),
                                 new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                                 GraphicsUnit.Pixel);

            pictureBox2.Refresh();
        }*/
    }
}
