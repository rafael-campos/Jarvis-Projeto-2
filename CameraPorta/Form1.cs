using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Net;

namespace testeFormFacRecog
{
    public partial class Form1 : Form
    {
        CameraPorta.ServiceReference1.WebService1SoapClient service1 = new CameraPorta.ServiceReference1.WebService1SoapClient();

        public static SpeechSynthesizer synthVoice;
        private static SpeechSynthesizer sintetizador = new SpeechSynthesizer();
        public static string F1, F2;

        const string SUBSCRIPTION_KEY = "5a0fbfca8aaf47b4b58efe0d611fe405";
        const string ENDPOINT = "https://facerecogjarvis.cognitiveservices.azure.com/";

        //função do aForge pra capturar os dispositivos de video conectados no pc
        FilterInfoCollection filter;

        //para selecionar o dispositivo de video
        VideoCaptureDevice device;


        [DllImport("user32")]
        private static extern bool ReleaseCapture();
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wp, int lp);//p. evento arrastar janela
        public static string linkWemos;

        public Form1()
        {
            InitializeComponent();
        }

        //Sintetizar fala
        public static void speak01(string content = "")
        {
            try
            {
                sintetizador.SelectVoice("IVONA 2 Ricardo");
                sintetizador.Volume = 100;
                sintetizador.Rate = 0;
                sintetizador.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(synthesizer_SpeakProgress);
                sintetizador.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(synthesizer_SpeakCompleted);
                sintetizador.SetOutputToDefaultAudioDevice();
                sintetizador.SpeakAsync(content);
                //sintetizador.SelectVoice("Microsoft Irina Desktop");
            }
            catch (InvalidOperationException e) { Console.WriteLine(e.Message); }
        }

        //capturar posição da fala
        private static void synthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            Console.WriteLine("SpeakProgress: AudioPosition= CharacterPosition=,\tCharacterCount=,\tText=");
            F1 = "Completo";
        }
        //Pegando a posição do audio/ para saber se está na ultima palavra ou no inicio da frase
        private static void synthesizer_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            Console.WriteLine("SpeakProgress: AudioPosition=" + e.AudioPosition + ",\tCharacterPosition=" + e.CharacterPosition + ",\tCharacterCount=" + e.CharacterCount + ",\tText=" + e.Text);
            //MessageBox.Show(e.AudioPosition.ToString());
            F1 = e.Text;
            //MessageBox.Show(F1);
        }

        private FaceClient _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY)) { Endpoint = ENDPOINT };

        

        int contNident = 0;
        int contIdent = 0;
        int cont = 0;
        int contrst = 0;
        int ntem = 0;
        string me1 = "nao";
        string familia = "nao";
        string mae = "nao";
        string desconhecido = "nao";

        List<string> gids = new List<string>();

        static string GetString(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                Stream resStream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
                string ContentHtml = sr.ReadToEnd();

                return ContentHtml;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Erro: " + ex);
                return null;
            }
        }

        private string data;
        //buscar correspondencia de face
        private async void busca(string imgSrc)
        {
            int counter = 0;
            string line;

            gids.Clear();

            var CurrentDirectory = Directory.GetCurrentDirectory();
            String path = CurrentDirectory + @"\groupId.txt";

            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                gids.Add(line);
                System.Console.WriteLine(line);
                counter++;
            }

            file.Close();
            //PersonGroupId = "leadr";
            string PersonGroupId;
            foreach (string idg in gids)
            {
                PersonGroupId = idg;
                //Console.WriteLine(PersonGroupId);
                using (Stream s = File.OpenRead(imgSrc))
                {

                    try
                    {
                        var faces = await _faceClient.Face.DetectWithStreamAsync(s);
                        var fID = faces.Select(face => face.FaceId).ToArray();

                        List<Guid> sourceFaceIds = new List<Guid>();
                        foreach (var detectedFace in faces) { sourceFaceIds.Add(detectedFace.FaceId.Value); }
                        var results = await _faceClient.Face.IdentifyAsync(sourceFaceIds, PersonGroupId);

                        foreach (var identifyResult in results)
                        {
                            Console.WriteLine("Result face: {0}", identifyResult.FaceId);
                            Console.WriteLine("ESSEEE>=", identifyResult.Candidates.Count);

                            if (identifyResult.Candidates.Count == 0)
                            {
                                contNident++;
                                Console.WriteLine("Não identificado.");
                                //salvar img pasta desconhecidos
                            }
                            else
                            {
                                contIdent++;

                                var candidateId = identifyResult.Candidates[0].PersonId;
                                Person person = await _faceClient.PersonGroupPerson.GetAsync(PersonGroupId, candidateId);


                                Console.WriteLine($"Person '{person.Name}'");
                                
                                if (person.Name == "Rafael")
                                {
                                    me1 = "ME";
                                    try
                                    {
                                        string urlAddress1 = linkWemos + "/12/on/SmFydmlzODA6MTIzNDU2/";
                                        GetString(urlAddress1);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(" " + ex);
                                    }
                                }
                                if (person.Name != "Rafael")
                                {
                                    if(person.Name == "Familia")
                                    {
                                        familia = person.Name;
                                    }   
                                    if(person.Name == "Mae")
                                    {
                                        mae = person.Name;
                                    }
                                    if (person.Name == "Desconhecido")
                                    {
                                        desconhecido = person.Name;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //erro foto não legivel ou erro no servidor.
                        Console.WriteLine("Error: " + ex);
                    }
                } 
                Thread.Sleep(10);
            }

            //
            if (me1 == "ME")
            {               

                if (F2 == null && F1 == null)
                {
                    speak01("Pórta aberta póde entrar!");
                }
                else
                {
                    if (F2 == null && F1 == "Completo")
                    {
                        speak01("Pórta aberta póde entrar!");
                    }
                    else if (F2 == "Completo" && F1 == "Completo")
                    {
                        speak01("Pórta aberta póde entrar!");
                    }
                    else
                    {
                        //
                    }
                }
                
                me1 = "nao";
            }
            if(familia != "nao")
            {
                if (F2 == null && F1 == null)
                {
                    speak01("Olá família, aguárde um instante vou verificar se posso abrir a porta.");
                }
                else
                {
                    if (F2 == null && F1 == "Completo")
                    {
                        speak01("Olá família, aguárde um instante vou verificar se posso abrir a porta.");
                    }
                    else if (F2 == "Completo" && F1 == "Completo")
                    {
                        speak01("Olá família, aguárde um instante vou verificar se posso abrir a porta.");
                    }
                    else
                    {
                        //
                    }
                }

                

                //MessageBox.Show("enviar foto para o face");
                File.WriteAllText("sendImg\\nomePessoa.txt", familia);
                familia = "nao";
                File.WriteAllText("sendImg\\send_img.txt", "sim");

                string getResp = "";
                bool condF = true;
                while (condF)
                {
                    getResp = service1.ResponseFamily();
                    if (getResp != "nada" && getResp != "sim")
                    {
                        if(getResp.ToLower() != "abre a porta")
                        {
                            if (F2 == null && F1 == null)
                            {
                                speak01(getResp);
                                condF = true;
                            }
                            else
                            {
                                if (F2 == null && F1 == "Completo")
                                {
                                    speak01(getResp);
                                    condF = true;
                                }
                                else if (F2 == "Completo" && F1 == "Completo")
                                {
                                    speak01(getResp);
                                    condF = true;
                                }
                                else
                                {
                                    //
                                }
                            }

                            
                        }
                        else
                        {
                            if (F2 == null && F1 == null)
                            {
                                speak01("Porta aberta pode entrar.");
                                condF = true;
                            }
                            else
                            {
                                if (F2 == null && F1 == "Completo")
                                {
                                    speak01("Porta aberta pode entrar.");
                                    condF = true;
                                }
                                else if (F2 == "Completo" && F1 == "Completo")
                                {
                                    speak01("Porta aberta pode entrar.");
                                    condF = true;
                                }
                                else
                                {
                                    //
                                }
                            }

                            
                        }
                    }
                }
            }
            if (mae != "nao")
            {
                if (F2 == null && F1 == null)
                {
                    speak01("Olá senhóra, aguárde um instante vou verificar se posso abrir a porta.");
                }
                else
                {
                    if (F2 == null && F1 == "Completo")
                    {
                        speak01("Olá senhóra, aguárde um instante vou verificar se posso abrir a porta.");
                    }
                    else if (F2 == "Completo" && F1 == "Completo")
                    {
                        speak01("Olá senhóra, aguárde um instante vou verificar se posso abrir a porta.");
                    }
                    else
                    {
                        //
                    }
                }

                

                //MessageBox.Show("enviar foto para o face");
                File.WriteAllText("sendImg\\nomePessoa.txt", mae);
                mae = "nao";
                File.WriteAllText("sendImg\\send_img.txt", "sim");

                string getResp = "";
                bool cond = true;
                while (cond)
                {
                    getResp = service1.ResponseFamily();
                    if (getResp != "nada" && getResp != "sim")
                    {
                        if (getResp.ToLower() != "abre a porta")
                        {
                            if (F2 == null && F1 == null)
                            {
                                speak01(getResp);
                                cond = false;
                            }
                            else
                            {
                                if (F2 == null && F1 == "Completo")
                                {
                                    speak01(getResp);
                                    cond = false;
                                }
                                else if (F2 == "Completo" && F1 == "Completo")
                                {
                                    speak01(getResp);
                                    cond = false;
                                }
                                else
                                {
                                    //
                                }
                            }

                            
                        }
                        else
                        {
                            if (F2 == null && F1 == null)
                            {
                                speak01("Porta aberta senhóra, pode entrar.");
                                cond = false;
                            }
                            else
                            {
                                if (F2 == null && F1 == "Completo")
                                {
                                    speak01("Porta aberta senhóra, pode entrar.");
                                    cond = false;
                                }
                                else if (F2 == "Completo" && F1 == "Completo")
                                {
                                    speak01("Porta aberta senhóra, pode entrar.");
                                    cond = false;
                                }
                                else
                                {
                                    //
                                }
                            }

                            
                        }
                    }
                }
            }
            if (desconhecido != "nao")
            {
                if (F2 == null && F1 == null)
                {
                    speak01("Olá desconhecido, aguárde um instante vou verificar se posso abrir a porta.");
                }
                else
                {
                    if (F2 == null && F1 == "Completo")
                    {
                        speak01("Olá desconhecido, aguárde um instante vou verificar se posso abrir a porta.");
                    }
                    else if (F2 == "Completo" && F1 == "Completo")
                    {
                        speak01("Olá desconhecido, aguárde um instante vou verificar se posso abrir a porta.");
                    }
                    else
                    {
                        //
                    }
                }

                

                //MessageBox.Show("enviar foto para o face");
                File.WriteAllText("sendImg\\nomePessoa.txt", desconhecido);
                desconhecido = "nao";
                File.WriteAllText("sendImg\\send_img.txt", "sim");

                string getResp = "";
                bool condDes = true;
                while (true)
                {
                    getResp = service1.ResponseFamily();
                    if (getResp != "nada" && getResp != "sim")
                    {
                        if (getResp.ToLower() != "abre a porta")
                        {
                            if (F2 == null && F1 == null)
                            {
                                speak01(getResp);
                                condDes = true;
                            }
                            else
                            {
                                if (F2 == null && F1 == "Completo")
                                {
                                    speak01(getResp);
                                    condDes = true;
                                }
                                else if (F2 == "Completo" && F1 == "Completo")
                                {
                                    speak01(getResp);
                                    condDes = true;
                                }
                                else
                                {
                                    //
                                }
                            }

                            
                        }
                        else
                        {
                            if (F2 == null && F1 == null)
                            {
                                speak01("Porta aberta desconhecido, pode entrar.");
                                condDes = true;
                            }
                            else
                            {
                                if (F2 == null && F1 == "Completo")
                                {
                                    speak01("Porta aberta desconhecido, pode entrar.");
                                    condDes = true;
                                }
                                else if (F2 == "Completo" && F1 == "Completo")
                                {
                                    speak01("Porta aberta desconhecido, pode entrar.");
                                    condDes = true;
                                }
                                else
                                {
                                    //
                                }
                            }                            
                        }
                    }
                }
            }
        }

        public string imgSrc = "nada";

        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_default.xml");
        int i = -1;
        private void Form1_Load(object sender, EventArgs e)
        {
            //Process.Start("curl  http://localhost:4040/api/tunnels > tunnels.json");
            Process cmd = new Process();
            cmd.StartInfo.FileName = "getUrl.bat";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.Start();
            try
            {
                string jsonFilePath = "tunnels.json";
                string json = File.ReadAllText(jsonFilePath);
                //Console.WriteLine("Json Tunels: " + json);

                string[] subsJson = json.Split(',');

                foreach (string j in subsJson)
                {
                    if (j.Contains("https:"))
                    {
                        string[] lk = j.Split(new[] { "public_url\":\"" }, StringSplitOptions.None);
                        string lkWemos = lk[1].Replace("\"", "");

                        Console.WriteLine("Link WEMOS: " + lkWemos);
                        linkWemos = lkWemos;
                    }
                    //Console.WriteLine(j);
                }
            }
            catch
            {
                MessageBox.Show("Não foi possivel obter o link publico Ngrok");
            }

            //Process cmd01 = new Process();
            //cmd01.StartInfo.FileName = "sendImg\\verify_if_family_img.py";
            //cmd01.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //cmd01.Start();

            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filter)
            {
                comboBox1.Items.Add(device.Name);
            }
            
            foreach( string itens in comboBox1.Items)
            {
                i++;
                if(itens == "USB Video Device")
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
            device = new VideoCaptureDevice();

            this.Activated += AfterLoading;

            synthVoice = new SpeechSynthesizer();

        }

        private void btDtct()
        {
            if (btnDetect.Text == "Detect")
            {
                btnDetect.Text = "Stop";

                try
                {
                    device = new VideoCaptureDevice(filter[comboBox1.SelectedIndex].MonikerString);
                    device.NewFrame += Device_NewFrame;
                    device.Start();
                }
                catch
                {
                    btnDetect.Text = "Detect";

                    MessageBox.Show("Erro na camera");
                    device.Stop();

                    pic1.Image = null;
                    picSearch.Image = null;
                }

            }
            else
            {
                btnDetect.Text = "Detect";
                device.Stop();

                pic1.Image = null;
                picSearch.Image = null;

                ntem = 0;
                cont = 0;
                contrst = 0;
            }
        }
        private void btnDetect_Click(object sender, EventArgs e)
        {
            ntem = 0;
            cont = 0;
            contrst = 0;

            btDtct();
        }

        private void AfterLoading(object sender, EventArgs e)
        {
           

            this.Activated -= AfterLoading;
            ntem = 0;
            cont = 0;
            contrst = 0;

            var CurrentDirectory = Directory.GetCurrentDirectory();
            String path = CurrentDirectory + @"\sendImg\";
            var startInfo = new ProcessStartInfo("verify_if_family_img.py");
            startInfo.WorkingDirectory = path;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(startInfo);


            btDtct();

            Thread.Sleep(4);

            //this.WindowState = FormWindowState.Minimized;
        }


        private void Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);
            System.Drawing.Size Size = new System.Drawing.Size(120, 40);
            Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.2, 1, Size);

            if(rectangles.Count() > 0)
            {
                ntem = 0;
                //"Tem rosto"
                contrst++;
                if(contrst >= 40)
                {
                    if (cont == 0)
                    {
                        picSearch.SizeMode = PictureBoxSizeMode.Zoom;
                        picSearch.Image = bitmap;
                        Image img = bitmap;

                        Thread.Sleep(400);
                        try
                        {
                            picSearch.Image.Save("sendImg\\imgSendGdriv\\photo.png");
                        }
                        catch
                        {
                            Console.WriteLine("Erro ao salvar imgSendGdriv");
                        }
                        try
                        {
                            picSearch.Image.Save("processandoImg/photo.png");
                        }
                        catch
                        {
                            Console.WriteLine("Erro ao salvar processandoImg");
                        }
                        
                        Thread.Sleep(400);

                        cont = 1;
                        busca("processandoImg/photo.png");
                    }
                }  
            }
            else
            {
                //"Não tem!"
                ntem++;
                if(ntem >= 60)
                {
                    cont = 0;
                    contrst = 0;
                    ntem = 0;
                }
            }
            foreach (Rectangle rectangle in rectangles)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Purple, 8))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }
                }
            }
            pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pic1.Image = bitmap;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            device.Stop();
            pic1.Image = null;
            picSearch.Image = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfCam_Click(object sender, EventArgs e)
        {
            ConfCam confCam = new ConfCam();
            confCam.StartPosition = FormStartPosition.CenterScreen;
            confCam.Show();
            device.Stop();
            btnDetect.Text = "Detect";

            pic1.Image = null;
            picSearch.Image = null;

            ntem = 0;
            cont = 0;
            contrst = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }

        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }
    }
}
