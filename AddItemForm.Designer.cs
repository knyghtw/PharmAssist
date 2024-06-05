namespace PharmAssist
{
    partial class AddItemForm
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
            label1 = new Label();
            tb_nama_barang = new TextBox();
            tb_supp_barang = new TextBox();
            label2 = new Label();
            btn_new_supp = new Button();
            tb_satuan = new TextBox();
            label3 = new Label();
            tb_harga_beli = new TextBox();
            label4 = new Label();
            label5 = new Label();
            tb_hpp = new TextBox();
            tb_diskon = new TextBox();
            label6 = new Label();
            btn_savenew = new Button();
            btn_saveclose = new Button();
            btn_close = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "Nama Barang";
            // 
            // tb_nama_barang
            // 
            tb_nama_barang.Location = new Point(108, 6);
            tb_nama_barang.Name = "tb_nama_barang";
            tb_nama_barang.Size = new Size(464, 23);
            tb_nama_barang.TabIndex = 1;
            // 
            // tb_supp_barang
            // 
            tb_supp_barang.Location = new Point(108, 35);
            tb_supp_barang.Name = "tb_supp_barang";
            tb_supp_barang.Size = new Size(343, 23);
            tb_supp_barang.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 38);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 2;
            label2.Text = "Supplier Barang";
            // 
            // btn_new_supp
            // 
            btn_new_supp.Location = new Point(457, 34);
            btn_new_supp.Name = "btn_new_supp";
            btn_new_supp.Size = new Size(115, 23);
            btn_new_supp.TabIndex = 4;
            btn_new_supp.Text = "Supplier Baru";
            btn_new_supp.UseVisualStyleBackColor = true;
            btn_new_supp.Click += btn_new_supp_Click;
            // 
            // tb_satuan
            // 
            tb_satuan.Location = new Point(108, 63);
            tb_satuan.Name = "tb_satuan";
            tb_satuan.Size = new Size(464, 23);
            tb_satuan.TabIndex = 6;
            tb_satuan.Text = "Pcs";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 66);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 5;
            label3.Text = "Satuan Dasar";
            // 
            // tb_harga_beli
            // 
            tb_harga_beli.Location = new Point(108, 92);
            tb_harga_beli.Name = "tb_harga_beli";
            tb_harga_beli.Size = new Size(215, 23);
            tb_harga_beli.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 95);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 7;
            label4.Text = "Harga Beli";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(329, 95);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 9;
            label5.Text = "HPP (AVG)";
            // 
            // tb_hpp
            // 
            tb_hpp.Location = new Point(398, 92);
            tb_hpp.Name = "tb_hpp";
            tb_hpp.Size = new Size(174, 23);
            tb_hpp.TabIndex = 10;
            // 
            // tb_diskon
            // 
            tb_diskon.Location = new Point(108, 121);
            tb_diskon.Name = "tb_diskon";
            tb_diskon.Size = new Size(464, 23);
            tb_diskon.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 124);
            label6.Name = "label6";
            label6.Size = new Size(66, 15);
            label6.TabIndex = 11;
            label6.Text = "Diskon Jual";
            // 
            // btn_savenew
            // 
            btn_savenew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_savenew.Location = new Point(108, 160);
            btn_savenew.Name = "btn_savenew";
            btn_savenew.Size = new Size(155, 23);
            btn_savenew.TabIndex = 13;
            btn_savenew.Text = "Simpan && Baru";
            btn_savenew.UseVisualStyleBackColor = true;
            // 
            // btn_saveclose
            // 
            btn_saveclose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_saveclose.Location = new Point(269, 160);
            btn_saveclose.Name = "btn_saveclose";
            btn_saveclose.Size = new Size(140, 23);
            btn_saveclose.TabIndex = 14;
            btn_saveclose.Text = "Simpan && Tutup";
            btn_saveclose.UseVisualStyleBackColor = true;
            // 
            // btn_close
            // 
            btn_close.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_close.Location = new Point(417, 160);
            btn_close.Name = "btn_close";
            btn_close.Size = new Size(155, 23);
            btn_close.TabIndex = 15;
            btn_close.Text = "Tutup";
            btn_close.UseVisualStyleBackColor = true;
            // 
            // AddItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 201);
            Controls.Add(btn_close);
            Controls.Add(btn_saveclose);
            Controls.Add(btn_savenew);
            Controls.Add(tb_diskon);
            Controls.Add(label6);
            Controls.Add(tb_hpp);
            Controls.Add(label5);
            Controls.Add(tb_harga_beli);
            Controls.Add(label4);
            Controls.Add(tb_satuan);
            Controls.Add(label3);
            Controls.Add(btn_new_supp);
            Controls.Add(tb_supp_barang);
            Controls.Add(label2);
            Controls.Add(tb_nama_barang);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AddItemForm";
            Text = "Entry Data Barang";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tb_nama_barang;
        private TextBox tb_supp_barang;
        private Label label2;
        private Button btn_new_supp;
        private TextBox tb_satuan;
        private Label label3;
        private TextBox tb_harga_beli;
        private Label label4;
        private Label label5;
        private TextBox tb_hpp;
        private TextBox tb_diskon;
        private Label label6;
        private Button btn_savenew;
        private Button btn_saveclose;
        private Button btn_close;
    }
}