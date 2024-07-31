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

namespace Dummy.Audit.Core.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly DomainAuditContext _context;

        public AuditLogRepository(DomainAuditContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Auditlog>> GetAudit(string tableName, int primaryKey)
        {
            var sql = @"SELECT * 
                        FROM AuditLogs 
                        WHERE TableName = @tableName 
                        AND JSON_VALUE(PrimaryKey, '$.Id') = @primaryKey";

            var result = await _context.Auditlogs
                .FromSqlRaw(sql,
                    new MySqlParameter("@tableName", tableName),
                    new MySqlParameter("@primaryKey", primaryKey))
                        .ToListAsync();

            return result;
        }
    }
}
