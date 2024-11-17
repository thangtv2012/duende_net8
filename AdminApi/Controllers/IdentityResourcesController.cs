﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skoruba.Duende.IdentityServer.Admin.BusinessLogic.Dtos.Configuration;
using Skoruba.Duende.IdentityServer.Admin.BusinessLogic.Services.Interfaces;
using AdminApi.Configuration.Constants;
using AdminApi.Dtos.IdentityResources;
using AdminApi.ExceptionHandling;
using AdminApi.Mappers;
using AdminApi.Resources;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json", "application/problem+json")]
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    public class IdentityResourcesController : ControllerBase
    {
        private readonly IIdentityResourceService _identityResourceService;
        private readonly IApiErrorResources _errorResources;

        public IdentityResourcesController(IIdentityResourceService identityResourceService, IApiErrorResources errorResources)
        {
            _identityResourceService = identityResourceService;
            _errorResources = errorResources;
        }

        [HttpGet]
        public async Task<ActionResult<IdentityResourcesApiDto>> Get(string searchText, int page = 1, int pageSize = 10)
        {
            var identityResourcesDto = await _identityResourceService.GetIdentityResourcesAsync(searchText, page, pageSize);
            var identityResourcesApiDto = identityResourcesDto.ToIdentityResourceApiModel<IdentityResourcesApiDto>();

            return Ok(identityResourcesApiDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityResourceApiDto>> Get(int id)
        {
            var identityResourceDto = await _identityResourceService.GetIdentityResourceAsync(id);
            var identityResourceApiModel = identityResourceDto.ToIdentityResourceApiModel<IdentityResourceApiDto>();

            return Ok(identityResourceApiModel);
        }
        
        [HttpGet(nameof(CanInsertIdentityResource))]
        public async Task<ActionResult<bool>> CanInsertIdentityResource(int id, string name)
        {
            var exists = await _identityResourceService.CanInsertIdentityResourceAsync(new IdentityResourceDto
            {
                Id = id,
                Name = name
            });

            return exists;
        }
        
        [HttpGet(nameof(CanInsertIdentityResourceProperty))]
        public async Task<ActionResult<bool>> CanInsertIdentityResourceProperty(int id, string key)
        {
            var exists = await _identityResourceService.CanInsertIdentityResourcePropertyAsync(new IdentityResourcePropertiesDto()
            {
                IdentityResourceId = id,
                Key = key
            });

            return exists;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IdentityResourceApiDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]IdentityResourceApiDto identityResourceApi)
        {
            var identityResourceDto = identityResourceApi.ToIdentityResourceApiModel<IdentityResourceDto>();

            if (!identityResourceDto.Id.Equals(default))
            {
                return BadRequest(_errorResources.CannotSetId());
            }

            var id = await _identityResourceService.AddIdentityResourceAsync(identityResourceDto);
            identityResourceApi.Id = id;

            return CreatedAtAction(nameof(Get), new { id }, identityResourceApi);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody]IdentityResourceApiDto identityResourceApi)
        {
            var identityResource = identityResourceApi.ToIdentityResourceApiModel<IdentityResourceDto>();

            await _identityResourceService.GetIdentityResourceAsync(identityResource.Id);
            await _identityResourceService.UpdateIdentityResourceAsync(identityResource);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var identityResource = new IdentityResourceDto { Id = id };

            await _identityResourceService.GetIdentityResourceAsync(identityResource.Id);
            await _identityResourceService.DeleteIdentityResourceAsync(identityResource);

            return Ok();
        }

        [HttpGet("{id}/Properties")]
        public async Task<ActionResult<IdentityResourcePropertiesApiDto>> GetProperties(int id, int page = 1, int pageSize = 10)
        {
            var identityResourcePropertiesDto = await _identityResourceService.GetIdentityResourcePropertiesAsync(id, page, pageSize);
            var identityResourcePropertiesApiDto = identityResourcePropertiesDto.ToIdentityResourceApiModel<IdentityResourcePropertiesApiDto>();

            return Ok(identityResourcePropertiesApiDto);
        }

        [HttpGet("Properties/{propertyId}")]
        public async Task<ActionResult<IdentityResourcePropertyApiDto>> GetProperty(int propertyId)
        {
            var identityResourcePropertiesDto = await _identityResourceService.GetIdentityResourcePropertyAsync(propertyId);
            var identityResourcePropertyApiDto = identityResourcePropertiesDto.ToIdentityResourceApiModel<IdentityResourcePropertyApiDto>();

            return Ok(identityResourcePropertyApiDto);
        }

        [HttpPost("{id}/Properties")]
        [ProducesResponseType(typeof(IdentityResourcePropertyApiDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostProperty(int id, [FromBody]IdentityResourcePropertyApiDto identityResourcePropertyApi)
        {
            var identityResourcePropertiesDto = identityResourcePropertyApi.ToIdentityResourceApiModel<IdentityResourcePropertiesDto>();
            identityResourcePropertiesDto.IdentityResourceId = id;

            if (!identityResourcePropertiesDto.IdentityResourcePropertyId.Equals(default))
            {
                return BadRequest(_errorResources.CannotSetId());
            }

            var propertyId = await _identityResourceService.AddIdentityResourcePropertyAsync(identityResourcePropertiesDto);
            identityResourcePropertyApi.Id = propertyId;

            return CreatedAtAction(nameof(GetProperty), new { propertyId }, identityResourcePropertyApi);
        }

        [HttpDelete("Properties/{propertyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            var identityResourceProperty = new IdentityResourcePropertiesDto { IdentityResourcePropertyId = propertyId };

            await _identityResourceService.GetIdentityResourcePropertyAsync(identityResourceProperty.IdentityResourcePropertyId);
            await _identityResourceService.DeleteIdentityResourcePropertyAsync(identityResourceProperty);

            return Ok();
        }
    }
}