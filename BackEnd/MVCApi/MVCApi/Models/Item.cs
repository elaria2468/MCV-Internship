namespace MVCApi.Models
{
    public class Item
    {
        public int Id { get; set; }   // Primary Key
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation property
        public Category Category { get; set; }

    }
}
