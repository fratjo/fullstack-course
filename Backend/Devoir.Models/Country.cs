using System.Diagnostics.CodeAnalysis;

namespace Devoir.Models;

public sealed class Country
{
    public int Id { get; init; }
    public string? Name { get; set; } = string.Empty!;
    public string? Short { get; set; } = string.Empty!;
    
    public void Update(CountryUpdateRequest country)
    {
        Name = !string.IsNullOrWhiteSpace(country.Name) ? country.Name : Name;
        Short = !string.IsNullOrWhiteSpace(country.Short) ? country.Short : Short;
    }
    
    public static Country FromInsertRequest(int id, CountryInsertRequest country)
    {
        return new Country()
        {
            Id = id,
            Name = country.Name,
            Short = country.Short
        };
    }
}

public sealed class CountryInsertRequest
{
    public string Name { get; set; } = string.Empty!;
    public string Short { get; set; } = string.Empty!;
}

public sealed class CountryUpdateRequest
{
    public string? Name { get; set; } = null;
    public string? Short { get; set; } = null;
}