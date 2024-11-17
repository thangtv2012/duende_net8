// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.AspNetCore.Mvc;
using Skoruba.Duende.IdentityServer.STS.Identity.Configuration.Interfaces;

namespace Skoruba.Duende.IdentityServer.STS.Identity.ViewComponents
{
    public class NET8Duende_SkorubaLinkViewComponent : ViewComponent
    {
        private readonly IRootConfiguration _configuration;

        public NET8Duende_SkorubaLinkViewComponent(IRootConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var identityAdminUrl = _configuration.AdminConfiguration.IdentityAdminBaseUrl;

            return View(model: identityAdminUrl);
        }
    }
}