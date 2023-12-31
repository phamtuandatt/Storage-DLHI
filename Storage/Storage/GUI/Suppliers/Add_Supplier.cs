﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;

namespace Storage.GUI.Suppliers
{
    public partial class Add_Supplier : KryptonForm
    {
        public Add_Supplier()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

            if (SupplierDAO.Add(supplier))
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

        private void LoadData()
        {
            cboType.DataSource = SupplierDAO.GetSuppierTypes();
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "ID";
        }

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            string code = cboType.Text;
            string number = SupplierDAO.GetCurrentCodeSupplier(code.ToUpper().Trim());

            txtCode.Text = code + number;
        }
    }
}
