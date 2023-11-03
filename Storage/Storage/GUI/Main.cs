
using Storage.GUI.Groups;
using Storage.GUI.Types;
using Storage.GUI.Units;
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
    }
}
