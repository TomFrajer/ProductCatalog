using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.DTOs
{
    namespace ProductApi.Application.DTOs
    {
        public class ProductDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImgUri { get; set; }
            public int Price { get; set; }
            public string? Description { get; set; }

            public ProductDto() { }

            public ProductDto(Product product)
            {
                Id = product.Id;
                Name = product.Name;
                ImgUri = product.ImgUri;
                Price = product.Price;
                Description = product.Description;
            }

            public Product ToEntity()
            {
                return new Product
                {
                    Id = this.Id,
                    Name = this.Name,
                    ImgUri = this.ImgUri,
                    Price = this.Price,
                    Description = this.Description
                };
            }
        }
    }

}
