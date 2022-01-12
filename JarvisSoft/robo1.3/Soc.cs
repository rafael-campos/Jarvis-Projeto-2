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
    public partial class Soc : Form
    {
        string connectionString = @"Data Source=dbon.sdb; Version=3; New=False; Compress = true;";
        string ok5;
        string selU;

        public Soc()
        {
            InitializeComponent();
        }

        void PopulateDataGridView()
        {
            using (SQLiteConnection sqlCon = new SQLiteConnection(connectionString))
            {
                sqlCon.Open();
                SQLiteDataAdapter sqlDa = new SQLiteDataAdapter("SELECT * FROM comandSoci", sqlCon);
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

        private void DVGcomandSoci_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (DVGcomandSoci.CurrentRow != null)
            {
                DataGridViewRow row = DVGcomandSoci.Rows[e.RowIndex];
                string selU = row.Cells["txtSociaisID"].Value.ToString();
                string colunValue = DVGcomandSoci.Columns[e.ColumnIndex].HeaderText;
                string value2 = DVGcomandSoci.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                try
                {

                    if (selU.Length >= 1)
                    {
                        string value1 = DVGcomandSoci.CurrentRow.Cells[1].Value.ToString();
                        string txtQuery = "UPDATE comandSoci set " + colunValue + "='" + value2 + "' WHERE SociaisID =" + selU;
                        ExecuteQuery(txtQuery);
                        PopulateDataGridView();
                    }
                    else
                    {

                        string value1 = DVGcomandSoci.CurrentRow.Cells[1].Value.ToString();
                        //insert into X(id, dt) values(1, @dt)", conn
                        string txtQuery = "INSERT INTO comandSoci('" + ok5 + "')Values('" + value2 + "')";
                        ExecuteQuery(txtQuery);
                        PopulateDataGridView();
                    }
                }
                catch
                {
                    MessageBox.Show("Um valor nulo pode gerar futuros erros");
                    MessageBox.Show("Se deseja apagar um comando apenas selecione a linha e então(DELL)");
                }

            }

        }
        private void DVGcomandSoci_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DVGcomandSoci.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            DVGcomandSoci.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DVGcomandSoci.AllowUserToDeleteRows = false;
        }


        private void DVGcomandSoci_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = DVGcomandSoci.Rows[e.RowIndex];

            selU = row.Cells["txtSociaisID"].Value.ToString();
            DVGcomandSoci.CurrentRow.Selected = true;

            string colunValue = DVGcomandSoci.Columns[e.ColumnIndex].HeaderText;
            //string value2 = dgvEmployee.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            int columnIndex = DVGcomandSoci.CurrentCell.ColumnIndex;
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
        }

        private void DVGcomandSoci_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Excluir linha! /Deseja Apagar esta linha?", "Alerta!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)

                {
                    MessageBox.Show("Linha deletada!");

                    string deletarComand =
                    DVGcomandSoci.SelectedRows[0].Cells[1].Value.ToString();
                    string txtQuery = "delete from comandSoci where SociaisID='" + selU + "'";
                    ExecuteQuery(txtQuery);

                    DVGcomandSoci.DataSource = DVGcomandSoci.DataSource;
                    DVGcomandSoci.Refresh();
                    PopulateDataGridView();

                }
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
                //if (s.Length < 1)
                //{
                //    e.Cancel = true;

                //    MessageBox.Show("Valores nulos não são permitidos");
                //}
            }
        }
    }
}
