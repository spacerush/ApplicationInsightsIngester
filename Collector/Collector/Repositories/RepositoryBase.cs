using Collector.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Collector.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApptelemetryContext Context { get; set; }

        public RepositoryBase(ApptelemetryContext apptelemetryContext)
        {
            this.Context = apptelemetryContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.Context.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.Context.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            this.Context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.Context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.Context.Set<T>().Remove(entity);
        }

        public T FindRandom()
        {
            return this.Context.Set<T>().OrderBy(o => Guid.NewGuid()).Take(1).Single();
        }
    }
}
