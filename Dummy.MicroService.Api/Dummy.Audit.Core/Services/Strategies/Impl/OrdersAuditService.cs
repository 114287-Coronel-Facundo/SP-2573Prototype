using AutoMapper;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Models;
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
        private readonly DomainContext _context;
        private readonly IMapper _mapper;

        public OrdersAuditService(DomainContext domainContext, IMapper mapper) : base(domainContext)
        {
            _context = domainContext;
            _mapper = mapper;
        }
        // productbagdetials workshop
        public async Task<IEnumerable<AuditLogGetViewModel>> GetAuditData(IEnumerable<Auditlog> auditLog)
        {
            List<int> usersId = auditLog.Select(p => p.UserId).Distinct().ToList();
            GetUserFullName(usersId);
            var resultForeingKey = ExtractIdFields(auditLog); // ver yield return y dictionary
            var dataForeingKeys = new List<Object>();
            await GetDataForeingKey(dataForeingKeys, resultForeingKey); // ver yield return
            var map = await MapAudit(auditLog);
            await BuildResult(map, auditLog, dataForeingKeys);

            return map;
        }


        private async Task BuildResult(List<AuditLogGetViewModel> map, IEnumerable<Auditlog> auditlogs, List<Object> dataForeingKeys)
        {

            foreach (var log in auditlogs)
            {
                var oldValues = string.IsNullOrEmpty(log.OldValues) ? null : JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(log.OldValues);
                var newValues = string.IsNullOrEmpty(log.NewValues) ? null : JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(log.NewValues);
                var auditViewModel = map.FirstOrDefault(map => map.Id == log.Id);

                var auditEnumType = ToEnum<AuditType>(log.Type);

                if (await ValidateOperation(newValues, oldValues, "OrderTypeId", auditEnumType))
                {
                    auditViewModel.Values.Add(AddValues<OrderType>(dataForeingKeys, newValues, oldValues, "OrderTypeId", auditEnumType));
                }
                if (await ValidateOperation(newValues, oldValues, "OrderColorCubeId", auditEnumType))
                {
                    auditViewModel.Values.Add(AddValues<OrderColorCube>(dataForeingKeys, newValues, oldValues, "OrderColorCubeId", auditEnumType));
                }
                if (await ValidateOperation(newValues, oldValues, "InsuranceCompanyId", auditEnumType))
                {
                    auditViewModel.Values.Add(AddValues<InsuranceCompany>(dataForeingKeys, newValues, oldValues, "InsuranceCompanyId", auditEnumType));
                }
                if(await ValidateOperation(newValues, oldValues, "PhoneCountryId", auditEnumType))
                {
                    auditViewModel.Values.Add(AddValues<Country>(dataForeingKeys, newValues, oldValues, "PhoneCountryId", auditEnumType));
                }

                //Refactorizar...
                if(auditEnumType == AuditType.Update)
                {
                    oldValues.Remove("OrderTypeId");
                    oldValues.Remove("OrderColorCubeId");
                    oldValues.Remove("InsuranceCompanyId");
                    oldValues.Remove("PhoneCountryId");

                    newValues.Remove("OrderTypeId");
                    newValues.Remove("OrderColorCubeId");
                    newValues.Remove("InsuranceCompanyId");
                    newValues.Remove("PhoneCountryId");



                    foreach (var value in newValues)
                    {
                        var logViewModelValue = new ValuesViewModel();
                        logViewModelValue.FieldName = FindTransalte(value.Key);
                        logViewModelValue.NewValue = value.Value.ToString() == "" ? null : value.Value.ToString();
                        logViewModelValue.OldValue = oldValues[value.Key].ToString() == ""? null : oldValues[value.Key].ToString();
                        auditViewModel.Values.Add(logViewModelValue);
                    }

                }else if(auditEnumType == AuditType.Delete)
                {
                    oldValues.Remove("OrderTypeId");
                    oldValues.Remove("OrderColorCubeId");
                    oldValues.Remove("InsuranceCompanyId");
                    oldValues.Remove("PhoneCountryId");
                    foreach (var value in oldValues)
                    {
                        auditViewModel.Values.Add(new ValuesViewModel
                        {
                            FieldName = FindTransalte(value.Key),
                            OldValue = value.Value.ToString() == "" ? null : value.Value.ToString(),
                            NewValue = null 
                        });
                    }
                }else if(auditEnumType == AuditType.Create)
                {
                    newValues.Remove("OrderTypeId");
                    newValues.Remove("OrderColorCubeId");
                    newValues.Remove("InsuranceCompanyId");
                    newValues.Remove("PhoneCountryId");
                    foreach (var value in newValues)
                    {
                        auditViewModel.Values.Add(new ValuesViewModel
                        {
                            FieldName = FindTransalte(value.Key),
                            NewValue = value.Value.ToString() == "" ? null : value.Value.ToString(),
                            OldValue = null
                        });
                    }
                }
            }
        }



        private async Task GetDataForeingKey(List<Object> dataForeingKeys, List<(string Field, string Id)> resultForeingKey)
        {
            dataForeingKeys.Add(await GetDataForeingKey<OrderType>(resultForeingKey.Where(p => p.Field == "OrderTypeId" && !string.IsNullOrEmpty(p.Id)).Select(p => int.Parse(p.Id)).ToList()));
            dataForeingKeys.Add(await GetDataForeingKey<OrderColorCube>(resultForeingKey.Where(p => p.Field == "OrderColorCubeId" &&  !string.IsNullOrEmpty(p.Id)).Select(p => int.Parse(p.Id)).ToList()));
            dataForeingKeys.Add(await GetDataForeingKey<InsuranceCompany>(resultForeingKey.Where(p => p.Field == "InsuranceCompanyId" &&  !string.IsNullOrEmpty(p.Id)).Select(p => int.Parse(p.Id)).ToList()));
            dataForeingKeys.Add(await GetDataForeingKey<Country>(resultForeingKey.Where(p => p.Field == "PhoneCountryId" && !string.IsNullOrEmpty(p.Id)).Select(p => int.Parse(p.Id)).ToList()));
        }
    }
}
