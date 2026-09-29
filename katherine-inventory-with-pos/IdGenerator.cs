using System;
using MySql.Data.MySqlClient;

namespace katherine_inventory_with_pos.DataAccess
{
    /// <summary>
    /// Generates the next sequential prefixed ID for a table (e.g. "ACC-001", "ACC-002" ...)
    /// by looking at the highest existing ID with that prefix and incrementing it.
    /// </summary>
    public static class IdGenerator
    {
        /// <param name="tableName">Table to check, e.g. "tbl_account".</param>
        /// <param name="idColumn">Primary key / ID column, e.g. "acc_id".</param>
        /// <param name="prefix">ID prefix without the dash, e.g. "ACC" for "ACC-001".</param>
        /// <param name="digits">How many digits to zero-pad the number to (3 => 001).</param>
        public static string Next(string tableName, string idColumn, string prefix, int digits)
        {
            // tableName/idColumn are always passed as fixed literals from our own code
            // (never user input), so building the identifiers into the SQL text here is safe.
            string query =
                $"SELECT `{idColumn}` FROM `{tableName}` " +
                $"WHERE `{idColumn}` LIKE @Prefix " +
                $"ORDER BY `{idColumn}` DESC LIMIT 1;";

            object result = DBConnection.ExecuteScalar(query, new MySqlParameter("@Prefix", prefix + "-%"));

            int nextNumber = 1;
            if (result != null && result != DBNull.Value)
            {
                string lastId = result.ToString();
                string numberPart = lastId.Length > prefix.Length + 1
                    ? lastId.Substring(prefix.Length + 1)
                    : string.Empty;

                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}-{nextNumber.ToString().PadLeft(digits, '0')}";
        }
    }
}
