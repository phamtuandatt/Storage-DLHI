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

namespace Storage.GUI.TypeConsumable
{
    public partial class Add_TypeConsumable : Form
    {
        public Add_TypeConsumable()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TypeConsumableDto typeConsumableDto = new TypeConsumableDto()
            {
                Id = Guid.NewGuid(),
                Name = txtName.Text
            };

            if (TypeConsumable_DAO.Add(typeConsumableDto))
            {
                MessageBox.Show("Done", "Notification");
            }
        }
    }
}
