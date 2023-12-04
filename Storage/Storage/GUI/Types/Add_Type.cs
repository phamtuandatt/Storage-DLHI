using ComponentFactory.Krypton.Ribbon;
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

namespace Storage.GUI.Types
{
    public partial class Add_Type : KryptonForm
    {
        public Add_Type()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtType.Text))
            {
                KryptonMessageBox.Show("Please fill all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return;
            }
            TypeDto type = new TypeDto()
            {
                Id = Guid.NewGuid(),
                Name = txtType.Text,
            };

            if (await Type_DAO.Add(type))
            {
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED_ITEMTYPE.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
