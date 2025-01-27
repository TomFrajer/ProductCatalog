using ProductCatalog.Domain.Entities;
using System.ComponentModel.DataAnnotations; // Required for validation attributes

namespace ProductCatalog.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Url(ErrorMessage = "ImgUri must be a valid URL.")]
        [MaxLength(500, ErrorMessage = "ImgUri cannot exceed 500 characters.")]
        public string? ImgUri { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public int Price { get; set; }

        [MaxLength(10000, ErrorMessage = "Description cannot exceed 10000 characters.")]
        public string? Description { get; set; } = string.Empty;

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
