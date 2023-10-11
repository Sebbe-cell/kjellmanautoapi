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
                })))
                .ForMember(dest => dest.Facts, opt => opt.MapFrom(src => src.Facts.Select(e => new GetFactsDto
                {
                    Id = e.Id,
                    DisplayName = e.DisplayName
                })));

            CreateMap<AddInventoryDto, Inventory>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
            CreateMap<Users, UserLoginDto>();
            CreateMap<UserLoginDto, Users>();
            CreateMap<Equipments, GetEquipmentsDto>();
            CreateMap<Facts, GetFactsDto>();
            CreateMap<Images, GetImagesDto>();
        }
    }
}