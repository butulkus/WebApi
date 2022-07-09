using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.DataContext.Repositories.Base
{
    public abstract class WriteRepository<TEntity, TModel> : IWriteRepository<TEntity, TModel> where TEntity : class
    {
        private protected readonly ILogger _logger;
        private protected readonly CatalogDBContext _catalogDBContext;
        private protected readonly DbSet<TEntity> _dbSet;
        private protected readonly IMapper _mapper;
        public WriteRepository(
            ILogger logger,
            CatalogDBContext catalogDBContext,
            IMapper mapper)
        {
            _logger = logger;
            _catalogDBContext = catalogDBContext;
            _mapper = mapper;
            _dbSet = _catalogDBContext.Set<TEntity>();
        }

        ///<inheritdoc/>
        public TModel AddEnity(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _logger.LogInformation("Adding model in DB. Model - {model}", model.GetType().Name);
            _dbSet.Add(entity);

            return model;
        }
    }
}
