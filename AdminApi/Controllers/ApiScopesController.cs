﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminApi.Dtos.ApiScopes;
using AdminApi.Mappers;
using AdminApplication.Constants;
using DataAccess.Interfaces;
using AdminApplication.Dtos.Configuration;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json", "application/problem+json")]
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    public class ApiScopesController : ControllerBase
    {
        //private readonly IApiErrorResources _errorResources;
        private readonly IApiScopeService _apiScopeService;

        public ApiScopesController(
            //IApiErrorResources errorResources, 
            IApiScopeService apiScopeService)
        {
            //_errorResources = errorResources;
            _apiScopeService = apiScopeService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiScopesApiDto>> GetScopes(string search, int page = 1, int pageSize = 10)
        {
            var apiScopesDto = await _apiScopeService.GetApiScopesAsync(search, page, pageSize);
            var apiScopesApiDto = apiScopesDto.ToApiScopeApiModel<ApiScopesApiDto>();

            return Ok(apiScopesApiDto);
        }
        
        [HttpGet(nameof(CanInsertApiScope))]
        public async Task<ActionResult<bool>> CanInsertApiScope(int id, string name)
        {
            var exists = await _apiScopeService.CanInsertApiScopeAsync(new ApiScopeDto()
            {
                Id = id,
                Name = name
            });

            return exists;
        }
        
        [HttpGet(nameof(CanInsertApiScopeProperty))]
        public async Task<ActionResult<bool>> CanInsertApiScopeProperty(int id, string key)
        {
            var exists = await _apiScopeService.CanInsertApiScopePropertyAsync(new ApiScopePropertiesDto()
            {
                ApiScopeId = id,
                Key = key
            });

            return exists;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiScopeApiDto>> GetScope(int id)
        {
            var apiScopesDto = await _apiScopeService.GetApiScopeAsync(id);
            var apiScopeApiDto = apiScopesDto.ToApiScopeApiModel<ApiScopeApiDto>();

            return Ok(apiScopeApiDto);
        }

        [HttpGet("{id}/Properties")]
        public async Task<ActionResult<ApiScopePropertiesApiDto>> GetScopeProperties(int id, int page = 1, int pageSize = 10)
        {
            var apiScopePropertiesDto = await _apiScopeService.GetApiScopePropertiesAsync(id, page, pageSize);
            var apiScopePropertiesApiDto = apiScopePropertiesDto.ToApiScopeApiModel<ApiScopePropertiesApiDto>();

            return Ok(apiScopePropertiesApiDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiScopeDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostScope([FromBody]ApiScopeApiDto apiScopeApi)
        {
            var apiScope = apiScopeApi.ToApiScopeApiModel<ApiScopeDto>();
            
            if (!apiScope.Id.Equals(default))
            {
                //return BadRequest(_errorResources.CannotSetId());
                return BadRequest("CannotSetId");
            }

            var apiScopeId = await _apiScopeService.AddApiScopeAsync(apiScope);
            apiScope.Id = apiScopeId;

            return CreatedAtAction(nameof(GetScope), new {id = apiScopeId}, apiScope);
        }

        [HttpPost("{id}/Properties")]
        [ProducesResponseType(typeof(ApiScopePropertyApiDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostProperty(int id, [FromBody]ApiScopePropertyApiDto apiScopePropertyApi)
        {
            var apiResourcePropertiesDto = apiScopePropertyApi.ToApiScopeApiModel<ApiScopePropertiesDto>();
            apiResourcePropertiesDto.ApiScopeId = id;

            if (!apiResourcePropertiesDto.ApiScopePropertyId.Equals(default))
            {
                //return BadRequest(_errorResources.CannotSetId());
                return BadRequest("CannotSetId");
            }

            var propertyId = await _apiScopeService.AddApiScopePropertyAsync(apiResourcePropertiesDto);
            apiScopePropertyApi.Id = propertyId;

            return CreatedAtAction(nameof(GetProperty), new { propertyId }, apiScopePropertyApi);
        }

        [HttpGet("Properties/{propertyId}")]
        public async Task<ActionResult<ApiScopePropertyApiDto>> GetProperty(int propertyId)
        {
            var apiScopePropertyAsync = await _apiScopeService.GetApiScopePropertyAsync(propertyId);
            var resourcePropertyApiDto = apiScopePropertyAsync.ToApiScopeApiModel<ApiScopePropertyApiDto>();

            return Ok(resourcePropertyApiDto);
        }

        [HttpDelete("Properties/{propertyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            var apiScopePropertiesDto = new ApiScopePropertiesDto { ApiScopePropertyId = propertyId };

            await _apiScopeService.GetApiScopePropertyAsync(apiScopePropertiesDto.ApiScopePropertyId);
            await _apiScopeService.DeleteApiScopePropertyAsync(apiScopePropertiesDto);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutScope([FromBody]ApiScopeApiDto apiScopeApi)
        {
            var apiScope = apiScopeApi.ToApiScopeApiModel<ApiScopeDto>();
            
            await _apiScopeService.GetApiScopeAsync(apiScope.Id);

            await _apiScopeService.UpdateApiScopeAsync(apiScope);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteScope(int id)
        {
            var apiScope = new ApiScopeDto { Id = id };

            await _apiScopeService.GetApiScopeAsync(apiScope.Id);

            await _apiScopeService.DeleteApiScopeAsync(apiScope);

            return Ok();
        }
    }
}