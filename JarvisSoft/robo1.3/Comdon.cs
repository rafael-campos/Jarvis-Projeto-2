using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace robo1._3
{
    class Comdon
    {
        static SQLiteConnection sqlCon = null;
        static string strCon1 = "Data Source=dbon.sdb; Version=3; New=False; Compress = true;";
        static string strSql1 = string.Empty;
        public static List<string> comandoPredefinido = new List<string>();
        public static string predefSo;
        public static string respostaPredef;
        public static string verifyObrigado;
        public static string comandoComando;
        static string position;
        public static List<string> resultFind = new List<string>();
        public static List<string> findResponse = new List<string>();
        public static List<string> findResponseFinal = new List<string>();
        public static string resp00223 = "";
        int contResult = 1;
        int addCnt = 1;

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

        //Para operações matematicas foi preciso substituir os operadores reconhecidos
        //por conter um formato diferente
        private static string replacedstr(string opr)
        {
            string r1 = opr;
            if (r1.Contains("−"))
            {
                r1 = r1.Replace("−", "-");
            }

            if (r1.Contains("÷"))
            {
                r1 = r1.Replace("÷", "/");
            }

            if (r1.Contains("×"))
            {
                r1 = r1.Replace("×", "*");
            }
            if (r1.Contains("x"))
            {
                r1 = r1.Replace("x", "*");
            }
            return r1;
        }

        //Obter os operadores no texto reconhecido
        private static string getOperators(string opr)
        {
            string st = RemoveAccents(opr);
            string numberOnly = Regex.Replace(st, "[^0-9.+-.*/]", "");
            return numberOnly;
        }
        private static string getResultOp(string o)
        {
            string r = new DataTable().Compute(o, null).ToString();
            return r;
        }       

        //GET RESPONSE//
        public string response = "";
        public string GetResponse(string text)
        {
            Extrating comandBD = new Extrating();
            string iff = RemoveAccents(text);
            string cm = iff.ToLower();
            string cmm = "";
            string md = "";

            if (cm.Contains("jax"))
            {
                cmm = cm.Replace("jax", "jarvis");
                md = comandBD.mudFal(cmm);
            }
            else if (cm.Contains("Jars"))
            {
                cmm = cm.Replace("Jars", "jarvis");
                md = comandBD.mudFal(cmm);
            }
            else if (cm.Contains("chaves"))
            {
                cmm = cm.Replace("caves", "jarvis");
                md = comandBD.mudFal(cmm);
            }
            else if (cm.Contains("jackson"))
            {
                cmm = cm.Replace("jackson", "jarvis");
                md = comandBD.mudFal(cmm);
            }
            else
            {
                md = comandBD.mudFal(cm);
            }

            if (comandBD.mud != "sim")
            {
                if (Form1.rotulo.Text != "Mudo")
                {

                    if (text.ToLower().Contains("quantos é") || text.ToLower().Contains("quanto é") || text.ToLower().Contains("quantos são"))
                    {
                        string opr = text;
                        string ropr = replacedstr(opr);

                        response = "O resultado é " + getResultOp(getOperators(ropr));
                        return response;
                    } 
                    else
                    {
                        string comand = text.ToLower().Replace("jarvis", "");
                        string comand0 = Regex.Replace(comand, @"[^\w\s]", "");
                        //Console.WriteLine("EEEE>> " + comand0.TrimStart().TrimEnd());

                        string textt = text.TrimStart().TrimEnd();

                        #region AllCommands
                        #region social
                        string resSoci = comandBD.sociFinale(textt);
                        if (string.IsNullOrEmpty(resSoci))
                        {
                            //
                        }
                        else
                        {
                            response = resSoci;
                            return response;
                        }
                        #endregion

                        #region webCom
                        string resWeb = comandBD.webFinale(text);
                        if (string.IsNullOrEmpty(resWeb))
                        {
                            //
                        }
                        else
                        {
                            response = resWeb;
                            //MessageBox.Show(response);
                            return response;

                        }
                        #endregion

                        #region predefinidos
                        string resPrede = comandBD.predeFinale(textt);
                        if (string.IsNullOrEmpty(resPrede))
                        {
                            //
                        }
                        else
                        {
                            response = resPrede;
                            return response;
                        }
                        #endregion
                        #endregion

                        return response;
                    }
                }
            }
            else
            {
                comandBD.mud = "nao";
                //Console.WriteLine("Akii: " + md);
                if (string.IsNullOrEmpty(md))
                {
                    //
                }
                else
                {
                    response = md;
                    //Console.WriteLine("Akii: " + response);
                    return response;
                }

                return response;
            }



            return response;
        }               
       
    }
}
