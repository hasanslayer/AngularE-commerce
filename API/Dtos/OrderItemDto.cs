using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderItemDto
    {
        public int ProductItemId { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public string ImgUrl { get; set; } 
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}