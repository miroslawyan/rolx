import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { mapPlainToInstances } from '@app/core/util/operators';
import { Billability } from '@app/projects/core/billability';
import { environment } from '@env/environment';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BillabilityService {
  private static readonly Url = environment.apiBaseUrl + '/v1/billability';

  constructor(private readonly httpClient: HttpClient) {}

  getAll(): Observable<Billability[]> {
    return this.httpClient.get<object[]>(BillabilityService.Url).pipe(
      mapPlainToInstances(Billability),
      tap((bs) => bs.forEach((b) => b.validateModel())),
    );
  }
}
