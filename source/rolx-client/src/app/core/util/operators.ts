import { NgZone } from '@angular/core';
import { environment } from '@env/environment';
import { plainToClass } from 'class-transformer';
import { ClassType } from 'class-transformer/ClassTransformer';
import { ClassTransformOptions } from 'class-transformer/ClassTransformOptions';
import { Observable } from 'rxjs';
import { delay, map } from 'rxjs/operators';

export function enterZone(zone: NgZone) {
  return <T>(source: Observable<T>) =>
    new Observable<T>(observer =>
      source.subscribe({
        next: (x) => zone.run(() => observer.next(x)),
        error: (err) => observer.error(err),
        complete: () => observer.complete(),
      }),
    );
}

export function delayDebug(delayMillis: number) {
  return <T>(source: Observable<T>) => !environment.production ? source.pipe(delay(delayMillis)) : source;
}

export function mapPlainToClassArray<T, R>(cls: ClassType<R>, options?: ClassTransformOptions): (s: Observable<T[]>) => Observable<R[]> {
  return (source: Observable<T[]>) => source.pipe(map(plain => plainToClass(cls, plain, options)));
}

export function mapPlainToClass<T, R>(cls: ClassType<R>, options?: ClassTransformOptions): (s: Observable<T>) => Observable<R> {
  return (source: Observable<T>) => source.pipe(map(plain => plainToClass(cls, plain, options)));
}
