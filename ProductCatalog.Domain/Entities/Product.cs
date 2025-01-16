namespace ProductCatalog.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUri { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
    }
}
