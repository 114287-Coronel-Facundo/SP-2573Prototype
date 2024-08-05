using AutoMapper;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Utils;
using Dummy.Audit.Core.Utils.Enums;
using Dummy.Audit.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Dummy.Audit.Core.Services.Strategies
{
    public abstract class BaseTemplate
    {
        private readonly ValuesDictionary _valuesDictionary;
        private readonly IUserRepository _userRepository;
        private readonly IFirstOrdersRepository _descriptionRepository;

        public BaseTemplate(IUserRepository userRepository, IFirstOrdersRepository descriptionRepository)
        {
            _valuesDictionary = new ValuesDictionary();
            _userRepository = userRepository;
            _descriptionRepository = descriptionRepository;
        }

        public List<UserViewModel> UserViewModel { get; set; }

        public async Task GetDataForeingKey(List<FirstOrderRelationship> firstOrders)
        {
            await _descriptionRepository.GetDataFirstOrders(firstOrders);
        }
        public async Task GetUserFullName(List<int> userIds)
        {
            UserViewModel = await _userRepository.GetUsers(userIds);
        }


        public async Task SetFirstOrderRelationshipData(List<FirstOrderRelationship> configuration, List<AuditLogGetViewModel> auditLogGetViewModels)
        {
            //await Parallel.ForEachAsync(configuration, async (propertyConfiguration, cancelationToken) =>
            //{
            //    await ExtractIdFields(auditLogGetViewModels, propertyConfiguration);
            //});

            foreach(var entity in configuration)
            {
                await ExtractIdFields(auditLogGetViewModels, entity);
            }
        }

        public async Task BuildResult(List<AuditLogGetViewModel> auditLogs, List<FirstOrderRelationship> firstOrders, List<UserViewModel> userViewModels)
        {
            await Parallel.ForEachAsync(auditLogs, async (auditLog, CancellationToken) =>
            {
                auditLog.User = userViewModels.Find(p => p.Id == auditLog.UserId);
                var action = ToEnum<AuditType>(auditLog.Action);
                switch (action)
                {
                    case AuditType.Update:
                        ContainPrimaryKey(auditLog.NewValues, auditLog.Id, firstOrders);
                        ContainPrimaryKey(auditLog.OldValues, auditLog.Id, firstOrders);
                        JValuesAction(auditLog.NewValues, auditLog);
                        JValuesAction(auditLog.OldValues, auditLog);
                        break;
                    case AuditType.Create:
                        ContainPrimaryKey(auditLog.NewValues, auditLog.Id, firstOrders);
                        JValuesAction(auditLog.NewValues, auditLog);
                        break;
                    case AuditType.Delete:
                        ContainPrimaryKey(auditLog.NewValues, auditLog.Id, firstOrders);
                        JValuesAction(auditLog.OldValues, auditLog);
                        break;
                }
            });
        }

        #region Private Methods 

        private void JValuesAction(JObject? jValues, AuditLogGetViewModel auditLog)
        {
            auditLog.Values.AddRange(jValues.Properties()
                .Select(value => new ValuesViewModel
                {
                    FieldName = value.Name,
                    NewValue = value.Value.ToString() == "" ? null : value.Value.ToString(),
                    OldValue = null
                }));
        }

        private void ContainPrimaryKey(JObject? jValue, int auditLogId, List<FirstOrderRelationship> firstOrders)
        {
            //Parallel.ForEach(firstOrders, fo =>
            //{
            //    if (fo.ContainsSpecificId(auditLogId))
            //    {
            //        var property = jValue.Property(fo.PropertyName);
            //        if (property.Value.ToString() != "{}" && property != null)
            //        {
            //            var newValue = fo.GetKeyValue((int)property.Value);
            //            property.Replace(new JProperty(fo.PropertyName, newValue));
            //        }
            //    }
            //});
            foreach(var entity in firstOrders)
            {
                if (entity.ContainsSpecificId(auditLogId))
                {
                    var property = jValue.Property(entity.PropertyName);
                    var asd = property.Value.ToString();
                    if (!string.IsNullOrEmpty(property.Value.ToString()) && property != null)
                    {
                        var newValue = entity.GetKeyValue((int)property.Value);
                        property.Replace(new JProperty(entity.PropertyName, newValue));
                    }
                }
            }
        }

        private string FindTransalte(string key)
        {
            return _valuesDictionary.FindTranslate(key);
        }

        private T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private void AddFirstOrderConfiguration(JObject? json, FirstOrderRelationship configuration, int auditId)
        {
            if (json == null)
            {
                return;
            }
            var idNewValueFields = json.Properties()
                .Where(p => p.Name.Equals(configuration.PropertyName))
                .Select(p => p.Value).FirstOrDefault();

            if (!JTokenIsNullOrEmpty(idNewValueFields))
            {
                configuration.AddAuditLogId(auditId);
                configuration.AddFirstOrderData(idNewValueFields.Value<int>());
            }
        }


        private bool JTokenIsNullOrEmpty(JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null) ||
                   (token.Type == JTokenType.Undefined);
        }


        private async Task ExtractIdFields(IEnumerable<AuditLogGetViewModel> auditLogs, FirstOrderRelationship configuration)
        {
            foreach (var auditLog in auditLogs.Where(p => (p.NewValues != null && p.NewValues.ContainsKey(configuration.PropertyName))
                                                    || (p.OldValues != null && p.OldValues.ContainsKey(configuration.PropertyName))))
            {
                AddFirstOrderConfiguration(auditLog.NewValues, configuration, auditLog.Id);
                AddFirstOrderConfiguration(auditLog.OldValues, configuration, auditLog.Id);
            }
        }

        #endregion

        //public virtual ValuesViewModel AddValues<T>(List<Object> dataForeingKeys, Dictionary<string, JsonElement>? newValues, Dictionary<string, JsonElement>? oldValues, string fieldName, AuditType auditType) where T : class, IEntity
        //{
        //    var valuesViewModel = new ValuesViewModel();
        //    valuesViewModel.FieldName = FindTransalte(fieldName);

        //    if (newValues is not null)
        //    {
        //        try
        //        {
        //            var idNewValue = newValues[fieldName].TryGetInt32(out int idNew);
        //            valuesViewModel.NewValue = (auditType == AuditType.Create || auditType == AuditType.Update) ? dataForeingKeys.Where(p => p is T && (int)(p as T).Id == idNew)
        //                                                        .Select(p => (p as T).Name).FirstOrDefault() : null;
        //            valuesViewModel.OldValue = null;
        //        }
        //        catch
        //        {
        //            valuesViewModel.NewValue = null;
        //        }

        //    }

        //    if (oldValues is not null)
        //    {
        //        try
        //        {
        //            var idOldValue = oldValues[fieldName].TryGetInt32(out int idOld);
        //            valuesViewModel.OldValue = auditType == AuditType.Delete || auditType == AuditType.Update ? dataForeingKeys.Where(p => p is T && (int)(p as T).Id == idOld)
        //                                    .Select(p => (p as T).Name).FirstOrDefault() : null;
        //            valuesViewModel.NewValue = null;
        //        }
        //        catch
        //        {
        //            valuesViewModel.OldValue = null;
        //        }
        //    }
        //    return valuesViewModel;
        //}




        //public async Task<bool> ValidateOperation(Dictionary<string, JsonElement>? newValues, Dictionary<string, JsonElement>? oldValues, string fieldName, AuditType auditType)
        //{
        //    bool validate = false;

        //    if (auditType == AuditType.Create)
        //    {

        //        if (newValues != null && newValues.ContainsKey(fieldName))
        //        {
        //            if (newValues[fieldName].ToString() == "")
        //            {
        //                validate = false;
        //            }
        //            else
        //            {
        //                validate = true;
        //            }
        //        }
        //    }
        //    else if (auditType == AuditType.Delete)
        //    {
        //        if (oldValues != null && oldValues.ContainsKey(fieldName))
        //        {
        //            if (oldValues[fieldName].ToString() == "")
        //            {
        //                validate = true;
        //            }
        //            else
        //            {
        //                validate = true;
        //            }
        //        }
        //    }
        //    else if (auditType == AuditType.Update)
        //    {
        //        if (newValues != null && newValues.ContainsKey(fieldName))
        //        {
        //            if (newValues[fieldName].ToString() == "")
        //            {
        //                validate = false;
        //            }
        //            else
        //            {
        //                validate = true;
        //            }
        //        }
        //        if (oldValues != null && oldValues.ContainsKey(fieldName))
        //        {
        //            if (oldValues[fieldName].ToString() == "" && validate == false)
        //            {
        //                validate = false;
        //            }
        //            else
        //            {
        //                validate = true;
        //            }
        //        }
        //    }
        //    return validate;
        //}
    }
}
