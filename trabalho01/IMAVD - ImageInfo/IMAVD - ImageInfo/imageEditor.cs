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
        Bitmap OriginalImage, previousImage;
        Bitmap CheckColorImage;
        bool canSelectColor = false;
        bool mouseOverTL, mouseOverTR, mouseOverBL, mouseOverBR;
        int countPixels;
        float imgScale = 0,
              imgVerticalMargin = 0,
              imgHorizontalMargin = 0;
        Point mouseDownLocation;
        Size cropRectOriginalSize = new Size(332, 237);


        public List<Control> GetControls(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetControls(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type).ToList();
        }

        private void InitializeFont()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

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
                    /*
                    if (label.Name.Contains("label"))
                        label.Font = new Font(pfc.Families[1], label.Font.Size);
                    else
                        */
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

                numericUpDown5.Minimum = 0;
                numericUpDown4.Minimum = 0;
                numericUpDown5.Value = 0;
                numericUpDown4.Value = 0;
                numericUpDown5.Value = pictureBox1.Image.Width;
                numericUpDown4.Value = pictureBox1.Image.Height;
                numericUpDown5.Minimum = 1;
                numericUpDown4.Minimum = 1;

                tabControl1.SelectedTab = tabPage2;
            }

            chckClrBtn.Enabled = true;

            this.imgResize();

            previousImage = new Bitmap(OriginalImage);

            cropRectImg();
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
                        CheckColorImage.SetPixel(j, i, Color.FromArgb(trackBar1.Value, now_color.R, now_color.G, now_color.B)); // Sets other pixels (non selected) to transparent
                    }
                }
            }

            pictureBox2.Image = CheckColorImage;
            panel1.Visible = true;
            label15.Text = "There are " + countPixels + " " + clr.Name + " pixels.";
        }

        // Applies check color changes
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
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

                if(tabControl2.SelectedTab == detailsFindColor) setColorValues(colorPicked);
                else if(tabControl2.SelectedTab == detailsColorReplace)
                {
                    label58.BackColor = colorPicked;
                    replaceColor();
                }
                canSelectColor = false;
                Cursor = Cursors.Default;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e) // Changes cursor to default when it's not above the image
        {
            if (canSelectColor) return;
            
            Cursor = Cursors.Default;
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
        
        // Zoom
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            int zoomValue = Int32.Parse(zoomDropDown.SelectedItem.ToString().Replace('%', ' ').Trim());  // Converts string to int
            Bitmap tempBitmap = new Bitmap(pictureBox1.Image, pictureBox1.Image.Width, pictureBox1.Image.Height);

            // Set the resolution of the bitmap to match the original resolution.
            /*
            tempBitmap.SetResolution(OriginalImage.HorizontalResolution,
                                     OriginalImage.VerticalResolution);*/

            // Create a Graphics object to further edit the temporary bitmap

            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            // First clear the image with the current backcolor

            // Set the interpolationmode since we are resizing an image here

            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draws the original image on the temporary bitmap, resizing it using
            // the calculated values of targetWidth and targetHeight.

            bmGraphics.DrawImage(OriginalImage,
                                 new Rectangle(0, 0, (int)(pictureBox1.Image.Width * zoomValue / 100), (int)(pictureBox1.Image.Height * zoomValue / 100)),
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

        private void pictureBox4_Click(object sender, EventArgs e) // Picks a color on the image
        {
            canSelectColor = true;
            Cursor = Cursors.Cross;
        }

        private void replaceColor()
        {
            Color color = label58.BackColor;
            bool removeColor = removeBtn.Checked;
            int threshold = trackBar2.Value;
            int opacity = trackBar1.Value;
            Bitmap bitmap = new Bitmap(pictureBox1.Image);

            for (int i = 0; i < pictureBox1.Image.Height; i++)
            {
                for (int j = 0; j < pictureBox1.Image.Width; j++)
                {
                    // Checks if pixel color is inside the threshold's range
                    Color pixelColor = OriginalImage.GetPixel(j, i);
                    bool red = color.R - threshold <= pixelColor.R && pixelColor.R <= color.R + threshold,
                         green = color.G - threshold <= pixelColor.G && pixelColor.G <= color.G + threshold,
                         blue = color.B - threshold <= pixelColor.B && pixelColor.B <= color.B + threshold,
                         pixelInsideRange = red && green && blue;
                    int alpha = 255;

                    if (pixelInsideRange && removeColor) alpha = opacity;
                    else if (!pixelInsideRange && !removeColor) alpha = opacity;

                    bitmap.SetPixel(j, i, Color.FromArgb(alpha, pixelColor.R, pixelColor.G, pixelColor.B));
                }
            }

            pictureBox1.Image = bitmap;
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
                    pic.SetPixel(x, y, Color.FromArgb(color.A, color.R * r, color.G * g, color.B * b));
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

        private void setBrightnessContrast()
        {
            // Makes the ColorMatrix.
            float b = (float)((brightnessSlider.Value + 255) / 255);
            float c = (float)((contrastSlider.Value + 100) * 255 / 100 / 255);
            ColorMatrix cm = new ColorMatrix(new float[][]
            {
            new float[] {c, 0, 0, 0, 0},
            new float[] {0, c, 0, 0, 0},
            new float[] {0, 0, c, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {b, b, b, 0, 1},
            });
            ImageAttributes attributes = new ImageAttributes();
            attributes.ClearColorMatrix();
            attributes.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            attributes.ClearGamma();
            attributes.SetGamma((float) gammaValue.Value, ColorAdjustType.Bitmap);
            /*
            // Draws the image onto the new bitmap while applying
            // the new ColorMatrix.
            Point[] points =
            {
            new Point(0, 0),
            new Point(OriginalImage.Width, 0),
            new Point(0, OriginalImage.Height),
            };    
            Rectangle rect = new Rectangle(0, 0, OriginalImage.Width, OriginalImage.Height);

            // Makes the result bitmap.
            Bitmap bm = new Bitmap(OriginalImage);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(OriginalImage, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }*/

            Bitmap bm = new Bitmap(OriginalImage);
            Graphics g = Graphics.FromImage(bm);
            g.DrawImage(OriginalImage, new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height)
                , 0, 0, OriginalImage.Width, OriginalImage.Height,
                GraphicsUnit.Pixel, attributes);

            // Returns the result.
            pictureBox1.Image = bm;
        }
        
        private void brightnessSlider_Scroll(object sender, EventArgs e)
        {
            brightnessValue.Value = brightnessSlider.Value;
            setBrightnessContrast();
        }

        private void brightnessValue_ValueChanged(object sender, EventArgs e)
        {
            brightnessSlider.Value = (int) brightnessValue.Value;
            setBrightnessContrast();
        }

        private void contrastSlider_Scroll(object sender, EventArgs e)
        {
            contrastValue.Value = contrastSlider.Value;
            setBrightnessContrast();
        }

        private void contrastValue_ValueChanged(object sender, EventArgs e)
        {
            contrastSlider.Value = (int)contrastValue.Value;
            setBrightnessContrast();
        }

        // Cuts the image in two triangles
        private void topCorner_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);

            // Função y = ax + b
            float a = img.Height / (float) img.Width, // y1 - y2 / x1 - x2 --> img.Height - 0 / img.Width - 0
                  b = img.Height;

            for (int x = 0; x < img.Width; x++)
            {
                int y = (int)Math.Round(b - a * x);

                for (int i = y; i < img.Height; i++)
                {
                    img.SetPixel(x, i, Color.Transparent);
                }
            }

            pictureBox1.Image = img;
        }

        private void bottomCorner_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);

            // Função y = ax + b
            float a = img.Height / (float) img.Width, // y1 - y2 / x1 - x2 --> img.Height - 0 / img.Width - 0
                  b = img.Height;

            for (int x = 0; x < img.Width; x++)
            {
                int y = (int)Math.Round(b - a * x);

                for (int i = 0; i < y; i++)
                {
                    img.SetPixel(x, i, Color.Transparent);
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
            Rectangle rect = new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2);
            this.cropImg(rect);
        }

        private void fourAreas_bottomLeft_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2);
            this.cropImg(rect);
        }

        private void fourAreas_bottomRight_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Rectangle rect = new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2);
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
            Rectangle rect = new Rectangle(0, img.Height / 2, img.Width, img.Height / 2);
            this.cropImg(rect);
        }
        private void cropImg(Rectangle rectangle)
        {
            Bitmap newImg = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics graphics = Graphics.FromImage(newImg);
            graphics.DrawImage(pictureBox1.Image, new Rectangle(0, 0, rectangle.Width, rectangle.Height), rectangle, GraphicsUnit.Pixel);
            pictureBox1.Image = newImg;
        }

        private void cropRectImg() // Draws Crop Grid
        { 
            Bitmap img = new Bitmap(cropRect.Width, cropRect.Height);
            Graphics graphics = Graphics.FromImage(img);

            Color lineColor = Color.White;
            int lineStroke = 5,
                cornerLineSize = 20,
                verticalSpacing = (int) ((cropRect.Height - (lineStroke * 2)) / 3f),
                horizontalSpacing = (int)((cropRect.Width - (lineStroke * 2)) / 3f);

            PointF[,] points = {
                { new PointF(0, verticalSpacing + lineStroke), new PointF(cropRect.Width, verticalSpacing + lineStroke) }, // 1ª Linha Vertical
                { new PointF(0, 2 * verticalSpacing + lineStroke), new PointF(cropRect.Width, 2 * verticalSpacing + lineStroke) }, // 2ª Linha Vertical
                { new PointF(horizontalSpacing + lineStroke, 0), new PointF(horizontalSpacing + lineStroke, cropRect.Height) }, // 1ª Linha Horizontal
                { new PointF(2 * horizontalSpacing + lineStroke, 0), new PointF(2 * horizontalSpacing + lineStroke, cropRect.Height) }, // 2ª Linha Horizontal
                { new PointF(0, 0), new PointF(cornerLineSize, 0) },
                { new PointF(0, 0), new PointF(0, cornerLineSize) },
                { new PointF(cropRect.Width, 0), new PointF(cropRect.Width - cornerLineSize, 0) },
                { new PointF(cropRect.Width, 0), new PointF(cropRect.Width, cornerLineSize) },
                { new PointF(0, cropRect.Height), new PointF(cornerLineSize, cropRect.Height) },
                { new PointF(0, cropRect.Height), new PointF(0, cropRect.Height - cornerLineSize) },
                { new PointF(cropRect.Width, cropRect.Height), new PointF(cropRect.Width - cornerLineSize, cropRect.Height) },
                { new PointF(cropRect.Width, cropRect.Height), new PointF(cropRect.Width, cropRect.Height - cornerLineSize) }
            };

            Pen pen = new Pen(lineColor, lineStroke);

            for(int i = 0; i < points.GetLength(0); i++)
            {
                graphics.DrawLine(pen, points[i, 0], points[i, 1]);
            }

            graphics.Dispose();

            cropRect.Image = img;
        }

        private void button3_Click(object sender, EventArgs e) // Crop Image Button
        {
            if (button3.Text.Equals("CROP"))
            {
                button3.Text = "DONE";
                cropRect.Visible = true;
            }
            else
            {
                Rectangle cropArea = new Rectangle(cropRect.Left - (int)imgHorizontalMargin, cropRect.Top - (int)imgVerticalMargin, cropRect.Width, cropRect.Height);

                button3.Text = "CROP";
                cropRect.Visible = false;

                cropArea.X = (int) (cropArea.X / imgScale);
                cropArea.Y = (int)(cropArea.Y / imgScale);
                cropArea.Width = (int)(cropArea.Width / imgScale);
                cropArea.Height = (int)(cropArea.Height / imgScale);
                cropImg(cropArea);
                imgResize();
            }
        }

        private bool pointsAreClose(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) <= 5 && Math.Abs(p1.Y - p2.Y) <= 5;
        }

        private void cropRect_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePos = new Point(e.Location.X, e.Location.Y),
                  cropRectTL = new Point(0, 0),
                  cropRectTR = new Point(cropRect.Width, 0),
                  cropRectBL = new Point(0, cropRect.Height),
                  cropRectBR = new Point(cropRect.Width, cropRect.Height);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
                
                mouseOverTL = pointsAreClose(mousePos, cropRectTL);
                mouseOverTR = pointsAreClose(mousePos, cropRectTR);
                mouseOverBL = pointsAreClose(mousePos, cropRectBL);
                mouseOverBR = pointsAreClose(mousePos, cropRectBR);
            }
        }

        private void cropRect_MouseMove(object sender, MouseEventArgs e) 
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (mouseOverTL)
                {
                    int width = cropRect.Width - (e.X - mouseDownLocation.X),
                        height = cropRect.Height - (e.Y - mouseDownLocation.Y),
                        x = cropRect.Left + e.X - mouseDownLocation.X,
                        y = cropRect.Top + e.Y - mouseDownLocation.Y;

                    if(x < imgHorizontalMargin) 
                    {
                        width -= (int)imgHorizontalMargin - x;
                        x = (int)imgHorizontalMargin;
                    }
                    if(y < imgVerticalMargin) 
                    {
                        height -= (int)imgVerticalMargin - y;
                        y = (int)imgVerticalMargin;
                    }

                    cropRect.Size = new Size(width, height);
                    cropRect.Left = x;
                    cropRect.Top = y;
                }
                else if (mouseOverTR)
                {
                    int width = cropRectOriginalSize.Width + e.X - mouseDownLocation.X,
                        height = cropRect.Height - (e.Y - mouseDownLocation.Y),
                        y = cropRect.Top + e.Y - mouseDownLocation.Y;

                    if (cropRect.Left + width > panel5.Width - imgHorizontalMargin) width = panel5.Width - (int)imgHorizontalMargin - cropRect.Left;
                    if (y < imgVerticalMargin)
                    {
                        height -= (int)imgVerticalMargin - y;
                        y = (int)imgVerticalMargin;
                    }

                    cropRect.Size = new Size(width, height);
                    cropRect.Top = y;
                }
                else if (mouseOverBL)
                {
                    int width = cropRect.Width - (e.X - mouseDownLocation.X),
                        height = cropRectOriginalSize.Height + e.Y - mouseDownLocation.Y,
                        x = cropRect.Left + e.X - mouseDownLocation.X;

                    if (cropRect.Top + height > panel5.Height - imgVerticalMargin) height = panel5.Height - (int)imgVerticalMargin - cropRect.Top;
                    if (x < imgHorizontalMargin)
                    {
                        width -= (int)imgHorizontalMargin - x;
                        x = (int)imgHorizontalMargin;
                    }

                    cropRect.Size = new Size(width, height);
                    cropRect.Left = x;
                }
                else if (mouseOverBR)
                {
                    int width = cropRectOriginalSize.Width + e.X - mouseDownLocation.X,
                        height = cropRectOriginalSize.Height + e.Y - mouseDownLocation.Y;

                    if (cropRect.Left + width > panel5.Width - imgHorizontalMargin) width = panel5.Width - (int)imgHorizontalMargin - cropRect.Left;
                    if (cropRect.Top + height > panel5.Height - imgVerticalMargin) height = panel5.Height - (int)imgVerticalMargin - cropRect.Top;
                    cropRect.Size = new Size(width, height);
                }
                else
                {
                    int nextXPos = cropRect.Left + e.X - mouseDownLocation.X,
                        nextYPos = cropRect.Top + e.Y - mouseDownLocation.Y;

                    if (nextXPos < imgHorizontalMargin) cropRect.Left = (int) imgHorizontalMargin;
                    else if (nextXPos + cropRect.Width > panel5.Width - imgHorizontalMargin) cropRect.Left = panel5.Width - (int)imgHorizontalMargin - cropRect.Width;
                    else cropRect.Left += e.X - mouseDownLocation.X;

                    if (nextYPos < imgVerticalMargin) cropRect.Top = (int)imgVerticalMargin;
                    else if (nextYPos + cropRect.Height > panel5.Height - imgVerticalMargin) cropRect.Top = panel5.Height - (int)imgVerticalMargin - cropRect.Height;
                    else cropRect.Top += e.Y - mouseDownLocation.Y;
                }

                bool mouseOverAnchorNWSE = mouseOverTL || mouseOverBR,
                     mouseOverAnchorNESW = mouseOverBL || mouseOverTR;

                if (mouseOverAnchorNWSE) Cursor = Cursors.SizeNWSE;
                else if (mouseOverAnchorNESW) Cursor = Cursors.SizeNESW;
                else Cursor = Cursors.SizeAll;

                return;
            }

            Point mousePos = new Point(e.Location.X, e.Location.Y),
                  cropRectTL = new Point(0, 0),
                  cropRectTR = new Point(cropRect.Width, 0),
                  cropRectBL = new Point(0, cropRect.Height),
                  cropRectBR = new Point(cropRect.Width, cropRect.Height);

            bool mouseOverTL2 = pointsAreClose(mousePos, cropRectTL),
                 mouseOverTR2 = pointsAreClose(mousePos, cropRectTR),
                 mouseOverBL2 = pointsAreClose(mousePos, cropRectBL),
                 mouseOverBR2 = pointsAreClose(mousePos, cropRectBR);

            bool mouseOverAnchorNWSE2 = mouseOverTL2 || mouseOverBR2,
                 mouseOverAnchorNESW2 = mouseOverBL2 || mouseOverTR2;

            if (mouseOverAnchorNWSE2) Cursor = Cursors.SizeNWSE;
            else if (mouseOverAnchorNESW2) Cursor = Cursors.SizeNESW;
            else Cursor = Cursors.SizeAll;
        }

        private void cropRect_MouseUp(object sender, MouseEventArgs e)
        {
            cropRectOriginalSize = new Size(cropRect.Width, cropRect.Height);
            mouseOverTL = false;
            mouseOverTR = false;
            mouseOverBL = false;
            mouseOverBR = false;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e) // Resize
        {
            if (numericUpDown5.Value == 0 || numericUpDown4.Value == 0) return;

            Size size = new Size((int)numericUpDown5.Value, (int)numericUpDown4.Value); // numericUpDown5.Value = width, numericUpDown4.Value = height 
            Bitmap resizedImg = new Bitmap(pictureBox1.Image, size);

            pictureBox1.Image = resizedImg;
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            Bitmap TempImage;
            TempImage = new Bitmap(OriginalImage);
            OriginalImage = new Bitmap(previousImage);
            pictureBox1.Image = new Bitmap(TempImage);
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            previousImage = new Bitmap(OriginalImage);
            OriginalImage = new Bitmap(pictureBox1.Image);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            gammaSlider.Value = (int)(gammaValue.Value * 10);
            setBrightnessContrast();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            gammaValue.Value = (decimal) (gammaSlider.Value / 10.0f);
            setBrightnessContrast();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Image originalImage = pictureBox1.Image;
            int rows = (int)numericUpDown8.Value,
                columns = (int)numericUpDown7.Value;
            int imgWidth = originalImage.Width / columns,
                imgHeight = originalImage.Height * imgWidth / originalImage.Width; // altura original -> width original, nova altura -> nova width
            Bitmap newImage = new Bitmap(originalImage.Width, (int)imgHeight * rows);
            Graphics imageMultiply = Graphics.FromImage(newImage);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Retira-se o i e o j para a imagem não ficar com linhas (arredondamentos)
                    imageMultiply.DrawImage(originalImage, j * imgWidth - j, i * imgHeight - i, imgWidth, imgHeight);
                }
            }

            imageMultiply.Dispose();
            pictureBox1.Image = newImage;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            float angle = (float)numericUpDown6.Value;
            Image image = pictureBox1.Image;
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            // Posiciona a imagem no centro de rotação e volta a colocá-la na posição original
            graphics.TranslateTransform(image.Width / 2.0f, image.Height / 2.0f);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-image.Width / 2.0f, -image.Height / 2.0f);
            graphics.DrawImage(image, 0, 0, image.Width, image.Height);
            graphics.Dispose();

            pictureBox1.Image = bitmap;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Image image = pictureBox1.Image;
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.ScaleTransform(1, -1);
            graphics.TranslateTransform(0, -image.Height);
            graphics.DrawImage(image, 0, 0, image.Width, image.Height);
            graphics.Dispose();

            pictureBox1.Image = bitmap;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Image image = pictureBox1.Image;
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.ScaleTransform(-1, 1);
            graphics.TranslateTransform(-image.Width, 0);
            graphics.DrawImage(image, 0, 0, image.Width, image.Height);
            graphics.Dispose();

            pictureBox1.Image = bitmap;
        }

        private void updateReplaceColor(object sender, EventArgs e)
        {
            replaceColor();
        }

        private void colorValue_ValueChanged(object sender, EventArgs e)
        {
            Color color = Color.FromArgb(255, (int) redValue.Value, (int) greenValue.Value, (int) blueValue.Value);
            colorSelected.BackColor = color;
        }
    }
}
