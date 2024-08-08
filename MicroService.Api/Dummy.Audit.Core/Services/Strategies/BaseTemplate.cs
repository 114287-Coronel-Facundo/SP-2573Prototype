using Audit.Core.Resources;
using AutoMapper;
using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Repositories.Interfaces;
//using Dummy.Audit.Core.Resources;
using Dummy.Audit.Core.Services.IFactoryService.Interfaces;
using Dummy.Audit.Core.Utils;
using Dummy.Audit.Core.Utils.Enums;
using Dummy.Audit.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Security.AccessControl;
using System.Text.Json;

namespace Dummy.Audit.Core.Services.Strategies
{
    public abstract class BaseTemplate
    {
        private readonly IUserRepository _userRepository;
        private readonly IFirstOrderRepositoriesFactory _firstOrderRepositoriesFactory;

        public BaseTemplate(IUserRepository userRepository, IFirstOrderRepositoriesFactory firstOrderRepositoriesFactory)
        {
            _userRepository = userRepository;
            _firstOrderRepositoriesFactory = firstOrderRepositoriesFactory;
        }

        public List<UserViewModel> UserViewModel { get; set; }

        public async Task GetUserFullName(List<int> userIds)
        {
            UserViewModel = await _userRepository.GetUsers(userIds);
        }

        public async Task RemovePropertyWithMutedPropertyConfiguration(List<MutedPropertyConfiguration> mutedPropertyConfigurations, List<AuditLogGetViewModel> auditLog)
        {
            foreach(var propertyConfiguration in mutedPropertyConfigurations)
            {
                var propertyNameToRemove = propertyConfiguration.MutedPropertyName;
                await RemovePropertyByName(propertyConfiguration.MutedPropertyName, auditLog.Where(p => (p.NewValues != null && p.NewValues.ContainsKey(propertyNameToRemove))
                                                    || (p.OldValues != null && p.OldValues.ContainsKey(propertyNameToRemove))).ToList());
            }
        }

        private async Task RemovePropertyByName(string propertyNameToRemove, List<AuditLogGetViewModel> auditLogs)
        {
            foreach (var log in auditLogs)
            {
                if(log.OldValues is not null)
                    log.OldValues.Property(propertyNameToRemove).Remove();

                if(log.NewValues is not null)
                    log.NewValues.Property(propertyNameToRemove).Remove();
            }
        }

        public async Task SetFirstOrderRelationshipData(List<FirstOrderPropertyConfiguration> configuration, List<AuditLogGetViewModel> auditLogGetViewModels)
        {
            await Parallel.ForEachAsync(configuration, async (propertyConfiguration, cancelationToken) =>
            {
                ExtractIdFields(auditLogGetViewModels, propertyConfiguration);
                GetValuables(propertyConfiguration);
                HumanizedValues(auditLogGetViewModels, propertyConfiguration);
            });
        }

        public async Task AddValuesViewModel(List<AuditLogGetViewModel> auditLogs)
        {
            await Parallel.ForEachAsync(auditLogs, async (auditLog, CancellationToken) =>
            {
                var action = ToEnum<AuditType>(auditLog.Action);
                switch (action)
                {
                    case AuditType.Update:
                        JValueValuesUpdateAction(auditLog.NewValues, auditLog.OldValues, auditLog);
                        break;
                    case AuditType.Create:
                        JValuesAction(auditLog.NewValues, auditLog, action);
                        break;
                    case AuditType.Delete:
                        JValuesAction(auditLog.OldValues, auditLog, action);
                        break;
                }
            });
        }

        public async Task AddUserViewModel(List<AuditLogGetViewModel> auditLogs, List<UserViewModel> userViewModels)
        {
            foreach (var auditLog in auditLogs)
            {
                auditLog.User = userViewModels.FirstOrDefault(p => p.Id == auditLog.UserId);
            }
        }

        public void OrderByDateTime(List<AuditLogGetViewModel> auditLogs)
        {
            var orderedAuditLogs = auditLogs
                .OrderBy(log => log.DateTime)
                .ToList();
        }
        #region Private Methods 

        private async Task GetValuables(FirstOrderPropertyConfiguration propertyConfiguration)
        {
            var strategy = _firstOrderRepositoriesFactory.GetStrategy(propertyConfiguration);
            var values = (await strategy.GetByIds<ValuableViewModel>(propertyConfiguration.GetKeysFirstOrderData())).ToList();
            foreach (var item in values)
            {
                propertyConfiguration.SetValueFirstOrderData(item);
            }
        }

        private async Task HumanizedValues(List<AuditLogGetViewModel> auditLogGetViewModels, FirstOrderPropertyConfiguration firstOrderRelationship)
        {
            var ids = firstOrderRelationship.GetAuditLogIds();
            var entities = auditLogGetViewModels.Where(p => ids.Contains(p.Id));
            foreach (var item in entities)
            {
                ReplaceJsonProperties(item.NewValues, firstOrderRelationship);
                ReplaceJsonProperties(item.OldValues, firstOrderRelationship);
            }
        }

        private void ReplaceJsonProperties(JObject? jValues, FirstOrderPropertyConfiguration firstOrderRelationship)
        {
            if (jValues == null)
            {
                return;
            }
            var property = jValues.Property(firstOrderRelationship.PropertyName);
            if (property == null || string.IsNullOrEmpty(property.Value.ToString()) || property.Type == JTokenType.Integer)
            {
                return;
            }
            var newValue = firstOrderRelationship.GetKeyValue((int)property.Value);
            property.Replace(new JProperty(firstOrderRelationship.PropertyName, newValue));
        }

        private void JValuesAction(JObject? jValues, AuditLogGetViewModel auditLog, AuditType auditType)
        {
            if (!jValues.HasValues)
                return;

            if(auditType == AuditType.Create)
                auditLog.Values.AddRange(jValues.Properties()
                    .Select(value => new ValuesViewModel
                    {
                        FieldName = PropertiesNames.ResourceManager.GetString(value.Name),
                        NewValue = auditType == AuditType.Create || auditType == AuditType.Update ? (value.Value.ToString() == "" ? null : value.Value.ToString() ): null,
                        OldValue = null,
                    }));

            if(auditType == AuditType.Delete)
                auditLog.Values.AddRange(jValues.Properties()
                    .Select(value => new ValuesViewModel
                    {
                        FieldName = PropertiesNames.ResourceManager.GetString(value.Name),
                        OldValue = auditType == AuditType.Create || auditType == AuditType.Update ? (value.Value.ToString() == "" ? null : value.Value.ToString()) : null,
                        NewValue = null,
                    }));
        }

        private void JValueValuesUpdateAction(JObject? jNewValues, JObject? JOldValue, AuditLogGetViewModel auditLog)
        {
            if (!jNewValues.HasValues)
                return;

            auditLog.Values.AddRange(jNewValues.Properties()
                .Select(value => new ValuesViewModel
                {
                    FieldName = PropertiesNames.ResourceManager.GetString(value.Name),
                    NewValue = value.Value.ToString() == "" ? null : value.Value.ToString(),
                    OldValue = string.IsNullOrEmpty(JOldValue.GetValue(value.Name).ToString()) ? null : JOldValue.GetValue(value.Name).ToString(),
                }));
        }

        private T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private void AddFirstOrderConfiguration(JObject? json, FirstOrderPropertyConfiguration configuration, int auditId)
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


        private async Task ExtractIdFields(IEnumerable<AuditLogGetViewModel> auditLogs, FirstOrderPropertyConfiguration configuration)
        {
            foreach (var auditLog in auditLogs.Where(p => (p.NewValues != null && p.NewValues.ContainsKey(configuration.PropertyName))
                                                    || (p.OldValues != null && p.OldValues.ContainsKey(configuration.PropertyName))))
            {
                AddFirstOrderConfiguration(auditLog.NewValues, configuration, auditLog.Id);
                AddFirstOrderConfiguration(auditLog.OldValues, configuration, auditLog.Id);
            }
        }

        #endregion
    }
}
