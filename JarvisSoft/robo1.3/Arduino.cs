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
    public partial class Arduino : Form
    {
        string connectionString = @"Data Source=dbon.sdb; Version=3; New=False; Compress = true;";
        string ok5;
        string selU;

        public Arduino()
        {
            InitializeComponent();
        }

        void PopulatePositionComboBox()
        {
            using (SQLiteConnection sqlCon = new SQLiteConnection(connectionString))
            {
                sqlCon.Open();
                SQLiteDataAdapter sqlDa = new SQLiteDataAdapter("SELECT * FROM Porta1", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                cbxPortaCom.ValueMember = "PortaCom";
                cbxPortaCom.DisplayMember = "Porta";
                DataRow topItem = dtbl.NewRow();
                topItem[0] = 0;
                topItem[1] = "-Select-";
                dtbl.Rows.InsertAt(topItem, 0);
                cbxPortaCom.DataSource = dtbl;
            }
        }

        void PopulateDataGridView()
        {
            using (SQLiteConnection sqlCon = new SQLiteConnection(connectionString))
            {
                sqlCon.Open();
                SQLiteDataAdapter sqlDa = new SQLiteDataAdapter("SELECT * FROM Employee", sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dgvEmployee.DataSource = dtbl;
            }
        }

        private void Arduino_Load(object sender, EventArgs e)
        {
            PopulatePositionComboBox();
            PopulateDataGridView();
        }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        //private SQLiteDataAdapter DB;
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

        private void dgvEmployee_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEmployee.CurrentRow != null)
            {
                DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];
                string selU = row.Cells["txtEmployeeID"].Value.ToString();
                string colunValue = dgvEmployee.Columns[e.ColumnIndex].HeaderText;
                string value2 = dgvEmployee.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                try
                {

                    if (selU.Length > 1)
                    {
                        string value1 = dgvEmployee.CurrentRow.Cells[1].Value.ToString();
                        string txtQuery = "UPDATE Employee set " + colunValue + "='" + value2 + "' WHERE EmployeeID =" + selU;
                        ExecuteQuery(txtQuery);
                        PopulateDataGridView();
                    }
                    else
                    {

                        string value1 = dgvEmployee.CurrentRow.Cells[1].Value.ToString();
                        //insert into X(id, dt) values(1, @dt)", conn
                        string txtQuery = "INSERT INTO Employee('" + ok5 + "')Values('" + value2 + "')";
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
        private void dgvEmployee_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvEmployee.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvEmployee.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvEmployee.AllowUserToDeleteRows = false;
        }


        private void dgvEmployee_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];
            selU = row.Cells["txtEmployeeID"].Value.ToString();
            dgvEmployee.CurrentRow.Selected = true;

            string colunValue = dgvEmployee.Columns[e.ColumnIndex].HeaderText;
            //string value2 = dgvEmployee.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            int columnIndex = dgvEmployee.CurrentCell.ColumnIndex;
            string ok4 = columnIndex.ToString();

            switch (ok4)
            {
                case "1":
                    ok5 = "Comando";
                    break;
                case "2":
                    ok5 = "Resposta";
                    break;
                case "3":
                    ok5 = "ComandoArduino";
                    break;
                case "4":
                    ok5 = "portaCom";
                    break;
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

        private void dgvEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Excluir linha! /Deseja Apagar esta linha?", "Alerta!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)

                {
                    MessageBox.Show("Linha deletada!");

                    string deletarComand =
                    dgvEmployee.SelectedRows[0].Cells[1].Value.ToString();
                    string txtQuery = "delete from Employee where EmployeeID='" + selU + "'";
                    ExecuteQuery(txtQuery);

                    dgvEmployee.DataSource = dgvEmployee.DataSource;
                    dgvEmployee.Refresh();
                    PopulateDataGridView();
                }
            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvEmployee.CurrentRow.Selected = true;
        }

        private void dgvEmployee_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string s = e.FormattedValue.ToString();
            if (ok5 == "Comando" | ok5 == "portaCom" | ok5 == "ComandoArduino")
            {
                if (s.ToLower().Contains('/'))
                {
                    dgvEmployee.Rows[e.RowIndex].ErrorText =
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
