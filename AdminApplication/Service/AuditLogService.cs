﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Skoruba.AuditLogging.EntityFramework.Entities;
using AdminApplication.Dtos.Dashboard;
using AdminApplication.Dtos.Log;
using DataAccess.Mappers;
using DataAccess.Interfaces;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Repositories.Interfaces;

namespace DataAccess.Services
{
    public class AuditLogService<TAuditLog> : IAuditLogService
        where TAuditLog : AuditLog
    {
        protected readonly IAuditLogRepository<TAuditLog> AuditLogRepository;

        public AuditLogService(IAuditLogRepository<TAuditLog> auditLogRepository)
        {
            AuditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogsDto> GetAsync(AuditLogFilterDto filters)
        {
            var pagedList = await AuditLogRepository.GetAsync(filters.Event, filters.Source, filters.Category, filters.Created, filters.SubjectIdentifier, filters.SubjectName, filters.Page, filters.PageSize);
            var auditLogsDto = pagedList.ToModel();

            return auditLogsDto;
        }

        public virtual async Task DeleteLogsOlderThanAsync(DateTime deleteOlderThan)
        {
            await AuditLogRepository.DeleteLogsOlderThanAsync(deleteOlderThan);
        }

        public virtual Task<int> GetDashboardAuditLogsAverageAsync(int lastNumberOfDays, CancellationToken cancellationToken = default)
        {
            return AuditLogRepository.GetDashboardAuditLogsAverageAsync(lastNumberOfDays, cancellationToken);
        }

        public virtual async Task<List<DashboardAuditLogDto>> GetDashboardAuditLogsAsync(int lastNumberOfDays, CancellationToken cancellationToken = default)
        {
           var auditLogs = await AuditLogRepository.GetDashboardAuditLogsAsync(lastNumberOfDays, cancellationToken);

           return auditLogs.Select(auditLog => new DashboardAuditLogDto
           {
               Created = auditLog.Created,
               Total = auditLog.Total
           }).ToList();
        }
    }
}
