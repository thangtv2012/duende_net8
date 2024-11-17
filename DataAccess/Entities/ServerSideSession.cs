using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ServerSideSession
{
    public long Id { get; set; }

    public string Key { get; set; } = null!;

    public string Scheme { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public string? SessionId { get; set; }

    public string? DisplayName { get; set; }

    public DateTime Created { get; set; }

    public DateTime Renewed { get; set; }

    public DateTime? Expires { get; set; }

    public string Data { get; set; } = null!;
}
