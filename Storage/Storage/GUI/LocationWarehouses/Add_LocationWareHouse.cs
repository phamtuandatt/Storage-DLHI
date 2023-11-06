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

namespace Storage.GUI.LocationWarehouses
{
    public partial class Add_LocationWareHouse : KryptonForm
    {
        public Add_LocationWareHouse()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LocationWarehousseDto locationWarehousseDto = new LocationWarehousseDto()
            {
                Id = Guid.NewGuid(),
                Name = txtLocation.Text
            };

            if (LocationWareHouse_DAO.Add(locationWarehousseDto))
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
