namespace kjellmanautoapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Inventory, GetInventoryDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(e => new GetImagesDto
                {
                    Id = e.Id,
                    FileName = e.FileName
                })))
                .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment.Select(e => new GetEquipmentsDto
                {
                    Id = e.Id,
                    Name = e.Name
                })));

            CreateMap<AddInventoryDto, Inventory>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<Users, UserLoginDto>();
            CreateMap<UserLoginDto, Users>();
            CreateMap<Equipments, GetEquipmentsDto>();
            CreateMap<Images, GetImagesDto>();
        }
    }
}