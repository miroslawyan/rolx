import { Component, ElementRef, Input, OnDestroy, OnInit } from '@angular/core';
import { ErrorService } from '@app/core/error/error.service';
import { ListService } from '@app/core/persistence/list-service';
import { assertDefined } from '@app/core/util/utils';
import { Activity } from '@app/projects/core/activity';
import { FavouriteActivityService } from '@app/projects/core/favourite-activity.service';
import { Record } from '@app/records/core/record';
import { WorkRecordService } from '@app/records/core/work-record.service';
import { User } from '@app/users/core/user';
import { Subscription } from 'rxjs';

import { TreeNode } from './tree-node';

@Component({
  selector: 'rolx-week-table',
  templateUrl: './week-table.component.html',
  styleUrls: ['./week-table.component.scss'],
})
export class WeekTableComponent implements OnInit, OnDestroy {
  private readonly expandedNodes = new Set<string>(this.listService.get<string>('expanded-nodes', []));
  private readonly allWeekdays = [
    'monday',
    'tuesday',
    'wednesday',
    'thursday',
    'friday',
    'saturday',
    'sunday',
  ];

  private _showWeekends = false;
  private _asTreeView = false;
  private inputActivities: Activity[] = [];
  private favouriteActivities: Activity[] = [];
  private homegrownActivities: Activity[] = [];
  private subscriptions = new Subscription();

  weekdays: string[] = [];
  displayedColumns: string[] = [];

  @Input()
  records: Record[] = [];

  @Input()
  user!: User;

  @Input()
  isCurrentUser = false;

  @Input()
  get showWeekends() {
    return this._showWeekends;
  }
  set showWeekends(value: boolean) {
    this._showWeekends = value;

    this.weekdays = this.showWeekends
      ? this.allWeekdays
      : this.allWeekdays.slice(0, this.allWeekdays.length - 2);
    this.displayedColumns = ['activity', ...this.weekdays];
  }
  @Input()
  get asTreeView() {
    return this._asTreeView;
  }
  set asTreeView(value: boolean) {
    this._asTreeView = value;
    this.update();
  }

  allActivities: Activity[] = [];
  items: (TreeNode | Activity | null)[] = [];
  tree: TreeNode[] = [];

  isAddingActivity = false;

  constructor(
    private favouriteActivityService: FavouriteActivityService,
    private workRecordService: WorkRecordService,
    private errorService: ErrorService,
    private elementRef: ElementRef,
    private listService: ListService,
  ) {
    this.showWeekends = false;
  }

  @Input()
  get activities(): Activity[] {
    return this.inputActivities;
  }
  set activities(value: Activity[]) {
    this.inputActivities = value.filter((activity) =>
      this.records.some((record) => activity.isOpenAt(record.date)),
    );
    this.homegrownActivities = [];
    this.isAddingActivity = false;

    this.update();
  }

  isActivity(item: TreeNode | Activity | null): boolean {
    return item instanceof Activity;
  }

  isTreeNode(item: TreeNode | Activity | null): boolean {
    return item instanceof TreeNode;
  }

  ngOnInit() {
    assertDefined(this, 'user');

    if (this.isCurrentUser) {
      this.subscriptions.add(
        this.favouriteActivityService.favourites$.subscribe((phs) => (this.favourites = phs)),
      );
    }
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  startAdding() {
    this.isAddingActivity = true;
    this.update();
    setTimeout(() => this.scrollToBottom());
  }

  addHomegrown(activity: Activity) {
    this.isAddingActivity = false;
    this.homegrownActivities.push(activity);
    this.update();
  }

  trackByNode(index: number, node: TreeNode | Activity | null): number {
    if (node instanceof TreeNode) {
      return +(((node.parentNumber ?? '') + node.number).replace('#', ''));
    } else if (node instanceof Activity) {
      return node.id;
    }
    return -1;
  }

  toggleExpand(node: TreeNode) {
    node.isExpanded = !node.isExpanded;
    if (node.isExpanded) {
      this.expandedNodes.add((node.parentNumber ?? '') + node.number);
    } else {
      this.expandedNodes.delete((node.parentNumber ?? '') + node.number);
    }
    this.update();
  }

  submit(record: Record, index: number) {
    this.workRecordService.update(this.user.id, record).subscribe({
      next: (r) => (this.records[index] = r),
      error: (err) => {
        console.error(err);
        this.errorService.notifyGeneralError();
      },
    });
  }

  private set favourites(value: Activity[]) {
    this.favouriteActivities = value;
    this.update();
  }

  private createTree(value: Activity[]): TreeNode[] {
    const baseNodes: TreeNode[] = [];
    for (const activity of value) {
      const parsedNumbers = activity.fullNumber.split('.');
      let baseNode = baseNodes.find(n => n.number === parsedNumbers[0]);
      if (baseNode == null) {
        baseNode = new TreeNode(`${activity.customerName} - ${activity.projectName}`, parsedNumbers[0]);
        baseNode.isExpanded = this.expandedNodes.has(baseNode.number);
        baseNodes.push(baseNode);
      }

      let subNode = baseNode.children.filter(c => c instanceof TreeNode).find(c => c.number === parsedNumbers[1]) as TreeNode | undefined;
      if (subNode == null) {
        subNode = new TreeNode(activity.subprojectName, parsedNumbers[1]);
        subNode.parentNumber = baseNode.number;
        subNode.isExpanded = this.expandedNodes.has(baseNode.number + subNode.number);
        baseNode.children.push(subNode);
      }

      subNode.children.push(activity);
    }
    return baseNodes;
  }

  private *flattenTree(nodes: (TreeNode | Activity)[]): IterableIterator<TreeNode | Activity> {
    for (const node of nodes) {
      yield node;
      if (node instanceof TreeNode && node.isExpanded) {
        for (const child of this.flattenTree(node.children)) {
          yield child;
        }
      }
    }
  }

  private update() {
    const localActivitiesIds = new Set<number>(
      [...this.inputActivities, ...this.homegrownActivities].map((ph) => ph.id),
    );

    const nonLocalFavourites = this.favouriteActivities.filter(
      (ph) => !localActivitiesIds.has(ph.id),
    );

    const sortedActivities = [...this.inputActivities, ...nonLocalFavourites].sort((a, b) =>
      a.fullName.localeCompare(b.fullName),
    );

    this.allActivities = [...sortedActivities, ...this.homegrownActivities];

    if (this.asTreeView) {
      this.tree = this.createTree(this.allActivities);
      const tempItems = Array.from(this.flattenTree(this.tree));
      this.items = this.isAddingActivity ? [...tempItems, null] : tempItems;
      this.listService.set<string>('expanded-nodes', Array.from(this.expandedNodes.keys()));
    } else {
      this.items = this.isAddingActivity ? [...this.allActivities, null] : this.allActivities;
    }
  }

  private scrollToBottom() {
    try {
      this.elementRef.nativeElement.scroll({
        top: this.elementRef.nativeElement.scrollHeight,
        behavior: 'instant',
      });
    } catch (err) {}
  }
}
