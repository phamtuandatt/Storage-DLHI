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

namespace Storage.GUI.Suppliers
{
    public partial class Add_Supplier_V2 : KryptonForm
    {
        public Add_Supplier_V2()
        {
            InitializeComponent();
            LoadData();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtCusName.Text))
            {
                KryptonMessageBox.Show("Please fill in all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SupplierDto supplier = new SupplierDto()
            {
                ID = Guid.NewGuid(),
                Code = txtCode.Text,
                NameSupplier = txtCusName.Text,
                NameCompany = txtComName.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Note = txtNote.Text,
            };

            if (await SupplierDAO.Add(supplier))
            {
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Do you want to cancel the current action ?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                 == DialogResult.Cancel)
                return;

            this.Close();
        }

        private async void LoadData()
        {
            cboType.DataSource = await SupplierDAO.GetSuppierTypes();
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "ID";
        }

        private async void btnGetCode_Click(object sender, EventArgs e)
        {
            string code = cboType.Text;
            string number = await SupplierDAO.GetCurrentCodeSupplier(code.ToUpper().Trim());

            txtCode.Text = code + number;
        }

        private void txtCusName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtComName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.EMAIL.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
