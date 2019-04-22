using Collector.Models;
using Collector.Models.Documents;
using Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collector.Repositories
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<TelemetryContainer> TelemetryRepository { get; }
        IRepositoryBase<CollectorUser> UserRepository { get; }
        IRepositoryBase<WebSession> WebSessionRepository { get; }


    }
}
