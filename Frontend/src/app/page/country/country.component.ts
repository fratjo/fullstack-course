import { Component, inject, OnInit } from '@angular/core';
import { CountryService } from '../../service/country.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Country } from '../../models/country.interface';
import { Observable } from 'rxjs';
import { CountryCardComponent } from './country-card/country-card.component';

@Component({
  selector: 'app-country',
  standalone: true,
  imports: [CommonModule, AsyncPipe, CountryCardComponent, FormsModule],
  templateUrl: './country.component.html',
  styleUrl: './country.component.css',
})
export class CountryComponent implements OnInit {
  private readonly countryService: CountryService = inject(CountryService);
  countries$: Observable<Country[]> = this.countryService.countries$;
  newName: string = '';
  newShort: string = '';

  ngOnInit(): void {
    this.countryService.fetchCountries();
  }

  addCountry(): void {
    // check empty fields
    if (
      !this.newName ||
      !this.newShort ||
      !this.newName.trim() ||
      !this.newShort.trim() ||
      this.newShort.trim().length !== 3
    ) {
      alert('Please correctly fill in all fields before adding a country.');
      return;
    }

    // Add the country
    const newCountry: Country = {
      name: this.capitaliseFirstLetter(this.newName.trim().toLowerCase()),
      short: this.newShort.trim().toUpperCase(),
    };
    this.countryService.addCountry(newCountry);
    this.newName = '';
    this.newShort = '';
  }

  saveCountry(country: Country): void {
    // Save the country
    this.countryService.saveCountry(country);
  }

  deleteCountry(id: number): void {
    // Delete the country
    this.countryService.deleteCountry(id);
  }

  capitaliseFirstLetter(word: string): string {
    return word.charAt(0).toUpperCase() + word.slice(1);
  }
}
