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
    public partial class Predef : Form
    {
        string connectionString = @"Data Source=dbon.sdb; Version=3; New=False; Compress = true;";
        string ok5;
        string selU;

        public Predef()
        {
            InitializeComponent();
        }

        void PopulateDataGridView()
        {
            using (SQLiteConnection sqlCon = new SQLiteConnection(connectionString))
            {
                sqlCon.Open();
                SQLiteDataAdapter sqlDa = new SQLiteDataAdapter("SELECT * FROM ComandosPredef", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                DVGpredefinidos.DataSource = dtbl;
            }
        }

        private void Predef_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

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

        private void DVGpredefinidos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (DVGpredefinidos.CurrentRow != null)
            {
                DataGridViewRow row = DVGpredefinidos.Rows[e.RowIndex];
                string selU = row.Cells["txtPredeID"].Value.ToString();
                string colunValue = DVGpredefinidos.Columns[e.ColumnIndex].HeaderText;
                string value2 = DVGpredefinidos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                try
                {

                    string value1 = DVGpredefinidos.CurrentRow.Cells[1].Value.ToString();
                    string txtQuery = "UPDATE ComandosPredef set " + colunValue + "='" + value2 + "' WHERE PredeID =" + selU;
                    ExecuteQuery(txtQuery);
                    PopulateDataGridView();


                }
                catch
                {
                    MessageBox.Show("Um valor nulo pode gerar futuros erros");
                    MessageBox.Show("Se deseja apagar um comando apenas selecione a linha e então(DELL)");
                }

            }

        }
        private void DVGpredefinidos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DVGpredefinidos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            DVGpredefinidos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DVGpredefinidos.AllowUserToDeleteRows = false;
        }


        private void DVGpredefinidos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = DVGpredefinidos.Rows[e.RowIndex];
            selU = row.Cells["txtPredeID"].Value.ToString();
            DVGpredefinidos.CurrentRow.Selected = true;

            string colunValue = DVGpredefinidos.Columns[e.ColumnIndex].HeaderText;
            string value2 = DVGpredefinidos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int columnIndex = DVGpredefinidos.CurrentCell.ColumnIndex;
            string ok4 = columnIndex.ToString();

            switch (ok4)
            {
                case "1":
                    ok5 = "Comando";
                    break;
                case "2":
                    ok5 = "Resposta";
                    break;
            }
            //if (columnIndex == 2)
            //{
            //    this.DVGpredefinidos.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            //}
            if(selU == "13")
            {
                this.DVGpredefinidos.Rows[e.RowIndex].Cells["txtComando"].ReadOnly = true;
            }
            if (selU == "5" | selU == "6" | selU == "7" | selU == "8" | selU == "9")
            {
                this.DVGpredefinidos.Rows[e.RowIndex].Cells["txtResposta"].ReadOnly = true;
            }
            else if (selU.Length < 1)
            {
                this.DVGpredefinidos.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }
            else
            {
                this.DVGpredefinidos.Rows[e.RowIndex].Cells["txtResposta"].ReadOnly = false;
            }

            //long number1 = 0;
            //bool canConvert = long.TryParse(textBox2.Text, out number1);
            //if (canConvert == true)
            //{
            //    Console.WriteLine("é numero", number1);
            //}
            //else
            //{
            //    Console.WriteLine("é string");
            //}
        }


        private void DVGpredefinidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //
        }

        private void DVGpredefinidos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = DVGpredefinidos.Rows[e.RowIndex];

            
            string s = e.FormattedValue.ToString();
            if (ok5 == "Comando")
            {
                if (s.ToLower().Contains('/'))
                {
                    DVGpredefinidos.Rows[e.RowIndex].ErrorText =
                        "Não é permitido caracteres barras (/)";

                    e.Cancel = true;

                    MessageBox.Show("Não é permitido caracteres barras (/)");
                }
                //if(s.Length < 1)
                //{
                //    e.Cancel = true;

                //    MessageBox.Show("Valores nulos não são permitidos");
                //}
            }
        }

        private void DVGpredefinidos_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter | e.KeyCode == Keys.Tab)
            //{
            //}
        }
    }
}
