using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ImageTools
{
    public partial class Form1 : Form
    {
        private ImageList imgList = new ImageList() { ImageSize = new Size(60, 60) };

        public Form1()
        {
            InitializeComponent();
            GC.Collect(2, GCCollectionMode.Optimized);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //listView1.VirtualMode = true;           
        }

        private void BrowserDialogOrigem(object sender, EventArgs e)
        {
            DialogResult result_origem = folderBrowserOrigem.ShowDialog();
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imgList;
            int qtdeImagens = Directory.GetFiles(folderBrowserOrigem.SelectedPath).Length;

            if (result_origem == DialogResult.OK)
            {
                boxOrigem.Text = folderBrowserOrigem.SelectedPath;
                boxDestino.Text = folderBrowserOrigem.SelectedPath + "\\" + Guid.NewGuid().ToString();
                lbQtde.Text = "Total de imagens na pasta: " + qtdeImagens.ToString();
                listView1.VirtualListSize = qtdeImagens;
            }
        }

        private void BrowserDialogDestino(object sender, EventArgs e)
        {
            DialogResult result_destino = folderBrowserDestino.ShowDialog();

            if (result_destino == DialogResult.OK)
            {
                boxDestino.Text = folderBrowserDestino.SelectedPath;
            }
        }

        private void AddImageToPreview(string Path)
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new MethodInvoker(delegate
                {
                    
                    if(listView1.Items.Count >= 9) { listView1.Clear(); GC.Collect(1, GCCollectionMode.Forced); }

                    string name = System.IO.Path.GetFileName(Path);
                    imgList.Images.Add(name, Image.FromFile(Path));

                    listView1.BeginUpdate();

                    ListViewItem lv = listView1.Items.Add(name, name);
                    lv.Tag = name;

                    listView1.EndUpdate();
                    
                }));
            }
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(e.ItemIndex.ToString());
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string path_origem = boxOrigem.Text;
                string path_destino = boxDestino.Text;
                string[] files = Directory.GetFiles(path_origem);

                if (!Directory.Exists(path_destino))
                {
                    Directory.CreateDirectory(path_destino);
                }

                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;

                btnEnviar.Enabled = false;

                int qualidade = Convert.ToInt32(cbQualidade.Text.Replace("%", "").ToString());

                ThreadStart thSt = delegate
                {
                    Thread.Sleep(500);

                    StepByStepCompress(files, path_destino, qualidade);
                };

                Thread th = new Thread(thSt);
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StepByStepCompress(object files, object path_destino, object qualidade)
        {
            int numberOfCores = Convert.ToInt32(Math.Round(Environment.ProcessorCount * 0.8, MidpointRounding.AwayFromZero));
            Thread[] ManyTh = new Thread[numberOfCores];

            int qtdeImagens = Directory.GetFiles(folderBrowserOrigem.SelectedPath).Length;
            int qtdeProcessImages = 0;

            foreach (string file in (string[])files)
            {

                if (File.Exists(file) &&  (new FileInfo(file).Length > 0))
                {

                    CompressImageAndSave(file, (string)path_destino, (int)qualidade);

                    AddImageToPreview((string)path_destino + "\\" + Path.GetFileName(file));

                    qtdeProcessImages++;

                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke(new MethodInvoker(delegate
                        {
                            progressBar1.Value = qtdeProcessImages;
                        }));
                    }

                    Thread.Sleep(100);
                }
            }

            if (btnEnviar.InvokeRequired)
            {
                btnEnviar.Invoke(new MethodInvoker(delegate
                {
                    btnEnviar.Enabled = true;
                }));
            }
        }

        public void CompressImageAndSave(string path_image, string destino, int qualidade)
        {
            try
            {
                using Bitmap bmp = new Bitmap(path_image);

                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualidade);
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                bmp.Save(destino + "\\" + Path.GetFileName(path_image), jpegCodec, encoderParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Drawing.Bitmap ResizeImage(string path, string destino)
        {
            try
            {
                System.Drawing.Bitmap original_image = new System.Drawing.Bitmap(path);
                decimal porcent = decimal.Parse(cbQualidade.SelectedItem.ToString());

                int width = original_image.Width;
                int target_width = Convert.ToInt32(width * (porcent / 100));

                int height = original_image.Height;
                int target_height = Convert.ToInt32(height * (porcent / 100));

                int new_width, new_height;

                if (height <= target_height && width <= target_width)
                {
                    new_height = height;
                    new_width = width;
                }
                else
                {
                    float target_ratio = (float)target_width / (float)target_height;
                    float image_ratio = (float)width / (float)height;

                    if (target_ratio > image_ratio)
                    {
                        new_height = target_height;
                        new_width = (int)Math.Floor(image_ratio * (float)target_height);
                    }
                    else
                    {
                        new_height = (int)Math.Floor((float)target_width / image_ratio);
                        new_width = target_width;
                    }

                    new_width = new_width > target_width ? target_width : new_width;
                    new_height = new_height > target_height ? target_height : new_height;
                }

                System.Drawing.Bitmap final_image = new System.Drawing.Bitmap(new_width, new_height);
                System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(final_image);
                graphic.FillRectangle
                    (
                        new System.Drawing.SolidBrush(System.Drawing.Color.Transparent),
                        new System.Drawing.Rectangle(0, 0, target_width, target_height)
                    );

                int paste_x = (target_width - new_width) / 2;
                int paste_y = (target_height - new_height) / 2;

                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                graphic.DrawImage(original_image, 0, 0, new_width, new_height);

                if (graphic != null) graphic.Dispose();

                if (original_image != null) original_image.Dispose();

                return final_image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }

    }
}
