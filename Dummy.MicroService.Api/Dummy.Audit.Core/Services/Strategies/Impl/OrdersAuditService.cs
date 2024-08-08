using AutoMapper;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Services.IFactoryService.Interfaces;
using Dummy.Audit.Core.Services.Strategies.Interfaces;
using Dummy.Audit.Core.Utils.Enums;
using Dummy.Audit.Core.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.Strategies.Impl
{
    public class OrdersAuditService : BaseTemplate, IOrdersAuditService //TODO: CAMBIAR NOMBRE DE BASETEMPLATE A OTRO
    {
        private readonly IMapper _mapper;

        public OrdersAuditService(IUserRepository userRepository, IFirstOrderRepositoriesFactory firstOrderRepositoriesFactory) : base(userRepository, firstOrderRepositoriesFactory)
        {
        }

        // productbagdetials workshop
        public async Task<IEnumerable<AuditLogGetViewModel>> GetAuditData(IEnumerable<AuditLogGetViewModel> auditLog)
        {
            var firstOrder = GetFirstOrderRelationshipsConfiguration();
            var mutedProperties = GetMutedPropertyConfiguration();

            await RemovePropertyWithMutedPropertyConfiguration(mutedProperties, auditLog.ToList());

            await SetFirstOrderRelationshipData(firstOrder, auditLog.ToList());
            await AddValuesViewModel(auditLog.ToList()); //HUMANIZAR Y PLANTEAR TEMAS DE USUARIO
            List<int> usersId = auditLog.Select(p => p.UserId).Distinct().ToList();
            await GetUserFullName(usersId);
            await AddUserViewModel(auditLog.ToList(), UserViewModel);
            OrderByDateTime(auditLog.ToList());

            return auditLog;
        }

        private List<FirstOrderPropertyConfiguration> GetFirstOrderRelationshipsConfiguration()
        {

            return new List<FirstOrderPropertyConfiguration>
            {
                new FirstOrderPropertyConfiguration("OrderTypeId", "order_types"),
                new FirstOrderPropertyConfiguration("OrderColorCubeId", "order_color_cubes"),
                //new FirstOrderRelationship("ProductBagId", "orders", "concat(products.Name, space(1), products.Code)", "Products.Id",
                //    new List<string[]> { new string[] { "product_bags", "orders.ProductBagId", "product_bags.Id" },
                //                         new string[] { "product_bag_details", "product_bag_details.ProductBagId", "product_bags.Id" },
                //                         new string[] { "products", "product_bag_details.ProductId", "products.Id" }},
                //                        "Product_bag_details.ProductBagId"),
                //new FirstOrderRelationship("InsuranceCompanyId", "insurance_companies", "Name", "Id"),
                //new FirstOrderRelationship("PhoneCountryId", "countries", "InternationalPhoneCode", "Id"),
            };
        }

        private List<MutedPropertyConfiguration> GetMutedPropertyConfiguration()
        {
            return new List<MutedPropertyConfiguration>
            {
                new MutedPropertyConfiguration("AppointmentId"),
                //new MutedPropertyConfiguration("InsuranceCompanyId"),
                //new MutedPropertyConfiguration("EmployeeSignatureId"),
            };
        }

    }
}
