﻿using ComponentFactory.Krypton.Ribbon;
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
    public partial class Add_SupplierType : KryptonForm
    {
        public Add_SupplierType()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSupplierType.Text))
            {
                KryptonMessageBox.Show("Please fill all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierType.Focus();
                return;
            }
            SupplierTypeDto supplierTypeDto = new SupplierTypeDto()
            {
                ID = Guid.NewGuid(),
                Name = txtSupplierType.Text,
            };

            if (await SupplierDAO.AddSupplierType(supplierTypeDto))
            {
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSupplierType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NOTALLOWED.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
