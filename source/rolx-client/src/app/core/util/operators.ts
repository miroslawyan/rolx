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
