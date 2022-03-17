import { assertDefined } from '@app/core/util/utils';
import { WorkItem } from '@app/reports/core/work-item';
import { Type } from 'class-transformer';

export class WorkItemGroup {
  name!: string;

  @Type(() => WorkItem)
  items!: WorkItem[];

  validateModel(): void {
    assertDefined(this, 'name');
    assertDefined(this, 'items');

    this.items.forEach((item) => item.validateModel());
  }
}
