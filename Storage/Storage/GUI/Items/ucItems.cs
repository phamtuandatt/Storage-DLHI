using Storage.DAO;
using Storage.GUI.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI.Items
{
    public partial class ucItems : UserControl
    {
        public ucItems()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            grdItems.RowTemplate.Height = 150;
            grdItems.Columns[4].Width = 150;
            
            grdItems.DataSource = Item_DAO.GetItems();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add_Item add_Item = new Add_Item();
            add_Item.ShowDialog();
            LoadData();
        }

        private void grdItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdItems.Rows.Count <= 0) return;
            int rsl = grdItems.CurrentRow.Index;
            if (grdItems.Rows[rsl].Cells[1].Value.ToString() != null)
            {
                Edit_Item edit_Item = new Edit_Item(Guid.Parse(grdItems.Rows[rsl].Cells[1].Value.ToString()));
                edit_Item.ShowDialog();
                LoadData();
            }
            else
            {

            }
        }
    }
}
