import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { AuthService } from '@app/auth/core/auth.service';
import { InstallationIdService } from '@app/auth/core/installation-id.service';
import { PendingRequestService } from '@app/core/pending-request/pending-request.service';
import { Observable, Subscription } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'rolx-main',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent {
  private readonly subscriptions = new Subscription();
  private overlayRef: OverlayRef | null = null;

  @ViewChild('requestPendingSpinner') requestPendingSpinner?: TemplateRef<any>;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset).pipe(
    map((result) => result.matches),
    shareReplay(),
  );

  constructor(
    public readonly authService: AuthService,
    private readonly breakpointObserver: BreakpointObserver,
    private readonly pendingRequestService: PendingRequestService,
    private readonly viewContainerRef: ViewContainerRef,
    private readonly overlay: Overlay,
    public readonly installationIdService: InstallationIdService,
  ) {
    this.subscriptions.add(
      this.pendingRequestService.hasOverdueRequest$.subscribe((v) =>
        v ? this.showOverlay() : this.hideOverlay(),
      ),
    );
  }

  private showOverlay() {
    if (this.overlayRef != null) {
      return;
    }

    if (!this.requestPendingSpinner) {
      console.warn('requestPendingSpinner is undefined');
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
