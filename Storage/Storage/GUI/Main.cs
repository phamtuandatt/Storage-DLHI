
using ComponentFactory.Krypton.Toolkit;
using Storage.GUI.Groups;
using Storage.GUI.Items;
using Storage.GUI.Suppliers;
using Storage.GUI.Types;
using Storage.GUI.Units;
using Storage.GUI.UserControll;
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
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            Add_Supplier add_Supplier = new Add_Supplier();
            add_Supplier.ShowDialog();
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            Add_Unit add_Unit = new Add_Unit();
            add_Unit.ShowDialog();
        }

        private void btnAddGroupConsumable_Click(object sender, EventArgs e)
        {
            Add_Group add_Group = new Add_Group();
            add_Group.ShowDialog();
        }

        private void btnAddTypeConsumable_Click(object sender, EventArgs e)
        {
            Add_Type type = new Add_Type();
            type.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Item add_Item = new Add_Item();
            add_Item.ShowDialog();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            ucItems ucItems = new ucItems();
            ucItems.Dock= DockStyle.Fill; 
            pnMain.Controls.Add(ucItems);
            ucItems.BringToFront();
        }

        private void mnuCommon_Click(object sender, EventArgs e)
        {
            ucCommon ucCommon = new ucCommon();
            ucCommon.Dock= DockStyle.Fill;
            pnMain.Controls.Add(ucCommon);
            ucCommon.BringToFront();
        }

        private void mnuSupplier_Click(object sender, EventArgs e)
        {
            ucSupplier ucSupplier = new ucSupplier();
            ucSupplier.Dock= DockStyle.Fill;    
            pnMain.Controls.Add(ucSupplier); 
            ucSupplier.BringToFront();
        }
    }
}
