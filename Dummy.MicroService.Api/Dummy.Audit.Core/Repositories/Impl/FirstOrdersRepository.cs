using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.Utils;
using Dummy.Audit.Core.Utils.Enums;
using Dummy.Audit.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Dummy.Audit.Core.Repositories.Impl
{
    public class FirstOrdersRepository : IFirstOrdersRepository1
    {
        DomainContext _domainContext;
        private string _select = "";
        private string? _idsToSearch = "";
        private List<string> _joins = new List<string>();
        private List<string> _conditions = new List<string>();
        private string _from = "";
        private string _id = "ID";
        private bool _hasJoin = false;

        public FirstOrdersRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public async Task GetDataFirstOrders(List<FirstOrderRelationship> firstOrders)
        {
            foreach(var fo in firstOrders)
            {
                var idsWhere = fo.GetKeysFirstOrderData().ToArray();
                if (!idsWhere.Any())
                    continue;

                Select(fo.FindIdName, fo.FindColumnName);
                From(fo.TableName);
                if (fo.Joins is not null)
                {
                    foreach (var join in fo.Joins)
                    {
                        LeftJoin(join);
                    }
                }

                WhereIn(fo.IdWhere == null ? fo.FindIdName : fo.IdWhere, idsWhere);
                var query = ToString();
                var entities = _domainContext.Database.SqlQueryRaw<ValuableViewModel>(query);
                if (_hasJoin)
                    fo.AddFirstOrderData(idsWhere[0], entities.Select(p => p.Value).ToList());
                else
                {
                    foreach (var entity in entities)
                    {
                        fo.AddFirstOrderData(entity.Id, new List<string> { entity.Value });
                    }
                }
                Cleared();
            }
        }

        private void Select(string id, string columnName)
        {
             _select = $"SELECT {id} AS \"Id\", {columnName} AS \"Value\"";
        }

        private void From(string tableName)
        {
            _from = $" From {tableName} ";
        }


        private void LeftJoin(string[] join)
        {
             _joins.Add($"LEFT JOIN {join[0]} ON {join[1]} = {join[2]}");
            _hasJoin = true;
        }

        private void WhereIn(string column, int[] values)
        {
            string valueList = string.Join(", ", values);
            _conditions.Add($"{column} IN ({valueList})");
        }

        private string ToString()
        {
            return _select + _from + string.Join(" ", _joins) + " WHERE " + string.Join(" AND ", _conditions);
        }

        private void Cleared()
        {
            _conditions.Clear();
            _joins.Clear();
            _select = string.Empty;
            _joins.Clear();
            _from = string.Empty;
            _hasJoin= false;
        }
    }
}
