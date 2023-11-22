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
        public DataTable data = null; 
        public ucItems()
        {
            InitializeComponent();
            LoadData();
        }

        public async void LoadData()
        {
            grdItems.RowTemplate.Height = 120;
            grdItems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            data = await Item_DAO.GetItemsAsync();
            grdItems.DataSource = data;
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

        private void grdItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdItems.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdItems.Rows[e.RowIndex].Cells[5].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                grdItems.DataSource = data;
            }
            DataView dv = data.DefaultView;
            dv.RowFilter = $"NAME LIKE '%{txtSearch.Text}%' " +
                        $"OR CODE LIKE '%{txtSearch.Text}%' " +
                        $"OR NOTE LIKE '%{txtSearch.Text}%' " +
                        $"OR UNIT LIKE '%{txtSearch.Text}%' " +
                        $"OR GROUPS LIKE '%{txtSearch.Text}%' " +
                        $"OR SUPPLIER LIKE '%{txtSearch.Text}%' " +
                        $"OR ENG_NAME LIKE '%{txtSearch.Text}%'";
            grdItems.DataSource = dv.ToTable();
        }

        private void grdItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
