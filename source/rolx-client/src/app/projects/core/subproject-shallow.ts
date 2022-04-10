import { assertDefined } from '@app/core/util/utils';

export class SubprojectShallow {
  id!: number;
  fullNumber!: string;
  customerName!: string;
  projectName!: number;
  name!: string;
  managerName!: string;
  isClosed!: boolean;

  validateModel(): void {
    assertDefined(this, 'id');
    assertDefined(this, 'fullNumber');
    assertDefined(this, 'customerName');
    assertDefined(this, 'projectName');
    assertDefined(this, 'name');
    assertDefined(this, 'managerName');
    assertDefined(this, 'isClosed');
  }
}
