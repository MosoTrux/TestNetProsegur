using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestNetProsegur.Application.Dtos
{
    public class ServiceResponseDto<TData> where TData : class
    {
        public ServiceResponseDto()
        {
            ValidationMessages = new List<string>();
        }

        public ICollection<string> ValidationMessages { get; set; }
        public TData Data { get; set; }
        public bool IsSuccess { get; set; }
    }
}
