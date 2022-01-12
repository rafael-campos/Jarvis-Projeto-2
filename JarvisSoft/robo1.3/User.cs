using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo1._3
{
    public partial class User : Form
    {
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        SQLiteConnection sqlCon = null;
        string strCon = "Data Source=DGVDB.sdb; Version=3; New=False; Compress = true;";
        string strSql = string.Empty;

        public User()
        {
            InitializeComponent();

            strSql = "select * from dadosUser where lblUser=@LabelNome";
            sqlCon = new SQLiteConnection(strCon);

            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);
            comando.Parameters.Add("@LabelNome", DbType.String).Value = "Usuário";
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                dr.Read();
                lblUser.Text = Convert.ToString(dr["lblUser"]);
                dr.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            cbbTitulo.Visible = false;
            textBoxName.Visible = false;
            dateTime.Visible = false;
            btnSelect.Visible = false;
            textBoxMusic.Visible = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = false;
            btnDell.Enabled = false;
            lblUser.Visible = false;
            labelCidade.Visible = false;
            textBoxCidade.Visible = false;

            if (lblUser.Text.Length > 1)
            {
                cbbTitulo.Visible = false;
                textBoxName.Visible = false;
                dateTime.Visible = false;
                btnSelect.Visible = false;
                textBoxMusic.Visible = false;
                btnEdit.Enabled = true;
                btnSave.Enabled = false;
                btnDell.Enabled = false;
                btnAdd.Enabled = false;
                lblUser.Visible = false;
                labelCidade.Visible = false;
                textBoxCidade.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cbbTitulo.SelectedItem = null;
            dateTime.Value = DateTime.Today;
            textBoxName.Clear();
            textBoxMusic.Clear();

            cbbTitulo.Visible = true;
            textBoxName.Visible = true;
            dateTime.Visible = true;
            btnSelect.Visible = true;
            textBoxMusic.Visible = true;
            labelCidade.Visible = true;
            textBoxCidade.Visible = true;

            cbbTitulo.Enabled = true;
            textBoxName.Enabled = true;
            dateTime.Enabled = true;
            btnSelect.Enabled = true;
            textBoxMusic.Enabled = true;

            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnDell.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lblUser.Text.Length < 1)
            {
                strSql = "INSERT INTO dadosUser(Titulo,Nome,Data,Musicas,lblUser)Values(@Titulo,@Nome,@Data,@Musicas,@lblUser)";

                sqlCon = new SQLiteConnection(strCon);

                SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);
                comando.Parameters.Add("@Titulo", DbType.String).Value = cbbTitulo.SelectedItem;
                comando.Parameters.Add("@Nome", DbType.String).Value = textBoxName.Text;
                comando.Parameters.Add("@Data", DbType.String).Value = dateTime.Value;
                comando.Parameters.Add("@Musicas", DbType.String).Value = textBoxMusic.Text;
                comando.Parameters.Add("@lblUser", DbType.String).Value = "Usuário";
                comando.Parameters.Add("@Cidade", DbType.String).Value = textBoxMusic.Text;
                try
                {
                    sqlCon.Open();

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastrado com Sucesso!");

                    cbbTitulo.Visible = true;
                    textBoxName.Visible = true;
                    dateTime.Visible = true;
                    btnSelect.Visible = true;
                    textBoxMusic.Visible = true;
                    textBoxCidade.Visible = true;

                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnDell.Enabled = false;
                    btnAdd.Enabled = false;

                    cbbTitulo.Enabled = false;
                    textBoxName.Enabled = false;
                    dateTime.Enabled = false;
                    btnSelect.Enabled = false;
                    textBoxMusic.Enabled = false;

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
            else
            {
                strSql = "UPDATE dadosUser SET Titulo=@Titulo,Nome=@Nome,Data=@Data,Musicas=@Musicas";

                sqlCon = new SQLiteConnection(strCon);

                SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);
                comando.Parameters.Add("@Titulo", DbType.String).Value = cbbTitulo.SelectedItem;
                comando.Parameters.Add("@Nome", DbType.String).Value = textBoxName.Text;
                comando.Parameters.Add("@Data", DbType.String).Value = dateTime.Value;
                comando.Parameters.Add("@Musicas", DbType.String).Value = textBoxMusic.Text;
                comando.Parameters.Add("@Cidade", DbType.String).Value = textBoxMusic.Text;
                try
                {
                    sqlCon.Open();

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastrado Atualizado com Sucesso!");

                    cbbTitulo.Visible = true;
                    textBoxName.Visible = true;
                    dateTime.Visible = true;
                    btnSelect.Visible = true;
                    textBoxMusic.Visible = true;
                    textBoxCidade.Visible = true;

                    cbbTitulo.Enabled = false;
                    textBoxName.Enabled = false;
                    dateTime.Enabled = false;
                    btnSelect.Enabled = false;
                    textBoxMusic.Enabled = false;

                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnDell.Enabled = false;
                    btnAdd.Enabled = false;

                    cbbTitulo.SelectedItem = null;
                    dateTime.Value = DateTime.Today;
                    textBoxName.Clear();
                    textBoxMusic.Clear();

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
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxMusic.Text = fbd.SelectedPath;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            strSql = "select * from dadosUser where lblUser=@LabelNome";
            sqlCon = new SQLiteConnection(strCon);

            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);
            comando.Parameters.Add("@LabelNome", DbType.String).Value = "Usuário";
            try
            {
                sqlCon.Open();
                SQLiteDataReader dr = comando.ExecuteReader();

                dr.Read();
                lblUser.Text = Convert.ToString(dr["lblUser"]);
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            cbbTitulo.Visible = true;
            textBoxName.Visible = true;
            dateTime.Visible = true;
            btnSelect.Visible = true;
            textBoxMusic.Visible = true;
            textBoxCidade.Visible = true;

            cbbTitulo.Enabled = false;
            textBoxName.Enabled = false;
            dateTime.Enabled = false;
            btnSelect.Enabled = false;
            textBoxMusic.Enabled = false;

            btnAdd.Enabled = false;
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnDell.Enabled = false;
            lblUser.Visible = true;
        }

        private void btnDell_Click(object sender, EventArgs e)
        {
            strSql = "DELETE FROM dadosUser where lblUser=@LabelNome";

            sqlCon = new SQLiteConnection(strCon);

            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);
            comando.Parameters.Add("@LabelNome", DbType.String).Value = "Usuário";
            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();


                cbbTitulo.Visible = false;
                textBoxName.Visible = false;
                dateTime.Visible = false;
                btnSelect.Visible = false;
                textBoxMusic.Visible = false;
                textBoxCidade.Visible = false;

                cbbTitulo.Enabled = false;
                textBoxName.Enabled = false;
                dateTime.Enabled = false;
                btnSelect.Enabled = false;
                textBoxMusic.Enabled = false;

                btnEdit.Enabled = false;
                btnSave.Enabled = false;
                btnDell.Enabled = false;
                btnAdd.Enabled = true;

                lblUser.Text = "";


                cbbTitulo.SelectedItem = null;
                dateTime.Value = DateTime.Today;
                textBoxName.Clear();
                textBoxMusic.Clear();

                MessageBox.Show("Usuario deletado com Sucesso!");
            }
        }

        private void lblUser_Click(object sender, EventArgs e)
        {
            strSql = "select * from dadosUser where lblUser=@LabelNome";
            sqlCon = new SQLiteConnection(strCon);

            SQLiteCommand comando = new SQLiteCommand(strSql, sqlCon);
            comando.Parameters.Add("@LabelNome", DbType.String).Value = "Usuário";

            try
            {
                if (lblUser.Text.Length < 1)
                {
                    MessageBox.Show("Não há usuario Cadastrado no sistema!");
                }
                else
                {
                    cbbTitulo.Visible = true;
                    textBoxName.Visible = true;
                    dateTime.Visible = true;
                    btnSelect.Visible = true;
                    textBoxMusic.Visible = true;

                    cbbTitulo.Enabled = true;
                    textBoxName.Enabled = true;
                    dateTime.Enabled = true;
                    btnSelect.Enabled = true;
                    textBoxMusic.Enabled = true;

                    btnEdit.Enabled = true;
                    btnSave.Enabled = true;
                    btnDell.Enabled = true;

                    sqlCon.Open();
                    SQLiteDataReader dr = comando.ExecuteReader();

                    dr.Read();
                    cbbTitulo.SelectedItem = Convert.ToString(dr["Titulo"]);
                    textBoxName.Text = Convert.ToString(dr["Nome"]);
                    string date = Convert.ToString(dr["Data"]);
                    dateTime.Value = DateTime.Parse(date);
                    textBoxMusic.Text = Convert.ToString(dr["Musicas"]);
                    textBoxCidade.Text = Convert.ToString(dr["Cidade"]);
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
    }
}
