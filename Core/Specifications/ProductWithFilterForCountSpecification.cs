using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpesification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
        : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.NameAr.ToLower().Contains(productParams.Search) || x.NameEn.ToLower().Contains(productParams.Search)) &&
                 (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                 (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
        }
    }
}