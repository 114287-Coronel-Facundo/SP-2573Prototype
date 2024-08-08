using AutoMapper;
using Dummy.Audit.Core.Model.ClassesDomain;
using Dummy.Audit.Core.Models;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Security.AccessControl;
using Newtonsoft.Json;
using Dummy.Audit.Core.Model;
using System.Text.Json;

namespace Dummy.Audit.Core.Repositories.Impl
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly DomainAuditContext _context;

        public AuditLogRepository(DomainAuditContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuditLogGetViewModel>> GetAudit(string tableName, int primaryKey)
        {
            var sql = @"SELECT * 
                        FROM AuditLogs 
                        WHERE TableName = @tableName 
                        AND JSON_VALUE(PrimaryKey, '$.Id') = @primaryKey order by DateTime desc";

            var result = await _context.Auditlogs
                .FromSqlRaw(sql,
                    new MySqlParameter("@tableName", tableName),
                    new MySqlParameter("@primaryKey", primaryKey))
                        .ToListAsync();

            return await MapAudit(result);
        }


        public virtual async Task<List<AuditLogGetViewModel>> MapAudit(IEnumerable<Auditlog> auditLog)
        {
            var auditLogGetViewModel = new List<AuditLogGetViewModel>();

            foreach (var item in auditLog)
            {   //TODO VER SELECT
                auditLogGetViewModel.Add(new AuditLogGetViewModel
                {
                    Id = item.Id,
                    //User = UserViewModel.Where(p => p.Id == item.UserId).ToList().FirstOrDefault(), //TODO PARA EMPRESA FANTASY NAME
                    UserId = item.UserId,
                    Action = item.Type,
                    TableName = item.TableName,
                    DateTime = item.DateTime,
                    //PrimaryKey = item.PrimaryKey,
                    OldValues = item.OldValues is null ? null : JObject.Parse(item.OldValues),
                    NewValues = item.NewValues is null ? null : JObject.Parse(item.NewValues)
                });
            }

            return auditLogGetViewModel;
        }
    }
}
