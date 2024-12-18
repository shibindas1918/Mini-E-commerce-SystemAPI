namespace Mini_E_commerce_SystemAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

}
