using Storage.DAO;
using Storage.GUI.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI_MPR
{
    public partial class ucMPR : UserControl
    {
        private DataTable data;
        public ucMPR()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            data = Item_DAO.GetItems();

            grdItems.DataSource = data;
            grdItems.RowTemplate.Height = 100;
            grdItems.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Page add MPR
            grdAddMPR.DataSource = data;
            grdAddMPR.RowTemplate.Height = 100;
            grdAddMPR.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Page MPRs
            grdMRPs.DataSource = MPR_DAO.GetMPRs();
            grdMRPs.RowTemplate.Height = 100;
            grdMRPs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            cboSupplier.DataSource = SupplierDAO.GetSuppiers();
            cboSupplier.DisplayMember = "NAME_SUPPIER";
            cboSupplier.ValueMember = "ID";
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

        private void grdAddMPR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdAddMPR.Rows.Count <= 0) return;
            int rsl = grdAddMPR.CurrentRow.Index;
            if (grdAddMPR.Rows[rsl].Cells[1].Value.ToString() != null)
            {
                Add_MPR add_MPR = new Add_MPR(Guid.Parse(grdAddMPR.Rows[rsl].Cells[1].Value.ToString()));
                add_MPR.ShowDialog();
            }
            else
            {

            }
        }
    }
}
