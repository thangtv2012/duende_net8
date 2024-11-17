// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminApplication.Dtos.Dashboard;
using AdminApplication.Dtos.Log;

namespace DataAccess.Interfaces
{
    public interface IAuditLogService
    {
        Task<AuditLogsDto> GetAsync(AuditLogFilterDto filters);

        Task DeleteLogsOlderThanAsync(DateTime deleteOlderThan);

        Task<int> GetDashboardAuditLogsAverageAsync(int lastNumberOfDays,
            CancellationToken cancellationToken = default);

        Task<List<DashboardAuditLogDto>> GetDashboardAuditLogsAsync(int lastNumberOfDays,
            CancellationToken cancellationToken = default);
    }
}
