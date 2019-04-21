using Collector.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Repositories
{
    public interface IRepoWrapper
    {
        IRepositoryBase<TelemetryLog> TelemetryLogRepository { get; }

        /// <summary>
        /// Save all changes to underlying data store.
        /// </summary>
        int Save();
    }
}
