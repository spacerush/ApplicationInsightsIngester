using Collector.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Hubs
{
    public interface IJavascriptClient
    {
        Task ReceiveMessage(MessageEnvelope obj);
    }
}
