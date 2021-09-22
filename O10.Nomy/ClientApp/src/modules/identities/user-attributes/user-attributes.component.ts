import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AttributesAccessService } from '../attributes-access.service';
import { AttributeScheme } from '../models/attribute-scheme';
import { DataSource } from '@angular/cdk/collections';
import { Observable, ReplaySubject } from 'rxjs';
import { AppStateService } from '../../../app/app-state.service';
import { AttributeState } from '../models/attribute-state';
import { MatDialog } from '@angular/material/dialog';
import { PasswordConfirmDialog } from '../../password-confirm/password-confirm/password-confirm.dialog';

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

  public displayedColumns: string[] = ['state', 'schemeName', 'rootAttributeContent', 'issuerName']
  public associatedAttrsDisplayedColumns: string[] = ['alias', 'content']
  public expandedElement: AttributeScheme | null;

  constructor(
    private attributesService: AttributesAccessService,
    private appState: AppStateService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))
    this.attributesService.getUserAttributes(this.userId).subscribe(
      r => {
        this.attributeSchemes = r

        for (var attrScheme of this.attributeSchemes) {
          var rootIdx = attrScheme.associatedSchemes.findIndex(v => v.issuerAddress === attrScheme.issuerAddress);

          if (rootIdx >= 0) {
            attrScheme.rootAssociatedScheme = attrScheme.associatedSchemes[rootIdx];
          }
        }

        this.dataSource.setData(this.attributeSchemes)
        this.isLoaded = true
      }
    )
  }

  public get attributeState(): typeof AttributeState {
    return AttributeState;
  }

  onBack() {
    this.router.navigate(['/user-details', this.userId])
  }

  onRerequest(attributeScheme: AttributeScheme) {
    let that = this;
    var dialogRef = this.dialog.open(PasswordConfirmDialog, { data: { title: "Confirm attribute re-request", confirmButtonText: "Confirm" } });
    dialogRef.afterClosed().subscribe(r => {
      if (r) {
        that.attributesService.requestAttributes(this.userId, r, attributeScheme).subscribe(
          r1 => {
            console.info('Rerequest of attributes passed successfully');
            this.router.navigate(['user-details', this.userId])
          },
          e => {
            console.error('Failed to rerequest attributes', e);
          });
      }
    })
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
