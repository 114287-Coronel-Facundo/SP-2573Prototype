using AutoMapper;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Services.Strategies.Interfaces;
using Dummy.Audit.Core.Utils.Enums;
using Dummy.Audit.Core.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.Strategies.Impl
{
    public class OrdersAuditService : BaseTemplate, IOrdersAuditService
    {
        private readonly IMapper _mapper;

        public OrdersAuditService(IUserRepository userRepository, IFirstOrdersRepository descriptionRepository) : base(userRepository, descriptionRepository)
        {
        }

        // productbagdetials workshop
        public async Task<IEnumerable<AuditLogGetViewModel>> GetAuditData(IEnumerable<AuditLogGetViewModel> auditLog)
        {
            List<int> usersId = auditLog.Select(p => p.UserId).Distinct().ToList();
            await GetUserFullName(usersId);
            var firstOrder = GetFirstOrderRelationshipsConfiguration();
            await SetFirstOrderRelationshipData(firstOrder, auditLog.ToList());
            await GetDataForeingKey(firstOrder);
            await BuildResult(auditLog.ToList(), firstOrder, UserViewModel);
            return auditLog;
        }

        private List<FirstOrderRelationship> GetFirstOrderRelationshipsConfiguration()
        {

            return new List<FirstOrderRelationship>
            {                               //Propiedad a buscar // Tabla      // columna         //Id
                new FirstOrderRelationship("ProductBagId", "product_bag_details", "Products.Name", "Products.Id",new List<string[]> {
                    new string[] {"Products", "Products.Id", "product_bag_details.ProductId" } //FROM + JOIN
                }, "Product_bag_details.ProductBagId"),
                new FirstOrderRelationship("InsuranceCompanyId", "insurance_companies", "Name", "Id"),
                new FirstOrderRelationship("OrderTypeId", "order_types", "Name", "Id"),
                new FirstOrderRelationship("OrderColorCubeId", "order_color_cubes", "Name", "Id"),
                new FirstOrderRelationship("PhoneCountryId", "countries", "InternationalPhoneCode", "Id"),
            };
        }

        //private async Task BuildResult(List<AuditLogGetViewModel> auditLogs, List<FirstOrderRelationship> firstOrders)
        //{
        //    foreach (var auditLog in auditLogs)
        //    {
        //        var action = ToEnum<AuditType>(auditLog.Action);
        //        switch (action)
        //        {
        //            case AuditType.Update:
        //                //UpdateAction();
        //                break;
        //            case AuditType.Create:
        //                firstOrders.ForEach(fo =>
        //                {
        //                    if (fo.ContainsSpecificId(auditLog.Id))
        //                    {
        //                        var property = auditLog.NewValues.Property(fo.PropertyName);
        //                        if (property != null)
        //                        {
        //                            var newValue = fo.GetKeyValue((int)property.Value);
        //                            property.Replace(new JProperty(fo.PropertyName, newValue));
        //                        }
        //                    }
        //                });
        //                CreateAction(auditLog.NewValues, auditLog);
        //                break;
        //            case AuditType.Delete:
        //                //DeleteAction();
        //                break;
        //        }
        //    }
        //}

        //private void CreateAction(JObject? newValues, AuditLogGetViewModel auditLog)
        //{
        //    auditLog.Values.AddRange(newValues.Properties()
        //        .Select(value => new ValuesViewModel
        //        {
        //            FieldName = value.Name,
        //            NewValue = value.Value.ToString() == "" ? null : value.Value.ToString(),
        //            OldValue = null
        //        }));
        //}


        //private async Task BuildResult(List<AuditLogGetViewModel> auditLogs, List<FirstOrderRelationship> firstOrders)
        //{
        //    foreach(var auditLog in auditLogs)
        //    {
        //        var action = ToEnum<AuditType>(auditLog.Action);
        //        switch (action)
        //        {
        //            case AuditType.Update:
        //                UpdateAction();
        //                break;
        //            case AuditType.Create:
        //                foreach (var fo in firstOrders)
        //                {
        //                    var property = auditLog.NewValues.Property(fo.PropertyName);
        //                    if (property != null)
        //                    {
        //                        var newValue = fo.GetKeyValue((int)property.Value);
        //                        property.Replace(new JProperty(fo.PropertyName, newValue));
        //                    }
        //                }
        //                CreateAction(auditLog.NewValues, auditLog);
        //                break;
        //            case AuditType.Delete:
        //                DeleteAction();
        //                break;
        //        }
        //    }
        //}

        //private void UpdateAction()
        //{

        //}

        //private void CreateAction(JObject? newValues, AuditLogGetViewModel auditLog)
        //{
        //    foreach(var value in newValues)
        //    {
        //        var logViewModelValue = new ValuesViewModel();
        //        logViewModelValue.FieldName = value.Key;
        //        logViewModelValue.NewValue = value.Value.ToString() == "" ? null : value.Value.ToString();
        //        logViewModelValue.OldValue = null;
        //        auditLog.Values.Add(logViewModelValue);
        //    }
        //}

        //private void DeleteAction()
        //{

        //}

        //private async Task BuildResult(List<AuditLogGetViewModel> auditlogs, List<Object> dataForeingKeys)
        //{

        //    foreach (var log in auditlogs)
        //    {
        //        var oldValues = log.OldValues == null ? null : JsonSerializer.Deserialize<Dictionary<string, JsonElement>>((string)log.OldValues);
        //        var newValues = log.NewValues == null ? null : JsonSerializer.Deserialize<Dictionary<string, JsonElement>>((string)log.NewValues);

        //        var auditEnumType = base.ToEnum<AuditType>((string)log.Action);

        //        if (await ValidateOperation(newValues, oldValues, "OrderTypeId", auditEnumType))
        //        {
        //            log.Values?.Add(AddValues<OrderType>(dataForeingKeys, newValues, oldValues, "OrderTypeId", auditEnumType));
        //        }
        //        if (await ValidateOperation(newValues, oldValues, "OrderColorCubeId", auditEnumType))
        //        {
        //            log.Values?.Add(AddValues<OrderColorCube>(dataForeingKeys, newValues, oldValues, "OrderColorCubeId", auditEnumType));
        //        }
        //        if (await ValidateOperation(newValues, oldValues, "InsuranceCompanyId", auditEnumType))
        //        {
        //            log.Values?.Add(AddValues<InsuranceCompany>(dataForeingKeys, newValues, oldValues, "InsuranceCompanyId", auditEnumType));
        //        }
        //        if(await ValidateOperation(newValues, oldValues, "PhoneCountryId", auditEnumType))
        //        {
        //            log.Values?.Add(AddValues<Country>(dataForeingKeys, newValues, oldValues, "PhoneCountryId", auditEnumType));
        //        }

        //        //Refactorizar...
        //        if(auditEnumType == AuditType.Update)
        //        {
        //            oldValues.Remove("OrderTypeId");
        //            oldValues.Remove("OrderColorCubeId");
        //            oldValues.Remove("InsuranceCompanyId");
        //            oldValues.Remove("PhoneCountryId");

        //            newValues.Remove("OrderTypeId");
        //            newValues.Remove("OrderColorCubeId");
        //            newValues.Remove("InsuranceCompanyId");
        //            newValues.Remove("PhoneCountryId");



        //            foreach (var value in newValues)
        //            {
        //                var logViewModelValue = new ValuesViewModel();
        //                logViewModelValue.FieldName = FindTransalte(value.Key);
        //                logViewModelValue.NewValue = value.Value.ToString() == "" ? null : value.Value.ToString();
        //                logViewModelValue.OldValue = oldValues[value.Key].ToString() == ""? null : oldValues[value.Key].ToString();
        //                log.Values.Add(logViewModelValue);
        //            }

        //        }else if(auditEnumType == AuditType.Delete)
        //        {
        //            oldValues.Remove("OrderTypeId");
        //            oldValues.Remove("OrderColorCubeId");
        //            oldValues.Remove("InsuranceCompanyId");
        //            oldValues.Remove("PhoneCountryId");
        //            foreach (var value in oldValues)
        //            {
        //                log.Values.Add(new ValuesViewModel
        //                {
        //                    FieldName = FindTransalte(value.Key),
        //                    OldValue = value.Value.ToString() == "" ? null : value.Value.ToString(),
        //                    NewValue = null 
        //                });
        //            }
        //        }else if(auditEnumType == AuditType.Create)
        //        {
        //            newValues.Remove("OrderTypeId");
        //            newValues.Remove("OrderColorCubeId");
        //            newValues.Remove("InsuranceCompanyId");
        //            newValues.Remove("PhoneCountryId");
        //            foreach (var value in newValues)
        //            {
        //                log.Values.Add(new ValuesViewModel
        //                {
        //                    FieldName = FindTransalte(value.Key),
        //                    NewValue = value.Value.ToString() == "" ? null : value.Value.ToString(),
        //                    OldValue = null
        //                });
        //            }
        //        }
        //    }
        //}

    }
}
