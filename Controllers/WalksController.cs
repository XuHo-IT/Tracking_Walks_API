using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Model.Domain;
using NZWalk.API.Model.DTO;
using NZWalk.API.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalksRepository walksRepository;

        public WalksController(IMapper mapper, IWalksRepository walksRepository)
        {
            this.mapper = mapper;
            this.walksRepository = walksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDTO addWalksRequestDTO)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to domain model
                var walkDomainModel = mapper.Map<Walk>(addWalksRequestDTO);
                await walksRepository.CreateAsync(walkDomainModel);

                // Map domain model to DTO
                var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
                return Ok(walkDTO);
            }
            return BadRequest(ModelState);
        }

        // Get walks
        // Get api/walks?filterOn=Name&filterQuery=Tracksortby=Name&isAscending=true&pageNumber=1&pagesize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber =1 , [FromQuery] int pageSize=100)
        {
          //  try    --khi ko su dung gloabal handling exception
           // {
             //   throw new Exception("This was the error");
                var walksDomainModel = await walksRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

                // Map domain model to DTO
                var walksDTO = mapper.Map<List<WalkDTO>>(walksDomainModel);
            throw new Exception("This is a new exception");
                return Ok(walksDTO);
           // }
           // catch (Exception ex)
           // {
                //Log this exception
             //   return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            //}
        }



        // Get walk by id
        // GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walksRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map domain model to DTO
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        // Update walk by id
        // PUT: /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to domain model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);

                var updatedWalk = await walksRepository.UpdateAsync(id, walkDomainModel);
                if (updatedWalk == null)
                {
                    return NotFound();
                }

                // Map domain model to DTO
                return Ok(mapper.Map<WalkDTO>(updatedWalk));
            }
            return BadRequest(ModelState);
        }

        // Delete walk by id
        // DELETE: /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await walksRepository.DeleteAsync(id);
            if (deleteWalkDomainModel == null)
            {
                return NotFound();
            }
            // Map domain model to DTO
            return Ok(mapper.Map<WalkDTO>(deleteWalkDomainModel));
        }
    }
}
