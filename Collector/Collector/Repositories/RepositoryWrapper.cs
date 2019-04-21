using Collector.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collector.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IMongoClient _mongoClient;

        private IRepositoryBase<TelemetryContainer> telemetryRepository;

        public RepositoryWrapper(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IRepositoryBase<TelemetryContainer> TelemetryRepository
        {
            get
            {
                if (this.telemetryRepository == null)
                {
                    this.telemetryRepository = new RepositoryBase<TelemetryContainer>(_mongoClient);
                }
                return this.telemetryRepository;
            }
        }

    }
}
