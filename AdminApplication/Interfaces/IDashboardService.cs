using System.Threading;
using System.Threading.Tasks;
using AdminApplication.Dtos.Dashboard;

namespace DataAccess.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardIdentityServerAsync(int auditLogsLastNumberOfDays,
        CancellationToken cancellationToken = default);
}