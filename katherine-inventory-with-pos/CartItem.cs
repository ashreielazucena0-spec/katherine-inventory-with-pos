namespace katherine_inventory_with_pos.Forms
{
    /// <summary>One line of the shopping cart in the Point of Sale screen.</summary>
    public class CartItem
    {
        public string ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int AvailableStock { get; set; }

        public decimal Subtotal
        {
            get { return UnitPrice * Quantity; }
        }
    }
}
