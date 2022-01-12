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
    public partial class VerComandos : Form
    {
        string connectionString = @"Data Source=DGVDB.sdb; Version=3; New=False; Compress = true;";
        string ok5;
        string selU;

        public VerComandos()
        {
            InitializeComponent();
        }

        void PopulateDataGridView()
        {
            using (SQLiteConnection sqlCon = new SQLiteConnection(connectionString))
            {
                sqlCon.Open();
                SQLiteDataAdapter sqlDa = new SQLiteDataAdapter("SELECT * FROM GramaticaRobo", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                DVGcomandSoci.DataSource = dtbl;
            }
        }

        private void Soc_Load(object sender, EventArgs e)
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
            sql_con = new SQLiteConnection("Data Source=DGVDB.sdb; Version=3; New=False; Compress = true;");
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

        private void DVGcomandSoci_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //
        }
        private void DVGcomandSoci_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DVGcomandSoci.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            DVGcomandSoci.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DVGcomandSoci.AllowUserToDeleteRows = false;

        }


        private void DVGcomandSoci_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.DVGcomandSoci.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
        }

        private void DVGcomandSoci_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                MessageBox.Show("Não pode apagar!");
            }
        }

        private void DVGcomandSoci_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //string colunValue = DVGcomandSoci.Columns[e.ColumnIndex].HeaderText;
            //string value2 = DVGcomandSoci.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            DVGcomandSoci.CurrentRow.Selected = true;
            // MessageBox.Show(colunValue);
        }

        private void DVGcomandSoci_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string s = e.FormattedValue.ToString();
            if (ok5 == "Comando")
            {
                if (s.ToLower().Contains('/'))
                {
                    DVGcomandSoci.Rows[e.RowIndex].ErrorText =
                        "Não é permitido caracteres barras (/)";

                    e.Cancel = true;

                    MessageBox.Show("Não é permitido caracteres barras (/)");
                }
            }
        }
    }
}
