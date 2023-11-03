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

namespace Storage.GUI.Types
{
    public partial class Add_Type : Form
    {
        public Add_Type()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TypeDto type = new TypeDto()
            {
                Id = Guid.NewGuid(),
                Name = txtType.Text,
            };

            if (Type_DAO.Add(type))
            {

            }
        }
    }
}
