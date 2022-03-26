using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }

        public ProductItemOrdered(int productItemId, string productName, string imgUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            ImgUrl = imgUrl;
        }

        public ProductItemOrdered()
        {

        }
    }
}