using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomFilterAttributes;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public sealed class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dBContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(NZWalksDBContext nZWalksDBContext,IRegionRepository regionRepository,IMapper mapper)
        {
            dBContext = nZWalksDBContext;
            this.regionRepository = regionRepository;
            this.mapper=mapper;
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync() 
        {
            List<Region> regionsModel = await regionRepository.GetAllAsync();
            /*List<RegionsDTO> regionsDTO = new List<RegionsDTO>();
            foreach (Region region in regionsModel)
            {
                regionsDTO.Add(new RegionsDTO()
                {
                    RegionImageURL = region.RegionImageURL,
                    Id = region.Id,
                    Code= region.Code,
                    Name= region.Name
                });
            }*/
            List<RegionsDTO> regionsDTO = mapper.Map<List<RegionsDTO>>(regionsModel);
            return Ok(regionsDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIDAsync([FromRoute]Guid id)
        {
            Region? region = await this.regionRepository.GetByIDAsync(id);

            if(region==null)
            {
                return NotFound("Region Not found");
            }
            /*return Ok(new RegionsDTO()
            {
                RegionImageURL = region.RegionImageURL,
                Id = region.Id,
                Code = region.Code,
                Name = region.Name
            });*/
            return Ok(mapper.Map<RegionsDTO>(region));
        }
        [HttpPost(Name ="Create Region")]
        [ValidateModel]
        public async Task<IActionResult> CreateRegionAsync([FromBody] RegionsRequestDTO regionsRequestDTO)
        {
            //if(!ModelState.IsValid){ return BadRequest(ModelState); }
            Region regionDomainModel = new Region()
            {
                Name = regionsRequestDTO.Name,
                Code = regionsRequestDTO.Code,
                RegionImageURL = regionsRequestDTO.RegionImageURL
            };
            await regionRepository.CreateAsync(regionDomainModel);
            RegionsDTO regionsDTO = new RegionsDTO()
            {
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                Id = regionDomainModel.Id,
                RegionImageURL = regionDomainModel.RegionImageURL
            };
            return Ok(regionsDTO);
            //return CreatedAtAction(nameof(GetByIDAsync), new { id=regionsDTO.Id }, regionsDTO);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRegionAsync([FromBody] RegionsRequestDTO regionsRequestDTO, [FromQuery]Guid regionID)
        {
            Region? region = await this.regionRepository.UpdateAsync(regionID, regionsRequestDTO);
            if (region == null) { return NotFound("Region not found"); }
            RegionsDTO regionsDTO = new RegionsDTO()
            {
                Name = region.Name,
                Code = region.Code,
                RegionImageURL= region.RegionImageURL
            };
            return Ok(regionsDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute]Guid id)
        {
            Region? region = await this.regionRepository.DeleteAsync(id);
            if (region == null)
            {
                return NotFound("Region Not found");
            }
            return Ok(region);
        }
    }
}
