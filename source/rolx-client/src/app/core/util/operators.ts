import { ClassTransformOptions, plainToInstance } from 'class-transformer';
import { ClassConstructor } from 'class-transformer/types/interfaces';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export const mapPlainToInstances =
  <T, R>(
    cls: ClassConstructor<R>,
    options?: ClassTransformOptions,
  ): ((s: Observable<T[]>) => Observable<R[]>) =>
  (source: Observable<T[]>) =>
    source.pipe(map((plain) => plainToInstance(cls, plain, options)));

export const mapPlainToInstance =
  <T, R>(
    cls: ClassConstructor<R>,
    options?: ClassTransformOptions,
  ): ((s: Observable<T>) => Observable<R>) =>
  (source: Observable<T>) =>
    source.pipe(map((plain) => plainToInstance(cls, plain, options)));

export const rxDebug =
  <T>(desc: string) =>
  (observable: Observable<T>) =>
    new Observable<T>((subscriber) => {
      console.log('RxJS subscribed: ' + desc);
      const subscription = observable.subscribe({
        next: (value) => {
          console.log('RxJS next: ' + desc, value);
          subscriber.next(value);
        },
        error: (err) => {
          console.log('RxJS error: ' + desc, err);
          subscriber.error(err);
        },
        complete: () => {
          console.log('RxJS complete: ' + desc);
          subscriber.complete();
        },
      });

      // Return the teardown logic. This will be invoked when
      // the result errors, completes, or is unsubscribed.
      return () => {
        console.log('RxJS unsubscribe: ' + desc);
        subscription.unsubscribe();
      };
    });
