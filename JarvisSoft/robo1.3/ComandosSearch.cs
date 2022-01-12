using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo1._3
{
    class ComandosSearch
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        SQLiteConnection sqlCon = null;
        string strCon = "Data Source=dbon.sdb; Version=3; New=False; Compress = true;";
        string strSql = string.Empty;

        public string comandCmd;

        public List<string> comandoSoci = new List<string>();
        public string comandSo;
        public string resposta;

        public List<string> comandoArduino = new List<string>();
        public string arduinoSo;
        public string respostaArduino;
        public string comandoParaArduino;
        public string portaCom;

        public List<string> comandoNomeIA = new List<string>();
        public string nomeIASo;
        public string respostaNomeIA;

        public List<string> coletaRespNameIa = new List<string>();
        public string nomeIaPredef;
        public string respNomeIaPredef;



        public List<string> comandoNomeUser = new List<string>();
        public string userSo;
        public string respostaNomeUser;
        public string tituloUser;
        public string niverUser;
        public string pastaUser;
        public string cidadeUser;
        public string comandoMusicaID;

        public List<string> comandoPredefinido = new List<string>();
        public string paraDefalar;
        public string respParadefalar;

        public string predefSo;
        public string respostaPredef;
        public string verifyObrigado;
        public string comandoComando;

        //Comandos Player musicas
        public List<string> comandoPlayer = new List<string>();
        public string playerSo;
        public string respostaPlayer;
        public string playerID;

        //Portas
        public List<string> PosiveisPortas = new List<string>();
        public string PosiveisPSo;
        public string verifyPosiveisPo;

        public void Comandos_Sociais()
        {
            strSql = "select * from comandSoci where Comando=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = comandCmd;
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        comandoSoci.Clear();
                        comandSo = "";
                        resposta = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();
                        comandoSoci.Add(dr["Resposta"].ToString());

                        int index = random.Next(comandoSoci.Count);
                        comandSo = comandoSoci[index];

                        List<string> stringList = comandSo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        resposta = stringList[index1];
                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }


        public void Comandos_Arduino()
        {
            strSql = "select * from Employee where Comando=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = comandCmd;
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        comandoArduino.Clear();
                        comandoParaArduino = "";
                        respostaArduino = "";
                        portaCom = "";
                        arduinoSo = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        comandoArduino.Add(dr["Resposta"].ToString());

                        comandoParaArduino = Convert.ToString(dr["ComandoArduino"]);
                        portaCom = Convert.ToString(dr["PortaCom"]);

                        int index = random.Next(comandoArduino.Count);
                        arduinoSo = comandoArduino[index];

                        List<string> stringList = arduinoSo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        respostaArduino = stringList[index1];
                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        //COM WEB??
        //comandoArduino.Clear();
        //comandoParaArduino = "";
        //respostaArduino = "";
        //arduinoSo = "";

        public List<string> comandoWeb = new List<string>();
        public string webSo;
        public string respostaWeb;
        public string webLink;

        public void Comandos_Web()
        {
            strSql = "select * from comandosWeb where Comando=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = comandCmd;
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        comandoWeb.Clear();
                        webSo = "";
                        respostaWeb = "";
                        webLink = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        webLink = dr["Link"].ToString();

                        comandoWeb.Add(dr["Resposta"].ToString());

                        int index = random.Next(comandoWeb.Count);
                        webSo = comandoWeb[index];

                        List<string> stringList = webSo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        respostaWeb = stringList[index1];
                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public void PredefinidoComand()
        {
            strSql = "select * from ComandosPredef where Comando=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = comandCmd;
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        comandoPredefinido.Clear();
                        respostaPredef = "";
                        verifyObrigado = "";
                        predefSo = "";
                        comandoComando = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        comandoPredefinido.Add(dr["Resposta"].ToString());

                        verifyObrigado = Convert.ToString(dr["PredeID"]);
                        comandoComando = Convert.ToString(dr["Comando"]);
                        int index = random.Next(comandoPredefinido.Count);
                        predefSo = comandoPredefinido[index];

                        List<string> stringList = predefSo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        respostaPredef = stringList[index1];
                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }


        public void paraDeFalar()
        {
            strSql = "select * from ComandosPredef where PredeID=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = "14";
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");
                        comandoPredefinido.Clear();
                        respostaPredef = "";
                        verifyObrigado = "";
                        predefSo = "";
                        comandoComando = "";
                        paraDefalar = "";
                        respParadefalar = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        paraDefalar = Convert.ToString(dr["Comando"]);
                        //respParadefalar = Convert.ToString(dr["Resposta"]);
                        comandoPredefinido.Add(dr["Resposta"].ToString());

                        int index = random.Next(comandoPredefinido.Count);
                        predefSo = comandoPredefinido[index];

                        List<string> stringList = predefSo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        respParadefalar = stringList[index1];

                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public void MusicaComandoID()
        {
            strSql = "select * from ComandoMusicas where Comando=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = comandCmd;
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        comandoPlayer.Clear();
                        respostaPlayer = "";
                        playerID = "";
                        playerSo = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        comandoPlayer.Add(dr["Comando"].ToString());

                        playerID = Convert.ToString(dr["MusicasID"]);

                        int index = random.Next(comandoPlayer.Count);
                        playerSo = comandoPlayer[index];

                        List<string> stringList = playerSo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        respostaPlayer = stringList[index1];
                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }



        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=dbon.sdb; Version=3; New=False; Compress = true;");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }



        public void carregarNameIaPredef()
        {
            strSql = "select * from NomeIa where iaID=@LabelNome";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando1 = new SQLiteCommand(strSql, sqlCon);
            comando1.Parameters.Add("@LabelNome", DbType.String).Value = "1";
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr1 = comando1.ExecuteReader();

                dr1.Read();
                nomeIaPredef = Convert.ToString(dr1["Nome"]);
                dr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            string txtQuery = "UPDATE ComandosPredef set Comando='" + nomeIaPredef + "' WHERE PredeID= 13";
            ExecuteQuery(txtQuery);


            strSql = "select * from ComandosPredef where PredeID=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = "13";
            try
            {

                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    //MessageBox.Show("Comando não cadastrado");

                    coletaRespNameIa.Clear();
                    nomeIaPredef = "";
                    respNomeIaPredef = "";
                }
                else
                {
                    var random = new Random();
                    dr.Read();

                    coletaRespNameIa.Add(dr["Resposta"].ToString());

                    int index = random.Next(coletaRespNameIa.Count);
                    nomeIaPredef = coletaRespNameIa[index];

                    List<string> stringList = nomeIaPredef.Split('/').ToList();
                    int index1 = random.Next(stringList.Count);
                    respNomeIaPredef = stringList[index1];
                    //MessageBox.Show(stringList[index1]);
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }



        public void Nome_IA()
        {
            strSql = "select * from NomeIa where Nome=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.String).Value = comandCmd;
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        comandoNomeIA.Clear();
                        nomeIASo = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        comandoNomeIA.Add(dr["Nome"].ToString());

                        int index = random.Next(comandoNomeIA.Count);
                        nomeIASo = comandoNomeIA[index];

                        List<string> stringList = nomeIASo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        respostaNomeIA = stringList[index1];
                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        // Usuarios
        public void User_Dados()
        {
            strSql = "select * from dadosUser where userID=@pesquisa";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);

            comando.Parameters.Add("@pesquisa", DbType.Int32).Value = 1;
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        comandoNomeUser.Clear();
                        userSo = "";
                        //niverUser = "";
                        cidadeUser = "";
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        comandoNomeUser.Add(dr["Nome"].ToString());
                        pastaUser = dr["Musicas"].ToString();
                        //niverUser = dr["Data"].ToString();
                        tituloUser = dr["Titulo"].ToString();
                        cidadeUser = dr["Cidade"].ToString();

                        int index = random.Next(comandoNomeUser.Count);
                        userSo = comandoNomeUser[index];

                        List<string> stringList = userSo.Split('/').ToList();
                        int index1 = random.Next(stringList.Count);
                        respostaNomeUser = stringList[index1];
                        //MessageBox.Show(stringList[index1]);
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }


        public void Select_Portas()
        {
            strSql = "select * from Employee where PortaCom";
            sqlCon = new SQLiteConnection(strCon);
            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);
            try
            {
                if (comandCmd == string.Empty)
                {
                    //
                }
                else
                {
                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    if (dr.HasRows == false)
                    {
                        //MessageBox.Show("Comando não cadastrado");

                        PosiveisPortas.Clear();
                    }
                    else
                    {
                        var random = new Random();
                        dr.Read();

                        PosiveisPortas.Add(dr["PortaCom"].ToString());

                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
