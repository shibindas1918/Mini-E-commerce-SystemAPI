namespace Mini_E_commerce_SystemAPI.Models
{
    public class ShoppingCartItem
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

}
