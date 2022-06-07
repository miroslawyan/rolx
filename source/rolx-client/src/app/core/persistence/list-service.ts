import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ListService {
  private static readonly Key = 'rolx-list-service';
  private readonly data: { [key: string]: any[] } = ListService.Load();

  get<T>(key: string, defaultValue: T[]): T[] {
    const value = this.data[key];
    return value ?? defaultValue;
  }

  set<T>(key: string, value: T[]) {
    this.data[key] = value;
    this.save();
  }

  private static Load(): { [key: string]: any[] } {
    const text = localStorage.getItem(ListService.Key);
    return text ? JSON.parse(text) : {};
  }

  private save() {
    localStorage.setItem(ListService.Key, JSON.stringify(this.data));
  }
}
