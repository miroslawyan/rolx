import { NgZone } from '@angular/core';
import { Observable } from 'rxjs';
import { delay } from 'rxjs/operators';

import { environment } from '@env/environment';

export function enterZone(zone: NgZone) {
  return <T>(source: Observable<T>) =>
    new Observable<T>(observer =>
      source.subscribe({
        next: (x) => zone.run(() => observer.next(x)),
        error: (err) => observer.error(err),
        complete: () => observer.complete()
      })
    );
}

export function delayDebug() {
  return <T>(source: Observable<T>) => !environment.production ? source.pipe(delay(500)) : source;
}
