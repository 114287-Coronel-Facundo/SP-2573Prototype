using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Services.Strategies
{
    public abstract class BaseTemplate
    {
        private readonly DomainContext _domainContext;

        public BaseTemplate(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public List<UserViewModel> FullNameUser { get; set; }


        public string GetUserFullName(List<int> userIds)
        {
            var query = _domainContext.Users.AsQueryable().Include(p => p.Person);
            
            FullNameUser = query.Where(p => userIds.Contains(p.Id)).Select(p => new UserViewModel
            {
                Name = p.Person.Name,
                Surname = p.Person.Surname,
                LegalStatusId = p.Person.LegalStatusId,
                BusinessName = p.Person.BusinessName,
            }).ToList();     

            return string.Empty;
        }


        public async Task<T> GetDataForeingKey<T>(List<int> id) where T : class, IEntity
        {
            //var entity = await _domainContext.Set<T>().AsQueryable().Where(p => id.Contains(p.Id)).FirstOrDefaultAsync();

            var entity = await _domainContext.Set<T>()
                .AsQueryable()
                .Where(p => id.Contains((p as IEntity).Id))
                .FirstOrDefaultAsync();
            return entity;
        }

        public List<(string Field, string Id)> ExtractIdFields(IEnumerable<Auditlog> auditLogs)
        {
            var TableFields = new List<(string Field, string Id)>();
            var idNewValueFields = new List<string>();
            var idOldValuesFields = new List<string>(); 
            foreach (var json in auditLogs)
            {
                if(json.NewValues != null)
                {
                    var jObjectNewValues = JObject.Parse(json.NewValues);
                    idNewValueFields = jObjectNewValues.Properties()
                        .Where(p => p.Name.EndsWith("Id") && p.Value != null)
                        .Select(p => $"{p.Name},{p.Value}").Distinct()
                        .ToList();
                }

                if(json.OldValues != null)
                {
                    var jObjectNewValues = JObject.Parse(json.NewValues);
                    idOldValuesFields = jObjectNewValues.Properties()
                        .Where(p => p.Name.EndsWith("Id") && p.Value != null)
                        .Select(p => $"{p.Name},{p.Value}").Distinct()
                        .ToList();
                }

                foreach (var item in idNewValueFields)
                {
                    var fieldAux = item.Split(',')[0];
                    var idAux = item.Split(',')[1];
                    if (!string.IsNullOrEmpty(idAux))
                        TableFields.Add((fieldAux, idAux));
                }


                foreach(var item in idOldValuesFields)
                {
                    var fieldAux = item.Split(',')[0];
                    var idAux = item.Split(',')[1];
                    if (!string.IsNullOrEmpty(idAux))
                        TableFields.Add((fieldAux, idAux));
                }
            }


            return TableFields;
        }


        //private string GetTableSecondary(string fieldAux, string id)
        //{
        //    var dicc = new Dictionary<string, class>();
        //    dicc.Add("PhoneCountryId", "countries");
        //    dicc.Add("OrderTypeId", "order_types");

        //    if (dicc.ContainsKey(fieldAux))
        //    {
        //        var table = dicc[fieldAux];
        //    }

        //    return string.Empty;
        //}


    }
}
