using Core.Entities;

namespace API.Core.Entities
{
    public class Product : BaseEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }

        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}