using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Dto
{
    public class MessageEnvelope
    {
        public MessageEnvelope(string message)
        {
            this.Message = message;
            this.MessageId = Guid.NewGuid();
            this.DateTimeUtc = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }
        public Guid MessageId { get; set; }
        public string DateTimeUtc { get; set; }
        public string Message { get; set; }
    }
}
