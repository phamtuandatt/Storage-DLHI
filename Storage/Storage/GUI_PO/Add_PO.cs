﻿using ComponentFactory.Krypton.Toolkit;
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

namespace Storage.GUI_PO
{
    public partial class Add_PO : KryptonForm
    {
        public Add_PO()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void txtMPR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Validation.NO.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
