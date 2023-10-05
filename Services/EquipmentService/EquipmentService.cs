namespace kjellmanautoapi.Services.EquipmentService
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EquipmentService(IMapper mapper, DataContext dbContext)
        {
            _mapper = mapper;
            _context = dbContext;
        }
        public async Task<ServiceResponse<List<GetEquipmentsDto>>> GetAllEquipments()
        {
            var serviceResponse = new ServiceResponse<List<GetEquipmentsDto>>();
            var dbEquipments = await _context.Equipments.ToListAsync();

            serviceResponse.Data = dbEquipments.Select(i =>
            {
                var equipmentsDto = _mapper.Map<GetEquipmentsDto>(i);
                return equipmentsDto;
            }).ToList();

            return serviceResponse;
        }
    }
}