namespace LuftbornTask.src.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Link to ASP.NET Identity user
        public string? UserId { get; set; }
    }

}
