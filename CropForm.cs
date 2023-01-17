using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PictureBoxExample;

namespace PhotoSorter
{
    public partial class CropForm : Form
    {
        int xDown = 0;
        int yDown = 0;
        int xUp = 0;
        int yUp = 0;
        int rotation = 0;
        bool mouseDown = false;
        Image CropImage;

        Rectangle rec;
        Pen pen;

        Rectangle rectCropArea = new Rectangle();
        System.IO.MemoryStream ms = new System.IO.MemoryStream();

        public CropForm()
        {
            InitializeComponent();
            CropBox.Image = Form1.CurImage;
            Crop.Enabled = false;
            Save.Enabled = false;
            CropBox.SizeMode = PictureBoxSizeMode.Zoom;
            CropBox.Cursor = Cursors.Cross;
        }


    private void Crop_Click(object sender, EventArgs e)
        {
            // Crop and display image
            try
            {
                int wx;
                int rx;

                // Prepare a new Bitmap on which the cropped image will be drawn
                // We need to scale the rectangle to the photo size 
                Bitmap x = (Bitmap)CropBox.Image;
                wx = (x.Width * 100) / CropBox.Width;       // Number of photo pixels per picturebox pixels width
                rx = (x.Height * 100) / CropBox.Height;     // Number of photo pixels per picturebox pixels height
                // rec is the crop rectangle drawn on the picturebox image.
                CropImage = x.Clone(new Rectangle((rec.Location.X * wx) / 100, 
                    (rec.Location.Y * rx) / 100, (rec.Width * wx) / 100, (rec.Height * rx) / 100), x.PixelFormat);
                CropBox.Image = CropImage;
                Crop.Enabled = false;
                Save.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            // Save cropped image
            try
            {
                // Get a file name to save to.
                SaveCrop.DefaultExt = ".jpg";
                SaveCrop.AddExtension = true;
                SaveCrop.ShowDialog();
                if (SaveCrop.FileName != "")
                {                    
                    CropBox.Image.Save(SaveCrop.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CropBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Adjust cropping
            CropBox.Invalidate();

            Save.Enabled = false;
            Crop.Enabled = false;
            mouseDown = true;
            xDown = e.X;
            yDown = e.Y;
        }

        private void CropBox_MouseUp(object sender, MouseEventArgs e)
        {
            // Adjust cropping
            int x, y;

            xUp = e.X;
            yUp = e.Y;

            if (xDown > e.X)
                x = e.X;
            else
                x = xDown;
            if (yDown > e.Y)
                y = e.Y;
            else
                y = yDown;

            mouseDown = false;
            rec = new Rectangle(x, y, Math.Abs(xUp - xDown), Math.Abs(yUp - yDown));
            using (pen = new Pen(Color.YellowGreen, 3))
            {

                CropBox.CreateGraphics().DrawRectangle(pen, rec);
            }
            rectCropArea = rec;
            Crop.Enabled = true;
        }

        private void CropBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                CropBox.Refresh();
                int x, y;
;
                if (xDown > e.X)
                    x = e.X;
                else
                    x = xDown;
                if (yDown > e.Y)
                    y = e.Y;
                else
                    y = yDown;                

                rec = new Rectangle(x, y, Math.Abs(e.X - xDown), Math.Abs(e.Y - yDown));
                using (pen = new Pen(Color.YellowGreen, 3))
                {

                    CropBox.CreateGraphics().DrawRectangle(pen, rec);
                }
                rectCropArea = rec;
            }
        }

        private void CropBox_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Form1.CroppedImage = CropBox.Image;            
            this.Close();
        }
    }
}
