
namespace IMAVD___ImageInfo
{
    partial class imageEditor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("", 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(imageEditor));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ldBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imgCrtOn = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.imgSize = new System.Windows.Forms.Label();
            this.imgDim = new System.Windows.Forms.Label();
            this.imgLoc = new System.Windows.Forms.Label();
            this.imgExt = new System.Windows.Forms.Label();
            this.imgName = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.chckClrBtn = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.svImagebtn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.zoomValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.detailsDimension = new System.Windows.Forms.TabPage();
            this.detailsColorSearch = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.detailsProperties = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.detailsDimension.SuspendLayout();
            this.detailsColorSearch.SuspendLayout();
            this.detailsProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ldBtn
            // 
            this.ldBtn.BackColor = System.Drawing.Color.Black;
            this.ldBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ldBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ldBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ldBtn.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ldBtn.Location = new System.Drawing.Point(570, 426);
            this.ldBtn.Name = "ldBtn";
            this.ldBtn.Size = new System.Drawing.Size(157, 51);
            this.ldBtn.TabIndex = 1;
            this.ldBtn.Text = "LOAD IMAGE";
            this.ldBtn.UseVisualStyleBackColor = false;
            this.ldBtn.Click += new System.EventHandler(this.ldBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(25, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "PROPERTIES";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(25, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Name:";
            // 
            // imgCrtOn
            // 
            this.imgCrtOn.AutoSize = true;
            this.imgCrtOn.ForeColor = System.Drawing.Color.White;
            this.imgCrtOn.Location = new System.Drawing.Point(28, 526);
            this.imgCrtOn.MaximumSize = new System.Drawing.Size(235, 0);
            this.imgCrtOn.Name = "imgCrtOn";
            this.imgCrtOn.Size = new System.Drawing.Size(160, 16);
            this.imgCrtOn.TabIndex = 3;
            this.imgCrtOn.Text = "i.e.: December 20th, 2020";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(26, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Extension:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(28, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Location:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(27, 363);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Dimension:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(28, 430);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Size:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(27, 496);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Date Created:";
            // 
            // imgSize
            // 
            this.imgSize.AutoSize = true;
            this.imgSize.ForeColor = System.Drawing.Color.White;
            this.imgSize.Location = new System.Drawing.Point(28, 457);
            this.imgSize.MaximumSize = new System.Drawing.Size(235, 0);
            this.imgSize.Name = "imgSize";
            this.imgSize.Size = new System.Drawing.Size(68, 16);
            this.imgSize.TabIndex = 3;
            this.imgSize.Text = "i.e: 250MB";
            // 
            // imgDim
            // 
            this.imgDim.AutoSize = true;
            this.imgDim.ForeColor = System.Drawing.Color.White;
            this.imgDim.Location = new System.Drawing.Point(28, 391);
            this.imgDim.MaximumSize = new System.Drawing.Size(235, 0);
            this.imgDim.Name = "imgDim";
            this.imgDim.Size = new System.Drawing.Size(114, 16);
            this.imgDim.TabIndex = 3;
            this.imgDim.Text = "i.e: 2000 x 2000px";
            // 
            // imgLoc
            // 
            this.imgLoc.AutoSize = true;
            this.imgLoc.ForeColor = System.Drawing.Color.White;
            this.imgLoc.Location = new System.Drawing.Point(28, 264);
            this.imgLoc.MaximumSize = new System.Drawing.Size(235, 0);
            this.imgLoc.Name = "imgLoc";
            this.imgLoc.Size = new System.Drawing.Size(233, 48);
            this.imgLoc.TabIndex = 3;
            this.imgLoc.Text = "i.e: C:\\Documents\\ISEP\\IMAVD\\imagem.jpg\r\n";
            // 
            // imgExt
            // 
            this.imgExt.AutoSize = true;
            this.imgExt.ForeColor = System.Drawing.Color.White;
            this.imgExt.Location = new System.Drawing.Point(28, 196);
            this.imgExt.MaximumSize = new System.Drawing.Size(235, 0);
            this.imgExt.Name = "imgExt";
            this.imgExt.Size = new System.Drawing.Size(50, 16);
            this.imgExt.TabIndex = 3;
            this.imgExt.Text = "i.e: JPG";
            // 
            // imgName
            // 
            this.imgName.AutoSize = true;
            this.imgName.ForeColor = System.Drawing.Color.White;
            this.imgName.Location = new System.Drawing.Point(28, 124);
            this.imgName.MaximumSize = new System.Drawing.Size(235, 0);
            this.imgName.Name = "imgName";
            this.imgName.Size = new System.Drawing.Size(86, 16);
            this.imgName.TabIndex = 3;
            this.imgName.Text = "i.e: image.jpg";
            // 
            // chckClrBtn
            // 
            this.chckClrBtn.Location = new System.Drawing.Point(94, 279);
            this.chckClrBtn.Name = "chckClrBtn";
            this.chckClrBtn.Size = new System.Drawing.Size(97, 23);
            this.chckClrBtn.TabIndex = 1;
            this.chckClrBtn.Text = "Check Color";
            this.chckClrBtn.UseVisualStyleBackColor = true;
            this.chckClrBtn.Click += new System.EventHandler(this.chckClrBtn_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(52, 333);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(184, 187);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // svImagebtn
            // 
            this.svImagebtn.BackColor = System.Drawing.Color.Black;
            this.svImagebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.svImagebtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.svImagebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.svImagebtn.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.svImagebtn.ForeColor = System.Drawing.Color.White;
            this.svImagebtn.Location = new System.Drawing.Point(832, 624);
            this.svImagebtn.Name = "svImagebtn";
            this.svImagebtn.Size = new System.Drawing.Size(119, 32);
            this.svImagebtn.TabIndex = 6;
            this.svImagebtn.Text = "SAVE IMAGE";
            this.svImagebtn.UseVisualStyleBackColor = false;
            this.svImagebtn.Click += new System.EventHandler(this.svImagebtn_Click);
            // 
            // zoomValue
            // 
            this.zoomValue.Location = new System.Drawing.Point(144, 244);
            this.zoomValue.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.zoomValue.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.zoomValue.Name = "zoomValue";
            this.zoomValue.Size = new System.Drawing.Size(51, 22);
            this.zoomValue.TabIndex = 7;
            this.zoomValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.zoomValue.ValueChanged += new System.EventHandler(this.zoomValue_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(148, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Zoom:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(28, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(923, 519);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-6, -25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1282, 720);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.pictureBox3);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.ldBtn);
            this.tabPage1.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabPage1.ForeColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1274, 691);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox3.Location = new System.Drawing.Point(172, 199);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(334, 278);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Montserrat", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(559, 259);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(392, 73);
            this.label11.TabIndex = 4;
            this.label11.Text = "Image Editor";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Montserrat", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(570, 355);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(383, 26);
            this.label10.TabIndex = 3;
            this.label10.Text = "Please load your image to start editing.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Montserrat", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(559, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 73);
            this.label9.TabIndex = 2;
            this.label9.Text = "MTS";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.svImagebtn);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1274, 691);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(970, -7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(10, 702);
            this.label13.TabIndex = 11;
            this.label13.Text = "label13";
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.BackColor = System.Drawing.Color.Black;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(28, 24);
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.Size = new System.Drawing.Size(936, 34);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "crop.png");
            this.imageList1.Images.SetKeyName(1, "crop.png");
            this.imageList1.Images.SetKeyName(2, "crop.png");
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.detailsDimension);
            this.tabControl2.Controls.Add(this.detailsColorSearch);
            this.tabControl2.Controls.Add(this.detailsProperties);
            this.tabControl2.Location = new System.Drawing.Point(970, -39);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(304, 773);
            this.tabControl2.TabIndex = 10;
            this.tabControl2.Visible = false;
            // 
            // detailsDimension
            // 
            this.detailsDimension.BackColor = System.Drawing.Color.Black;
            this.detailsDimension.Controls.Add(this.label3);
            this.detailsDimension.Controls.Add(this.zoomValue);
            this.detailsDimension.ForeColor = System.Drawing.Color.Black;
            this.detailsDimension.Location = new System.Drawing.Point(4, 25);
            this.detailsDimension.Name = "detailsDimension";
            this.detailsDimension.Padding = new System.Windows.Forms.Padding(3);
            this.detailsDimension.Size = new System.Drawing.Size(296, 744);
            this.detailsDimension.TabIndex = 1;
            this.detailsDimension.Text = "tabPage4";
            // 
            // detailsColorSearch
            // 
            this.detailsColorSearch.BackColor = System.Drawing.Color.Black;
            this.detailsColorSearch.Controls.Add(this.label12);
            this.detailsColorSearch.Controls.Add(this.chckClrBtn);
            this.detailsColorSearch.Controls.Add(this.pictureBox2);
            this.detailsColorSearch.Location = new System.Drawing.Point(4, 25);
            this.detailsColorSearch.Name = "detailsColorSearch";
            this.detailsColorSearch.Size = new System.Drawing.Size(296, 744);
            this.detailsColorSearch.TabIndex = 2;
            this.detailsColorSearch.Text = "tabPage5";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(-8, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 658);
            this.label12.TabIndex = 5;
            this.label12.Text = "label12";
            // 
            // detailsProperties
            // 
            this.detailsProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.detailsProperties.Controls.Add(this.label1);
            this.detailsProperties.Controls.Add(this.imgCrtOn);
            this.detailsProperties.Controls.Add(this.imgSize);
            this.detailsProperties.Controls.Add(this.label8);
            this.detailsProperties.Controls.Add(this.label7);
            this.detailsProperties.Controls.Add(this.imgName);
            this.detailsProperties.Controls.Add(this.imgDim);
            this.detailsProperties.Controls.Add(this.label2);
            this.detailsProperties.Controls.Add(this.label6);
            this.detailsProperties.Controls.Add(this.imgExt);
            this.detailsProperties.Controls.Add(this.imgLoc);
            this.detailsProperties.Controls.Add(this.label5);
            this.detailsProperties.Controls.Add(this.label4);
            this.detailsProperties.Location = new System.Drawing.Point(4, 25);
            this.detailsProperties.Name = "detailsProperties";
            this.detailsProperties.Padding = new System.Windows.Forms.Padding(3);
            this.detailsProperties.Size = new System.Drawing.Size(296, 744);
            this.detailsProperties.TabIndex = 0;
            this.detailsProperties.Text = "tabPage3";
            // 
            // imageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "imageEditor";
            this.Text = "Image Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.detailsDimension.ResumeLayout(false);
            this.detailsDimension.PerformLayout();
            this.detailsColorSearch.ResumeLayout(false);
            this.detailsProperties.ResumeLayout(false);
            this.detailsProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ldBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label imgCrtOn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label imgSize;
        private System.Windows.Forms.Label imgDim;
        private System.Windows.Forms.Label imgLoc;
        private System.Windows.Forms.Label imgExt;
        private System.Windows.Forms.Label imgName;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button chckClrBtn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button svImagebtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown zoomValue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage detailsProperties;
        private System.Windows.Forms.TabPage detailsDimension;
        private System.Windows.Forms.TabPage detailsColorSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
    }
}

