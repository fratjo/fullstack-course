using Devoir.CustomException;
using Devoir.Repositories.Country;

namespace Devoir.Services.Country;

public class CountryService(ICountryRepository countryRepository) : ICountryService
{
    #region Gets
    public async Task<IEnumerable<Devoir.Models.Country>> GetAllAsync()
    {
        try
        {
            var countries = await countryRepository.GetAllAsync();
            return countries;

        }
        catch (BaseApplicationException e)
        {   
            Console.WriteLine($"Service layer caught an exception: {e.Message}", e);
            throw;
        }
    }

    public async Task<Devoir.Models.Country?> GetByIdAsync(int id)
    {
        try
        {
            var country = await countryRepository.GetByIdAsync(id);
            return country ?? throw new NotFoundException($"Country with id {id} not found.");
        }
        catch (BaseApplicationException e)
        {
            Console.WriteLine($"Service layer caught an exception: {e.Message}", e);
            throw;
        }
    }

    #endregion
    
    #region Posts
    
    public async Task<Devoir.Models.Country> AddAsync(Devoir.Models.CountryInsertRequest? country)
    {
        
        try
        {
            var missingFields = new List<string>();
            if (country is null) missingFields.Add("Country object is null");
            if (string.IsNullOrEmpty(country?.Name)) missingFields.Add("Name is missing");
            if (string.IsNullOrEmpty(country?.Short)) missingFields.Add("Short is missing");

            if (missingFields.Any()) throw new ValidationException($"Validation failed: {string.Join(", ", missingFields)}");
            
            var id = await countryRepository.AddAsync(country!);
            return Devoir.Models.Country.FromInsertRequest(id, country!);
        }
        catch (BaseApplicationException e)
        {
            Console.WriteLine($"Service layer caught an exception: {e.Message}", e);
            throw;
        }
    }

    #endregion
    
    #region Puts
    public async Task<bool> UpdateAsync(int id, Devoir.Models.CountryUpdateRequest country)
    {
        try
        {
            // get object
            var c = await countryRepository.GetByIdAsync(id);

            // check if object exists
            // throw exception if not
            if (c is null) throw new NotFoundException($"Country with id {id} not found.");

            // update object
            c.Update(country);

            // save object & return
            return await countryRepository.UpdateAsync(id, c);
        }
        catch (BaseApplicationException e)
        {
            Console.WriteLine($"Service layer caught an exception: {e.Message}", e);
            throw;
        }
    }

    #endregion
    
    #region Deletes
    
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var isMoreThanOneRowDeleted = await countryRepository.DeleteAsync(id);
            
            if (!isMoreThanOneRowDeleted) throw new NotFoundException($"Country with id {id} not found.");
            
            return isMoreThanOneRowDeleted;
        }
        catch (BaseApplicationException e)
        {
            Console.WriteLine($"Service layer caught an exception: {e.Message}", e);
            throw;
        }
    }
    
    #endregion
}