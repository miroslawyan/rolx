import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Phase } from '@app/account/core';
import { PhaseService } from '@app/account/core';
import { BehaviorSubject, combineLatest, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'rolx-phase-selector',
  templateUrl: './phase-selector.component.html',
  styleUrls: ['./phase-selector.component.scss'],
})
export class PhaseSelectorComponent implements OnInit {

  private phaseShadow: Phase;
  private allPhases$ = new BehaviorSubject<Phase[]>([]);
  private filterText$ = new BehaviorSubject<string>('');

  @Output()
  selected = new EventEmitter<Phase>();
  candidates$: Observable<Phase[]>;

  @Input()
  excluded: Phase[] = [];

  constructor(private phaseService: PhaseService) {}

  ngOnInit() {
    const excludedIds = new Set(this.excluded.map(ph => ph.id));
    this.phaseService.getAll().pipe(
      map(phs => phs.filter(ph => !excludedIds.has(ph.id))),
    ).subscribe(phs => this.allPhases$.next(phs));

    this.candidates$ = combineLatest(this.allPhases$, this.filterText$).pipe(
      map(([phases, filterText]) => this.filterByFullName(phases, filterText).slice(0, 5)),
    );
  }

  get phase() {
    return this.phaseShadow;
  }

  set phase(value: any) {
    if (!(value instanceof Phase)) {
      this.filterText$.next(value);
      value = this.allPhases$.value.find(ph => ph.fullName === value);
    }

    if (value) {
      this.phaseShadow = value;
      this.selected.emit(value);
    }
  }

  fullNameOf(phase: Phase): string {
    return phase ? phase.fullName : '';
  }

  private filterByFullName(phases: Phase[], filterText: string): Phase[] {
    filterText = filterText.toLocaleLowerCase();
    return phases.filter(ph => ph.fullName.toLocaleLowerCase().includes(filterText));
  }

}
