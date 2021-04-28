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
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace IMAVD___ImageInfo
{
    public partial class imageEditor : Form
    {
        public imageEditor()
        {
            InitializeComponent();
            InitializeFont();
            chckClrBtn.Enabled = false;
        }

        FileInfo OriginalImage_info;
        Bitmap OriginalImage;
        Bitmap CheckColorImage;
        bool IsColorFound;
        bool canSelectColor = false;
        int countPixels;

        float imgScale = 0,
              imgVerticalMargin = 0,
              imgHorizontalMargin = 0;

        public List<Control> GetControls(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetControls(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type).ToList();
        }

        private void InitializeFont()
        {
            //Create your private font collection object.
            PrivateFontCollection pfc = new PrivateFontCollection();

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            // int fontLength = Properties.Resources.Digireu.Length;
            int[] fontsLength =
            {
                Properties.Resources.Montserrat_Regular.Length,
                Properties.Resources.Montserrat_Italic.Length,
                Properties.Resources.Montserrat_Medium.Length,
                Properties.Resources.Montserrat_SemiBold.Length,
                Properties.Resources.Montserrat_Bold.Length,
                Properties.Resources.Montserrat_ExtraBold.Length
            };

            // create a buffer to read in to
            byte[][] fontsData =
            {
                Properties.Resources.Montserrat_Regular,
                Properties.Resources.Montserrat_Italic,
                Properties.Resources.Montserrat_Medium,
                Properties.Resources.Montserrat_SemiBold,
                Properties.Resources.Montserrat_Bold,
                Properties.Resources.Montserrat_ExtraBold
            };

            for (int i = 0; i < fontsLength.Length; i++)
            {
                // create an unsafe memory block for the font data
                System.IntPtr data = Marshal.AllocCoTaskMem(fontsLength[i]);

                // copy the bytes to the unsafe memory block
                Marshal.Copy(fontsData[i], 0, data, fontsLength[i]);

                // pass the font to the font collection
                pfc.AddMemoryFont(data, fontsLength[i]);
            }

            List<Control> labels = GetControls(this, typeof(Label));
            List<Control> buttons = GetControls(this, typeof(Button));

            foreach (Control label in labels)
            {
                if (label.GetType() == typeof(Label))
                {
                    ((Label)label).UseCompatibleTextRendering = true;

                    if (label.Name.Contains("label"))
                        label.Font = new Font(pfc.Families[1], label.Font.Size);
                    else
                        label.Font = new Font(pfc.Families[0], label.Font.Size);

                }
            }

          //  foreach (Control button in buttons)
               // button.Font = new Font(pfc.Families[3], button.Font.Size);
        }


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

                panel2.Location = new Point(
                    imgLoc.Location.X,
                    imgLoc.Location.Y + imgLoc.Height + 20
                );

                numericUpDown5.Maximum = pictureBox1.Image.Width;
                numericUpDown4.Maximum = pictureBox1.Image.Height;

                tabControl1.SelectedTab = tabPage2;
            }

            chckClrBtn.Enabled = true;

            this.imgResize();
        }


        private void chckClrBtn_Click(object sender, EventArgs e)
        {
            Color clr = colorSelected.BackColor;
            CheckColorImage = (Bitmap)OriginalImage.Clone();

            countPixels = 0;

            for (int i = 0; i < pictureBox1.Image.Height; i++)
            {
                for (int j = 0; j < pictureBox1.Image.Width; j++)
                {
                    //Gets the color of each pixel
                    Color now_color = OriginalImage.GetPixel(j, i);

                    //Compares Pixel's Color ARGB property with the picked color's ARGB property 
                    if (now_color.ToArgb() == clr.ToArgb())
                    {
                        countPixels++;
                    } 
                    else
                    {
                        CheckColorImage.SetPixel(j, i, Color.FromArgb(90, now_color.R, now_color.G, now_color.B)); // Sets other pixels (non selected) to transparent
                    }
                }
            }

            pictureBox2.Image = CheckColorImage;
            panel1.Visible = true;
            label15.Text = "There are " + countPixels + " " + clr.Name + " pixels.";
        }

        private void imgResize()
        {
            float imgWidth = pictureBox1.Image.Width,
                  imgHeight = pictureBox1.Image.Height,
                  pictureBoxWidth = pictureBox1.ClientSize.Width,
                  pictureBoxHeight = pictureBox1.ClientSize.Height,
                  imgAspect = imgWidth / imgHeight,
                  pictureBoxAspect = pictureBoxWidth / pictureBoxHeight;

            if (imgAspect < pictureBoxAspect) // imgWidth < pictureBox1.ClientSize.Width
            {
                this.imgScale = pictureBoxHeight / imgHeight;
                float width = imgWidth * this.imgScale;
                this.imgHorizontalMargin = (pictureBox1.ClientSize.Width - width) / 2;
            }
            else // imgHeight < pictureBox1.ClientSize.Height
            {
                this.imgScale = pictureBoxWidth / imgWidth;
                float height = imgHeight * this.imgScale;
                this.imgVerticalMargin = (pictureBox1.ClientSize.Height - height) / 2;
            }
        }

        private bool mouseIsHoverImage(int mouseX, int mouseY)
        {
            bool mouseInsideX = mouseX >= this.imgHorizontalMargin && mouseX <= pictureBox1.ClientSize.Width - this.imgHorizontalMargin,
                 mouseInsideY = mouseY >= this.imgVerticalMargin && mouseY <= pictureBox1.ClientSize.Height - this.imgVerticalMargin;

            if (mouseInsideX && mouseInsideY)
            {
                return true;
            }

            return false;
        }


        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null && canSelectColor)
            {
                // Doesn't allow click if mouse is outside image
                if (!this.mouseIsHoverImage(e.Location.X, e.Location.Y))
                {
                    return;
                }

                float imgWidth = pictureBox1.Image.Width * this.imgScale,
                      imgHeight = pictureBox1.Image.Height * this.imgScale;

                Bitmap bitmap = new Bitmap(pictureBox1.Image, (int)Math.Round(imgWidth), (int)Math.Round(imgHeight));
                Color colorPicked = bitmap.GetPixel(e.Location.X - (int) this.imgHorizontalMargin, e.Location.Y - (int)this.imgVerticalMargin);

                setColorValues(colorPicked);
                canSelectColor = false;
                Cursor = Cursors.Default;

                //bitmap = new Bitmap(pictureBox1.Image, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                //pictureBox1.Image = bitmap;

                //MessageBox.Show(e.X + "," + e.Y);

                //colorPicked(colorPicked_);

                /*CheckColorImage.SetPixel(e.X, e.Y, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X - 1, e.Y, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X, e.Y - 1, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X + 1, e.Y, Color.FromArgb(255, 0, 0, 0));
                CheckColorImage.SetPixel(e.X, e.Y + 1, Color.FromArgb(255, 0, 0, 0));*/
            }
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) // Changes cursor only when its position is above the image
        {
            if (canSelectColor) return;

            if (this.mouseIsHoverImage(e.Location.X, e.Location.Y))
            {
                Cursor = Cursors.SizeAll;
            } else
            {
                Cursor = Cursors.Default;
            }
        }


        private void buttons_MouseEnter(object sender, EventArgs e) // Changes buttons' design when the cursor is positioned above the button
        {
            Button btn = (Button)sender;

            btn.ForeColor = Color.Black;
            btn.BackColor = Color.White;
        }

        private void buttons_MouseLeave(object sender, EventArgs e) // Changes buttons' design when the cursor is positioned above the button
        {
            Button btn = (Button)sender;

            btn.ForeColor = Color.White;
            btn.BackColor = Color.Transparent;
        }


        private void svImagebtn_Click(object sender, EventArgs e) 
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
                    pictureBox1.Image.Save(dialog.FileName, ImageFormat.Png);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int zoomValue = Int32.Parse(comboBox1.SelectedItem.ToString().Replace('%', ' ').Trim());  // Converts string to int

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

            int HorizLeft = (int)(OriginalImage.Width / 2) - (int)((OriginalImage.Width / 2) / (zoomValue / 100));
            int VertiTop = (int)(OriginalImage.Height / 2) - (int)((OriginalImage.Height / 2) / (zoomValue / 100));


            int HorizRight = (int)(OriginalImage.Width / 2) + (int)((OriginalImage.Width / 2) / (zoomValue / 100));
            int VertiBottom = (int)(OriginalImage.Height / 2) + (int)((OriginalImage.Height / 2) / (zoomValue / 100));


            bmGraphics.DrawImage(OriginalImage,
                                 new Rectangle(0, 0, (int)(pictureBox1.ClientSize.Width * (zoomValue / 100)), (int)(pictureBox1.ClientSize.Height * (zoomValue / 100))),
                                 new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height),
                                 GraphicsUnit.Pixel);

            pictureBox1.BackColor = SystemColors.Control;
            pictureBox1.Image = tempBitmap;
            pictureBox1.Refresh();
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            countPixels = 0;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                setColorValues(colorDlg.Color);
            }
        }

        private void setColorValues(Color color)
        {
            redValue.Value = color.R;
            greenValue.Value = color.G;
            blueValue.Value = color.B;

            colorSelected.BackColor = color; // Sets label background color to RGB color
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            canSelectColor = true;
            Cursor = Cursors.Cross;
        }

        private void invertClrBtn_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(pictureBox1.Image);
            for (int y = 0; (y <= (pic.Height - 1)); y++)
            {
                for (int x = 0; (x <= (pic.Width - 1)); x++)
                {
                    Color inv = pic.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(x, y, inv);
                }
            }
            pictureBox1.Image = pic;
            if (invertClrBtn.Text.Equals("INVERT COLORS")) invertClrBtn.Text = "REVERT COLORS";
            else invertClrBtn.Text = "INVERT COLORS";
            
        }

        private void applyFilter(int r, int g, int b)
        {
            Bitmap pic = new Bitmap(pictureBox1.Image);
            for (int y = 0; (y <= (pic.Height - 1)); y++)
            {
                for (int x = 0; (x <= (pic.Width - 1)); x++)
                {
                    Color color = pic.GetPixel(x, y);
                    pic.SetPixel(x, y, Color.FromArgb(255, color.R * r, color.G * g, color.B * b));
                }
            }
            pictureBox1.Image = pic;
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Tag.ToString())
            {
                case "red":
                    applyFilter(1, 0, 0);
                    break;
                case "green":
                    applyFilter(0, 1, 0);
                    break;
                case "blue":
                    applyFilter(0, 0, 1);
                    break;
                default:
                    break;
            }
        }

        private void setBrightness()
        {
            // Make the ColorMatrix.
            float b = (float)(brightnessSlider.Value + 255) / 255;
            ColorMatrix cm = new ColorMatrix(new float[][]
            {
            new float[] {b, 0, 0, 0, 0},
            new float[] {0, b, 0, 0, 0},
            new float[] {0, 0, b, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1},
            });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying
            // the new ColorMatrix.
            Point[] points =
            {
            new Point(0, 0),
            new Point(OriginalImage.Width, 0),
            new Point(0, OriginalImage.Height),
            };
            Rectangle rect = new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(OriginalImage.Width, OriginalImage.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(OriginalImage, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            pictureBox1.Image = bm;
        }
        
        private void brightnessSlider_Scroll(object sender, EventArgs e)
        {
            brightnessValue.Value = brightnessSlider.Value;
            setBrightness();
        }

        private void brightnessValue_ValueChanged(object sender, EventArgs e)
        {
            brightnessSlider.Value = (int) brightnessValue.Value;
            setBrightness();
        }

        // Cuts the image in two triangles
        private void topCorner_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);

            for (int i = 1; i < img.Height; i++)
            {
                for (int j = img.Width - 1; j > img.Width - 1 - i; j--)
                {
                    img.SetPixel(j, i, Color.Transparent);
                }
            }

            pictureBox1.Image = img;
        }

        private void bottomCorner_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width - 1 - i; j++)
                {
                    img.SetPixel(j, i, Color.Transparent);
                }
            }

            pictureBox1.Image = img;
        }

        // Cuts the image in four areas
        private void fourAreas_topLeft_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(0, 0, img.Width / 2, img.Height / 2);
            this.cropImg(rect);
        }

        private void fourAreas_topRight_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(img.Width / 2, 0, img.Width, img.Height / 2);
            this.cropImg(rect);
        }

        private void fourAreas_bottomLeft_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(0, img.Height / 2, img.Width / 2, img.Height);
            this.cropImg(rect);
        }

        private void fourAreas_bottomRight_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(img.Width / 2, img.Height / 2, img.Width, img.Height);
            this.cropImg(rect);
        }

        // Cuts the image in two areas
        private void twoAreas_top_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height / 2);
            this.cropImg(rect);
        }

        private void twoAreas_bottom_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(0, img.Height / 2, img.Width, img.Height);
            this.cropImg(rect);
        }

        private void cropImg(Rectangle rectangle)
        {
            Bitmap newImg = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics graphics = Graphics.FromImage(newImg);

            Point ulCorner = new Point(rectangle.X, rectangle.Y),
                  urCorner = new Point(rectangle.X + rectangle.Width, rectangle.Y),
                  blCorner = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            Point[] desPoints = { ulCorner, urCorner, blCorner };

            graphics.DrawImage(pictureBox1.Image, desPoints, rectangle, GraphicsUnit.Pixel);
            pictureBox1.Image = newImg;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text.Equals("CROP")) button3.Text = "DONE";
            else button3.Text = "CROP";
        }

        private void colorValue_ValueChanged(object sender, EventArgs e)
        {
            Color color = Color.FromArgb(255, (int) redValue.Value, (int) greenValue.Value, (int) blueValue.Value);
            colorSelected.BackColor = color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
            panel1.Visible = false;
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
