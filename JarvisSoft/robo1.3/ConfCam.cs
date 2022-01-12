using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo1._3
{
    public partial class ConfCam : Form
    {
        [DllImport("user32")]
        private static extern bool ReleaseCapture();
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wp, int lp);//p. evento arrastar janela

        const string SUBSCRIPTION_KEY = "5a0fbfca8aaf47b4b58efe0d611fe405";
        const string ENDPOINT = "https://facerecogjarvis.cognitiveservices.azure.com/";

        public string imgSrc = "nada";
        private FaceClient _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY)) { Endpoint = ENDPOINT };

        int contNident = 0;
        int contIdent = 0;

        int cont = 0;
        int contrst = 0;
        int ntem = 0;
        string pessoaant;
        int contP = 0;

        string me1 = "nao";
        string pgid;
        List<string> gids = new List<string>();


        public ConfCam()
        {
            InitializeComponent();
        }

        private void btnPasta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog choofdlog = new FolderBrowserDialog();

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.SelectedPath;

                string directoryPath = sFileName;

                string srcc = directoryPath;
                txtSelectFolder.Text = srcc;
                imgSrc = srcc;
            }
        }

        private async void createGroup_Click(object sender, EventArgs e)
        {
            string PersonGroupId = txtGroupName.Text;
            string nomeGroup = txtNomePessoa.Text;
            string pasta = txtSelectFolder.Text;

            try
            {
                string groupsIds = File.ReadAllText("groupId.txt");
                string groupsNames = File.ReadAllText("groupId.txt");
                if (groupsIds.Contains(PersonGroupId))
                {
                    MessageBox.Show("Grupo ja existente!");
                }
                else
                {
                    File.AppendAllText("groupId.txt", PersonGroupId + "\n");
                    File.AppendAllText("groupName.txt", nomeGroup + "\n");
                    try
                    {
                        //criar um grupo de amigos
                        await _faceClient.PersonGroup.CreateAsync(PersonGroupId, "MyFriends");


                        Person friend1 = await _faceClient.PersonGroupPerson.CreateAsync(personGroupId: PersonGroupId, name: nomeGroup);


                        foreach (string imagePath in Directory.GetFiles(pasta + "\\", "*jpg"))
                        {
                            using (Stream s = File.OpenRead(imagePath))
                            {
                                try
                                {
                                    await _faceClient.PersonGroupPerson.AddFaceFromStreamAsync(PersonGroupId, friend1.PersonId, s);
                                }
                                catch
                                {
                                    //MessageBox.Show("Error: ");//
                                }
                            }
                        }

                        await _faceClient.PersonGroup.TrainAsync(PersonGroupId);
                        TrainingStatus trainingStatus = null;

                        while (true)
                        {
                            trainingStatus = await _faceClient.PersonGroup.GetTrainingStatusAsync(PersonGroupId);

                            if (trainingStatus.Status.ToString() != "running")
                            {
                                MessageBox.Show("Grupo " + nomeGroup + " criado!");
                                break;
                            }
                            await Task.Delay(1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }

            cbbxGrupID.Items.Clear();

            string[] lineOfContents = File.ReadAllLines("groupId.txt");
            foreach (var line3 in lineOfContents)
            {
                string[] tokens = line3.Split(',');
                cbbxGrupID.Items.Add(tokens[0]);
            }

            cbbxGrupID.SelectedIndex = 0;
        }

        private void btnSelecImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                string[] arrAllFiles = choofdlog.FileNames; //used when Multiselect = true           
            }

            string directoryPath = System.IO.Path.GetDirectoryName(choofdlog.FileName);
            string fileName = System.IO.Path.GetFileName(choofdlog.FileName);

            string srcc = directoryPath + "\\" + fileName;
            txtSorceImg.Text = srcc;
            imgSrc = srcc;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string image = txtSorceImg.Text;
            busca(image);
        }

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
                //System.Console.WriteLine(line);
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
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                    }
                }
                Thread.Sleep(10);
            }
        }

        string line = null;
        int line_number;
        int line_to_delete1;

        int lin;
        string enc = "nao";
        private async void btnDell_Click(object sender, EventArgs e)
        {
            string PersonGroupId = cbbxGrupID.Text;
            await DeletePersonGroup(_faceClient, PersonGroupId);

            string line = null;
            string line_to_delete = PersonGroupId;

            int counter = -1;
            System.IO.StreamReader file = new System.IO.StreamReader(@"groupId.txt");
            while ((line = file.ReadLine()) != null)
            {
                counter++;
                if (line.Contains(PersonGroupId))
                {
                    enc = "sim";
                    line_number = counter;
                }
                //System.Console.WriteLine(line);
            }
            file.Close();

            Thread.Sleep(2);
            if (enc == "sim")
            {
                var file1 = new List<string>(System.IO.File.ReadAllLines("groupName.txt"));
                file1.RemoveAt(line_number);
                File.WriteAllLines("groupName.txt", file1.ToArray());


                var file2 = new List<string>(System.IO.File.ReadAllLines("groupId.txt"));
                file2.RemoveAt(line_number);
                File.WriteAllLines("groupId.txt", file2.ToArray());

                enc = "nao";

            }

            cbbxGrupID.Items.Clear();

            string[] lineOfContents = File.ReadAllLines("groupId.txt");
            foreach (var line3 in lineOfContents)
            {
                string[] tokens = line3.Split(',');
                cbbxGrupID.Items.Add(tokens[0]);
            }

            cbbxGrupID.SelectedIndex = 0;
        }

        public static async Task DeletePersonGroup(IFaceClient client, String personGroupId)
        {
            try
            {
                await client.PersonGroup.DeleteAsync(personGroupId);
                MessageBox.Show($"Grupo deletado: {personGroupId}.");
            }
            catch
            {
                MessageBox.Show("Grupo não encontrado.");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 161, 2, 0);
            }
        }

        private void ConfCam_Load(object sender, EventArgs e)
        {
            string[] lineOfContents = File.ReadAllLines("groupId.txt");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                cbbxGrupID.Items.Add(tokens[0]);
            }

            cbbxGrupID.SelectedIndex = 0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
