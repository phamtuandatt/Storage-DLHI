using Storage.DAO;
using Storage.GUI.Groups;
using Storage.GUI.LocationWarehouses;
using Storage.GUI.Types;
using Storage.GUI.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI.UserControll
{
    public partial class ucCommon : UserControl
    {
        public ucCommon()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            Add_Unit add_Unit = new Add_Unit();
            add_Unit.ShowDialog();
            LoadData();
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            Add_Group add_Group = new Add_Group();
            add_Group.ShowDialog();
            LoadData();
        }

        private void btnType_Click(object sender, EventArgs e)
        {
            Add_Type add_Type = new Add_Type();
            add_Type.ShowDialog();
            LoadData();
        }

        private void btnAddLocationWarehouse_Click(object sender, EventArgs e)
        {
            Add_LocationWareHouse add_LocationWareHouse = new Add_LocationWareHouse();
            add_LocationWareHouse.ShowDialog();
            LoadData();
        }

        private async void LoadData()
        {
            grdUnit.DataSource = await Unit_DAO.GetUnits();

            grdGroup.DataSource = await Group_DAO.GetGroups();

            grdType.DataSource = Type_DAO.GetTypes();

            grdLocationWarehouse.DataSource = Warehouse_DAO.GetLocationWareHouses();
        }

        private void grdUnit_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Add_Serial_Number(sender, e);
        }

        private void grdGroup_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Add_Serial_Number(sender, e);
        }

        private void grdLocationWarehouse_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Add_Serial_Number(sender, e);
        }

        private void grdType_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Add_Serial_Number(sender, e);
        }

        private void Add_Serial_Number(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}
