import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import dayjs from 'dayjs';
import { Observable } from 'rxjs';

import { Record} from './record';

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
    return this.httpClient.get<Record[]>(url);
  }
}
