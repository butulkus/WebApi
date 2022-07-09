using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataContext.Repositories.Base
{
    public interface IWriteRepository<TEntity, TModel>
    {
        /// <summary>
        /// Add the entity to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Added TModel</returns>
        TModel AddEnity(TModel model);
    }
}
