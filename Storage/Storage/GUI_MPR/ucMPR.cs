
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using Storage.DAO;
using Storage.GUI.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Storage.GUI_MPR
{
    public partial class ucMPR : UserControl
    {
        private DataTable data;
        private DataTable dataMPRExportDetail;

        private DataTable dataExportExcel_DB;
        private DataTable dataExportExcel;

        public ucMPR()
        {
            InitializeComponent();
            LoadData();
            dataExportExcel_DB = MPR_DAO.GetMPRExportExcel();
            dataExportExcel = new DataTable();
            dataExportExcel.Columns.Add("NO");
            dataExportExcel.Columns.Add("Code");
            dataExportExcel.Columns.Add("Name");
            dataExportExcel.Columns.Add("Unit");
            dataExportExcel.Columns.Add("Link");
            dataExportExcel.Columns.Add("Picture", typeof(byte[]));
            dataExportExcel.Columns.Add("Usage");
            dataExportExcel.Columns.Add("ExpectedDelivery");
            dataExportExcel.Columns.Add("Qty");
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
            grdMPRExport.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
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
            if (grdMPRExport.Rows.Count > 0)
            {
                grdMPRExport.Rows[0].Selected = true;
                DataView dv = dataMPRExportDetail.DefaultView;
                dv.RowFilter = $"MPR_EXPORT_ID = '{Guid.Parse(grdMPRExport.Rows[0].Cells[0].Value.ToString())}'";
                grdMPRExportDetail.DataSource = dv.ToTable();

                grdMPRExportDetail.RowTemplate.Height = 100;
            }
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

                DataView dtView = dataExportExcel_DB.DefaultView;
                dtView.RowFilter = $"MPR_EXPORT_ID = '{Guid.Parse(grdMPRExport.Rows[rsl].Cells[0].Value.ToString())}'";
                int i = 1;
                foreach (DataRow item in dtView.ToTable().Rows)
                {
                    DataRow row = dataExportExcel.NewRow();
                    row[0] = i;
                    row[1] = item.Field<string>("CODE");
                    row[2] = item.Field<string>("NAME");
                    row[3] = item.Field<string>("UNIT");
                    row[4] = item.Field<string>("PICTURE_LINK");
                    row[5] = item.Field<byte[]>("PICTURE");
                    row[6] = item.Field<string>("USAGE");
                    row[7] = "";
                    row[8] = item.Field<int>("QUANTITY");

                    i++;
                    dataExportExcel.Rows.Add(row);
                }
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets.Add("MPR");
                    sheet.Row(1).Height = 40;
                    sheet.Row(2).Height = 40;
                    sheet.Row(3).Height = 40;

                    sheet.Column(1).Width = 5;
                    sheet.Column(2).Width = 15;
                    sheet.Column(3).Width = 20;
                    sheet.Column(4).Width = 20;
                    sheet.Column(5).Width = 30;
                    sheet.Column(6).Width = 20;
                    sheet.Column(7).Width = 20;
                    sheet.Column(8).Width = 15;
                    sheet.Column(9).Width = 15;
                    sheet.Column(10).Width = 15;
                    sheet.Column(11).Width = 15;
                    sheet.Column(12).Width = 15;

                    var cellStyleRange = sheet.Cells["A1:L2"].Style;
                    cellStyleRange.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    cellStyleRange.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                    sheet.Cells["A1:B1"].Merge = true;
                    sheet.Cells["A1"].Value = $"MPR No\n(요구서 번호)";
                    sheet.Cells["A2:B2"].Merge = true;
                    sheet.Cells["A2"].Value = "Project Name\n (공사명)";
                    sheet.Cells["A3"].Value = "No";
                    sheet.Cells["B3"].Value = "Code";

                    sheet.Cells["C1"].Value = "MPR-PE-026";
                    sheet.Cells["C2"].Value = "N/A";
                    sheet.Cells["C3"].Value = "Name";

                    sheet.Cells["D1"].Value = "WO No. (공사번호)\nP.O DEL.(계약납기)";
                    sheet.Cells["D2"].Value = "";
                    sheet.Cells["D3"].Value = "Unit";

                    sheet.Cells["E1:G2"].Merge = true;
                    sheet.Cells["E1"].Value = "구매 요구서(MPR)  □ 견적 의뢰서(RFQ)";
                    sheet.Cells["E3"].Value = "Link";

                    sheet.Cells["F3"].Value = "Picture";
                    sheet.Cells["G3"].Value = "Usage";

                    sheet.Cells["H1"].Value = "Rev.\n(개정)";
                    sheet.Cells["H2"].Value = "0";
                    sheet.Cells["H3"].Value = "EXPECT RECEIVE DATE";

                    sheet.Cells["I1"].Value = "Date\n(일자)";
                    sheet.Cells["I2"].Value = "" + DateTime.Now.ToString("dd-MM-yyyy");
                    sheet.Cells["I3"].Value = "Q'ty / Sh't";

                    sheet.Cells["J1"].Value = "Prepared\n(작성)";
                    sheet.Cells["J2"].Value = "" + DateTime.Now.ToString("dd-MM-yyyy");
                    sheet.Cells["J3"].Value = "Weight(kg)";

                    sheet.Cells["K1"].Value = "Reviewed\n(검토)";
                    sheet.Cells["K2"].Value = "Good";

                    sheet.Cells["L1"].Value = "Approve\n(승인)";
                    sheet.Cells["L2"].Value = "Good";

                    int rowIndex = 3;
                    foreach (DataRow item in dataExportExcel.Rows)
                    {
                        FileInfo imageFile = new FileInfo(item[4].ToString());
                        Image image = Image.FromFile(item[4].ToString());

                        // Add the image to the worksheet
                        ExcelPicture picture = sheet.Drawings.AddPicture($"PictureName {rowIndex}", imageFile, new ExcelHyperLink(item[4].ToString()));
                        picture.SetSize(130, 70);
                        picture.SetPosition(rowIndex, 0, 5, 0);
                        rowIndex++;
                        item[5] = new byte[0];
                    }

                    // Get the style object for the range of cells
                    var addressTable = $"A1:L{dataExportExcel.Rows.Count + 3}";
                    var rangeStyle = sheet.Cells[addressTable].Style;
                    var loop = dataExportExcel.Rows.Count + 4;
                    for (int i = 4; i < loop; i++)
                    {
                        sheet.Row(i).Height = 70;
                    }

                    // Center the content horizontally and vertically for the range
                    rangeStyle.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    rangeStyle.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                    // Set border for the range
                    rangeStyle.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rangeStyle.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rangeStyle.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rangeStyle.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    sheet.Cells["A4"].LoadFromDataTable(dataExportExcel, false);
                    package.SaveAs(new FileInfo(path));
                }
            }
        }
    }
}
