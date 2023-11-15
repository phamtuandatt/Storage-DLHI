
using ComponentFactory.Krypton.Toolkit;
using Storage.DAO;
using Storage.DTOs;
using Storage.GUI.Groups;
using Storage.GUI.Items;
using Storage.GUI.Suppliers;
using Storage.GUI.Types;
using Storage.GUI.Units;
using Storage.GUI.UserControll;
using Storage.GUI_Export;
using Storage.GUI_Import;
using Storage.GUI_MPR;
using Storage.GUI_PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.GUI
{
    public partial class Main : KryptonForm
    {
        public Main()
        {
            InitializeComponent();
            UpdateInventoryEachMonth();
        }

        private void UpdateInventoryEachMonth()
        {
            DateTime date = DateTime.Today;
            if (date.Day == 1)
            {
                DataTable dtInvetoriesImportExport = InventoryDAO.GetImportExportByLastMonth(date.Month - 1);
                if (dtInvetoriesImportExport != null)
                {
                    List<InventoryDto> inventoryDtos = new List<InventoryDto>();
                    foreach (DataRow item in dtInvetoriesImportExport.Rows)
                    {
                        InventoryDto dto = new InventoryDto()
                        {
                            Id = Guid.NewGuid(),
                            Item_Id = Guid.Parse(item["ITEM_ID"].ToString()),
                            Month = date.Month - 1,
                            Amount = int.Parse(item["INVENTORY"].ToString())
                        };
                        inventoryDtos.Add(dto);
                    }
                    if (InventoryDAO.AddRange(inventoryDtos))
                    {

                    }
                }
            }
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            ucItems ucItems = new ucItems();
            ucItems.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucItems);
            ucItems.BringToFront();
        }

        private void mnuCommon_Click(object sender, EventArgs e)
        {
            ucCommon ucCommon = new ucCommon();
            ucCommon.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucCommon);
            ucCommon.BringToFront();
        }

        private void mnuSupplier_Click(object sender, EventArgs e)
        {
            ucSupplier ucSupplier = new ucSupplier();
            ucSupplier.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucSupplier);
            ucSupplier.BringToFront();
        }

        private void btnMPR_Click(object sender, EventArgs e)
        {
            ucMPR ucMPR = new ucMPR();
            ucMPR.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucMPR);
            ucMPR.BringToFront();
        }

        private void btnPO_Click(object sender, EventArgs e)
        {
            ucPO ucPO = new ucPO();
            ucPO.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucPO);
            ucPO.BringToFront();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ucImport ucImport = new ucImport();
            ucImport.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucImport);
            ucImport.BringToFront();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ucExport ucExport = new ucExport();
            ucExport.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucExport);
            ucExport.BringToFront();
        }
    }
}
