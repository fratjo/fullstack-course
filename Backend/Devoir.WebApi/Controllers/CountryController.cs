using Devoir.CustomException;
using Devoir.Models;
using Devoir.Services.Country;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable All

namespace Devoir.WebApi.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountryController(ICountryService countryService, IEnumerable<IExceptionHandler> exceptionHandlers) : ControllerBase
    {
        #region Gets
        
        // GET: api/countries
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            return await this.HandleRequestAsync(async () => 
            {
                var countries = await countryService.GetAllAsync();
                return Ok(countries);
            }, exceptionHandlers);
        }
        
        // GET: api/countries/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return await this.HandleRequestAsync(async () => 
            {
                var country = await countryService.GetByIdAsync(id);
                return Ok(country);
            }, exceptionHandlers);
        }
        
        #endregion

        #region Posts

        // POST: api/countries
        [HttpPost("")]
        public async Task<IActionResult> AddAsync([FromBody]CountryInsertRequest country)
        {
            return await this.HandleRequestAsync(async () => 
            {
                var c = await countryService.AddAsync(country);
                return Created(c.Id.ToString(), c);
            }, exceptionHandlers);
        }
        
        #endregion
        
        #region Puts
        
        // PUT: api/countries/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]CountryUpdateRequest country)
        {
            return await this.HandleRequestAsync(async () =>
            {
                await countryService.UpdateAsync(id, country);
                return NoContent();
            }, exceptionHandlers);
        }
        
        #endregion
        
        #region Deletes
        
        // DELETE: api/countries/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await this.HandleRequestAsync(async () => 
            {
                await countryService.DeleteAsync(id);
                return NoContent();
            }, exceptionHandlers);
        }
        
        #endregion
    }
}
