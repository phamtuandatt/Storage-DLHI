using ComponentFactory.Krypton.Toolkit;
using Storage.DataProvider;
using Storage.Properties;
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
    public partial class ConnectSystem : KryptonForm
    {
        private AppSetting _setting = new AppSetting();
        private SQLServerProvider _serverProvider = new SQLServerProvider();
        public string ConnectionString { get; set; }
        //string connectionString = @"server=PC-DATPHAM\MSSQLSERVER01;database=STORAGE_DLHI;Integrated Security = true;uid=sa;pwd=Aa123456@";

        public ConnectSystem()
        {
            InitializeComponent();
            //ConnectionString = _setting.GetConnectionString("STORAGE_DLHI");
            //if (!string.IsNullOrEmpty(ConnectionString))
            //{
            //    _serverProvider.TestConnection(ConnectionString);
            //    Main main = new Main();
            //    this.Hide();
            //    main.ShowDialog();
            //    this.Close();
            //}
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                _setting.SetConnectionString("StorageDLHI", ConnectionString);
                Main main = new Main();
                this.Hide();
                main.ShowDialog();
                this.Close();
            }

        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServerName.Text) || string.IsNullOrEmpty(txtDatabase.Text))
            {
                KryptonMessageBox.Show("Please fill in all information !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtUser.Text))
            {
                ConnectionString = string.Format("server={0};database={1};Integrated Security = true;", txtServerName.Text, txtDatabase.Text);
            }
            else
            {
                ConnectionString = $"server={txtServerName.Text};database={txtDatabase.Text};Integrated Security = true;uid={txtUser.Text};pwd={txtPwd.Text}";
            }


            if (_serverProvider.TestConnection(ConnectionString))
            {
                KryptonMessageBox.Show("Connected successfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                KryptonMessageBox.Show("Connected unsuccessfully !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
