
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

using PhotoSorter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Xml.Linq;

namespace PictureBoxExample
{


    public partial class Form1 : Form
    {

        //
        // Entry point
        //
        public Form1()
        {
            InitializeComponent();
            Size = new Size(900, 900);
            Log = new LogWriter("Start log");
            DeleteSource.CheckedChanged += DeleteSource_CheckedChanged;
            this.MouseWheel += Form_MouseWheel;
        }

        public static string file;
        public int rotation = 0;
        int num = 0;
        int numFiles;
        int ZoomLevel = 0;        // 100 is max
        string delFolder;
        string sourceFolder;
        string[] folderList;
        public static LogWriter Log;
        Rectangle ZoomRec;
        int saveCount;
        bool cropping = false;
        bool cropped = false;
        bool mouseDown = false;
        int xDown, yDown;
        int xUp = 0;
        int yUp = 0;
        Image CropImage;
        Rectangle rec;
        Pen pen;
        Rectangle rectCropArea = new Rectangle();
        System.IO.MemoryStream ms = new System.IO.MemoryStream();



        public static string[] FindFiles(string folder, string pattern)
        {
            try
            {
                // Only get files that begin with the letter "c".
                string[] dirs = Directory.GetFiles(folder, pattern);
                return dirs;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString() + folder);
                Log.LogWrite("Error getting folder listing" + folder);
            }
            return null;
        }

        //
        // Exit program
        //
        private void button1_Click(object sender, EventArgs e)
         {
            // Exit program
            string deldest;
            string srcName;

            pictureBox1.Dispose();
            if (Directory.Exists(delFolder) && file != ""  && File.Exists(file))
            {
                deldest = delFolder;
                deldest += "\\";
                srcName = file;
                srcName = srcName.Remove(0, sourceFolder.Length + 1);
                deldest += srcName;
                try
                {
                    File.Move(file, deldest);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Folder Error");
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            this.Dispose();
        }


        private void Save_Click(object sender, EventArgs e)
        {
            // Copy the current image to the save folder
            string fname;

            fname = file.Remove(0, sourceFolder.Length + 1);
            saveFileDialog1.FileName = fname;
            saveFileDialog1.Filter = "JPEG Image (.jpeg)|*.jpeg |Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif |Png Image (.png)|*.png |Tiff Image (.tiff)|*.tiff";

            saveFileDialog1.Title = "Save an Image File";
            if (SavePrefix.Text != "" && SavePrefix.Text != "Enter project name here")
            {
                saveFileDialog1.FileName = SavePrefix.Text + "_" + saveCount.ToString();
            }
            DialogResult    res;
            
            res = saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                // If the file name is not an empty string open it for saving.
                string ext = "";
                if (saveFileDialog1.FileName != "")
                {
                    Log.LogWrite("Saving to" + saveFileDialog1.FileName);
                    try
                    {
                        string fn = saveFileDialog1.FileName;

                        if (fn.Contains(".jpg"))
                        {
                            pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                            ext = ".jpg";
                        }
                        else if (fn.Contains(".jpeg"))
                        {
                            pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                            ext = ".jpeg";
                        }
                        else if (fn.Contains(".tiff"))
                        {
                            pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                            ext = ".tiff";
                        }
                        else if (fn.Contains(".bmp"))
                        {
                            pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                            ext = ".bmp";
                        }
                        else if (fn.Contains(".gif"))
                        {
                            pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                            ext = ".gif";                            
                        }
                        else if (fn.Contains(".png"))
                        {
                            pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
                            ext = ".png";                            
                        }
                    
                        if (fn.Contains('_'))
                        {
                            while (File.Exists(fn))
                            {
                                saveCount++;
                                fn = fn.Remove(fn.LastIndexOf('_'), fn.Length - fn.LastIndexOf('_'));
                                fn += "_" + saveCount.ToString() + ext;
                            }
                        }

                        cropped = false;
                        ZoomIn.Enabled = true;
                        ZoomOut.Enabled = true;
                    }
                    catch (PathTooLongException)
                    {
                        // Path too long
                        MessageBox.Show("The path is too long");
                        Log.LogWrite("Path is too long: " + saveFileDialog1.FileName);
                    }
                    catch (FileNotFoundException)
                    {
                        // File not found
                        MessageBox.Show("The file was not found");
                        Log.LogWrite("Could not find " + saveFileDialog1.FileName);
                    }
                    catch (IOException)
                    {
                        // Io exception
                        MessageBox.Show("IO Error");
                        Log.LogWrite("Exception  ");
                    }
                    finally
                    {
                    }
                }
            }
        }

        // DeleteSource actually moves the source file to a processed folder.
        private void DeleteSource_CheckedChanged(Object sender, EventArgs e)
        {
            if (DeleteSource.Checked)
            {
                MessageBox.Show("Source files will be moved to Processed subfolder after viewing!.");
                delFolder = "";
                if (sourceFolder != "")
                {
                    delFolder = sourceFolder;
                    delFolder += "\\Processed";
                }

                Prev.Enabled = false;
            }
            else
                Prev.Enabled = true;
        }

        private void CorrectExifOrientation(Image image)
        {
            if (image == null) return;
            int orientationId = 0x0112;
            if (image.PropertyIdList.Contains(orientationId))
            {
                var orientation = (int)image.GetPropertyItem(orientationId).Value[0];
                var rotateFlip = RotateFlipType.RotateNoneFlipNone;
                switch (orientation)
                {
                    case 1: rotateFlip = RotateFlipType.RotateNoneFlipNone; break;
                    case 2: rotateFlip = RotateFlipType.RotateNoneFlipX; break;
                    case 3: rotateFlip = RotateFlipType.Rotate180FlipNone; break;
                    case 4: rotateFlip = RotateFlipType.Rotate180FlipX; break;
                    case 5: rotateFlip = RotateFlipType.Rotate90FlipX; break;
                    case 6: rotateFlip = RotateFlipType.Rotate90FlipNone; break;
                    case 7: rotateFlip = RotateFlipType.Rotate270FlipX; break;
                    case 8: rotateFlip = RotateFlipType.Rotate270FlipNone; break;
                    default: rotateFlip = RotateFlipType.RotateNoneFlipNone; break;
                }
                if (rotateFlip != RotateFlipType.RotateNoneFlipNone)
                {
                    image.RotateFlip(rotateFlip);
                    image.RemovePropertyItem(orientationId);
                }
                if (rotation != 0)
                {
                    int rt = rotation;
                    if (rotation > 0)
                    {
                        while (rt != 0)
                        {
                            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            rt--;
                        }
                    }
                    else
                    {
                        while (rt != 0)
                        {
                            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            rt++;
                        }
                    }
                }
            }
            else
            {
                if (rotation != 0)
                {
                    int rt = rotation;
                    if (rotation > 0)
                    {
                        while (rt != 0)
                        {
                            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            rt--;
                        }
                    }
                    else
                    {
                        while (rt != 0)
                        {
                            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            rt++;
                        }
                    }
                }
            }

        }

        //
        // Advance to the next photo
        //
        private void Next_Click(object sender, EventArgs e)        
        {
            DialogResult res;
            cropping= false;
            if (cropped)
            {
                // See if they want to save the cropped file
                res = MessageBox.Show("Press Cancel then Save to save your changes or OK to continue.", "Unsaved changes",
                    MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    cropped = false;
                }
            }
            // If the restart button is checked then do a new scan.
            if (Restart.Checked)
            {
                Restart.Checked = false;
                Restart.Refresh();
                num = 0;
                if (folderList != null)
                    folderList = null;
            }
            if (num == 0)
            {
                FolderSelect.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                // If a ToProcess folder exists use it as the default.
                if (Directory.Exists(FolderSelect.SelectedPath + "\\ToProcess"))
                {
                    FolderSelect.SelectedPath += "\\ToProcess";
                }
                res = FolderSelect.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                sourceFolder = FolderSelect.SelectedPath;
                delFolder = sourceFolder;
                delFolder += "\\Processed";
                List<string> files = new List<string>();

                folderList = FindFiles(sourceFolder, "*.gif");
                for (int ix = 0; ix < folderList.Count(); ix++)
                    files.Add(folderList[ix]);

                folderList = FindFiles(sourceFolder, "*.jpeg");
                for (int ix = 0; ix < folderList.Count(); ix++)
                    files.Add(folderList[ix]);

                folderList = FindFiles(sourceFolder, "*.jpg");
                for (int ix = 0; ix < folderList.Count(); ix++)
                    files.Add(folderList[ix]);

                folderList = FindFiles(sourceFolder, "*.bmp");
                for (int ix = 0; ix < folderList.Count(); ix++)
                    files.Add(folderList[ix]);

                folderList = FindFiles(sourceFolder, "*.png");
                for (int ix = 0; ix < folderList.Count(); ix++)
                    files.Add(folderList[ix]);

                folderList = files.ToArray();
                numFiles = folderList.Count();
                if (numFiles > 0)
                    num = 1;
            }
            else
            {
                num++;
            }
            
            if ((num - 1) == numFiles || num == 0)
            {
                MessageBox.Show("No more files");
                num = 0;
                // move current image.
                if (file != null && DeleteSource.Checked)
                {
                    string deldest;
                    string srcName;

                    if (!Directory.Exists(delFolder))
                    {
                        try
                        {
                            Directory.CreateDirectory(delFolder);
                        }
                        catch (DirectoryNotFoundException)
                        {
                            MessageBox.Show("Folder not found");
                        }
                        catch (IOException)
                        {
                            MessageBox.Show("IO Exception");
                        }
                        finally
                        {
                        }
                    }
                    pictureBox1.ImageLocation = null;
                    pictureBox1.Image = null;
                    pictureBox1.Refresh();
                    if (Directory.Exists(delFolder) && file != "")
                    {
                        deldest = delFolder;
                        deldest += "\\";
                        srcName = file;
                        srcName = srcName.Remove(0, sourceFolder.Length + 1);
                        deldest += srcName;
                        try
                        {
                            File.Move(file, deldest);
                        }
                        catch (DirectoryNotFoundException)
                        {
                            MessageBox.Show("Folder Error");
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            file = null;
                        }
                    }
                }

                return;
            }

            string lastfile = file;
            try
            {
                file = folderList[num - 1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            FolderText.Text = file.Remove(0, file.LastIndexOf('\\') + 1);
            cropped = false;
            ZoomLevel = 0;
            rotation = 0;
            ZoomIn.Enabled = true;
            ZoomOut.Enabled = false;

            // Specify a valid picture file path on your computer.
            //System.IO.FileStream fs;
            //fs = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //pictureBox1.Image = System.Drawing.Image.FromStream(fs);
            //fs.Close();
            pictureBox1.Load(file);
            CorrectExifOrientation(pictureBox1.Image);
            Log.LogWrite("Processing " + file);
            // move current image.
            if (lastfile != "" && delFolder != "" && DeleteSource.Checked)
            {
                string deldest;
                string srcName;

                if (!Directory.Exists(delFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(delFolder);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        MessageBox.Show("Folder not found");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                    }
                }
                if (Directory.Exists(delFolder) && lastfile != null)
                {
                    deldest = delFolder;
                    deldest += "\\";
                    srcName = lastfile;
                    srcName = srcName.Remove(0, sourceFolder.Length + 1);
                    deldest += srcName;
                    try
                    {
                        File.Move(lastfile, deldest);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        MessageBox.Show("Folder Error");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                    }
                }
            }
        }
    

        private void Prev_Click_1(object sender, EventArgs e)
        {
            cropping = false;
            if (cropped)
            {
                // See if they want to save the cropped file
                DialogResult res;
                res = MessageBox.Show("Press Cancel then Save to save changes or OK to continue.", "Unsaved changes",
                    MessageBoxButtons.OKCancel);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    cropped = false;
                }
            }
            if (num == 0)
            {
                MessageBox.Show("No more files");
                return;
            }
            if (DeleteSource.Checked)
            {
                MessageBox.Show("Cannot go back with Move to Processed checked.");
                return;
            }
            
            num--;
            try
            {
                file = folderList[num - 1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            int idx = file.LastIndexOf('\\');
            FolderText.Text = file.Remove(0, idx + 1);
            System.IO.FileStream fs;
            fs = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            pictureBox1.Image = System.Drawing.Image.FromStream(fs);
            fs.Close();
            cropped = false;
            rotation = 0;
            ZoomLevel = 0;
            ZoomIn.Enabled = true;
            ZoomOut.Enabled = false;
            CorrectExifOrientation(pictureBox1.Image);
        }

        private void RotateLeft_Click_1(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Image = img;
            rotation--;
        }

        private void RotateRight_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;

            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = img;
            rotation++;
        }

        private void Crop_Click(object sender, EventArgs e)
        {
            if (cropping)
            {
                Bitmap x = (Bitmap) null;
                cropping = false;
                try
                {
                    int wx, newwx;
                    int rx, newrx;
                    PixelFormat p;

                    // Prepare a new Bitmap on which the cropped image will be drawn
                    // We need to scale the rectangle to the photo size 
                    x = (Bitmap)pictureBox1.Image;
                    wx = (x.Width * 10000) / pictureBox1.Width;       // Number of photo pixels per picturebox pixels width
                    rx = (x.Height * 10000) / pictureBox1.Height;     // Number of photo pixels per picturebox pixels height
                    // rec is the cropping rectangle drawn on the picturebox image.
                    newrx = (rec.Height * rx) / 10000;
                    newwx = (rec.Width * wx) / 10000;
                    p = x.PixelFormat;
                    CropImage = x.Clone(new Rectangle((rec.Location.X * wx) / 10000,
                        (rec.Location.Y * rx) / 10000, newwx, newrx), p);
                    //CropImage = x.Clone(rec, p);
                    x.Dispose();
                    pictureBox1.Image = CropImage;
                    Crop.Enabled = true;
                    Save.Enabled = true;
                    cropping = false;
                    cropped = true;
                    ZoomIn.Enabled = false;
                    ZoomOut.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    if (x != (Bitmap) null)
                        x.Dispose();
                }
            }
            else 
                cropping = true;
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            if (ZoomLevel < 95)
            {
                ZoomLevel += 5;
                if (ZoomLevel > 95)
                    ZoomLevel = 95;

                ZoomOut.Enabled = true;
                Image img = Image.FromFile(file);
                CorrectExifOrientation(img);
                Bitmap x = (Bitmap) img;
                // rec is the zoom area.
                ZoomRec = new Rectangle((((img.Width * ZoomLevel) / 100) / 2), (((img.Height * ZoomLevel) / 100) / 2),
                    (img.Width * (100 - ZoomLevel)) / 100, (img.Height * (100 - ZoomLevel)) / 100);

                pictureBox1.Image = x.Clone(ZoomRec, x.PixelFormat);
                x.Dispose();
            }
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            if (ZoomLevel > 0)
            {
                ZoomLevel -= 5;
                if (ZoomLevel > 0)
                {
                    Image img = Image.FromFile(file);
                    CorrectExifOrientation(img);
                    Bitmap x = (Bitmap)img;
                    ZoomRec = new Rectangle((((img.Width * ZoomLevel) / 100) / 2), (((img.Height * ZoomLevel) / 100) / 2),
                        (img.Width * (100 - ZoomLevel)) / 100, (img.Height * (100 - ZoomLevel)) / 100);

                    pictureBox1.Image = x.Clone(ZoomRec, x.PixelFormat);
                    x.Dispose();
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(file);
                    CorrectExifOrientation(pictureBox1.Image);
                    ZoomLevel = 0;
                    ZoomOut.Enabled = false;
                }
            }
        }

        //
        // Use the mouse wheel to zoom
        // For each increment of zoomconst or more we zoom in or out 1%
        //
        void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            int     zoomchange;
            Image   ZoomImage;
            int     zoomconst = 15;

            if (num != 0 && !cropped)
            {
                if (e.Delta > 0)
                {
                    // Scroll up (zoom in)
                    if (ZoomLevel < 95)
                    {
                        // Zoom in                         
                        zoomchange = e.Delta / zoomconst;
                        ZoomLevel += zoomchange;
                        if (ZoomLevel > 95)
                            ZoomLevel = 95;
                        if (ZoomLevel > 0)
                            ZoomOut.Enabled = true;
                        Image img = Image.FromFile(file);
                        CorrectExifOrientation(img);
                        Bitmap x = (Bitmap)img;        // Full file
                        // rec is the zoom area.
                        ZoomRec = new Rectangle((((img.Width * ZoomLevel) / 100) / 2), (((img.Height * ZoomLevel) / 100) / 2),
                            (img.Width * (100 - ZoomLevel)) / 100, (img.Height * (100 - ZoomLevel)) / 100);


                        ZoomImage = x.Clone(ZoomRec, x.PixelFormat);
                        x.Dispose();
                        pictureBox1.Image = ZoomImage;
                        pictureBox1.Refresh();
                    }
                }
                else if (ZoomLevel > 0)
                {
                    // Scroll down (zoom out)
                    zoomchange = e.Delta / zoomconst;
                    ZoomLevel += zoomchange;
                    if (ZoomLevel > 0)
                    {
                        Image img = Image.FromFile(file);
                        CorrectExifOrientation(img);
                        Bitmap x = (Bitmap)img;        // Full file

                        ZoomOut.Enabled = true;
                        // ZoomRec is the zoom area.
                        ZoomRec = new Rectangle((((img.Width * ZoomLevel) / 100) / 2), (((img.Height * ZoomLevel) / 100) / 2),
                            (img.Width * (100 - ZoomLevel)) / 100, (img.Height * (100 - ZoomLevel)) / 100);

                        ZoomImage = x.Clone(ZoomRec, x.PixelFormat);
                        x.Dispose();
                        pictureBox1.Image = ZoomImage;
                        pictureBox1.Refresh();
                    }
                    else
                    {
                        pictureBox1.Image = Image.FromFile(file);
                        CorrectExifOrientation(pictureBox1.Image);
                        pictureBox1.Refresh();
                        ZoomLevel = 0;
                        ZoomOut.Enabled = false;
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        int StartMoveX, StartMoveY;
        bool Moving = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // the Mouse is used to move the photo after zooming
            // When the mouse button is pressed record the current position of the mouse in the frame.
            if (cropping)
            {
                Save.Enabled = false;
                Crop.Enabled = false;
                mouseDown = true;
                xDown = e.X;
                yDown = e.Y;
            }
            else if (ZoomLevel > 0 && !cropped)
            {
                StartMoveX = e.X;
                StartMoveY = e.Y;
                Log.LogWrite("Startmove x = " + StartMoveX.ToString() + "  y = " + StartMoveY.ToString());
                Moving = true;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // When the mouse button is released look at the current position and adjust the photo to show where it was moved to
            int     newx, newy;
            int rx, wx;

            if (Moving && num > 0 && ZoomLevel > 0)
            {
                Image img = Image.FromFile(file);
                CorrectExifOrientation(img);
                Bitmap x = (Bitmap) img;
                int adjustedX, adjustedY;
                int deltaX, deltaY;

                Moving = false;

                deltaX = StartMoveX - e.X;      // Number of picturebox pixels moved.
                deltaY = StartMoveY - e.Y;
                Log.LogWrite("end move x = " + e.X.ToString() + "  y = " + e.Y.ToString());

                StartMoveX = e.X;
                StartMoveY = e.Y;

                adjustedX = ((100 - ZoomLevel) * x.Width) / 100;
                adjustedY = ((100 - ZoomLevel) * x.Height) / 100;
                wx = ((adjustedX * 100000) / pictureBox1.Width) / 100000;      // Number of photo pixels per picturebox pixels width
                rx = ((adjustedY * 100000) / pictureBox1.Height) / 100000;     // Number of photo pixels per picturebox pixels height
                newx = ZoomRec.X + (deltaX * wx);
                newy = ZoomRec.Y + (deltaY * rx);

                if (newx + ZoomRec.Width > img.Width)
                    newx = x.Width - ZoomRec.Width;
                if (newy + ZoomRec.Height > img.Height)
                    newy = x.Height - ZoomRec.Height;
                if (newx < 0)
                    newx = ZoomRec.X;
                if (newy < 0)
                    newy = ZoomRec.Y;
                
                try
                {
                    ZoomRec = new Rectangle(newx, newy,
                        (img.Width * (100 - ZoomLevel)) / 100, (img.Height * (100 - ZoomLevel)) / 100);
                    pictureBox1.Image = x.Clone(ZoomRec, x.PixelFormat);  
                    x.Dispose();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());   
                    x.Dispose();
                }
            }
            else if (cropping)
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
                    pictureBox1.CreateGraphics().DrawRectangle(pen, rec);
                }
                rectCropArea = rec;
                Crop.Enabled = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // As the mouse moves adjust the photo position if the button is pressed.
            if (Moving)
            {
                int newx, newy;
                int rx, wx;
                if (num > 0 && ZoomLevel > 0)
                {
                    Image img = Image.FromFile(file);
                    CorrectExifOrientation(img);
                    Bitmap x = (Bitmap)img;
                    int adjustedX, adjustedY;
                    int deltaX, deltaY;

                    deltaX = StartMoveX - e.X;      // Number of picturebox pixels moved.
                    deltaY = StartMoveY - e.Y;
                    StartMoveX = e.X;
                    StartMoveY = e.Y;

                    // Adjust the size of the picturebox to fit the photo
                    wx = this.Height;
                    rx = this.Width;
                    if ((100 - ZoomLevel) > (x.Width))
                    {
                        adjustedX = ((100 - ZoomLevel) * x.Width) / 100;
                        adjustedY = ((100 - ZoomLevel) * x.Height) / 100;
                    }
                    else
                    {
                        adjustedX = x.Width;
                        adjustedY = x.Height;
                    }
                    wx = ((adjustedX * 100000) / pictureBox1.Width) / 100000;      // Number of photo pixels per picturebox pixels width
                    rx = ((adjustedY * 100000) / pictureBox1.Height) / 100000;     // Number of photo pixels per picturebox pixels height
                    newx = ZoomRec.X + (deltaX * wx);
                    newy = ZoomRec.Y + (deltaY * rx);

                    if (newx + ZoomRec.Width > img.Width)
                        newx = x.Width - ZoomRec.Width;
                    if (newy + ZoomRec.Height > img.Height)
                        newy = x.Height - ZoomRec.Height;
                    if (newx < 0)
                        newx = ZoomRec.X;
                    if (newy < 0)
                        newy = ZoomRec.Y;

                    try
                    {
                        ZoomRec = new Rectangle(newx, newy,
                            (img.Width * (100 - ZoomLevel)) / 100, (img.Height * (100 - ZoomLevel)) / 100);
                        pictureBox1.Image = x.Clone(ZoomRec, x.PixelFormat);
                        pictureBox1.Refresh();
                        x.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        x.Dispose();
                    }
                }
            }
            else  if (cropping)
            {
                if (mouseDown == true)
                {
                    pictureBox1.Refresh();
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

                        pictureBox1.CreateGraphics().DrawRectangle(pen, rec);
                    }
                    rectCropArea = rec;
                }
            }
        }
    }
}


public class LogWriter
{
    private string m_exePath = string.Empty;
    public LogWriter(string logMessage)
    {
        LogWrite(logMessage);
    }
    public void LogWrite(string logMessage)
    {
        m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        try
        {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
            {
                Log(logMessage, w);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    public void Log(string logMessage, TextWriter txtWriter)
    {
        try
        {
            txtWriter.Write("\r\n");
            txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            txtWriter.WriteLine(logMessage);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }
}

