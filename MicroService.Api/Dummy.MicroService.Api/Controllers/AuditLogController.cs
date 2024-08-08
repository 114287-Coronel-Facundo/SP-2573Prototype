﻿using Dummy.Audit.Core.Services.Interfaces;
using Dummy.Audit.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dummy.MicroService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuditLogGetViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string command, int primaryKey)
        {
            return Ok( await _auditLogService.GetAudit(command, primaryKey));
        }
    }
}
