namespace kjellmanautoapi.Services.FactsService
{
    public interface IFactsService
    {
        Task<ServiceResponse<List<GetFactsDto>>> GetAllFacts();
    }
}