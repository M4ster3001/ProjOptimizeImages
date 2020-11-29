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
        private Uteis uteis = new Uteis();
        private ImageList imgList = new ImageList() { ImageSize = new Size(60, 60) };
        private int numberOfCores = Convert.ToInt32(Math.Round(Environment.ProcessorCount * 0.8, MidpointRounding.AwayFromZero));

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

            if (result_origem == DialogResult.OK)
            {
                int qtdeImagens = Directory.GetFiles(folderBrowserOrigem.SelectedPath).Length;

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

        private void AddImageToPreview(string fileName)
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new MethodInvoker(delegate
                {

                    if (listView1.Items.Count >= 9) { listView1.Clear(); GC.Collect(1, GCCollectionMode.Forced); }

                    if(File.Exists(Session.Path_destino + "\\" + fileName) && (new FileInfo(Session.Path_destino + "\\" + fileName).Length > 0))
                    {

                        string name = System.IO.Path.GetFileName(Session.Path_destino + "\\" + fileName);
                        imgList.Images.Add(name, Image.FromFile(Session.Path_destino + "\\" + fileName));

                        listView1.BeginUpdate();

                        ListViewItem lv = listView1.Items.Add(name, name);
                        lv.Tag = name;

                        listView1.EndUpdate();
                    }

                }));
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                timerBar.Enabled = true;
                string path_origem = boxOrigem.Text;
                string path_destino = boxDestino.Text;

                if (cbQualidade.Text == "") { MessageBox.Show("Selecione a qualidade"); return; }
                if (path_origem == "") { MessageBox.Show("Selecione a pasta de origem"); return; }
                if (path_destino == "") { MessageBox.Show("Selecione a pasta de destino"); return; }

                Session.Path_origem = path_origem;
                Session.Path_destino = path_destino;

                int qualidade = Convert.ToInt32(cbQualidade.Text.Replace("%", "").ToString());

                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = Directory.GetFiles(Session.Path_origem).Length;

                Session.QtdeImagens = Directory.GetFiles(Session.Path_origem).Length;
                Session.QtdeProcessImagens = 0;
                Session.TypeCompression = cbCompression.Text;

                btnEnviar.Enabled = false;

                Thread th = new Thread(GerenciamentoThreads);
                th.Name = "ThreadBackground";
                th.IsBackground = true;
                th.Start(qualidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GerenciamentoThreads(object qualidade)
        {
            try
            {
                Int32 index = 0, interval = 0, limit = 0;
                Thread[] AvaiablesThreads = new Thread[numberOfCores];

                List<string> files = new List<string>(Directory.GetFiles(Session.Path_origem));
                List<Dictionary<string, string>> filesPerCore = new List<Dictionary<string, string>>();

                uteis.ChecarDirectory(Session.Path_destino);

                interval = (Int32)Math.Round((double)(files.Count / (double)numberOfCores), MidpointRounding.AwayFromZero);

                for (int t = 0; t < numberOfCores; t++)
                {

                    limit = (files.Count - index >= interval ? interval : files.Count - index);
                    Dictionary<string, string> list = new Dictionary<string, string>();

                    list.Add("Files", String.Join(",", files.GetRange(index, limit).ToArray()));
                    list.Add("Qualidade", (string)qualidade.ToString());

                    filesPerCore.Add(list);

                    if (t + 1 != numberOfCores)
                        index += interval;
                }

                #region
                //for (int t = 0; t < numberOfCores; t++)
                //{

                //    limit = (files.Count - index >= interval ? interval : files.Count - index - 1);

                //    AvaiablesThreads[t] = new Thread(() => StepByStepCompress(files.GetRange(index, limit), (string)path_destino, (int)qualidade))
                //    {
                //        Name = t.ToString()
                //    };

                //    AvaiablesThreads[t].IsBackground = true;
                //    AvaiablesThreads[t].Start();

                //    if (t + 1 != numberOfCores)
                //        index += interval;
                //}

                //for (int t = 0; t < numberOfCores; t++)
                //{
                //    Int32 actualSize = Math.Min(interval, files.Count - index - 1);

                //    limit = (files.Count + interval <= files.Count ? interval : files.Count - index - 1);
                //    var teste = files.GetRange(index, actualSize);

                //    AvaiablesThreads[t] = new Thread(() => StepByStepCompress(files.GetRange(index, actualSize), (string)path_destino, (int)qualidade))
                //    {
                //        Name = t.ToString()
                //    };

                //    AvaiablesThreads[t].Start();

                //    if (t + 1 != numberOfCores)
                //        index += interval;
                //}
                #endregion

                for (int t = 0; t < numberOfCores; t++)
                {
                    AvaiablesThreads[t] = new Thread(StepByStepCompress);
                    AvaiablesThreads[t].Name = (t + 1).ToString();
                    AvaiablesThreads[t].Start(filesPerCore[t]);
                }

                Thread.Sleep(500);

                for (int t = 0; t < numberOfCores; t++)
                {
                    AvaiablesThreads[t].Join();
                }

                if (btnEnviar.InvokeRequired)
                {
                    btnEnviar.Invoke(new MethodInvoker(delegate
                    {
                        btnEnviar.Enabled = true;
                        timerBar.Enabled = false;
                        //MessageBox.Show("Concluido");
                    }));
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void StepByStepCompress(object list_dados)
        {

            try
            {
                Dictionary<string, string> dados = (Dictionary<string, string>)list_dados;

                List<string> files = new List<string>(dados["Files"].Split(","));
                Int32 qualidade = Convert.ToInt32(dados["Qualidade"]);

                foreach (string file in (List<string>)files)
                {

                    if (File.Exists(file) && (new FileInfo(file).Length > 0))
                    {
                        string fileName = Path.GetFileName(file);

                        if (File.Exists(Session.Path_destino + "\\" + fileName) && (new FileInfo(Session.Path_destino + "\\" + fileName).Length > 0))
                        {
                            fileName = Guid.NewGuid().ToString() + "_" + fileName;
                        }

                        //CompressImageAndSave(file, fileName, qualidade);
                        uteis.CompressAndSave(file, qualidade);

                        AddImageToPreview(fileName);

                        Session.QtdeProcessImagens += 1;

                        Thread.Sleep(100);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CompressImageAndSave(string path_image, string fileName, int qualidade)
        {
            try
            {
                using Bitmap bmp = new Bitmap(path_image);
                string extension = fileName.Split(".")[1].ToLower();

                ImageCodecInfo extensionCodec = GetEncoderInfo("image/jpeg");

                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualidade);
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                Thread tr = Thread.CurrentThread;
                ChecarDirectory(Session.Path_destino + "\\" + tr.Name);
                bmp.Save(Session.Path_destino + "\\" + tr.Name + "\\" + fileName, extensionCodec, encoderParams);

                //bmp.Save(Session.Path_destino + "\\" + fileName, extensionCodec, encoderParams);
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

        private void ChecarDirectory(string path_destino)
        {
            if (!Directory.Exists((string)path_destino))
            {
                Directory.CreateDirectory((string)path_destino);
            }
        }

        private void timerBar_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = Session.QtdeProcessImagens;
        }
    }
}
