using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;

namespace Storage.DataProvider
{
    internal class SQLServerProvider
    {
        SqlConnection connection = null;
        AppSetting appSetting = new AppSetting();

        internal SQLServerProvider()
        {
            connection = new SqlConnection(appSetting.GetConnectionString("StorageDLHI"));
        }

        internal bool TestConnection(string connectionString)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message + "", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        //internal bool TestConnection(string conn)
        //{
        //    try
        //    {
        //        connection = new SqlConnection(conn);
        //        connection.Open();
        //        connection.Close();

        //        return true;
        //    }
        //    catch (SqlException ex)
        //    {
        //        return false;
        //        throw new Exception(ex.Message);
        //    }
        //}

        internal DataTable GetData(string sql, string table_name)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sql, connection);
                da.Fill(ds, table_name);
                return ds.Tables[table_name];
            }
            catch (SqlException ex)
            {
                return null;
                throw new Exception(ex.Message);
            }
        }

        internal bool UpdateDatabase(string sql, DataTable table_update)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, connection);
                SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                da.Update(table_update);
                return true;
            }
            catch (SqlException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        internal int Insert(string sql)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                int rs = cmd.ExecuteNonQuery();
                connection.Close();

                return rs;
            }
            catch (SqlException ex)
            {
                return -1;
                throw new Exception(ex.Message);
            }
        }

        internal int Update(string sql)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                int rs = cmd.ExecuteNonQuery();
                connection.Close();

                return rs;
            }
            catch (SqlException ex)
            {
                return -1;
                throw new Exception(ex.Message);
            }
        }

        internal int Delete(string sql)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                int rs = cmd.ExecuteNonQuery();
                connection.Close();

                return rs;
            }
            catch (SqlException ex)
            {
                return -1;
                throw new Exception(ex.Message);
            }
        }
    }
}
