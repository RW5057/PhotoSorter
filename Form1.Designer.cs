
namespace PictureBoxExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Exit = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Prev = new System.Windows.Forms.Button();
            this.DeleteSource = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Restart = new System.Windows.Forms.CheckBox();
            this.FolderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.FolderText = new System.Windows.Forms.TextBox();
            this.RotateLeft = new System.Windows.Forms.Button();
            this.RotateRight = new System.Windows.Forms.Button();
            this.Crop = new System.Windows.Forms.Button();
            this.ZoomIn = new System.Windows.Forms.Button();
            this.ZoomOut = new System.Windows.Forms.Button();
            this.SavePrefix = new System.Windows.Forms.TextBox();
            this.Projectnameinfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Location = new System.Drawing.Point(187, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1028, 621);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(10, 333);
            this.Exit.Margin = new System.Windows.Forms.Padding(1);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(77, 30);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.button1_Click);
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(97, 271);
            this.Next.Margin = new System.Windows.Forms.Padding(1);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(77, 33);
            this.Next.TabIndex = 3;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(97, 216);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(77, 33);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Prev
            // 
            this.Prev.Location = new System.Drawing.Point(10, 271);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(77, 33);
            this.Prev.TabIndex = 5;
            this.Prev.Text = "Prev";
            this.Prev.UseVisualStyleBackColor = true;
            this.Prev.Click += new System.EventHandler(this.Prev_Click_1);
            // 
            // DeleteSource
            // 
            this.DeleteSource.AutoSize = true;
            this.DeleteSource.Location = new System.Drawing.Point(28, 54);
            this.DeleteSource.Name = "DeleteSource";
            this.DeleteSource.Size = new System.Drawing.Size(118, 17);
            this.DeleteSource.TabIndex = 6;
            this.DeleteSource.Text = "Move to Processed";
            this.DeleteSource.UseVisualStyleBackColor = true;
            // 
            // Restart
            // 
            this.Restart.AutoSize = true;
            this.Restart.Location = new System.Drawing.Point(28, 88);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(86, 17);
            this.Restart.TabIndex = 7;
            this.Restart.Text = "Reset Folder";
            this.Restart.UseVisualStyleBackColor = true;
            // 
            // FolderText
            // 
            this.FolderText.Location = new System.Drawing.Point(12, 12);
            this.FolderText.Name = "FolderText";
            this.FolderText.ReadOnly = true;
            this.FolderText.Size = new System.Drawing.Size(147, 20);
            this.FolderText.TabIndex = 8;
            this.FolderText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RotateLeft
            // 
            this.RotateLeft.Location = new System.Drawing.Point(12, 111);
            this.RotateLeft.Name = "RotateLeft";
            this.RotateLeft.Size = new System.Drawing.Size(77, 33);
            this.RotateLeft.TabIndex = 9;
            this.RotateLeft.Text = "Rotate Left";
            this.RotateLeft.UseVisualStyleBackColor = true;
            this.RotateLeft.Click += new System.EventHandler(this.RotateLeft_Click_1);
            // 
            // RotateRight
            // 
            this.RotateRight.CausesValidation = false;
            this.RotateRight.Location = new System.Drawing.Point(95, 111);
            this.RotateRight.Name = "RotateRight";
            this.RotateRight.Size = new System.Drawing.Size(77, 33);
            this.RotateRight.TabIndex = 10;
            this.RotateRight.Text = "Rotate Right";
            this.RotateRight.UseVisualStyleBackColor = true;
            this.RotateRight.Click += new System.EventHandler(this.RotateRight_Click);
            // 
            // Crop
            // 
            this.Crop.Location = new System.Drawing.Point(12, 216);
            this.Crop.Name = "Crop";
            this.Crop.Size = new System.Drawing.Size(75, 31);
            this.Crop.TabIndex = 11;
            this.Crop.Text = "Crop";
            this.Crop.UseVisualStyleBackColor = true;
            this.Crop.Click += new System.EventHandler(this.Crop_Click);
            // 
            // ZoomIn
            // 
            this.ZoomIn.Location = new System.Drawing.Point(97, 163);
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(75, 31);
            this.ZoomIn.TabIndex = 12;
            this.ZoomIn.Text = "Zoom In";
            this.ZoomIn.UseVisualStyleBackColor = true;
            this.ZoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
            // 
            // ZoomOut
            // 
            this.ZoomOut.Location = new System.Drawing.Point(12, 162);
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Size = new System.Drawing.Size(75, 32);
            this.ZoomOut.TabIndex = 13;
            this.ZoomOut.Text = "Zoom Out";
            this.ZoomOut.UseVisualStyleBackColor = true;
            this.ZoomOut.Click += new System.EventHandler(this.ZoomOut_Click);
            // 
            // SavePrefix
            // 
            this.SavePrefix.Location = new System.Drawing.Point(10, 381);
            this.SavePrefix.Name = "SavePrefix";
            this.SavePrefix.Size = new System.Drawing.Size(147, 20);
            this.SavePrefix.TabIndex = 14;
            this.SavePrefix.Text = "Enter project name here";
            // 
            // Projectnameinfo
            // 
            this.Projectnameinfo.Location = new System.Drawing.Point(4, 407);
            this.Projectnameinfo.Multiline = true;
            this.Projectnameinfo.Name = "Projectnameinfo";
            this.Projectnameinfo.ReadOnly = true;
            this.Projectnameinfo.Size = new System.Drawing.Size(179, 40);
            this.Projectnameinfo.TabIndex = 15;
            this.Projectnameinfo.Text = "The project name will be prepended to saved files.";
            this.Projectnameinfo.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1215, 621);
            this.Controls.Add(this.Projectnameinfo);
            this.Controls.Add(this.SavePrefix);
            this.Controls.Add(this.ZoomOut);
            this.Controls.Add(this.ZoomIn);
            this.Controls.Add(this.Crop);
            this.Controls.Add(this.RotateRight);
            this.Controls.Add(this.RotateLeft);
            this.Controls.Add(this.FolderText);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.DeleteSource);
            this.Controls.Add(this.Prev);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PhotoSorter";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Prev;
        private System.Windows.Forms.CheckBox DeleteSource;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox Restart;
        private System.Windows.Forms.FolderBrowserDialog FolderSelect;
        private System.Windows.Forms.TextBox FolderText;
        private System.Windows.Forms.Button RotateLeft;
        private System.Windows.Forms.Button RotateRight;
        private System.Windows.Forms.Button Crop;
        private System.Windows.Forms.Button ZoomIn;
        private System.Windows.Forms.Button ZoomOut;
        private System.Windows.Forms.TextBox SavePrefix;
        private System.Windows.Forms.TextBox Projectnameinfo;
    }
}

