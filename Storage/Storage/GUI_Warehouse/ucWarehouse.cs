using Storage.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI_Warehouse
{
    public partial class ucWarehouse : UserControl
    {
        public DataTable dtInventories;
        public ucWarehouse()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dtInventories = Warehouse_DAO.GetInventories(txtDate.Value.Month , txtDate.Value.Year);
            grdInventories.DataSource = dtInventories;
        }

        private void grdInventories_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void grdInventories_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                if (grdInventories.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdInventories.Rows[e.RowIndex].Cells[5].Value = 0;
                }

                e.Handled = true;
            }

            if (e.ColumnIndex == 6 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                if (grdInventories.Rows[e.RowIndex].Cells[6].Value.ToString().Length <= 0)
                {
                    grdInventories.Rows[e.RowIndex].Cells[6].Value = 0;
                }

                e.Handled = true;
            }

            if (e.ColumnIndex == 7 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                if (grdInventories.Rows[e.RowIndex].Cells[7].Value.ToString().Length <= 0)
                {
                    grdInventories.Rows[e.RowIndex].Cells[7].Value = 0;
                }

                e.Handled = true;
            }

            if (e.ColumnIndex == 8 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                if (grdInventories.Rows[e.RowIndex].Cells[8].Value.ToString().Length <= 0)
                {
                    grdInventories.Rows[e.RowIndex].Cells[8].Value = 0;
                }

                e.Handled = true;
            }

            if (e.ColumnIndex == 9 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                if (grdInventories.Rows[e.RowIndex].Cells[9].Value.ToString().Length <= 0)
                {
                    grdInventories.Rows[e.RowIndex].Cells[9].Value = 0;
                }

                e.Handled = true;
            }
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            dtInventories = Warehouse_DAO.GetInventories(txtDate.Value.Month, txtDate.Value.Year);
            grdInventories.DataSource = dtInventories;
        }
    }
}
