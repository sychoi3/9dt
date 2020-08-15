using _9dt.Web.Data;
using _9dt.Web.Dtos;
using _9dt.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _9dt.Web.Automapper
{
    // Automapper will scan for Profile base class and load mappings.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IList<Guid>, GetGamesResponse>()
                .ForMember(dest => dest.games, opt => opt.MapFrom(x => x));
            CreateMap<Guid, CreateGameResponse>()
                .ForMember(dest => dest.gameId, opt => opt.MapFrom(x => x));
            CreateMap<CreateGameRequest, CreateGameDto>();
            CreateMap<MoveInfo, MoveInfoDto>();
            CreateMap<IList<MoveInfoDto>, GameMovesResource>()
                .ForMember(dest => dest.moves, opt => opt.MapFrom(x => x));
        }
    }
}
