using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NZWalk.API.Model.Domain;
using NZWalk.API.Model.DTO;
using NZWalk.API.Repositories;
using NZWalk.API.Respositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
      //  [Authorize]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll Action method was invoked");

            // Get Data From Database - Domain.models
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTO
            var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain);

            // Return DTO
            logger.LogInformation($"Finished get all request with data: {JsonConvert.SerializeObject(regionsDTO)}");
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            logger.LogInformation($"GetById Action method was invoked with ID: {id}");

            // GET Region domain model from database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region domain model to region DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomain);

            // Return DTO back to client
            return Ok(regionDTO);
        }

        [HttpPost]
      //  [Authorize]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            logger.LogInformation("Create Action method was invoked");

            // Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model back to DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            logger.LogInformation($"Update Action method was invoked with ID: {id}");

            if (ModelState.IsValid)
            {
                // Map DTO to domain model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                // Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Convert Domain Model to DTO
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
                return Ok(regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            logger.LogInformation($"Delete Action method was invoked with ID: {id}");

            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return deleted Region back
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }
    }
}
