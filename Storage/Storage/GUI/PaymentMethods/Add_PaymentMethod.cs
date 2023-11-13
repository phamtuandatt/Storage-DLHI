using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI.PaymentMethods
{
    public partial class Add_PaymentMethod : KryptonForm
    {
        public Add_PaymentMethod()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PaymentMethodDto paymentMethod = new PaymentMethodDto()
            {
                Id = Guid.NewGuid(),
                Name = txtPaymentMethod.Text,
            };

            if (PaymentMethod_DAO.Add(paymentMethod))
            {
                this.Close();
            }
            else
            {
                KryptonMessageBox.Show("Failure !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
