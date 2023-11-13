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
    public partial class Edit_Supplier : KryptonForm
    {
        public Guid Id { get; set; }
        public Edit_Supplier()
        {
            InitializeComponent();
        }

        public Edit_Supplier(Guid guid)
        {
            InitializeComponent();
            this.Id = guid;
            LoadData();
        }

        private void LoadData()
        {
            var dto = SupplierDAO.GetSupplier(Id);
            if (dto != null)
            {
                txtCode.Text = dto.Code;
                txtCusName.Text = dto.NameSupplier;
                txtComName.Text = dto.NameCompany;
                txtAddress.Text = dto.Address;
                txtPhone.Text = dto.Phone;
                txtEmail.Text = dto.Email;
                txtNote.Text = dto.Note;
                cboType.SelectedText = dto.Code.Substring(0, 3);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Do you want to cancel the current action ?", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel)
                return;
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SupplierDto supplier = new SupplierDto()
            {
                ID = this.Id,
                Code = txtCode.Text,
                NameSupplier = txtCusName.Text,
                NameCompany = txtComName.Text,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Note = txtNote.Text,
            };

            if (SupplierDAO.Update(supplier))
            {
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.EMAIL.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
