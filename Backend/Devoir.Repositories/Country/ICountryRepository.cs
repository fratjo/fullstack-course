namespace Devoir.Repositories.Country;

public interface ICountryRepository : IRepository<Devoir.Models.Country>
{
    Task<int> AddAsync(Devoir.Models.CountryInsertRequest entity);
    Task<bool> UpdateAsync(int id, Devoir.Models.Country entity);
}