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

namespace Storage.GUI.Groups
{
    public partial class Add_Group : KryptonForm
    {
        public Add_Group()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGroup.Text)) 
            {
                KryptonMessageBox.Show("Please fill all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGroup.Focus();
                return;
            }
            GroupDto dto = new GroupDto()
            {
                Id = Guid.NewGuid(),
                Name = txtGroup.Text,
            };

            if (await Group_DAO.Add(dto))
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
