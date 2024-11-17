using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class PushedAuthorizationRequest
{
    public long Id { get; set; }

    public string ReferenceValueHash { get; set; } = null!;

    public DateTime ExpiresAtUtc { get; set; }

    public string Parameters { get; set; } = null!;
}
