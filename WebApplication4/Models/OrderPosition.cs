namespace WebApplication4.Models
{
    public class OrderPosition
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }

        public bool IsCanAddCount()
        {
            return Product.Count >= Count + 1;
        }
    }
}