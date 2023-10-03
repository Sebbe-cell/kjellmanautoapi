using kjellmanautoapi.Migrations;

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
        }
    }
}