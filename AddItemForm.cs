using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmAssist
{
    public partial class AddItemForm : Form
    {
        public AddItemForm()
        {
            InitializeComponent();
        }

        private void btn_new_supp_Click(object sender, EventArgs e)
        {
            var AddSuppForm = new AddSupplierForm();
            AddSuppForm.ShowDialog();
        }
    }
}
