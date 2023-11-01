using Storage.GUI.GroupConsumable;
using Storage.GUI.Supplier;
using Storage.GUI.TypeConsumable;
using Storage.GUI.Unit;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            Add_Supplier supplier = new Add_Supplier();
            supplier.ShowDialog();
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            Add_Unit unit = new Add_Unit();
            unit.ShowDialog();
        }

        private void btnAddGroupConsumable_Click(object sender, EventArgs e)
        {
            Add_Group_Consumable add_Group_Consumable = new Add_Group_Consumable();
            add_Group_Consumable.ShowDialog();
        }

        private void btnAddTypeConsumable_Click(object sender, EventArgs e)
        {
            Add_TypeConsumable add_TypeConsumable = new Add_TypeConsumable();
            add_TypeConsumable.ShowDialog();
        }
    }
}
