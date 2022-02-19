using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using System.Data.SQLite;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
//IMPORTAÇÕES PARA RECONHECIMENTO DE FALA(Api Microsoft Speech)
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
//Para sintetização de fala
using SpeechSynthesizer = System.Speech.Synthesis.SpeechSynthesizer;

namespace robo1._3
{
    public partial class Form1 : Form
    {        

        public static SpeechSynthesizer synthVoice2;
        private static SpeechSynthesizer sintetizador2 = new SpeechSynthesizer();
        static SQLiteConnection sqlCon = null;
        string strCon = "Data Source=DGVDB.sdb; Version=3; New=False; Compress = true;";
        string strSql = string.Empty;

        public static string
           nameCid, diaSemana, descricao, city = "", key = "21625a0b", linkWemos = "Nada",
           tempoAtual, umidade, velocidadeVento,sourceMusic, seParoudeFalar, data = "",
           recon, reiniciarCond, CondDes, condCancel, condMusica, porDoSol,
           kj, calado, respCalado;

        public static int ultimoValor;
        public static bool isStopped;
        public static Label rotulo;

        public Form1()
        {
            InitializeComponent();                      
        }
        /* Para verificar estado do servidor
         * Envia a url para o servidor contendo o link do ngrok para automação */
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
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
                return null;
            }
        }

        //Remover acentos das frases para um melhor reconhecimento de comandos
        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        //Primeira letra da frase em maiusculo
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        #region recognition on
        /* Para reconhecer do microphone:
         * (Usando o metodo reconhecimento continuo apresentou falhas ao reconhecer
         * com varias pessoas falando ao mesmo tempo. pode-se aprimorar isso e melhorar o
         * reconhecimento mas achei mais prático usar o reconhecimento padrão e colocar em 
         * um loop contínuo)
         */
        private async static Task FromMic(SpeechConfig speechConfig)
        {
            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            speechConfig.SpeechRecognitionLanguage = "pt-BR";
            var recognizer = new SpeechRecognizer(speechConfig, audioConfig);

            synthVoice2 = new SpeechSynthesizer();
            synthVoice2.Rate = 0;
            synthVoice2.Volume = 100;
            synthVoice2.SelectVoice("IVONA 2 Ricardo");
            Comdon comdon = new Comdon();

            var result = await recognizer.RecognizeOnceAsync();
            while (true)
            {
                //Armazenar o resultado do reconhecimento
                result = await recognizer.RecognizeOnceAsync();
                if(result != null)//Se for nulo não faça nada
                {
                    if (result.Text.Contains("Para de falar"))
                    {
                        synthVoice2.SpeakAsyncCancelAll();
                    }
                    Console.WriteLine($"RECONHECIDO: Texto={result.Text}");
                }

                #region luz do quarto
                //LUZ Quarto
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("apaga") && result.Text.ToLower().Contains("quarto") || result.Text.ToLower().Contains("desliga") && result.Text.ToLower().Contains("luz") && result.Text.ToLower().Contains("quarto"))
                    {
                        //Obtenha o texto da pagina de automação
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO14/D5 - State off"))//se a luz ja estiver desligada
                            {
                                //Diga
                                synthVoice2.SpeakAsync("Eu ja havia desligado senhor.");
                            }
                            else//Se não estiver desligada
                            {
                                //Desligue a luz
                                string urlAddress1 = linkWemos + "/14/off/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("Luz desligada senhor");
                            }
                        }
                        data = "";
                        result = null;
                        Console.WriteLine();
                    }
                }
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("acende") && result.Text.ToLower().Contains("quarto") || result.Text.ToLower().Contains("liga") && result.Text.ToLower().Contains("luz") && result.Text.ToLower().Contains("quarto"))
                    {
                        Console.WriteLine(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO14/D5 - State on"))
                            {
                                synthVoice2.SpeakAsync("Eu ja havia ligado senhor.");
                            }
                            else
                            {
                                string urlAddress1 = linkWemos + "/14/on/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("Luz ligada senhor");
                            }
                        }

                        data = "";
                        result = null;
                        Console.WriteLine();

                        Console.WriteLine();
                    }
                }
                #endregion

                #region tv
                //TV
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("desliga") && result.Text.ToLower().Contains("tv") || result.Text.ToLower().Contains("desliga") && result.Text.ToLower().Contains("televisão") || result.Text.ToLower().Contains("desligue") && result.Text.ToLower().Contains("tv"))
                    {
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO5/D3 - State off"))
                            {
                                synthVoice2.SpeakAsync("Eu ja havia desligado senhor.");
                            }
                            else
                            {
                                string urlAddress1 = linkWemos + "/5/off/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("Tv desligada senhor");
                            }
                        }
                        data = "";
                        result = null;
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("liga") && result.Text.ToLower().Contains("tv") || result.Text.ToLower().Contains("liga") && result.Text.ToLower().Contains("televisão") || result.Text.ToLower().Contains("ligue") && result.Text.ToLower().Contains("tv"))
                    {
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO5/D3 - State on"))
                            {
                                synthVoice2.SpeakAsync("Eu ja havia ligado senhor.");
                            }
                            else
                            {
                                string urlAddress1 = linkWemos + "/5/on/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("tv ligada senhor");
                            }
                        }
                        data = "";
                        result = null;
                        Console.WriteLine();
                    }
                }
                #endregion

                #region tomada
                //TOMADA
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("desliga") && result.Text.ToLower().Contains("tomada") && result.Text.ToLower().Contains("um") || result.Text.ToLower().Contains("desligue") && result.Text.ToLower().Contains("tomada") && result.Text.ToLower().Contains("um"))
                    {
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO13/D7 - State off"))
                            {
                                synthVoice2.SpeakAsync("Eu ja havia desligado senhor.");
                            }
                            else
                            {
                                string urlAddress1 = linkWemos + "/13/off/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("tomada desligada senhor");
                            }
                        }
                        data = "";
                        result = null;
                        Console.WriteLine();
                    }
                }
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("liga") && result.Text.ToLower().Contains("tomada") && result.Text.ToLower().Contains("um") || result.Text.ToLower().Contains("ligue") && result.Text.ToLower().Contains("tomada") && result.Text.ToLower().Contains("um"))
                    {
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO13/D7 - State on"))
                            {
                                synthVoice2.SpeakAsync("Eu ja havia ligado senhor.");
                            }
                            else
                            {
                                string urlAddress1 = linkWemos + "/13/on/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("tomada ligada senhor");
                            }
                        }
                        data = "";
                        result = null;
                        Console.WriteLine();
                    }
                }
                #endregion

                #region porta
                //PORTA
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("fecha") && result.Text.ToLower().Contains("porta") || result.Text.ToLower().Contains("feche") && result.Text.ToLower().Contains("porta") || result.Text.ToLower().Contains("fechar") && result.Text.ToLower().Contains("porta"))
                    {
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO12/D6 - State off"))
                            {
                                synthVoice2.SpeakAsync("Eu ja havia fechado senhor.");
                            }
                            else
                            {
                                string urlAddress1 = linkWemos + "/12/off/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("Porta fechada senhor");
                            }
                        }
                        data = "";
                        result = null;
                        Console.WriteLine();
                    }
                }
                if (result != null)
                {
                    if (result.Text.ToLower().Contains("abre") && result.Text.ToLower().Contains("porta") || result.Text.ToLower().Contains("abra") && result.Text.ToLower().Contains("porta") || result.Text.ToLower().Contains("abrir") && result.Text.ToLower().Contains("porta"))
                    {
                        data = GetString(linkWemos + "/SmFydmlzODA6MTIzNDU2");
                        if (string.IsNullOrEmpty(data))
                        {
                            //
                        }
                        else
                        {
                            if (data.Contains("GPIO12/D6 - State on"))
                            {
                                synthVoice2.SpeakAsync("Eu ja havia aberto senhor.");
                            }
                            else
                            {
                                string urlAddress1 = linkWemos + "/12/on/SmFydmlzODA6MTIzNDU2/";
                                GetString(urlAddress1);
                                synthVoice2.SpeakAsync("Porta aberta senhor");
                            }
                        }
                        data = "";
                        result = null;
                        Console.WriteLine();
                    }
                }
                #endregion

                #region spotify
                //Spotify
                if (result != null)
                {
                    /* 
                     * Para o spotify usei spotipy biblioteca em python
                     * um script para tocar musicas
                     * outro para parar e outro para avançar e voltar
                     */
                    string mus = "";
                    string tk = RemoveAccents(result.Text.ToLower());
                    if (tk.Contains("tocar"))
                    {
                        mus = tk.Replace("tocar", "toca");
                    }
                    else
                    {
                        mus = RemoveAccents(result.Text.ToLower());
                    }

                    if (mus.Contains("para") && mus.Contains("toca"))
                    {
                        Process cmd = new Process();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.Start();

                        //Iniciar a aplicação spotify python script para parar a musica
                        cmd.StandardInput.WriteLine("python spot.py ");
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.Close();
                        cmd.WaitForExit();
                        Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                    }

                    if (mus.Contains("proxima") && mus.Contains("musica"))
                    {
                        Process cmd = new Process();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.Start();

                        //Iniciar a aplicação spotify python script pular para proxima musica
                        cmd.StandardInput.WriteLine("python next.py ");
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.Close();
                        cmd.WaitForExit();
                        Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                    }

                    if (mus.Contains("musica") && mus.Contains("anterior") || mus.Contains("volta") && mus.Contains("musica"))
                    {
                        Process cmd = new Process();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.Start();

                        //Iniciar a aplicação spotify python script para voltar a musica anterior
                        cmd.StandardInput.WriteLine("python previous.py ");
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.Close();
                        cmd.WaitForExit();
                        Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                    }

                    if (mus.Contains("toca"))
                    {
                        string[] mus0 = mus.Split(new string[] { "toca" }, StringSplitOptions.None);
                        string mus1 = mus0[1];

                        if (mus1.Contains("musica"))
                        {
                            //Obter so o nome do artista falado pelo usuario
                            string[] mus2 = mus.Split(new string[] { "musica" }, StringSplitOptions.None);
                            string mus3 = mus0[1];
                            string[] mus4 = mus3.Split(' ');
                            string doda = mus4[0];
                            string music = mus4[1];
                            if (doda == "da")
                            {
                                //data.Split(new string[] { "xx" }, StringSplitOptions.None);
                                string[] ol = mus.Split(new string[] { "musica da" }, StringSplitOptions.None);
                                string olF = ol[1];
                                string itik = "";
                                if (olF.Contains("."))
                                {
                                    itik = olF.Replace(".", "");
                                }
                                else
                                {
                                    itik = olF;
                                }

                                Process cmd = new Process();
                                cmd.StartInfo.FileName = "cmd.exe";
                                cmd.StartInfo.RedirectStandardInput = true;
                                cmd.StartInfo.RedirectStandardOutput = true;
                                cmd.StartInfo.CreateNoWindow = true;
                                cmd.StartInfo.UseShellExecute = false;
                                cmd.Start();

                                //Iniciar a aplicação spotify python script para iniciar a musica
                                cmd.StandardInput.WriteLine("python sp.py " + '"' + FirstCharToUpper(itik.ToUpper()) + '"');
                                cmd.StandardInput.Flush();
                                cmd.StandardInput.Close();
                                cmd.WaitForExit();
                                Console.WriteLine(cmd.StandardOutput.ReadToEnd());

                                mus = "";                                

                            }

                            if (doda == "do")
                            {
                                string[] ol = mus.Split(new string[] { "musica do" }, StringSplitOptions.None);
                                string olF = ol[1];
                                string itik = "";
                                if (olF.Contains("."))
                                {
                                    itik = olF.Replace(".", "");
                                }
                                else
                                {
                                    itik = olF;
                                }

                                Process cmd = new Process();
                                cmd.StartInfo.FileName = "cmd.exe";
                                cmd.StartInfo.RedirectStandardInput = true;
                                cmd.StartInfo.RedirectStandardOutput = true;
                                cmd.StartInfo.CreateNoWindow = true;
                                cmd.StartInfo.UseShellExecute = false;
                                cmd.Start();

                                cmd.StandardInput.WriteLine("python sp.py " + '"' + FirstCharToUpper(itik.ToUpper()) + '"');
                                cmd.StandardInput.Flush();
                                cmd.StandardInput.Close();
                                cmd.WaitForExit();
                                Console.WriteLine(cmd.StandardOutput.ReadToEnd());

                                mus = "";
                                //Process.Start("python sp.py projota").WaitForExit();
                            }
                        }
                        string[] verfy1word = mus.Split(' ');
                        if (verfy1word[0] == "toca")
                        {
                            string[] ol1 = mus.Split(new string[] { "toca" }, StringSplitOptions.None);
                            string olF1 = ol1[1];
                            string itik1 = "";
                            if (olF1.Contains("."))
                            {
                                itik1 = olF1.Replace(".", "");
                            }
                            else
                            {
                                itik1 = olF1;
                            }

                            Process cmd1 = new Process();
                            cmd1.StartInfo.FileName = "cmd.exe";
                            cmd1.StartInfo.RedirectStandardInput = true;
                            cmd1.StartInfo.RedirectStandardOutput = true;
                            cmd1.StartInfo.CreateNoWindow = true;
                            cmd1.StartInfo.UseShellExecute = false;
                            cmd1.Start();

                            cmd1.StandardInput.WriteLine("python sp.py " + '"' + FirstCharToUpper(itik1.ToUpper()) + '"');
                            cmd1.StandardInput.Flush();
                            cmd1.StandardInput.Close();
                            cmd1.WaitForExit();
                            Console.WriteLine(cmd1.StandardOutput.ReadToEnd());
                        }
                    }

                    //Obtenha a resposta se tiver reconhecido alguma palavra chave
                    //do bd sqlite
                    string responseOn = comdon.GetResponse(result.Text);
                    if (responseOn != null && responseOn != "")
                    {
                        //se ouver respostas fale:
                        synthVoice2.SpeakAsync(responseOn);
                    }
                    responseOn = "";
                    comdon.response = "";

                    result = null;
                }
                #endregion
            }
        }
        #endregion

        //Para a sintetização de fala
        #region sintetizador de fala
        public static void speak02(string content = "")
        {
            try
            {
                sintetizador2.SelectVoice("IVONA 2 Ricardo");
                sintetizador2.Volume = 90;
                sintetizador2.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(synthesizer_SpeakProgress2);
                sintetizador2.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(synthesizer_SpeakCompleted2);
                sintetizador2.SetOutputToDefaultAudioDevice();
                sintetizador2.SpeakAsync(content);
            }
            catch (InvalidOperationException e) { Console.WriteLine(e.Message); }
        }

        //Metodo para verificar estado do sintetizador/ 
        //esperar até que termine de falar e pode então responder a outro comando
        private static void synthesizer_SpeakCompleted2(object sender, SpeakCompletedEventArgs e)
        {
            Console.WriteLine("SpeakProgress: AudioPosition= CharacterPosition=,\tCharacterCount=,\tText=");
        }
        private static void synthesizer_SpeakProgress2(object sender, SpeakProgressEventArgs e)
        {
            Console.WriteLine("SpeakProgress: AudioPosition=" + e.AudioPosition + ",\tCharacterPosition=" + e.CharacterPosition + ",\tCharacterCount=" + e.CharacterCount + ",\tText=" + e.Text);
        }
        
        //Parar a fala
        public static void stop2()
        {
            try
            {
                sintetizador2.SpeakAsyncCancelAll();
            }
            catch (ObjectDisposedException) { }
        }
        #endregion        

        //Checar se tem conexão com a internet
        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    WebProxy wp = new WebProxy();
                    client.Proxy = wp;
                    using (var stream = client.OpenRead("http://localhost:4040/api/tunnels"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        //Iniciar servidor
        private bool iniciaServ()
        {
            //https://42c1-52-67-118-77.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&img=nao&urlo=https://teste.teste
            string ret = GetString("https://ebc4-52-67-118-77.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&img=nao&urlo=" + linkWemos);
            if (ret.Contains("https"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region eventos form
        private void Form1_Shown(object sender, EventArgs e)
        {
            bool cond = CheckForInternetConnection();
            while (cond == false)
            {
                cond = CheckForInternetConnection();
            }

            //Process.Start("curl  http://localhost:4040/api/tunnels > tunnels.json");
            Process cmd = new Process();
            cmd.StartInfo.FileName = "getUrl.bat";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.Start();
            try
            {
                string jsonFilePath = "tunnels.json";
                string json = File.ReadAllText(jsonFilePath);
                string[] subsJson = json.Split(',');
                string i1 = subsJson[25];
                string lkw = i1.Replace("\"", "").Replace("public_url:", "");

                Console.WriteLine("KWemos:" + i1);
                linkWemos = lkw;//i1.Replace("\"", "").Replace("public_url:", "");
                Console.WriteLine("WEMOS:" + linkWemos);

            }
            catch
            {
                MessageBox.Show("Não foi possivel obter o link publico Ngrok");
            }

            string servinit = GetString("https://ebc4-52-67-118-77.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&img=nao&urlo=" + linkWemos);
            if (servinit.Contains("https"))
            {
                //
            }
            else
            {
                MessageBox.Show("Erro no servidor do bot.");
                MessageBox.Show("Não foi possivel iniciar servidor clique em: Atualizar servidor");
            }

        }
        private void panelMenu_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            int posX = Cursor.Position.X;
            int posY = Cursor.Position.Y;

            Console.WriteLine("Current mouse position in form's area is " +
                (Control.MousePosition.X - this.Location.X - 8).ToString() +
                "x" +
                (Control.MousePosition.Y - this.Location.Y - 30).ToString()
            );

            if (Control.MousePosition.X - this.Location.X - 8 >= 213)
            {
                panelMenu.Visible = false;
            }
        }
        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
        }
        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
        }       

        //form Load        
        private async void Form1_Load(object sender, EventArgs e)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "startNgrok.bat";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.Start();

            rotulo = labelRotulo;

            panelContent.Visible = true;
            panelContent.Controls.Clear();
            Inicio inicio = new Inicio();
            inicio.TopLevel = false;
            panelContent.Controls.Add(inicio); //AQUI
            inicio.FormBorderStyle = FormBorderStyle.None;
            inicio.Dock = DockStyle.Fill;
            inicio.Show();

            var speechConfig = SpeechConfig.FromSubscription("810ded2b10834a809285ff49df04d93b", "brazilsouth");
            await FromMic(speechConfig);
            //
        }
       
        //form closed
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           //
        }
        #endregion

        #region botoes
        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckForInternetConnection())
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
                    string[] subsJson = json.Split(',');
                    string i1 = subsJson[25];
                    string lkw = i1.Replace("\"", "").Replace("public_url:", "");
                    
                    Console.WriteLine("KWemos:" + i1);
                    linkWemos = lkw;//i1.Replace("\"", "").Replace("public_url:", "");
                    Console.WriteLine("WEMOS:" + linkWemos);
                }
                catch
                {
                    MessageBox.Show("Não foi possivel obter o link publico Ngrok");
                }

                string servinit = GetString("https://ebc4-52-67-118-77.ngrok.io/b5554efe14d4c89f59869d6c33c8e9410fe94802268b43f807/?hub.verify_token=1144&hub.challenge=OK%20MANO&img=nao&urlo=" + linkWemos + "/SmFydmlzODA6MTIzNDU2");//https://3832-177-72-23-86.ngrok.io/SmFydmlzODA6MTIzNDU2
                Console.WriteLine("AQUI:" + servinit);
                if (servinit.Contains("https"))
                {
                    //
                }
                else
                {
                    MessageBox.Show("Erro no servidor do bot.");
                    MessageBox.Show("Não foi possivel iniciar servidor clique em: Atualizar servidor");
                }

                if (iniciaServ())
                {
                    MessageBox.Show("Servidor atualizado");
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar servidor");
                }
            }
            else
            {
                MessageBox.Show("sem conexão com a internet");
            }

        }

        //Btn menu
        private void bunifuMenu_Click(object sender, EventArgs e)
        {
            

            if (panelMenu.Visible == Visible)
            {
                panelMenu.Visible = false;
            }
            else
            {
                panelMenu.Visible = true;
                this.panelMenu.BringToFront();
                //panelContent.SendToBack();
            }
        }

        //btn close
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        //btn max
        private void btnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                btnMax.Image = Image.FromFile("img1/minimize.png");
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                btnMax.Image = Image.FromFile("img1/icons8_full_screen_48px.png");
                WindowState = FormWindowState.Normal;
            }
        }

        //btn minimize
        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //Btn page inicio
        private void bunifuFlatButton3_Click_1(object sender, EventArgs e)
        {
            panelMenu.Visible = false;

            panelContent.Visible = true;
            panelContent.Controls.Clear();
            Inicio inicio = new Inicio();
            inicio.TopLevel = false;
            panelContent.Controls.Add(inicio); //AQUI
            inicio.FormBorderStyle = FormBorderStyle.None;
            inicio.Dock = DockStyle.Fill;
            inicio.Show();
            panelMenu.Visible = false;

        }

        //Btn arduino
        private void btnArduino(object sender, EventArgs e)
        {
            panelMenu.Visible = false;

            panelContent.Visible = true;
            panelContent.Controls.Clear();
            Spotify spotfy = new Spotify();
            spotfy.TopLevel = false;
            panelContent.Controls.Add(spotfy); //AQUI
            spotfy.FormBorderStyle = FormBorderStyle.None;
            spotfy.Dock = DockStyle.Fill;
            spotfy.Show();
        }

        //Btn sociais
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelContent.Visible = true;
            panelContent.Controls.Clear();
            Soc soc = new Soc();
            soc.TopLevel = false;
            panelContent.Controls.Add(soc); //AQUI
            soc.FormBorderStyle = FormBorderStyle.None;
            soc.Dock = DockStyle.Fill;
            soc.Show();
        }

        //Btn predefinidos
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelContent.Visible = true;
            panelContent.Controls.Clear();
            Predef predef = new Predef();
            predef.TopLevel = false;
            panelContent.Controls.Add(predef); //AQUI
            predef.FormBorderStyle = FormBorderStyle.None;
            predef.Dock = DockStyle.Fill;
            predef.Show();
        }

        //Btn configurações de camera
        private void butonCam_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;

        }

        //hiding menu when clicking away
        private void panelContent_MouseDown(object sender, MouseEventArgs e)
        {
            panelMenu.Visible = false;
        }

        [DllImport("user32")]
        private static extern bool ReleaseCapture();

        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wp, int lp);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panelMenu.Visible = false;
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }

        private void panelMenu_MouseDown(object sender, MouseEventArgs e)
        {
            panelMenu.Visible = false;
        }
        #endregion
    }
}