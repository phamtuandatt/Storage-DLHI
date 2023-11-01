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

namespace Storage.GUI.GroupConsumable
{
    public partial class Add_Group_Consumable : Form
    {
        public Add_Group_Consumable()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GroupConsumableDto group = new GroupConsumableDto()
            {
                Id = Guid.NewGuid(),
                Name = txtName.Text,
            };

            if (GroupConsumable_DAO.Add(group))
            {
                MessageBox.Show("Done", "Notification");
            }
        }
    }
}
