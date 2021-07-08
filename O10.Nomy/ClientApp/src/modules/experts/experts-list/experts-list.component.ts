import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpertsAccessService } from '../experts-access.service';
import { ExpertProfile } from '../models/expert-profile';
import { ExpertiseArea } from '../models/expertise-area';

@Component({
  selector: 'app-experts-list',
  templateUrl: './experts-list.component.html',
  styleUrls: ['./experts-list.component.css']
})
export class ExpertsListComponent implements OnInit {

  public isLoaded = false;
  public expertiseAreas: ExpertiseArea[] = null;
  public userId: number;

  constructor(private expertsAccessService: ExpertsAccessService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))

    this.expertsAccessService.getAllExpertiseAreas().subscribe(
      r => {
        this.expertiseAreas = r;
        this.isLoaded = true;
      },
      e => {
        console.error(e)
      }
    )
  }

  gotoExpert(expertProfile: ExpertProfile) {
    this.router.navigate(['expert-chat', this.userId, expertProfile.expertProfileId])
  }

}
