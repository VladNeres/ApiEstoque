using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Interfaces
{
    public interface IConsumer
    {
        Task ProcessMessageAsync(string message);
    }
}
