using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Services.Strategies.Interfaces;
using Dummy.Audit.Core.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.Strategies.Impl
{
    public class OrdersAuditService : BaseTemplate,IOrdersAuditService
    {
        private readonly DomainContext _context;

        public OrdersAuditService(DomainContext domainContext) : base(domainContext)
        {
            _context = domainContext;
        }

        //public OrdersAuditService(DomainContext context) : base(_context)
        //{
        //    _context = context;
        //}
        public async Task<IEnumerable<AuditLogGetViewModel>> GetAuditData(IEnumerable<Auditlog> auditLog)
        {
            Console.WriteLine("OrdersAuditService");

            List<int> usersId = auditLog.Select(p => p.UserId).ToList();

            GetUserFullName(usersId.Distinct().ToList());


            var resultForeingKey = ExtractIdFields(auditLog);

            var dataForeingKey = await GetDataForeingKey<OrderType>(resultForeingKey.Where(p => p.Field == "OrderTypeId").Select(p => int.Parse(p.Id)).ToList());

            return new List<AuditLogGetViewModel>();
        }
    }
}
