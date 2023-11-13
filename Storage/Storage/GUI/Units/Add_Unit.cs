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

namespace Storage.GUI.Units
{
    public partial class Add_Unit : KryptonForm
    {
        public Add_Unit()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UnitDto unitDto = new UnitDto() 
            {
                Id = Guid.NewGuid(),
                Name = txtUnit.Text,
            };

            if (Unit_DAO.Add(unitDto))
            {
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
