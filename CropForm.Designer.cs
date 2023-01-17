
namespace PhotoSorter
{
    partial class CropForm
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
            this.CropBox = new System.Windows.Forms.PictureBox();
            this.Crop = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.SaveCrop = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.CropBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CropBox
            // 
            this.CropBox.Location = new System.Drawing.Point(58, 58);
            this.CropBox.Name = "CropBox";
            this.CropBox.Size = new System.Drawing.Size(686, 388);
            this.CropBox.TabIndex = 0;
            this.CropBox.TabStop = false;
            this.CropBox.Click += new System.EventHandler(this.CropBox_Click);
            this.CropBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CropBox_MouseDown);
            this.CropBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CropBox_MouseMove);
            this.CropBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CropBox_MouseUp);
            // 
            // Crop
            // 
            this.Crop.Location = new System.Drawing.Point(36, 468);
            this.Crop.Name = "Crop";
            this.Crop.Size = new System.Drawing.Size(75, 23);
            this.Crop.TabIndex = 1;
            this.Crop.Text = "Crop";
            this.Crop.UseVisualStyleBackColor = true;
            this.Crop.Click += new System.EventHandler(this.Crop_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(250, 468);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(143, 468);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 3;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // CropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Crop);
            this.Controls.Add(this.CropBox);
            this.Name = "CropForm";
            this.Text = "CropForm";
            ((System.ComponentModel.ISupportInitialize)(this.CropBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CropBox;
        private System.Windows.Forms.Button Crop;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.SaveFileDialog SaveCrop;
    }
}