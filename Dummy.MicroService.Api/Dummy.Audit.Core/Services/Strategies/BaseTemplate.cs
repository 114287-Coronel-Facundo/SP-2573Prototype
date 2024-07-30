using AutoMapper;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Utils;
using Dummy.Audit.Core.Utils.Enums;
using Dummy.Audit.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Dummy.Audit.Core.Services.Strategies
{
    public abstract class BaseTemplate
    {
        private readonly DomainContext _domainContext;
        private readonly ValuesDictionary _valuesDictionary;

        public BaseTemplate(DomainContext domainContext)
        {
            _domainContext = domainContext;
            _valuesDictionary = new ValuesDictionary();
        }

        public List<UserViewModel> FullNameUser { get; set; }


        public void GetUserFullName(List<int> userIds)
        {
            var query = _domainContext.Users.AsQueryable().Include(p => p.Person);
            
            FullNameUser = query.Where(p => userIds.Contains(p.Id)).Select(p => new UserViewModel
            {
                Id = p.Id,
                Name = p.Person.Name,
                Surname = p.Person.Surname,
                LegalStatusId = p.Person.LegalStatusId,
                BusinessName = p.Person.BusinessName,
            }).ToList();
        }


        public async Task<T> GetDataForeingKey<T>(List<int> id) where T : class, IEntity
        {
            var entity = await _domainContext.Set<T>()
                .AsQueryable()
                .Where(p => id.Contains((p as IEntity).Id))
                .FirstOrDefaultAsync();
            return entity;
        }

        public List<(string Field, string Id)> ExtractIdFields(IEnumerable<Auditlog> auditLogs)
        {
            var TableFields = new List<(string Field, string Id)>();

            foreach (var json in auditLogs)
            {
                if (json.NewValues != null)
                {
                    var jObjectNewValues = JObject.Parse(json.NewValues);
                    var idNewValueFields = jObjectNewValues.Properties()
                        .Where(p => p.Name.EndsWith("Id") && p.Value != null)
                        .Select(p => (Field: p.Name, Id: p.Value.ToString()))
                        .Distinct();

                    TableFields.AddRange(idNewValueFields);
                }

                if (json.OldValues != null)
                {
                    var jObjectOldValues = JObject.Parse(json.OldValues);
                    var idOldValuesFields = jObjectOldValues.Properties()
                        .Where(p => p.Name.EndsWith("Id") && p.Value != null)
                        .Select(p => (Field: p.Name, Id: p.Value.ToString()))
                        .Distinct();

                    TableFields.AddRange(idOldValuesFields);
                }
            }

            return TableFields;
        }
        public async Task<List<AuditLogGetViewModel>> MapAudit(IEnumerable<Auditlog> auditLog)
        {
            var auditLogGetViewModel = new List<AuditLogGetViewModel>();

            foreach (var item in auditLog)
            {
                auditLogGetViewModel.Add(new AuditLogGetViewModel
                {
                    Id = item.Id,
                    User = FullNameUser.Where(p => p.Id == item.UserId).ToList().FirstOrDefault(),
                    Type = item.Type,
                    TableName = item.TableName,
                    DateTime = item.DateTime,
                    AffectedColumns = item.AffectedColumns,
                    PrimaryKey = item.PrimaryKey,
                });
            }

            return auditLogGetViewModel;
        }

        public ValuesViewModel AddValues<T>(List<Object> dataForeingKeys, Dictionary<string, JsonElement>? newValues, Dictionary<string, JsonElement>? oldValues, string fieldName, AuditType auditType) where T : class, IEntity
        {
            var valuesViewModel = new ValuesViewModel();
            valuesViewModel.FieldName = FindTransalte(fieldName);

            if (newValues is not null)
            {
                int idNew;
                try
                {
                    var idNewValue = newValues[fieldName].TryGetInt32(out idNew);
                    valuesViewModel.NewValue = (auditType == AuditType.Create || auditType == AuditType.Update) ? dataForeingKeys.Where(p => p is T && (int)(p as T).Id == idNew)
                                                                .Select(p => (p as T).Name).FirstOrDefault() : null;
                    valuesViewModel.OldValue = null;
                }
                catch
                {
                    valuesViewModel.NewValue = null;
                }

            }

            if(oldValues is not null)
            {
                int idOld;
                try
                {
                    var idOldValue = oldValues[fieldName].TryGetInt32(out idOld);
                    valuesViewModel.OldValue = auditType == AuditType.Delete || auditType == AuditType.Update ? dataForeingKeys.Where(p => p is T && (int)(p as T).Id == idOld)
                                            .Select(p => (p as T).Name).FirstOrDefault() : null;
                    valuesViewModel.NewValue = null;
                }
                catch
                {
                    valuesViewModel.OldValue = null;
                }
            }
            return valuesViewModel;
        }


        public T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public async Task<bool> ValidateOperation(Dictionary<string, JsonElement>? newValues, Dictionary<string, JsonElement>? oldValues, string fieldName, AuditType auditType)
        {
            bool validate = false;

            if(auditType == AuditType.Create)
            {

                if (newValues != null && newValues.ContainsKey(fieldName) && newValues[fieldName].ToString() != "")
                {
                    if (newValues[fieldName].ToString() == "")
                    {
                        validate = false;
                    }
                    else
                    {
                        validate = true;
                    }
                }
            }
            else if (auditType == AuditType.Delete)
            {
                if (oldValues != null && oldValues.ContainsKey(fieldName))
                {
                    if (oldValues[fieldName].ToString() == "")
                    {
                        validate = true;
                    }
                    else
                    {
                        validate = true;
                    }
                }
            }
            else if (auditType == AuditType.Update)
            {
                if (newValues != null && newValues.ContainsKey(fieldName))
                {
                    if (newValues[fieldName].ToString() == "")
                    {
                        validate = false;
                    }
                    else
                    {
                        validate = true;
                    }
                }
                if (oldValues != null && oldValues.ContainsKey(fieldName))
                {
                    if (oldValues[fieldName].ToString() == "" && validate == false)
                    {
                        validate = false;
                    }
                    else
                    {
                        validate = true;
                    }
                }
            }
            return validate;
        }

        public string FindTransalte(string key)
        {
            return _valuesDictionary.FindTranslate(key);
        }
    }
}
