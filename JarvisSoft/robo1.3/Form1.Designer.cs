namespace robo1._3
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butonCam = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuFlatButton3 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnMenuPredefinidos = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnMenuSociais = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnMenuArduino = new Bunifu.Framework.UI.BunifuFlatButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelSubCont = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelRotulo = new System.Windows.Forms.Label();
            this.bunifuMenu = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuImageButton6 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnMax = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton2 = new Bunifu.Framework.UI.BunifuImageButton();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelSubCont.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.AccessibleDescription = "panelMenu";
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panelMenu.Controls.Add(this.panel3);
            this.panelMenu.Controls.Add(this.panel2);
            this.panelMenu.Controls.Add(this.butonCam);
            this.panelMenu.Controls.Add(this.bunifuFlatButton3);
            this.panelMenu.Controls.Add(this.btnMenuPredefinidos);
            this.panelMenu.Controls.Add(this.btnMenuSociais);
            this.panelMenu.Controls.Add(this.btnMenuArduino);
            this.panelMenu.Location = new System.Drawing.Point(3, 38);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(237, 321);
            this.panelMenu.TabIndex = 12;
            this.panelMenu.Visible = false;
            this.panelMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMenu_MouseDown);
            this.panelMenu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMenu_MouseMove);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 238);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(237, 52);
            this.panel3.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(220, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(17, 52);
            this.panel5.TabIndex = 13;
            this.panel5.MouseEnter += new System.EventHandler(this.panel5_MouseEnter);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this.panel4.Size = new System.Drawing.Size(218, 52);
            this.panel4.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 290);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(237, 31);
            this.panel2.TabIndex = 10;
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            // 
            // butonCam
            // 
            this.butonCam.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.butonCam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.butonCam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butonCam.BorderRadius = 0;
            this.butonCam.ButtonText = "Configurar Camera";
            this.butonCam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butonCam.DisabledColor = System.Drawing.Color.Gray;
            this.butonCam.Iconcolor = System.Drawing.Color.Transparent;
            this.butonCam.Iconimage = ((System.Drawing.Image)(resources.GetObject("butonCam.Iconimage")));
            this.butonCam.Iconimage_right = null;
            this.butonCam.Iconimage_right_Selected = null;
            this.butonCam.Iconimage_Selected = null;
            this.butonCam.IconMarginLeft = 0;
            this.butonCam.IconMarginRight = 0;
            this.butonCam.IconRightVisible = true;
            this.butonCam.IconRightZoom = 0D;
            this.butonCam.IconVisible = true;
            this.butonCam.IconZoom = 60D;
            this.butonCam.IsTab = false;
            this.butonCam.Location = new System.Drawing.Point(3, 193);
            this.butonCam.Name = "butonCam";
            this.butonCam.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.butonCam.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.butonCam.OnHoverTextColor = System.Drawing.Color.White;
            this.butonCam.selected = false;
            this.butonCam.Size = new System.Drawing.Size(215, 44);
            this.butonCam.TabIndex = 9;
            this.butonCam.Text = "Configurar Camera";
            this.butonCam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.butonCam.Textcolor = System.Drawing.Color.White;
            this.butonCam.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butonCam.Click += new System.EventHandler(this.butonCam_Click);
            // 
            // bunifuFlatButton3
            // 
            this.bunifuFlatButton3.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton3.BorderRadius = 0;
            this.bunifuFlatButton3.ButtonText = "Inicio";
            this.bunifuFlatButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButton3.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton3.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton3.Iconimage = ((System.Drawing.Image)(resources.GetObject("bunifuFlatButton3.Iconimage")));
            this.bunifuFlatButton3.Iconimage_right = null;
            this.bunifuFlatButton3.Iconimage_right_Selected = null;
            this.bunifuFlatButton3.Iconimage_Selected = null;
            this.bunifuFlatButton3.IconMarginLeft = 0;
            this.bunifuFlatButton3.IconMarginRight = 0;
            this.bunifuFlatButton3.IconRightVisible = true;
            this.bunifuFlatButton3.IconRightZoom = 0D;
            this.bunifuFlatButton3.IconVisible = true;
            this.bunifuFlatButton3.IconZoom = 60D;
            this.bunifuFlatButton3.IsTab = false;
            this.bunifuFlatButton3.Location = new System.Drawing.Point(3, 4);
            this.bunifuFlatButton3.Name = "bunifuFlatButton3";
            this.bunifuFlatButton3.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton3.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bunifuFlatButton3.OnHoverTextColor = System.Drawing.Color.White;
            this.bunifuFlatButton3.selected = false;
            this.bunifuFlatButton3.Size = new System.Drawing.Size(215, 45);
            this.bunifuFlatButton3.TabIndex = 7;
            this.bunifuFlatButton3.Text = "Inicio";
            this.bunifuFlatButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuFlatButton3.Textcolor = System.Drawing.Color.White;
            this.bunifuFlatButton3.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton3.Click += new System.EventHandler(this.bunifuFlatButton3_Click_1);
            // 
            // btnMenuPredefinidos
            // 
            this.btnMenuPredefinidos.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuPredefinidos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuPredefinidos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMenuPredefinidos.BorderRadius = 0;
            this.btnMenuPredefinidos.ButtonText = "Comandos Predefinidos";
            this.btnMenuPredefinidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuPredefinidos.DisabledColor = System.Drawing.Color.Gray;
            this.btnMenuPredefinidos.Iconcolor = System.Drawing.Color.Transparent;
            this.btnMenuPredefinidos.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnMenuPredefinidos.Iconimage")));
            this.btnMenuPredefinidos.Iconimage_right = null;
            this.btnMenuPredefinidos.Iconimage_right_Selected = null;
            this.btnMenuPredefinidos.Iconimage_Selected = null;
            this.btnMenuPredefinidos.IconMarginLeft = 0;
            this.btnMenuPredefinidos.IconMarginRight = 0;
            this.btnMenuPredefinidos.IconRightVisible = true;
            this.btnMenuPredefinidos.IconRightZoom = 0D;
            this.btnMenuPredefinidos.IconVisible = true;
            this.btnMenuPredefinidos.IconZoom = 60D;
            this.btnMenuPredefinidos.IsTab = false;
            this.btnMenuPredefinidos.Location = new System.Drawing.Point(3, 145);
            this.btnMenuPredefinidos.Name = "btnMenuPredefinidos";
            this.btnMenuPredefinidos.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuPredefinidos.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnMenuPredefinidos.OnHoverTextColor = System.Drawing.Color.White;
            this.btnMenuPredefinidos.selected = false;
            this.btnMenuPredefinidos.Size = new System.Drawing.Size(215, 48);
            this.btnMenuPredefinidos.TabIndex = 3;
            this.btnMenuPredefinidos.Text = "Comandos Predefinidos";
            this.btnMenuPredefinidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMenuPredefinidos.Textcolor = System.Drawing.Color.White;
            this.btnMenuPredefinidos.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuPredefinidos.Click += new System.EventHandler(this.bunifuFlatButton3_Click);
            // 
            // btnMenuSociais
            // 
            this.btnMenuSociais.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuSociais.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuSociais.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMenuSociais.BorderRadius = 0;
            this.btnMenuSociais.ButtonText = "Comandos Sociais";
            this.btnMenuSociais.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuSociais.DisabledColor = System.Drawing.Color.Gray;
            this.btnMenuSociais.Iconcolor = System.Drawing.Color.Transparent;
            this.btnMenuSociais.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnMenuSociais.Iconimage")));
            this.btnMenuSociais.Iconimage_right = null;
            this.btnMenuSociais.Iconimage_right_Selected = null;
            this.btnMenuSociais.Iconimage_Selected = null;
            this.btnMenuSociais.IconMarginLeft = 0;
            this.btnMenuSociais.IconMarginRight = 0;
            this.btnMenuSociais.IconRightVisible = true;
            this.btnMenuSociais.IconRightZoom = 0D;
            this.btnMenuSociais.IconVisible = true;
            this.btnMenuSociais.IconZoom = 60D;
            this.btnMenuSociais.IsTab = false;
            this.btnMenuSociais.Location = new System.Drawing.Point(3, 97);
            this.btnMenuSociais.Name = "btnMenuSociais";
            this.btnMenuSociais.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuSociais.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnMenuSociais.OnHoverTextColor = System.Drawing.Color.White;
            this.btnMenuSociais.selected = false;
            this.btnMenuSociais.Size = new System.Drawing.Size(215, 48);
            this.btnMenuSociais.TabIndex = 2;
            this.btnMenuSociais.Text = "Comandos Sociais";
            this.btnMenuSociais.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMenuSociais.Textcolor = System.Drawing.Color.White;
            this.btnMenuSociais.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuSociais.Click += new System.EventHandler(this.bunifuFlatButton2_Click);
            // 
            // btnMenuArduino
            // 
            this.btnMenuArduino.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuArduino.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuArduino.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMenuArduino.BorderRadius = 0;
            this.btnMenuArduino.ButtonText = "Comandos Spotfy";
            this.btnMenuArduino.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuArduino.DisabledColor = System.Drawing.Color.Gray;
            this.btnMenuArduino.Iconcolor = System.Drawing.Color.Transparent;
            this.btnMenuArduino.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnMenuArduino.Iconimage")));
            this.btnMenuArduino.Iconimage_right = null;
            this.btnMenuArduino.Iconimage_right_Selected = null;
            this.btnMenuArduino.Iconimage_Selected = null;
            this.btnMenuArduino.IconMarginLeft = 0;
            this.btnMenuArduino.IconMarginRight = 0;
            this.btnMenuArduino.IconRightVisible = true;
            this.btnMenuArduino.IconRightZoom = 0D;
            this.btnMenuArduino.IconVisible = true;
            this.btnMenuArduino.IconZoom = 60D;
            this.btnMenuArduino.IsTab = false;
            this.btnMenuArduino.Location = new System.Drawing.Point(3, 49);
            this.btnMenuArduino.Name = "btnMenuArduino";
            this.btnMenuArduino.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnMenuArduino.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnMenuArduino.OnHoverTextColor = System.Drawing.Color.White;
            this.btnMenuArduino.selected = false;
            this.btnMenuArduino.Size = new System.Drawing.Size(215, 48);
            this.btnMenuArduino.TabIndex = 1;
            this.btnMenuArduino.Text = "Comandos Spotfy";
            this.btnMenuArduino.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMenuArduino.Textcolor = System.Drawing.Color.White;
            this.btnMenuArduino.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuArduino.Click += new System.EventHandler(this.btnArduino);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "(*.sdb)|*.sdb";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelSubCont, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 586);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelSubCont
            // 
            this.panelSubCont.Controls.Add(this.panelContent);
            this.panelSubCont.Controls.Add(this.panel1);
            this.panelSubCont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSubCont.Location = new System.Drawing.Point(0, 0);
            this.panelSubCont.Margin = new System.Windows.Forms.Padding(0);
            this.panelSubCont.Name = "panelSubCont";
            this.panelSubCont.Size = new System.Drawing.Size(800, 566);
            this.panelSubCont.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 35);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(800, 531);
            this.panelContent.TabIndex = 3;
            this.panelContent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelContent_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.labelRotulo);
            this.panel1.Controls.Add(this.bunifuMenu);
            this.panel1.Controls.Add(this.bunifuImageButton6);
            this.panel1.Controls.Add(this.btnMax);
            this.panel1.Controls.Add(this.bunifuImageButton2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 35);
            this.panel1.TabIndex = 2;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // labelRotulo
            // 
            this.labelRotulo.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelRotulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRotulo.ForeColor = System.Drawing.Color.Snow;
            this.labelRotulo.Location = new System.Drawing.Point(641, 0);
            this.labelRotulo.Name = "labelRotulo";
            this.labelRotulo.Size = new System.Drawing.Size(63, 35);
            this.labelRotulo.TabIndex = 15;
            this.labelRotulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuMenu
            // 
            this.bunifuMenu.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(102)))));
            this.bunifuMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.bunifuMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuMenu.BorderRadius = 0;
            this.bunifuMenu.ButtonText = "Menu";
            this.bunifuMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuMenu.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.bunifuMenu.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuMenu.Iconimage = ((System.Drawing.Image)(resources.GetObject("bunifuMenu.Iconimage")));
            this.bunifuMenu.Iconimage_right = null;
            this.bunifuMenu.Iconimage_right_Selected = null;
            this.bunifuMenu.Iconimage_Selected = null;
            this.bunifuMenu.IconMarginLeft = 0;
            this.bunifuMenu.IconMarginRight = 0;
            this.bunifuMenu.IconRightVisible = true;
            this.bunifuMenu.IconRightZoom = 0D;
            this.bunifuMenu.IconVisible = true;
            this.bunifuMenu.IconZoom = 60D;
            this.bunifuMenu.IsTab = false;
            this.bunifuMenu.Location = new System.Drawing.Point(0, 0);
            this.bunifuMenu.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.bunifuMenu.Name = "bunifuMenu";
            this.bunifuMenu.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(30)))));
            this.bunifuMenu.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(47)))), ((int)(((byte)(54)))));
            this.bunifuMenu.OnHoverTextColor = System.Drawing.Color.White;
            this.bunifuMenu.selected = false;
            this.bunifuMenu.Size = new System.Drawing.Size(224, 35);
            this.bunifuMenu.TabIndex = 10;
            this.bunifuMenu.Text = "Menu";
            this.bunifuMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuMenu.Textcolor = System.Drawing.Color.White;
            this.bunifuMenu.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuMenu.Click += new System.EventHandler(this.bunifuMenu_Click);
            // 
            // bunifuImageButton6
            // 
            this.bunifuImageButton6.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton6.Dock = System.Windows.Forms.DockStyle.Right;
            this.bunifuImageButton6.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton6.Image")));
            this.bunifuImageButton6.ImageActive = null;
            this.bunifuImageButton6.Location = new System.Drawing.Point(704, 0);
            this.bunifuImageButton6.Name = "bunifuImageButton6";
            this.bunifuImageButton6.Size = new System.Drawing.Size(32, 35);
            this.bunifuImageButton6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton6.TabIndex = 14;
            this.bunifuImageButton6.TabStop = false;
            this.bunifuImageButton6.Zoom = 10;
            this.bunifuImageButton6.Click += new System.EventHandler(this.bunifuImageButton6_Click);
            // 
            // btnMax
            // 
            this.btnMax.AccessibleDescription = "btnMax";
            this.btnMax.BackColor = System.Drawing.Color.Transparent;
            this.btnMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMax.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMax.Image = ((System.Drawing.Image)(resources.GetObject("btnMax.Image")));
            this.btnMax.ImageActive = null;
            this.btnMax.Location = new System.Drawing.Point(736, 0);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(32, 35);
            this.btnMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMax.TabIndex = 13;
            this.btnMax.TabStop = false;
            this.btnMax.Zoom = 10;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuImageButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.bunifuImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton2.Image")));
            this.bunifuImageButton2.ImageActive = null;
            this.bunifuImageButton2.Location = new System.Drawing.Point(768, 0);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(32, 35);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 12;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 10;
            this.bunifuImageButton2.Click += new System.EventHandler(this.bunifuImageButton2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(540, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Atualizar Servidor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(800, 586);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panelMenu.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelSubCont.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenu;
        private Bunifu.Framework.UI.BunifuFlatButton btnMenuPredefinidos;
        private Bunifu.Framework.UI.BunifuFlatButton btnMenuSociais;
        private Bunifu.Framework.UI.BunifuFlatButton btnMenuArduino;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton3;
        private Bunifu.Framework.UI.BunifuFlatButton butonCam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelSubCont;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuMenu;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton6;
        private Bunifu.Framework.UI.BunifuImageButton btnMax;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton2;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label labelRotulo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
    }
}

