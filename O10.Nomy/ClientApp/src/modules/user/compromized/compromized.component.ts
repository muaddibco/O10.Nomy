import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppStateService } from '../../../app/app-state.service';

@Component({
  selector: 'app-compromized',
  templateUrl: './compromized.component.html',
  styleUrls: ['./compromized.component.css']
})
export class CompromizedComponent implements OnInit {

  constructor(
    private appState: AppStateService,
    private router: Router,
    private route: ActivatedRoute ) { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    var userId = Number(this.route.snapshot.paramMap.get('userId'))
  }

}
