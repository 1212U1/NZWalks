using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoProfileMappers:Profile
    {
        public AutoProfileMappers()
        {
            CreateMap<Region, RegionsDTO>().ReverseMap();
        }
    }
}
