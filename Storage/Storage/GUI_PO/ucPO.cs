using Storage.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI_PO
{
    public partial class ucPO : UserControl
    {
        private DataTable dataItemAddPO;
        private DataTable dtItemPO;

        public ucPO()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dataItemAddPO = Item_DAO.GetItems();
            grdItemAddPO.DataSource = dataItemAddPO;
            grdItemAddPO.RowTemplate.Height = 100;

            grdItemPODetail.RowTemplate.Height = 100;

            dtItemPO = new DataTable();
            dtItemPO.Columns.Add("ID");
            dtItemPO.Columns.Add("CODE");
            dtItemPO.Columns.Add("NAME");
            dtItemPO.Columns.Add("IMAGE", typeof(byte[]));
            dtItemPO.Columns.Add("QUANTITY");
            dtItemPO.Columns.Add("PRICE");
            dtItemPO.Columns.Add("MPRNO");
            dtItemPO.Columns.Add("PONO");
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

        private void grdItemAddPO_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdItemAddPO.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdItemAddPO.Rows[e.RowIndex].Cells[5].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                grdItemAddPO.DataSource = dataItemAddPO;
            }
            DataView dv = dataItemAddPO.DefaultView;
            dv.RowFilter = $"NAME LIKE '%{txtSearch.Text}%' " +
                        $"OR CODE LIKE '%{txtSearch.Text}%' " +
                        $"OR NOTE LIKE '%{txtSearch.Text}%' " +
                        $"OR UNIT LIKE '%{txtSearch.Text}%' " +
                        $"OR GROUPS LIKE '%{txtSearch.Text}%' " +
                        $"OR SUPPLIER LIKE '%{txtSearch.Text}%' " +
                        $"OR ENG_NAME LIKE '%{txtSearch.Text}%'";
            grdItemAddPO.DataSource = dv.ToTable();
        }

        private void btnAddSingle_Click(object sender, EventArgs e)
        {
            if (grdItemAddPO.Rows.Count <= 0) return;
            int rsl = grdItemAddPO.CurrentRow.Index;
            if (grdItemAddPO.Rows[rsl].Cells[1].Value.ToString() != null)
            {
                if (dtItemPO.AsEnumerable().Any(row => (grdItemAddPO.Rows[rsl].Cells[1].Value.ToString()) == row.Field<string>("ID")))
                {

                }
                else
                {
                    DataRow r = dtItemPO.NewRow();
                    r["ID"] = grdItemAddPO.Rows[rsl].Cells[1].Value.ToString();
                    r["CODE"] = grdItemAddPO.Rows[rsl].Cells[2].Value.ToString();
                    r["NAME"] = grdItemAddPO.Rows[rsl].Cells[3].Value.ToString();
                    r["IMAGE"] = (byte[])grdItemAddPO.Rows[rsl].Cells[5].Value;
                    r["QUANTITY"] = txtQuantity.Text;
                    r["PRICE"] = txtPrice.Text;
                    r["MPRNO"] = txtMPR.Text;
                    r["PONO"] = txtPONo.Text;

                    dtItemPO.Rows.Add(r);
                    grdItemPODetail.DataSource = dtItemPO;
                }
            }
            else
            {

            }
        }
    }
}
