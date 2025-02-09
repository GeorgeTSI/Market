using MarketAPI.Entities;

namespace MarketAPI.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllCompaniesAsync();
    }

}
