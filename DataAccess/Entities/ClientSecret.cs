﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class ClientSecret
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string? Description { get; set; }

    public string Value { get; set; } = null!;

    public DateTime? Expiration { get; set; }

    public string Type { get; set; } = null!;

    public DateTime Created { get; set; }

    public virtual Client Client { get; set; } = null!;
}
