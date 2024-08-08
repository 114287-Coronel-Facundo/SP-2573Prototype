﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Repositories.Impl
{
    public class OrderTypesRepository : IOrderTypesRepository
    {
        private readonly DomainContext _domainContext;
        private readonly IMapper _mapper;
        public OrderTypesRepository(DomainContext domainContext, IMapper mapper)
        {
            _domainContext = domainContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<T>> GetByIds<T>(List<int> ids) where T : ValuableViewModel
        {
            lock (_domainContext)
            {
                return _domainContext.OrderTypes.Where(p => ids.Contains(p.Id)).ProjectTo<T>(_mapper.ConfigurationProvider).ToList(); 
            }
        }
    }
}