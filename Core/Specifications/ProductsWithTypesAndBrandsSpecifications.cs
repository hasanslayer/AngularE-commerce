using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpesification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string sort, string lang)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            if (lang == "ar")
            {
                AddOrderBy(x => x.NameAr);
            }
            else
            {
                AddOrderBy(x => x.NameEn);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.NameEn);
                        break;
                }
            }

        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}