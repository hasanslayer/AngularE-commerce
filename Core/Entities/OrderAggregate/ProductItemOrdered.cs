using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public int ProductItemId { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public string ImgUrl { get; set; }

        public ProductItemOrdered(int productItemId, string productNameAr, string productNameEn, string imgUrl)
        {
            ProductItemId = productItemId;
            ProductNameAr = productNameAr;
            ProductNameEn = productNameEn;
            ImgUrl = imgUrl;
        }

        public ProductItemOrdered()
        {

        }
    }
}