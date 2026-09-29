namespace katherine_inventory_with_pos.DataAccess
{
    /// <summary>Holds who is currently logged in for the lifetime of the app.</summary>
    public static class SessionManager
    {
        public static string UserID { get; set; }
        public static string Username { get; set; }
        public static string FullName { get; set; }

        public static string AccType { get; set; }

        public static void Clear()
        {
            UserID = null;
            Username = null;
            FullName = null;
        }
    }
}
