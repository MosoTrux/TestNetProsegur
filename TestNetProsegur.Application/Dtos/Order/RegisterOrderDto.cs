using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Application.Dtos.Order
{
    public class RegisterOrderDto
    {
        public long CustomerId { get; set; }
        public string ProvinceCode { get; set; }
        public long CreatedBy { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public long IdMenuItem { get; set; }
        public int Quantity { get; set; }
    }

}
