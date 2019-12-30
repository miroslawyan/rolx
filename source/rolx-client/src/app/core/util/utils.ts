
export function allowNull<TIn, TOut>(mapper: (value: TIn) => TOut): (TIn) => TOut | null {
  return v => v != null ? mapper(v) : null;
}
