using MarketAPI.Data;
using Microsoft.EntityFrameworkCore;
using MarketAPI.Entities;

namespace MarketAPI.Repositories
{
    public class CompanyRepository(AppDbContext _context) : ICompanyRepository
    {
        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }
    }
}
