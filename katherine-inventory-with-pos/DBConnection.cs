using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace katherine_inventory_with_pos.DataAccess
{
    /// <summary>
    /// Central place for talking to inventorydb (MySQL).
    /// Connection string is read from App.config (connectionStrings section).
    /// </summary>
    public static class DBConnection
    {
        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["katherineinventorydb"].ConnectionString; }
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        /// <summary>Quick check used on startup / login screen to confirm the DB is reachable.</summary>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            return table;
        }

        public static int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Runs an INSERT and returns the new AUTO_INCREMENT id (tbl_stock, tbl_stock_log, tbl_sale_item, etc.).</summary>
        public static long ExecuteInsertReturnId(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;
            }
        }

        public static object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
