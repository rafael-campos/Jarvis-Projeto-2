using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo1._3
{
    class Extrating
    {
        ComandosSearch comandosSearch = new ComandosSearch();
        static SQLiteConnection sqlCon = null;
        static string strCon1 = "Data Source=dbon.sdb; Version=3; New=False; Compress = true;";
        static string strSql1 = string.Empty;
                
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

        //Para previsão do tempo
        public void getWeather(string key, string city)
        {
            Form1.nameCid = comandosSearch.cidadeUser;
            string url = string.Format("https://api.hgbrasil.com/weather?key={0}&city_name={1}&format=json", key, city);
            WebClient web = new WebClient();
            var json = web.DownloadString(url);
            byte[] bytes = Encoding.Default.GetBytes(json);
            json = Encoding.UTF8.GetString(bytes);
            var result = JsonConvert.DeserializeObject<Weather.RootObject>(json);
            Weather.RootObject outPut = result;

            //diaAtual
            Form1.diaSemana = outPut.results.time;
            Form1.descricao = outPut.results.description;
            Form1.tempoAtual = outPut.results.temp.ToString();
            Form1.umidade = outPut.results.humidity + "%";
            Form1.velocidadeVento = outPut.results.wind_speedy.ToString();
            Form1.porDoSol = outPut.results.sunset;
        }

        //Definir cidade e Chave de api Previsão do tempo
        public void CarregaWeather()
        {
            try
            {
                getWeather(Form1.key, Form1.city);
            }
            catch (WebException)
            {
                MessageBox.Show("Serviço temporariamente indisponivel!");
            }
        }

        #region predefinidos
        public List<string> comandoPredefOn = new List<string>();
        public List<string> responsePredefOn = new List<string>();
        public List<int> idPredefOn = new List<int>();
        public List<string> respFinalPrede = new List<string>();

        //Procurar comando e resposta
        public int contFind = -1;
        public int resultFind;
        public string encontPred = "nao";
        public string findComPred(string text)
        {
            foreach (string i in comandoPredefOn)
            {
                string t = Regex.Replace(i, @"[^\w\s]", "");
                contFind++;
                if (RemoveAccents(text).ToLower().Contains(RemoveAccents(t).ToLower()))
                {
                    encontPred = "sim";
                    resultFind = contFind;
                    break;
                }
            }

            if (encontPred == "sim")
            {
                encontPred = "nao";

                Random rnd = new Random();
                List<string> resp = new List<string>();
                foreach (string e in responsePredefOn[resultFind].Split('/'))
                {
                    resp.Add(e);
                }

                int r = rnd.Next(resp.Count);

                respFinalPrede.Add(idPredefOn[resultFind].ToString());
                respFinalPrede.Add(resp[r]);
                return "id: " + respFinalPrede[0] + " resp: " + respFinalPrede[1];
            }
            else
            {
                encontPred = "nao";
                return "nada";
            }
        }

        //Buscar comandos
        public void getIdPredef()
        {
            strSql1 = "select * from ComandosPredef";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);

            //comando.Parameters.Add("@pesquisa", DbType.String).Value = "que horas são";
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        int comandos = Convert.ToInt32(dr["PredeID"]);

                        idPredefOn.Add(comandos);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public void getComandPredef()
        {
            strSql1 = "select * from ComandosPredef";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        string comandos = dr["Comando"].ToString();

                        comandoPredefOn.Add(comandos.ToLower());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public void getRespdPredef()
        {
            strSql1 = "select * from ComandosPredef";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        string comandos = dr["Resposta"].ToString();
                        responsePredefOn.Add(comandos.ToLower());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        #endregion

        #region web
        public List<string> comandoWeb = new List<string>();
        public List<string> responseWeb = new List<string>();
        public List<string> linkWeb = new List<string>();
        public int contFindWeb = -1;
        public int resultFindWeb;
        public string encontWeb = "nao";

        //Procurar comando e resposta
        public string findComWeb(string text)
        {

            foreach (string i in comandoWeb)
            {
                string t = Regex.Replace(i, @"[^\w\s]", "");
                contFindWeb++;
                if (RemoveAccents(text).ToLower().Contains(RemoveAccents(t).ToLower()))
                {
                    encontWeb = "sim";
                    resultFindWeb = contFindWeb;
                    break;
                }
            }
            if (encontWeb == "sim")
            {
                encontWeb = "nao";
                Random rnd = new Random();
                List<string> resp = new List<string>();
                foreach (string e in responseWeb[resultFindWeb].Split('/'))
                {
                    resp.Add(e);
                }

                int r = rnd.Next(resp.Count);
                return "resp: " + resp[r] + " link: " + linkWeb[resultFindWeb];
            }
            else
            {
                encontWeb = "nao";
                return "nada";
            }
        }
        public void getComandWeb()
        {
            strSql1 = "select * from comandosWeb";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        string comandos = dr["Comando"].ToString();

                        comandoWeb.Add(comandos.ToLower());
                    }
                    dr.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public void getRespdWeb()
        {
            strSql1 = "select * from comandosWeb";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        string comandos = dr["Resposta"].ToString();
                        responseWeb.Add(comandos.ToLower());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public void getLinkWeb()
        {
            strSql1 = "select * from comandosWeb";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        string comandos = dr["Link"].ToString();
                        linkWeb.Add(comandos.ToLower());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        #endregion

        #region Sociais
        public List<string> comandoSociOn = new List<string>();
        public List<string> responseSociOn = new List<string>();
        public int contFindSoc = -1;
        public int resultFindSoc;
        public string encontSoc = "nao";

        //Procurar comando e resposta
        public string findComSoc(string text)
        {
            foreach (string i in comandoSociOn)
            {
                string t = Regex.Replace(i, @"[^\w\s]", "");
                contFindSoc++;
                if (RemoveAccents(text).ToLower().Contains(RemoveAccents(t).ToLower()))
                {
                    encontSoc = "sim";
                    resultFindSoc = contFindSoc;
                    break;
                }
            }
            if (encontSoc == "sim")
            {
                encontSoc = "nao";
                Random rnd = new Random();
                List<string> resp = new List<string>();
                foreach (string e in responseSociOn[resultFindSoc].Split('/'))
                {
                    resp.Add(e);
                }

                int r = rnd.Next(resp.Count);

                return "resp: " + resp[r];
            }
            else
            {
                encontSoc = "nao";
                return "nada";
            }
        }

        //Buscar comandos
        public void getComandSoc()
        {
            strSql1 = "select * from comandSoci";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        string comandos = dr["Comando"].ToString();

                        comandoSociOn.Add(comandos.ToLower());
                    }
                    dr.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        public void getRespdSoc()
        {
            strSql1 = "select * from comandSoci";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    while (dr.Read())
                    {
                        string comandos = dr["Resposta"].ToString();
                        responseSociOn.Add(comandos.ToLower());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        #endregion

        
        public string getCit()
        {
            strSql1 = "select * from dadosUser";
            sqlCon = new SQLiteConnection(strCon1);
            SQLiteCommand comando = new SQLiteCommand(strSql1, sqlCon);
            string comandos = "";
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                if (dr.HasRows == false)
                {
                    Console.WriteLine("nada encontrado");
                }
                else
                {
                    dr.Read();
                    comandos = dr["Cidade"].ToString();
                    dr.Close();

                }
                return comandos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "erro";
            }
            finally
            {
                sqlCon.Close();
            }
        }

        //Obter resposta
        #region responsefinal
        public string mud = "nao";
        public string mudFal(string cmand)
        {
            contFind = -1;
            resultFind = 0;

            respFinalPrede.Clear();
            comandoPredefOn.Clear();
            responsePredefOn.Clear();
            idPredefOn.Clear();

            getComandPredef();
            getRespdPredef();
            getIdPredef();

            string text = cmand;
            string res = findComPred(Regex.Replace(text, @"[^\w\s]", ""));
            string response = "";

            if (res != "nada")
            {
                string[] y = res.Split(':');
                string y1 = y[1];
                string[] y2 = y1.Split(new string[] { "resp" }, StringSplitOptions.None);
                string id1 = y2[0].TrimStart().TrimEnd();//ID

                //RESPONSE//
                string t1 = y[2].TrimStart().TrimEnd();//RESPONSE
                switch (id1)
                {
                    case "4":
                        {
                            List<string> responses = new List<string>();
                            responses.Add("Não há de que senhor");
                            responses.Add("Por nada.");
                            responses.Add("Náda se precisar é só chamar.");

                            Random rnd = new Random();
                            int r = rnd.Next(responses.Count);
                            response = responses[r];
                            comandosSearch.respostaArduino = "";

                            Form1.rotulo.Text = "Mudo";

                            comandosSearch.comandoPredefinido.Clear();
                            comandosSearch.respostaPredef = "";
                            comandosSearch.verifyObrigado = "";
                            comandosSearch.predefSo = "";

                            mud = "sim";
                            return response;
                        }
                    case "13":
                        {
                            List<string> responses = new List<string>();
                            responses.Add("Estou ouvindo senhor.");
                            responses.Add("Em que posso ajudar?");
                            responses.Add("Estou aqui.");

                            Random rnd = new Random();
                            int r = rnd.Next(responses.Count);

                            response = responses[r];
                            comandosSearch.respostaArduino = "";
                            Form1.rotulo.Text = "";

                            comandosSearch.comandoPredefinido.Clear();
                            comandosSearch.respostaPredef = "";
                            comandosSearch.verifyObrigado = "";
                            comandosSearch.predefSo = "";

                            mud = "sim";
                            return response;
                        }
                }

            }
            return response;
        }

        public string predeFinale(string cmand)
        {
            contFind = -1;
            resultFind = 0;

            respFinalPrede.Clear();
            comandoPredefOn.Clear();
            responsePredefOn.Clear();
            idPredefOn.Clear();

            getComandPredef();
            getRespdPredef();
            getIdPredef();

            string text = cmand;
            string res = findComPred(Regex.Replace(text, @"[^\w\s]", ""));
            string response = "";

            if (res != "nada")
            {
                //ID//
                string[] y = res.Split(':');
                string y1 = y[1];
                string[] y2 = y1.Split(new string[] { "resp" }, StringSplitOptions.None);
                string id1 = y2[0].TrimStart().TrimEnd();//ID
                string t1 = y[2].TrimStart().TrimEnd();//RESPONSE
                switch (id1)
                {
                    case "5"://que horas são
                        {
                            int hours = DateTime.Now.Hour;
                            int minutes = DateTime.Now.Minute;
                            int seconds = DateTime.Now.Second;

                            if (hours == 1)
                            {
                                response = "Agora é, uma hora e " + minutes + " minutos";
                            }
                            else if (hours == 2)
                            {
                                response = "Agora são, duas horas e " + minutes + " minutos";
                            }
                            else if (hours == 21)
                            {
                                response = "Agora são,vinte e uma horas e " + minutes + " minutos";
                            }
                            else if (hours == 22)
                            {
                                response = "Agora são,vinte e duas horas e " + minutes + " minutos";
                            }
                            else
                            {
                                switch (minutes)
                                {
                                    case 01:
                                        response = "Agora são, " + hours + " hora e " + minutes + " minuto";
                                        break;
                                    default:
                                        response = "Agora são, " + hours + " horas e " + minutes + " minutos";
                                        break;
                                }
                            }
                            return response;
                        }
                    case "6"://previsão do tempo
                        {
                            Form1.city = getCit();
                            CarregaWeather();
                            if (Form1.city == null)
                            {
                                MessageBox.Show("Defina sua cidade no menu Usuário");
                            }
                            else
                            {
                                string repondTemp = Form1.descricao + " com temperatura de " + Form1.tempoAtual + " graus! " +
                                    "e ventos na velocidade de " + Form1.velocidadeVento;
                                response = repondTemp;
                            }
                            comandosSearch.comandoPredefinido.Clear();
                            comandosSearch.respostaPredef = "";
                            comandosSearch.verifyObrigado = "";
                            comandosSearch.predefSo = "";

                            comandosSearch.cidadeUser = "";
                            Form1.descricao = "";
                            Form1.tempoAtual = "";
                            Form1.velocidadeVento = "";
                            Form1.city = "";

                            Thread.Sleep(300);
                            return response;
                        }

                    case "7"://data exata
                        {
                            CultureInfo culture = new CultureInfo("pt-BR");
                            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                            int dia = DateTime.Now.Day;
                            int ano = DateTime.Now.Year;
                            string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                            string diaSemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                            string data = dia + " de " + mes + " de " + ano;
                            response = data;

                            comandosSearch.comandoPredefinido.Clear();
                            comandosSearch.respostaPredef = "";
                            comandosSearch.verifyObrigado = "";
                            comandosSearch.predefSo = "";

                            return response;
                        }
                    case "8"://dia?
                        {
                            int mes = 12;
                            string mesExtenso;
                            string diaExtenso;

                            //Mês em português por extenso
                            mesExtenso = new DateTime(1900, mes, 1).ToString("MMMM", new CultureInfo("pt-BR"));
                            //Mês abreviado em português também.
                            mesExtenso = new CultureInfo("pt-BR").DateTimeFormat.GetAbbreviatedMonthName(mes);
                            //Mês (int) por extenso com primeira letra maiúscula.
                            string month = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(mes);
                            mesExtenso = char.ToUpper(month[0]) + month.Substring(1);

                            //Dia da semana (int) por extenso em português (segunda-feira)
                            diaExtenso = new CultureInfo("pt-BR").DateTimeFormat.GetDayName((DayOfWeek)1);
                            //Dia da semana abreviado (seg)
                            diaExtenso = new CultureInfo("pt-BR").DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Monday);
                            //Dia atual por extenso
                            diaExtenso = DateTime.Now.ToString("dddd", new CultureInfo("pt-BR"));
                            //Dia por extenso com primeira letra maiúscula.
                            string day = new CultureInfo("pt-BR").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                            diaExtenso = char.ToUpper(day[0]) + day.Substring(1);
                            //Form1.speak02(diaExtenso);
                            response = diaExtenso;

                            comandosSearch.comandoPredefinido.Clear();
                            comandosSearch.respostaPredef = "";
                            comandosSearch.verifyObrigado = "";
                            comandosSearch.predefSo = "";
                            return response;
                        } 
                    case "14"://para de falar
                        {
                            Form1.respCalado = t1;
                            Form1.stop2();

                            response = Form1.respCalado;
                            Form1.seParoudeFalar = "Falando";

                            comandosSearch.comandoPredefinido.Clear();
                            comandosSearch.respostaPredef = "";
                            comandosSearch.verifyObrigado = "";
                            comandosSearch.predefSo = "";
                            comandosSearch.comandoComando = "";
                            comandosSearch.paraDefalar = "";
                            comandosSearch.respParadefalar = "";

                            return response;
                        }                   

                }
                return response;
            }
            return "";
        }

        public string linFinalWeb = "";
        public string webFinale(string cmand)
        {
            string response = "";

            comandoWeb.Clear();
            responseWeb.Clear();
            linkWeb.Clear();

            contFindWeb = -1;
            resultFindWeb = 0;
            linFinalWeb = "";

            getComandWeb();
            getRespdWeb();
            getLinkWeb();

            string text = cmand;
            string res = findComWeb(Regex.Replace(text, @"[^\w\s]", ""));

            if (res != "nada")
            {
                response = res;
                string[] y = res.Split(':');

                string ut = y[1].TrimStart().TrimEnd();
                response = ut.Replace("link", "");//RESPONSE

                string yi = y[2].TrimStart().TrimEnd();
                string[] linkww = res.Split(new string[] { "link:" }, StringSplitOptions.None);
                linFinalWeb = linkww[1];

                System.Diagnostics.Process.Start(linFinalWeb.TrimStart().TrimEnd());
                return response;
            }
            return response;
        }
        public string sociFinale(string cmand)
        {
            string response = "";
            contFindSoc = -1;
            resultFindSoc = 0;

            comandoSociOn.Clear();
            responseSociOn.Clear();

            getComandSoc();

            getRespdSoc();

            string text = cmand;
            string res = findComSoc(Regex.Replace(text, @"[^\w\s]", ""));

            if (res != "nada")
            {
                string[] y = res.Split(':');
                response = y[1].TrimStart().TrimEnd();//RESPONSE
                return response;
            }
            return response;
        }
     
        #endregion
    }
}
