using Collector.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Repositories
{
    public class RepoWrapper : IRepoWrapper
    {

        private readonly ApptelemetryContext context;

        public RepoWrapper(ApptelemetryContext appTelemetryContext)
        {
            this.context = appTelemetryContext;
        }

        private IRepositoryBase<TelemetryLog> telemetryLogRepository;
        public IRepositoryBase<TelemetryLog> TelemetryLogRepository
        {
            get
            {
                if (this.telemetryLogRepository == null)
                {
                    this.telemetryLogRepository = new RepositoryBase<TelemetryLog>(this.context);
                }
                return this.telemetryLogRepository;
            }
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }

    }
}
