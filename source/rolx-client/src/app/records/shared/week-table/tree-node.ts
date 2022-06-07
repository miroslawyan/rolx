import { Duration } from '@app/core/util/duration';
import { Activity } from '@app/projects/core/activity';
import { Record } from '@app/records/core/record';

export class TreeNode {
  parentNumber?: string;
  isExpanded = false;
  children: (TreeNode | Activity)[] = [];

  constructor(public name: string, public number: string) {}

  getRecordSum(record: Record): Duration {
    let duration = new Duration(0);
    for (const child of this.children) {
      if (child instanceof TreeNode) {
        duration = duration.add(child.getRecordSum(record));
      } else if (child instanceof Activity) {
        duration = duration.add(new Duration(record.entriesOf(child).reduce((sum, e) => sum + e.duration.seconds, 0)));
      }
    }
    return duration;
  }

  getRecordSumFormatted(record: Record): string {
    const sum = this.getRecordSum(record);
    if (sum.isZero) {
      return '';
    }

    return sum.toString();
  }
}
