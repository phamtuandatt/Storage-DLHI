
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
using Storage.GUI_Warehouse;
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
            //UpdateInventoryEachMonth();
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
            btnItems.BackColor = Color.WhiteSmoke;
            btnMPR.BackColor = Color.White;
            btnPO.BackColor = Color.White;
            btnImport.BackColor = Color.White;
            btnExport.BackColor = Color.White;
            btnWarehouse.BackColor = Color.White;

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
            btnItems.BackColor = Color.White;
            btnMPR.BackColor = Color.WhiteSmoke;
            btnPO.BackColor = Color.White;
            btnImport.BackColor = Color.White;
            btnExport.BackColor = Color.White;
            btnWarehouse.BackColor = Color.White;
            ucMPR ucMPR = new ucMPR();
            ucMPR.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucMPR);
            ucMPR.BringToFront();
        }

        private void btnPO_Click(object sender, EventArgs e)
        {
            btnItems.BackColor = Color.White;
            btnMPR.BackColor = Color.White;
            btnPO.BackColor = Color.WhiteSmoke;
            btnImport.BackColor = Color.White;
            btnExport.BackColor = Color.White;
            btnWarehouse.BackColor = Color.White;
            ucPO ucPO = new ucPO();
            ucPO.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucPO);
            ucPO.BringToFront();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            btnItems.BackColor = Color.White;
            btnMPR.BackColor = Color.White;
            btnPO.BackColor = Color.White;
            btnImport.BackColor = Color.WhiteSmoke;
            btnExport.BackColor = Color.White;
            btnWarehouse.BackColor = Color.White;
            ucImport ucImport = new ucImport();
            ucImport.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucImport);
            ucImport.BringToFront();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            btnItems.BackColor = Color.White;
            btnMPR.BackColor = Color.White;
            btnPO.BackColor = Color.White;
            btnImport.BackColor = Color.White;
            btnExport.BackColor = Color.WhiteSmoke;
            btnWarehouse.BackColor = Color.White;
            ucExport ucExport = new ucExport();
            ucExport.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucExport);
            ucExport.BringToFront();
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            btnItems.BackColor = Color.White;
            btnMPR.BackColor = Color.White;
            btnPO.BackColor = Color.White;
            btnImport.BackColor = Color.White;
            btnExport.BackColor = Color.White;
            btnWarehouse.BackColor = Color.WhiteSmoke;
            ucWarehouse ucWarehouse = new ucWarehouse();
            ucWarehouse.Dock = DockStyle.Fill;
            pnMain.Controls.Add(ucWarehouse);
            ucWarehouse.BringToFront();
        }
    }
}
