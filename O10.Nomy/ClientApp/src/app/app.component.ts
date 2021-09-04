import { ChangeDetectorRef, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AppStateService } from './app-state.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit {
  title = 'app';
  public isMobile = false;
  public showNavMenu = false;

  constructor(private router: Router, private appState: AppStateService, private changeDetector: ChangeDetectorRef) {
    this.appState.StateChanged.subscribe(s => {
      this.isMobile = s.isMobile
      this.showNavMenu = !s.isMobile
      this.changeDetector.detectChanges()
    })
  }

  ngOnInit() {
    var ua = navigator.userAgent;
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini|Mobile|mobile|CriOS/i.test(ua)) {
      this.showNavMenu = false;
      this.isMobile = true;
    }
    else {
      this.showNavMenu = true;
      this.isMobile = false;
    }
  }

  onIsMobileChangedHandler(isMobile: boolean) {
    this.isMobile = isMobile
    this.showNavMenu = !isMobile;

    if (isMobile) {
      this.appState.setIsMobile(isMobile)
    }
  }
}
