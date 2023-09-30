namespace kjellmanautoapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Inventory, GetInventoryDto>();
            CreateMap<AddInventoryDto, Inventory>();
        }
    }
}