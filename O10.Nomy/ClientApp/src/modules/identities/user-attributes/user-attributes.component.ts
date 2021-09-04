import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AttributesAccessService } from '../attributes-access.service';
import { AttributeScheme } from '../models/attribute-scheme';
import { DataSource } from '@angular/cdk/collections';
import { Observable, ReplaySubject } from 'rxjs';
import { AppStateService } from '../../../app/app-state.service';

@Component({
  selector: 'app-user-attributes',
  templateUrl: './user-attributes.component.html',
  styleUrls: ['./user-attributes.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ])
  ]
})
export class UserAttributesComponent implements OnInit {

  private userId: number
  public isLoaded = false
  public attributeSchemes: AttributeScheme[] = []
  public dataSource = new AttributeSchemesDataSource(this.attributeSchemes)

  public displayedColumns: string[] = ['schemeName', 'rootAttributeContent', 'issuerName']
  public associatedAttrsDisplayedColumns: string[] = ['alias', 'content']
  public expandedElement: AttributeScheme | null;

  constructor(
    private attributesService: AttributesAccessService,
    private appState: AppStateService,
    private router: Router,
    private route: ActivatedRoute ) { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))
    this.attributesService.getUserAttributes(this.userId).subscribe(
      r => {
        this.attributeSchemes = r

        for (var attrScheme of this.attributeSchemes) {
          var rootIdx = attrScheme.associatedSchemes.findIndex(v => v.issuerAddress === attrScheme.issuerAddress);

          if (rootIdx >= 0) {
            attrScheme.rootAssociatedScheme = attrScheme.associatedSchemes.splice(rootIdx, 1)[0];
          }
        }

        this.dataSource.setData(this.attributeSchemes)
        this.isLoaded = true
      }
    )
  }

  onBack() {
    this.router.navigate(['/user-details', this.userId])
  }
}

class AttributeSchemesDataSource extends DataSource<AttributeScheme> {
  private _dataStream = new ReplaySubject<AttributeScheme[]>();

  constructor(initialData: AttributeScheme[]) {
    super();
    this.setData(initialData);
  }

  connect(): Observable<AttributeScheme[]> {
    return this._dataStream;
  }

  disconnect() { }

  setData(data: AttributeScheme[]) {
    this._dataStream.next(data);
  }
}
