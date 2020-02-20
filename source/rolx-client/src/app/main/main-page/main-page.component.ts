import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { AuthService, Role } from '@app/auth/core';
import { PendingRequestService } from '@app/core/pending-request';
import { Observable, Subscription } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'rolx-main',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent implements OnInit {

  private readonly subscriptions = new Subscription();
  private overlayRef: OverlayRef | null = null;

  @ViewChild('requestPendingSpinner') requestPendingSpinner: TemplateRef<any>;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay(),
    );

  readonly Role = Role;

  constructor(public authService: AuthService,
              private breakpointObserver: BreakpointObserver,
              private pendingRequestService: PendingRequestService,
              private viewContainerRef: ViewContainerRef,
              private overlay: Overlay) {}

  ngOnInit() {
    this.subscriptions.add(
      this.pendingRequestService.hasOverdueRequest$
        .subscribe(v => v ? this.showOverlay() : this.hideOverlay()));
  }

  private showOverlay() {
    if (this.overlayRef != null) {
      return;
    }

    const overlayConfig = new OverlayConfig({
      hasBackdrop: true,
      scrollStrategy: this.overlay.scrollStrategies.block(),
      positionStrategy: this.overlay.position().global().centerHorizontally().centerVertically(),
    });
    const overlayRef = this.overlay.create(overlayConfig);
    const loadingPortal = new TemplatePortal(this.requestPendingSpinner, this.viewContainerRef);
    overlayRef.attach(loadingPortal);
    this.overlayRef = overlayRef;
  }

  private hideOverlay() {
    this.overlayRef?.dispose();
    this.overlayRef = null;
  }

}
