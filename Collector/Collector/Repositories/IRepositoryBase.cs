using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Collector.Repositories
{
    public interface IRepositoryBase<T> : IBaseMongoRepository
    {

    }
}
