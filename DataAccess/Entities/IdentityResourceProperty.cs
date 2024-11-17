﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class IdentityResourceProperty
{
    public int Id { get; set; }

    public int IdentityResourceId { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual IdentityResource IdentityResource { get; set; } = null!;
}
