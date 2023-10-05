namespace kjellmanautoapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Inventory, GetInventoryDto>();
            CreateMap<AddInventoryDto, Inventory>();
            CreateMap<Users, UserLoginDto>();
            CreateMap<UserLoginDto, Users>();
            CreateMap<Equipments, GetEquipmentsDto>();
            CreateMap<Inventory, GetInventoryDto>()
    .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment.Select(e => new GetEquipmentsDto
    {
        Id = e.Id,
        Name = e.Name
    })));
        }
    }
}