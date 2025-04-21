using Core.RequestHelpers;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interface
{
    public interface IPaginationService
    {
        Task<Pagination<TDto>> CreateAsync<T, TDto>(
            ISpecification<T> spec,
            IGenericRepository<T> repository,
            int pageIndex,
            int pageSize) where T : class;
    }
}
