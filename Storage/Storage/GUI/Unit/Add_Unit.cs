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

namespace Storage.GUI.Unit
{
    public partial class Add_Unit : Form
    {
        public Add_Unit()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UnitDto unit = new UnitDto()
            {
                Id = Guid.NewGuid(),
                Name = txtName.Text,
            };

            if (Unit_DAO.Add(unit))
            {
                MessageBox.Show("Done", "Notification");
            }
        }
    }
}
