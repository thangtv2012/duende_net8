﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class PersistedGrant
{
    public long Id { get; set; }

    public string? Key { get; set; }

    public string Type { get; set; } = null!;

    public string? SubjectId { get; set; }

    public string? SessionId { get; set; }

    public string ClientId { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? Expiration { get; set; }

    public DateTime? ConsumedTime { get; set; }

    public string Data { get; set; } = null!;
}
