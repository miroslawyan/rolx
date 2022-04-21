import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FlagService {
  private static readonly Key = 'rolx-flag-service';
  private readonly data: { [key: string]: boolean } = FlagService.Load();

  get(key: string, defaultValue: boolean): boolean {
    const value = this.data[key];
    return value ?? defaultValue;
  }

  set(key: string, value: boolean) {
    this.data[key] = value;
    this.save();
  }

  private static Load(): { [key: string]: boolean } {
    const text = localStorage.getItem(FlagService.Key);
    return text ? JSON.parse(text) : {};
  }

  private save() {
    localStorage.setItem(FlagService.Key, JSON.stringify(this.data));
  }
}
