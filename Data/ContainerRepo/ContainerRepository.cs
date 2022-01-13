using AutoMapper;
using Data.Context;
using Data.Generic;
using Data.Dtos;
using Infrastructure.Result;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.DataModel;

namespace Data.ContainerRepo
{
    public class ContainerRepository : GenericRepository<Container>, IContainerRepository
    {
        IMapper mapper;
        public ContainerRepository(WasteSystemDbContext context, ILogger logger, IMapper mapper) : base(context, logger)
        {
            this.mapper = mapper;
        }

        public IEnumerable<Container> Where(Expression<Func<Container, bool>> where)
        {
            return base.Where(where);
        }

        public async Task<Result<Container>> Add(Container entity)
        {
            return await base.Add(entity);
        }


        public async Task<Result> Delete(long id)
        {
            return await base.Delete(id);
        }

        public Task<Result<List<Container>>> GetAll()
        {
            return base.GetAll();
        }


        public Task<Result<Container>> Update(Container entity)
        {
            return base.Update(entity);

        }
    }
}
