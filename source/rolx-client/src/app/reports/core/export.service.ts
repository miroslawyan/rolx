import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NamedBlob } from '@app/core/util/named-blob';
import { environment } from '@env/environment';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ExportService {
  private static readonly Url = environment.apiBaseUrl + '/v1/export';

  constructor(private httpClient: HttpClient) {}

  download(monthOrBegin: moment.Moment, end?: moment.Moment): Observable<NamedBlob> {
    let params = new HttpParams();

    if (end == null) {
      params = params.append('month', monthOrBegin.format('YYYY-MM'));
    } else {
      params = params
        .append('begin', monthOrBegin.format('YYYY-MM-DD'))
        .append('end', end.clone().format('YYYY-MM-DD'));
    }

    return this.httpClient
      .get(ExportService.Url, { params, responseType: 'blob', observe: 'response' })
      .pipe(
        map((response) => {
          if (response.body == null) {
            throw new Error('No body received');
          }

          return {
            blob: response.body,
            name: ExportService.getFileName(response),
          };
        }),
      );
  }

  private static getFileName(response: HttpResponse<Blob>): string {
    const header = response.headers.get('Content-Disposition');
    if (header == null) {
      throw new Error('No content-disposition header found');
    }

    return header.split(';')[1].trim().split('=')[1].replace(/"/g, '');
  }
}
