using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;
using Storage.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI.LocationWarehouses
{
    public partial class Add_LocationWareHouse : KryptonForm
    {
        public Add_LocationWareHouse()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                KryptonMessageBox.Show("Please fill all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return;
            }
            LocationWarehousseDto locationWarehousseDto = new LocationWarehousseDto()
            {
                Id = Guid.NewGuid(),
                Name = txtLocation.Text
            };

            if (Warehouse_DAO.Add(locationWarehousseDto))
            {
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
