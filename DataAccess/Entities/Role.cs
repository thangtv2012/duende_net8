﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Role
{
    public string Id { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}