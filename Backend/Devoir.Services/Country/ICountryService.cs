namespace Devoir.Services.Country;

public interface ICountryService
{
    Task<IEnumerable<Devoir.Models.Country>> GetAllAsync();
    Task<Devoir.Models.Country?> GetByIdAsync(int id);
    Task<Devoir.Models.Country> AddAsync(Devoir.Models.CountryInsertRequest entity);
    Task<bool> UpdateAsync(int id, Devoir.Models.CountryUpdateRequest entity);
    Task<bool> DeleteAsync(int id);
}