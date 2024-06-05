namespace PharmAssist
{
    public partial class PharmAssist : Form
    {
        public PharmAssist()
        {
            InitializeComponent();
        }

        private void btn_tambah_supp_Click(object sender, EventArgs e)
        {
            var AddSuppForm = new AddSupplierForm();
            AddSuppForm.ShowDialog();
        }

        private void btn_tambah_brg_Click(object sender, EventArgs e)
        {
            var AddItemForm = new AddItemForm();
            AddItemForm.ShowDialog();
        }
    }
}
