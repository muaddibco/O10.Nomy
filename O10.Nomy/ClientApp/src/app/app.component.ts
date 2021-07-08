import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';

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

  constructor(private router: Router) {

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
}
