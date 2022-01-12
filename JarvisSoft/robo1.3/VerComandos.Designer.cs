namespace robo1._3
{
    partial class VerComandos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DVGcomandSoci = new System.Windows.Forms.DataGridView();
            this.txtgramaticaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGramatica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DVGcomandSoci)).BeginInit();
            this.SuspendLayout();
            // 
            // DVGcomandSoci
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.DVGcomandSoci.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DVGcomandSoci.BackgroundColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DVGcomandSoci.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DVGcomandSoci.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DVGcomandSoci.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtgramaticaID,
            this.txtGramatica});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DVGcomandSoci.DefaultCellStyle = dataGridViewCellStyle5;
            this.DVGcomandSoci.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DVGcomandSoci.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DVGcomandSoci.Location = new System.Drawing.Point(0, 0);
            this.DVGcomandSoci.Name = "DVGcomandSoci";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DVGcomandSoci.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.DVGcomandSoci.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.DVGcomandSoci.Size = new System.Drawing.Size(800, 586);
            this.DVGcomandSoci.TabIndex = 4;
            this.DVGcomandSoci.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DVGcomandSoci_CellClick);
            this.DVGcomandSoci.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DVGcomandSoci_CellEnter);
            this.DVGcomandSoci.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DVGcomandSoci_CellValidating);
            this.DVGcomandSoci.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DVGcomandSoci_CellValueChanged);
            this.DVGcomandSoci.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DVGcomandSoci_RowPostPaint);
            this.DVGcomandSoci.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DVGcomandSoci_KeyDown);
            // 
            // txtgramaticaID
            // 
            this.txtgramaticaID.DataPropertyName = "gramaticaID";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.txtgramaticaID.DefaultCellStyle = dataGridViewCellStyle3;
            this.txtgramaticaID.HeaderText = "SociaisID";
            this.txtgramaticaID.Name = "txtgramaticaID";
            this.txtgramaticaID.Visible = false;
            // 
            // txtGramatica
            // 
            this.txtGramatica.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtGramatica.DataPropertyName = "Gramatica";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.txtGramatica.DefaultCellStyle = dataGridViewCellStyle4;
            this.txtGramatica.HeaderText = "Comando";
            this.txtGramatica.Name = "txtGramatica";
            // 
            // VerComandos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 586);
            this.Controls.Add(this.DVGcomandSoci);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VerComandos";
            this.Text = "VerComandos";
            this.Load += new System.EventHandler(this.Soc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DVGcomandSoci)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DVGcomandSoci;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtgramaticaID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtGramatica;
    }
}