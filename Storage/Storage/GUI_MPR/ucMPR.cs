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
        private DataTable dataMPRExportDetail;
        public ucMPR()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            data = Item_DAO.GetItems();
            dataMPRExportDetail = MPR_DAO.GetMPRExportDetails();

            // Page add MPR
            grdAddMPR.DataSource = data;
            grdAddMPR.RowTemplate.Height = 100;
            grdAddMPR.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Page MPRs
            grdMRPs.DataSource = MPR_DAO.GetMPRs();
            grdMRPs.RowTemplate.Height = 100;
            grdMRPs.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Page MPRExport
            grdMPRExport.DefaultCellStyle.WrapMode= DataGridViewTriState.True;
            DataTable dtMPRExport = new DataTable();
            dtMPRExport.Columns.Add("ID");
            dtMPRExport.Columns.Add("CREATED");
            dtMPRExport.Columns.Add("ITEM_COUNT");
            dtMPRExport.Columns.Add("STATUS");

            foreach (DataRow item in MPR_DAO.GetMPRExports().Rows)
            {
                DataRow r = dtMPRExport.NewRow();
                r["ID"] = item["ID"];
                r["CREATED"] = item["CREATED"];
                r["ITEM_COUNT"] = item["ITEM_COUNT"];
                r["STATUS"] = int.Parse(item["STATUS"].ToString()) == 0 ? "Exported" : "Not exported";
                dtMPRExport.Rows.Add(r);
            }
            grdMPRExport.DataSource = dtMPRExport;

            grdMPRExportDetail.RowTemplate.Height = 100;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Length == 0)
            {
                grdAddMPR.DataSource = data;
            }
            DataView dv = data.DefaultView;
            dv.RowFilter = $"NAME LIKE '%{txtSearch.Text}%' " +
                        $"OR CODE LIKE '%{txtSearch.Text}%' " +
                        $"OR NOTE LIKE '%{txtSearch.Text}%' " +
                        $"OR UNIT LIKE '%{txtSearch.Text}%' " +
                        $"OR GROUPS LIKE '%{txtSearch.Text}%' " +
                        $"OR SUPPLIER LIKE '%{txtSearch.Text}%' " +
                        $"OR ENG_NAME LIKE '%{txtSearch.Text}%'";
            grdAddMPR.DataSource = dv.ToTable();
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

        private void grdMRPs_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdMRPs.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdMRPs.Rows[e.RowIndex].Cells[5].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void grdAddMPR_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdAddMPR.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdAddMPR.Rows[e.RowIndex].Cells[5].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void grdMPRExport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdMPRExport.Rows.Count <= 0) return;
            int rsl = grdMPRExport.CurrentRow.Index;
            if (grdMPRExport.Rows[rsl].Cells[0].Value.ToString() != null)
            {
                DataView dv = dataMPRExportDetail.DefaultView;
                dv.RowFilter = $"MPR_EXPORT_ID = '{Guid.Parse(grdMPRExport.Rows[rsl].Cells[0].Value.ToString())}'";
                grdMPRExportDetail.DataSource = dv.ToTable();
                
                //grdMPRExportDetail.DataSource = MPR_DAO.GetMPRExportDetail(Guid.Parse(grdMPRExport.Rows[rsl].Cells[0].Value.ToString()));
            }
            else
            {

            }
        }

        private void grdMPRExportDetail_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) //change 3 with your collumn index
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                if (grdMPRExportDetail.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                {
                    grdMPRExportDetail.Rows[e.RowIndex].Cells[5].Value = Properties.Resources.picture_bg;
                }

                e.Handled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRefeshMPR_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
