using System.Collections.Generic;

namespace AdminApplication.Dtos.Dashboard;

public class DashboardDto
{
    public int ClientsTotal { get; set; }

    public int ApiResourcesTotal { get; set; }

    public int ApiScopesTotal { get; set; }

    public int IdentityResourcesTotal { get; set; }
    
    public int IdentityProvidersTotal { get; set; }
    
    public long AuditLogsAvg { get; set; }
    
    public List<DashboardAuditLogDto> AuditLogsPerDaysTotal { get; set; }
}