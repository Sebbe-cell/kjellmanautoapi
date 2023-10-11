namespace kjellmanautoapi.Services.FactsService
{
    public class FactsService : IFactsService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public FactsService(IMapper mapper, DataContext dbContext)
        {
            _mapper = mapper;
            _context = dbContext;
        }
        public async Task<ServiceResponse<List<GetFactsDto>>> GetAllFacts()
        {
            var serviceResponse = new ServiceResponse<List<GetFactsDto>>();
            var dbFacts = await _context.Facts.ToListAsync();

            serviceResponse.Data = dbFacts.Select(i =>
            {
                var factsDto = _mapper.Map<GetFactsDto>(i);
                return factsDto;
            }).ToList();

            return serviceResponse;
        }
    }
}