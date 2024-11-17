﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using AdminApplication.Dtos.IdentityProvider;

namespace DataAccess.Interfaces
{
    public interface IIdentityProviderService
    {
        Task<IdentityProvidersDto> GetIdentityProvidersAsync(string search, int page = 1, int pageSize = 10);

        Task<IdentityProviderDto> GetIdentityProviderAsync(int identityProviderId);

        Task<bool> CanInsertIdentityProviderAsync(IdentityProviderDto identityProvider);

        Task<int> AddIdentityProviderAsync(IdentityProviderDto identityProvider);

        Task<int> UpdateIdentityProviderAsync(IdentityProviderDto identityProvider);

        Task<int> DeleteIdentityProviderAsync(IdentityProviderDto identityProvider);
    }
}