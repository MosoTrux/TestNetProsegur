using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNetProsegur.Application.Interfaces
{
    public interface IStartupInitializer
    {
        Task Initialize();
    }
}
