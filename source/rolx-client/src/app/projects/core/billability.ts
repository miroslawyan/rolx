import { assertDefined } from '@app/core/util/utils';

export class Billability {
  id!: number;
  name!: string;
  isBillable!: boolean;
  sortingWeight!: number;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'name');
    assertDefined(this, 'isBillable');
    assertDefined(this, 'sortingWeight');
  }
}
