using AutoMapper;
using Vidly.Models;
using Vidly.Models.Dtos;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Domain to Dto
            CreateMap<Customer, CustomerDto>();
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Genre, GenreDto>();

            //Dto to Domain
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}