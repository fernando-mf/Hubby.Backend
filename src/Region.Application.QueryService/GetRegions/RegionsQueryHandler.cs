﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using Library.Mongo.Persistence;
using MediatR;
using MongoDB.Driver;

namespace Region.Application.QueryService.GetRegions
{
    public class RegionsQueryHandler: IRequestHandler<RegionsQuery, IList<RegionReadModel>>
    {
        private readonly IMongoCollection<RegionReadModel> _collection;
        
        public RegionsQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<RegionReadModel>();
        }

        public async Task<IList<RegionReadModel>> Handle(RegionsQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(FilterDefinition<RegionReadModel>.Empty).ToListAsync(cancellationToken);
        }
    }
}
