import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import dayjs from 'dayjs';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Record, RecordData } from './record';

const WorkRecordUrl = environment.apiBaseUrl + '/v1/workrecord';

@Injectable({
  providedIn: 'root'
})
export class WorkRecordService {

  constructor(private httpClient: HttpClient) {
    console.log('--- WorkRecordService.ctor()');
  }

  getMonth(month: dayjs.Dayjs): Observable<Record[]> {
    const url = WorkRecordUrl + '/month/' + month.format('YYYY-MM');
    return this.httpClient.get<RecordData[]>(url).pipe(
      map(ds => ds.map(d => new Record(d)))
    );
  }
}
