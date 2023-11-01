using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Storage.DAO;
using Storage.DTOs;

namespace Storage.GUI.Supplier
{
    public partial class Add_Supplier : Form
    {
        public Add_Supplier()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SupplierDto supplier = new SupplierDto()
            {
                ID = Guid.NewGuid(),
                Name = txtName.Text,
                Address = "",
                Note = "",
            };

            if (SupplierDAO.Add(supplier))
            {
                MessageBox.Show("Done", "Notification");
            }
        }
    }
}
