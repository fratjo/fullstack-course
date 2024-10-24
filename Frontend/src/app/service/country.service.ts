import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Country } from '../models/country.interface';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CountryService {
  private readonly http: HttpClient = inject(HttpClient);

  public countries$: BehaviorSubject<Country[]> = new BehaviorSubject<
    Country[]
  >([]);

  fetchCountries(): void {
    this.http
      .get<Country[] | []>('http://localhost:5256/api/countries')
      .pipe(
        tap((countries: Country[]) => {
          this.countries$.next(countries);
        })
      )
      .subscribe();
  }

  addCountry(country: Country): void {
    // Add the country
    this.http
      .post<Country>('http://localhost:5256/api/countries', {
        name: country.name,
        short: country.short,
      })
      .pipe(
        tap((newCountry) => {
          this.countries$.next([...this.countries$.value, newCountry]);
        })
      )
      .subscribe();
  }

  saveCountry(country: Country): void {
    // Save the country
    this.http
      .put(`http://localhost:5256/api/countries/${country.id}`, {
        name: country.name,
        short: country.short,
      })
      .pipe(
        tap(() => {
          this.countries$.next(
            this.countries$.value.map((c: Country) => {
              if (c.id === country.id) {
                // Find the country by id
                return country; // Update the country with the new values
              } else {
                return c; // Return the country as is
              }
            })
          );
        })
      )
      .subscribe();
  }

  deleteCountry(id: number): void {
    // Delete the country
    this.http
      .delete(`http://localhost:5256/api/countries/${id}`)
      .pipe(
        tap(() => {
          this.countries$.next(
            this.countries$.value.filter((c: Country) => c.id !== id)
          );
        })
      )
      .subscribe();
  }
}
