import { Injectable } from '@angular/core';
import { MatSortable, Sort } from '@angular/material/sort';

export type Resource = 'Activity' | 'Subproject' | 'User';

@Injectable({
  providedIn: 'root',
})
export class SortService {
  private static readonly Key = 'rolx-sort-service';

  private readonly data: { [resource: string]: Sort } = SortService.Load();

  set(resource: Resource, sort: Sort) {
    this.data[resource] = sort;
    this.save();
  }

  get(resource: Resource, defaultSort: MatSortable): MatSortable {
    const sort = this.data[resource];
    if (sort != null) {
      return {
        id: sort.active,
        start: sort.direction || defaultSort.start,
        disableClear: defaultSort.disableClear,
      };
    }

    return defaultSort;
  }

  private static Load(): { [resource: string]: Sort } {
    const text = localStorage.getItem(SortService.Key);
    return text ? JSON.parse(text) : {};
  }

  private save() {
    localStorage.setItem(SortService.Key, JSON.stringify(this.data));
  }
}
