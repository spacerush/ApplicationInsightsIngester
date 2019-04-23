using Collector.Models;
using Collector.Models.Documents;
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
        private IRepositoryBase<TelemetryKey> telemetryKeyRepository;
        private IRepositoryBase<RejectedTelemetry> rejectedTelemetryRepository;

        private IRepositoryBase<CollectorUser> userRepository;
        private IRepositoryBase<WebSession> webSessionRepository;


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

        public IRepositoryBase<TelemetryKey> TelemetryKeyRepository
        {
            get
            {
                if (this.telemetryKeyRepository == null)
                {
                    this.telemetryKeyRepository = new RepositoryBase<TelemetryKey>(_mongoClient);
                }
                return this.telemetryKeyRepository;
            }
        }

        public IRepositoryBase<RejectedTelemetry> RejectedTelemetryRepository
        {
            get
            {
                if (this.rejectedTelemetryRepository == null)
                {
                    this.rejectedTelemetryRepository = new RepositoryBase<RejectedTelemetry>(_mongoClient);
                }
                return this.rejectedTelemetryRepository;
            }
        }

        public IRepositoryBase<CollectorUser> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new RepositoryBase<CollectorUser>(_mongoClient);
                }
                return this.userRepository;
            }
        }

        public IRepositoryBase<WebSession> WebSessionRepository
        {
            get
            {
                if (this.webSessionRepository == null)
                {
                    this.webSessionRepository = new RepositoryBase<WebSession>(_mongoClient);
                }
                return this.webSessionRepository;
            }
        }


    }
}
