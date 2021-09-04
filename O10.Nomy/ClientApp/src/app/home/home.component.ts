import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppStateService } from '../app-state.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  constructor(private router: Router, private appState: AppStateService) {
    this.appState.StateChanged.subscribe(s => {
      if (s.isMobile) {
        this.router.navigate(['/user-entry'])
      }
    })
  }

  ngOnInit() {
    var ua = navigator.userAgent;
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini|Mobile|mobile|CriOS/i.test(ua)) {
      this.router.navigate(['/user-entry'])
    }
  }
}
