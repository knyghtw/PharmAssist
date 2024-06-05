namespace PharmAssist
{
    partial class PharmAssist
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            menu_file = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            dataBarangToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            keluarToolStripMenuItem = new ToolStripMenuItem();
            adminToolStripMenuItem = new ToolStripMenuItem();
            resetMasterDataToolStripMenuItem = new ToolStripMenuItem();
            resetDataPembelianToolStripMenuItem = new ToolStripMenuItem();
            resetDataPenjualanToolStripMenuItem = new ToolStripMenuItem();
            hapusDaftarTransaksiToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            sqlCommandToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btn_hapus_supp = new Button();
            btn_edit_supp = new Button();
            btn_tambah_supp = new Button();
            panel1 = new Panel();
            btn_refresh_supp = new Button();
            textBox1 = new TextBox();
            tableSupplier = new TableLayoutPanel();
            tabPage2 = new TabPage();
            btn_hapus_brg = new Button();
            btn_edit_brg = new Button();
            btn_tambah_brg = new Button();
            tableBarang = new TableLayoutPanel();
            panel2 = new Panel();
            btn_refresh_brg = new Button();
            textBox2 = new TextBox();
            tabPage3 = new TabPage();
            btn_hapus_pembelian = new Button();
            btn_edit_pembelian = new Button();
            btn_tambah_pembelian = new Button();
            tabel_pembelian = new TableLayoutPanel();
            tb_harga = new TextBox();
            label4 = new Label();
            tb_diskon = new TextBox();
            label3 = new Label();
            tb_jumlah = new TextBox();
            label_jumlah = new Label();
            tb_satuan = new TextBox();
            label2 = new Label();
            tb_barang = new TextBox();
            label1 = new Label();
            tabPage4 = new TabPage();
            btn_hapus_pj = new Button();
            btn_edit_pj = new Button();
            btn_simpan_pj = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tb_pj_harga = new TextBox();
            tb_pj_disc = new TextBox();
            tb_pj_jml = new TextBox();
            tb_pj_satuan = new TextBox();
            tb_pj_brg = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            tabPage2.SuspendLayout();
            panel2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menu_file, adminToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(900, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menu_file
            // 
            menu_file.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem, dataBarangToolStripMenuItem, toolStripSeparator1, keluarToolStripMenuItem });
            menu_file.Name = "menu_file";
            menu_file.Size = new Size(37, 20);
            menu_file.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(144, 22);
            exitToolStripMenuItem.Text = "Data Supplier";
            // 
            // dataBarangToolStripMenuItem
            // 
            dataBarangToolStripMenuItem.Name = "dataBarangToolStripMenuItem";
            dataBarangToolStripMenuItem.Size = new Size(144, 22);
            dataBarangToolStripMenuItem.Text = "Data Barang";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(141, 6);
            // 
            // keluarToolStripMenuItem
            // 
            keluarToolStripMenuItem.Name = "keluarToolStripMenuItem";
            keluarToolStripMenuItem.Size = new Size(144, 22);
            keluarToolStripMenuItem.Text = "Keluar";
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resetMasterDataToolStripMenuItem, resetDataPembelianToolStripMenuItem, resetDataPenjualanToolStripMenuItem, hapusDaftarTransaksiToolStripMenuItem, toolStripSeparator3, sqlCommandToolStripMenuItem });
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new Size(55, 20);
            adminToolStripMenuItem.Text = "Admin";
            // 
            // resetMasterDataToolStripMenuItem
            // 
            resetMasterDataToolStripMenuItem.Name = "resetMasterDataToolStripMenuItem";
            resetMasterDataToolStripMenuItem.Size = new Size(193, 22);
            resetMasterDataToolStripMenuItem.Text = "Reset Master Data";
            // 
            // resetDataPembelianToolStripMenuItem
            // 
            resetDataPembelianToolStripMenuItem.Name = "resetDataPembelianToolStripMenuItem";
            resetDataPembelianToolStripMenuItem.Size = new Size(193, 22);
            resetDataPembelianToolStripMenuItem.Text = "Reset Data Pembelian";
            // 
            // resetDataPenjualanToolStripMenuItem
            // 
            resetDataPenjualanToolStripMenuItem.Name = "resetDataPenjualanToolStripMenuItem";
            resetDataPenjualanToolStripMenuItem.Size = new Size(193, 22);
            resetDataPenjualanToolStripMenuItem.Text = "Reset Data Penjualan";
            // 
            // hapusDaftarTransaksiToolStripMenuItem
            // 
            hapusDaftarTransaksiToolStripMenuItem.Name = "hapusDaftarTransaksiToolStripMenuItem";
            hapusDaftarTransaksiToolStripMenuItem.Size = new Size(193, 22);
            hapusDaftarTransaksiToolStripMenuItem.Text = "Hapus Daftar Transaksi";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(190, 6);
            // 
            // sqlCommandToolStripMenuItem
            // 
            sqlCommandToolStripMenuItem.Name = "sqlCommandToolStripMenuItem";
            sqlCommandToolStripMenuItem.Size = new Size(193, 22);
            sqlCommandToolStripMenuItem.Text = "Sql Command";
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(0, 23);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(900, 493);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btn_hapus_supp);
            tabPage1.Controls.Add(btn_edit_supp);
            tabPage1.Controls.Add(btn_tambah_supp);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(tableSupplier);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(892, 465);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Data Supplier";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_hapus_supp
            // 
            btn_hapus_supp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_hapus_supp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_hapus_supp.Enabled = false;
            btn_hapus_supp.Location = new Point(182, 440);
            btn_hapus_supp.Margin = new Padding(3, 2, 3, 2);
            btn_hapus_supp.Name = "btn_hapus_supp";
            btn_hapus_supp.Size = new Size(82, 23);
            btn_hapus_supp.TabIndex = 3;
            btn_hapus_supp.Text = "Hapus";
            btn_hapus_supp.UseVisualStyleBackColor = true;
            // 
            // btn_edit_supp
            // 
            btn_edit_supp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_edit_supp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_edit_supp.Enabled = false;
            btn_edit_supp.Location = new Point(94, 440);
            btn_edit_supp.Margin = new Padding(3, 2, 3, 2);
            btn_edit_supp.Name = "btn_edit_supp";
            btn_edit_supp.Size = new Size(82, 23);
            btn_edit_supp.TabIndex = 2;
            btn_edit_supp.Text = "Edit";
            btn_edit_supp.UseVisualStyleBackColor = true;
            // 
            // btn_tambah_supp
            // 
            btn_tambah_supp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_tambah_supp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_tambah_supp.Location = new Point(3, 440);
            btn_tambah_supp.Margin = new Padding(3, 2, 3, 2);
            btn_tambah_supp.Name = "btn_tambah_supp";
            btn_tambah_supp.Size = new Size(87, 23);
            btn_tambah_supp.TabIndex = 1;
            btn_tambah_supp.Text = "Tambah";
            btn_tambah_supp.UseVisualStyleBackColor = true;
            btn_tambah_supp.Click += btn_tambah_supp_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(btn_refresh_supp);
            panel1.Controls.Add(textBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 2);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(886, 31);
            panel1.TabIndex = 0;
            // 
            // btn_refresh_supp
            // 
            btn_refresh_supp.Location = new Point(251, 4);
            btn_refresh_supp.Margin = new Padding(3, 2, 3, 2);
            btn_refresh_supp.Name = "btn_refresh_supp";
            btn_refresh_supp.Size = new Size(82, 22);
            btn_refresh_supp.TabIndex = 1;
            btn_refresh_supp.Text = "Refresh";
            btn_refresh_supp.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(7, 4);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Cari data di sini...";
            textBox1.Size = new Size(239, 23);
            textBox1.TabIndex = 0;
            // 
            // tableSupplier
            // 
            tableSupplier.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableSupplier.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableSupplier.ColumnCount = 2;
            tableSupplier.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableSupplier.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95F));
            tableSupplier.Location = new Point(3, 32);
            tableSupplier.Margin = new Padding(3, 2, 3, 2);
            tableSupplier.Name = "tableSupplier";
            tableSupplier.RowCount = 10;
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableSupplier.Size = new Size(886, 404);
            tableSupplier.TabIndex = 1;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btn_hapus_brg);
            tabPage2.Controls.Add(btn_edit_brg);
            tabPage2.Controls.Add(btn_tambah_brg);
            tabPage2.Controls.Add(tableBarang);
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(3, 2, 3, 2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 2, 3, 2);
            tabPage2.Size = new Size(892, 465);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Data Barang";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_hapus_brg
            // 
            btn_hapus_brg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_hapus_brg.Enabled = false;
            btn_hapus_brg.Location = new Point(170, 435);
            btn_hapus_brg.Name = "btn_hapus_brg";
            btn_hapus_brg.Size = new Size(75, 23);
            btn_hapus_brg.TabIndex = 4;
            btn_hapus_brg.Text = "Hapus";
            btn_hapus_brg.UseVisualStyleBackColor = true;
            // 
            // btn_edit_brg
            // 
            btn_edit_brg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_edit_brg.Enabled = false;
            btn_edit_brg.Location = new Point(89, 435);
            btn_edit_brg.Name = "btn_edit_brg";
            btn_edit_brg.Size = new Size(75, 23);
            btn_edit_brg.TabIndex = 3;
            btn_edit_brg.Text = "Edit";
            btn_edit_brg.UseVisualStyleBackColor = true;
            // 
            // btn_tambah_brg
            // 
            btn_tambah_brg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_tambah_brg.Location = new Point(8, 435);
            btn_tambah_brg.Name = "btn_tambah_brg";
            btn_tambah_brg.Size = new Size(75, 23);
            btn_tambah_brg.TabIndex = 2;
            btn_tambah_brg.Text = "Tambah";
            btn_tambah_brg.UseVisualStyleBackColor = true;
            btn_tambah_brg.Click += this.btn_tambah_brg_Click;
            // 
            // tableBarang
            // 
            tableBarang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableBarang.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableBarang.ColumnCount = 7;
            tableBarang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableBarang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableBarang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableBarang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableBarang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableBarang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableBarang.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableBarang.Location = new Point(3, 32);
            tableBarang.Name = "tableBarang";
            tableBarang.RowCount = 10;
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableBarang.Size = new Size(886, 397);
            tableBarang.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Controls.Add(btn_refresh_brg);
            panel2.Controls.Add(textBox2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(886, 30);
            panel2.TabIndex = 0;
            // 
            // btn_refresh_brg
            // 
            btn_refresh_brg.Location = new Point(214, 3);
            btn_refresh_brg.Margin = new Padding(3, 2, 3, 2);
            btn_refresh_brg.Name = "btn_refresh_brg";
            btn_refresh_brg.Size = new Size(82, 23);
            btn_refresh_brg.TabIndex = 2;
            btn_refresh_brg.Text = "Refresh";
            btn_refresh_brg.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(8, 3);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Cari data disini...";
            textBox2.Size = new Size(200, 23);
            textBox2.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btn_hapus_pembelian);
            tabPage3.Controls.Add(btn_edit_pembelian);
            tabPage3.Controls.Add(btn_tambah_pembelian);
            tabPage3.Controls.Add(tabel_pembelian);
            tabPage3.Controls.Add(tb_harga);
            tabPage3.Controls.Add(label4);
            tabPage3.Controls.Add(tb_diskon);
            tabPage3.Controls.Add(label3);
            tabPage3.Controls.Add(tb_jumlah);
            tabPage3.Controls.Add(label_jumlah);
            tabPage3.Controls.Add(tb_satuan);
            tabPage3.Controls.Add(label2);
            tabPage3.Controls.Add(tb_barang);
            tabPage3.Controls.Add(label1);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Margin = new Padding(3, 2, 3, 2);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3, 2, 3, 2);
            tabPage3.Size = new Size(892, 465);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Pembelian";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_hapus_pembelian
            // 
            btn_hapus_pembelian.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_hapus_pembelian.Enabled = false;
            btn_hapus_pembelian.Location = new Point(162, 436);
            btn_hapus_pembelian.Name = "btn_hapus_pembelian";
            btn_hapus_pembelian.Size = new Size(75, 23);
            btn_hapus_pembelian.TabIndex = 13;
            btn_hapus_pembelian.Text = "Hapus";
            btn_hapus_pembelian.UseVisualStyleBackColor = true;
            // 
            // btn_edit_pembelian
            // 
            btn_edit_pembelian.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_edit_pembelian.Enabled = false;
            btn_edit_pembelian.Location = new Point(81, 436);
            btn_edit_pembelian.Name = "btn_edit_pembelian";
            btn_edit_pembelian.Size = new Size(75, 23);
            btn_edit_pembelian.TabIndex = 12;
            btn_edit_pembelian.Text = "Edit";
            btn_edit_pembelian.UseVisualStyleBackColor = true;
            // 
            // btn_tambah_pembelian
            // 
            btn_tambah_pembelian.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_tambah_pembelian.Location = new Point(0, 436);
            btn_tambah_pembelian.Name = "btn_tambah_pembelian";
            btn_tambah_pembelian.Size = new Size(75, 23);
            btn_tambah_pembelian.TabIndex = 11;
            btn_tambah_pembelian.Text = "Tambah";
            btn_tambah_pembelian.UseVisualStyleBackColor = true;
            // 
            // tabel_pembelian
            // 
            tabel_pembelian.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabel_pembelian.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tabel_pembelian.ColumnCount = 6;
            tabel_pembelian.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tabel_pembelian.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.293766F));
            tabel_pembelian.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.4265547F));
            tabel_pembelian.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.4265585F));
            tabel_pembelian.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.4265585F));
            tabel_pembelian.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.4265585F));
            tabel_pembelian.Location = new Point(0, 43);
            tabel_pembelian.Name = "tabel_pembelian";
            tabel_pembelian.RowCount = 10;
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tabel_pembelian.Size = new Size(892, 390);
            tabel_pembelian.TabIndex = 10;
            // 
            // tb_harga
            // 
            tb_harga.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_harga.Location = new Point(687, 20);
            tb_harga.Name = "tb_harga";
            tb_harga.Size = new Size(205, 23);
            tb_harga.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(687, 2);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 8;
            label4.Text = "Harga";
            // 
            // tb_diskon
            // 
            tb_diskon.Location = new Point(587, 20);
            tb_diskon.Name = "tb_diskon";
            tb_diskon.Size = new Size(100, 23);
            tb_diskon.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(587, 2);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 6;
            label3.Text = "Diskon %";
            // 
            // tb_jumlah
            // 
            tb_jumlah.Location = new Point(487, 20);
            tb_jumlah.Name = "tb_jumlah";
            tb_jumlah.Size = new Size(100, 23);
            tb_jumlah.TabIndex = 5;
            // 
            // label_jumlah
            // 
            label_jumlah.AutoSize = true;
            label_jumlah.Location = new Point(487, 2);
            label_jumlah.Name = "label_jumlah";
            label_jumlah.Size = new Size(45, 15);
            label_jumlah.TabIndex = 4;
            label_jumlah.Text = "Jumlah";
            // 
            // tb_satuan
            // 
            tb_satuan.Location = new Point(387, 20);
            tb_satuan.Name = "tb_satuan";
            tb_satuan.Size = new Size(100, 23);
            tb_satuan.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(387, 2);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 2;
            label2.Text = "Satuan";
            // 
            // tb_barang
            // 
            tb_barang.Location = new Point(0, 20);
            tb_barang.Name = "tb_barang";
            tb_barang.Size = new Size(387, 23);
            tb_barang.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 2);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "Nama Barang";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(btn_hapus_pj);
            tabPage4.Controls.Add(btn_edit_pj);
            tabPage4.Controls.Add(btn_simpan_pj);
            tabPage4.Controls.Add(tableLayoutPanel1);
            tabPage4.Controls.Add(tb_pj_harga);
            tabPage4.Controls.Add(tb_pj_disc);
            tabPage4.Controls.Add(tb_pj_jml);
            tabPage4.Controls.Add(tb_pj_satuan);
            tabPage4.Controls.Add(tb_pj_brg);
            tabPage4.Controls.Add(label9);
            tabPage4.Controls.Add(label8);
            tabPage4.Controls.Add(label7);
            tabPage4.Controls.Add(label6);
            tabPage4.Controls.Add(label5);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Margin = new Padding(3, 2, 3, 2);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3, 2, 3, 2);
            tabPage4.Size = new Size(892, 465);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Penjualan";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // btn_hapus_pj
            // 
            btn_hapus_pj.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_hapus_pj.Enabled = false;
            btn_hapus_pj.Location = new Point(162, 442);
            btn_hapus_pj.Name = "btn_hapus_pj";
            btn_hapus_pj.Size = new Size(75, 23);
            btn_hapus_pj.TabIndex = 13;
            btn_hapus_pj.Text = "Hapus";
            btn_hapus_pj.UseVisualStyleBackColor = true;
            // 
            // btn_edit_pj
            // 
            btn_edit_pj.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_edit_pj.Enabled = false;
            btn_edit_pj.Location = new Point(81, 442);
            btn_edit_pj.Name = "btn_edit_pj";
            btn_edit_pj.Size = new Size(75, 23);
            btn_edit_pj.TabIndex = 12;
            btn_edit_pj.Text = "Edit";
            btn_edit_pj.UseVisualStyleBackColor = true;
            // 
            // btn_simpan_pj
            // 
            btn_simpan_pj.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_simpan_pj.Location = new Point(0, 442);
            btn_simpan_pj.Name = "btn_simpan_pj";
            btn_simpan_pj.Size = new Size(75, 23);
            btn_simpan_pj.TabIndex = 11;
            btn_simpan_pj.Text = "Simpan";
            btn_simpan_pj.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.0199471F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2450161F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2450161F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2450161F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2450161F));
            tableLayoutPanel1.Location = new Point(0, 43);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 10;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(892, 388);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // tb_pj_harga
            // 
            tb_pj_harga.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_pj_harga.Location = new Point(713, 20);
            tb_pj_harga.Name = "tb_pj_harga";
            tb_pj_harga.Size = new Size(179, 23);
            tb_pj_harga.TabIndex = 9;
            // 
            // tb_pj_disc
            // 
            tb_pj_disc.Location = new Point(611, 20);
            tb_pj_disc.Name = "tb_pj_disc";
            tb_pj_disc.Size = new Size(103, 23);
            tb_pj_disc.TabIndex = 8;
            // 
            // tb_pj_jml
            // 
            tb_pj_jml.Location = new Point(510, 20);
            tb_pj_jml.Name = "tb_pj_jml";
            tb_pj_jml.Size = new Size(103, 23);
            tb_pj_jml.TabIndex = 7;
            // 
            // tb_pj_satuan
            // 
            tb_pj_satuan.Location = new Point(407, 20);
            tb_pj_satuan.Name = "tb_pj_satuan";
            tb_pj_satuan.Size = new Size(104, 23);
            tb_pj_satuan.TabIndex = 6;
            // 
            // tb_pj_brg
            // 
            tb_pj_brg.Location = new Point(0, 20);
            tb_pj_brg.Name = "tb_pj_brg";
            tb_pj_brg.Size = new Size(408, 23);
            tb_pj_brg.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(713, 2);
            label9.Name = "label9";
            label9.Size = new Size(39, 15);
            label9.TabIndex = 4;
            label9.Text = "Harga";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(611, 2);
            label8.Name = "label8";
            label8.Size = new Size(56, 15);
            label8.TabIndex = 3;
            label8.Text = "Diskon %";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(510, 2);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 2;
            label7.Text = "Jumlah";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(407, 2);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 1;
            label6.Text = "Satuan";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 2);
            label5.Name = "label5";
            label5.Size = new Size(79, 15);
            label5.TabIndex = 0;
            label5.Text = "Nama Barang";
            // 
            // PharmAssist
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 518);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "PharmAssist";
            Text = "PharmAssist";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabPage2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menu_file;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem adminToolStripMenuItem;
        private ToolStripMenuItem dataBarangToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem keluarToolStripMenuItem;
        private ToolStripMenuItem resetMasterDataToolStripMenuItem;
        private ToolStripMenuItem resetDataPembelianToolStripMenuItem;
        private ToolStripMenuItem resetDataPenjualanToolStripMenuItem;
        private ToolStripMenuItem hapusDaftarTransaksiToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem sqlCommandToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Panel panel1;
        public TextBox textBox1;
        private Button btn_tambah_supp;
        private TableLayoutPanel tableSupplier;
        private Button btn_refresh_supp;
        private Button btn_hapus_supp;
        private Button btn_edit_supp;
        private TableLayoutPanel tableBarang;
        private Panel panel2;
        private Button btn_refresh_brg;
        private TextBox textBox2;
        private Button btn_tambah_brg;
        private Button btn_hapus_brg;
        private Button btn_edit_brg;
        private TextBox tb_harga;
        private Label label4;
        private TextBox tb_diskon;
        private Label label3;
        private TextBox tb_jumlah;
        private Label label_jumlah;
        private TextBox tb_satuan;
        private Label label2;
        private TextBox tb_barang;
        private Label label1;
        private Button btn_tambah_pembelian;
        private TableLayoutPanel tabel_pembelian;
        private Button btn_edit_pembelian;
        private Button btn_hapus_pembelian;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox tb_pj_brg;
        private Label label9;
        private TextBox tb_pj_harga;
        private TextBox tb_pj_disc;
        private TextBox tb_pj_jml;
        private TextBox tb_pj_satuan;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btn_edit_pj;
        private Button btn_simpan_pj;
        private Button btn_hapus_pj;
    }
}
