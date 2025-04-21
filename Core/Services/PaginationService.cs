using AutoMapper;
using Core.RequestHelpers;
using Core.Services.Interface;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PaginationService : IPaginationService
    {
        private readonly IMapper _mapper;

        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Pagination<TDto>> CreateAsync<T, TDto>(
            ISpecification<T> spec,
            IGenericRepository<T> repository,
            int pageIndex,
            int pageSize) where T : class
        {
            var items = await repository.ListAsync(spec);
            var count = await repository.CountAsync(spec);
            var data = _mapper.Map<IReadOnlyList<TDto>>(items);

            return new Pagination<TDto>(pageIndex, pageSize, count, data);
        }
    }
}
