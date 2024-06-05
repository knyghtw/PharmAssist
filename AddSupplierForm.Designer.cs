namespace PharmAssist
{
    partial class AddSupplierForm
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
            tb_supplier_name = new TextBox();
            btn_simpan = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 0;
            label1.Text = "Nama Supplier";
            // 
            // tb_supplier_name
            // 
            tb_supplier_name.Location = new Point(12, 27);
            tb_supplier_name.Name = "tb_supplier_name";
            tb_supplier_name.Size = new Size(374, 23);
            tb_supplier_name.TabIndex = 1;
            // 
            // btn_simpan
            // 
            btn_simpan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_simpan.Location = new Point(12, 61);
            btn_simpan.Name = "btn_simpan";
            btn_simpan.Size = new Size(75, 23);
            btn_simpan.TabIndex = 2;
            btn_simpan.Text = "Simpan";
            btn_simpan.UseVisualStyleBackColor = true;
            // 
            // AddSupplierForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 96);
            Controls.Add(btn_simpan);
            Controls.Add(tb_supplier_name);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AddSupplierForm";
            Text = "Entry Data Supplier";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tb_supplier_name;
        private Button btn_simpan;
    }
}