﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

namespace AdminApplication.BusinessLogic.Shared.Dtos.Common
{
    public class Pager
	{
		public int TotalCount { get; set; }

		public int PageSize { get; set; }

		public string Action { get; set; }

        public string Search { get; set; }

	    public bool EnableSearch { get; set; } = false;

	    public int MaxPages { get; set; } = 10;

	    public Dictionary<string, string> AdditionalParameters { get; } = new();
	}
}
