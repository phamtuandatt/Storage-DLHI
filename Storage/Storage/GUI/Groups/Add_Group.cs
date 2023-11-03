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

namespace Storage.GUI.Groups
{
    public partial class Add_Group : Form
    {
        public Add_Group()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GroupDto dto = new GroupDto()
            {
                Id = Guid.NewGuid(),
                Name = txtGroup.Text,
            };

            if (Group_DAO.Add(dto))
            {

            }

        }
    }
}
