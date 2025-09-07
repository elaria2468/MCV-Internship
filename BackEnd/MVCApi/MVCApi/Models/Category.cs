namespace MVCApi.Models
{
  
    
        public class Category
        {
            public int Id { get; set; }   // Primary Key
            public string Name { get; set; }

            // Navigation property: One Category has many Items
            public ICollection<Item> Items { get; set; }
        }
    

}
