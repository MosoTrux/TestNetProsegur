using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNetProsegur.Application.Dtos
{
    public class GetInvoiceDto
    {
        public long OrderId { get; set; }
        public string Customer { get; set; }
        public string Employeed { get; set; }
        public string State { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public List<ItemInvoiceDto> InvoiceItems { get; set; }
    }

    public class ItemInvoiceDto
    {
        public string NameMenuItem { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotalItem { get; set; }
    }
}
