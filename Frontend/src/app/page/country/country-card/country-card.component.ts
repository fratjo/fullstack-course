import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { Country } from '../../../models/country.interface';

@Component({
  selector: 'app-country-card',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './country-card.component.html',
  styleUrl: './country-card.component.css',
})
export class CountryCardComponent {
  @Input() country?: Country | null = null;
  @Output() save = new EventEmitter<Country>();
  @Output() delete = new EventEmitter<number>();

  editName: string = '';
  editShort: string = '';
  editing: boolean = false;

  saveEdit(): void {
    // check if any changes were made
    if (
      this.editName === this.country?.name &&
      this.editShort === this.country?.short
    ) {
      this.editing = false;
      return;
    }

    // Save the edit
    if (this.country) {
      const updatedCountry: Country = {
        ...this.country,
        name: this.editName,
        short: this.editShort,
      };
      this.save.emit(updatedCountry);
    }
    this.editing = false;
  }

  cancelEdit(): void {
    this.editing = false;
  }

  editCountry(): void {
    this.editName = this.country?.name ?? '';
    this.editShort = this.country?.short ?? '';
    this.editing = !this.editing;
  }

  deleteCountry(): void {
    // Delete the country
    if (this.country) {
      this.delete.emit(this.country?.id);
    }
  }
}
