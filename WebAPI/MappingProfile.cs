using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace WebAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(p => p.ImageAsBytes, 
                opts => 
                    opts.MapFrom(p => File.ReadAllBytes(p.Image!.Path!)));
        CreateMap<PersonForCreationDto, Person>();
        CreateMap<PhoneNumberDto, PhoneNumber>().ReverseMap();
        CreateMap<RelationPersonDto, RelationPerson>().ReverseMap();
        CreateMap<PersonForUpdateDto, Person>();
    }
}