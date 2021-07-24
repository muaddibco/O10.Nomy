import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AttributesAccessService } from '../attributes-access.service';
import { AttributeScheme } from '../models/attribute-scheme';
import { DataSource } from '@angular/cdk/collections';
import { Observable, ReplaySubject } from 'rxjs';

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
    ]),
  ]
})
export class UserAttributesComponent implements OnInit {

  public isLoaded = false
  public attributeSchemes: AttributeScheme[] = []
  public dataSource = new AttributeSchemesDataSource(this.attributeSchemes)

  public displayedColumns: string[] = ['issuerName']
  public expandedElement: AttributeScheme | null;

  constructor(
    private attributesService: AttributesAccessService,
    private router: Router,
    private route: ActivatedRoute ) { }

  ngOnInit(): void {
    var userId = Number(this.route.snapshot.paramMap.get('userId'))
    this.attributesService.getUserAttributes(userId).subscribe(
      r => {
        this.attributeSchemes = r
        this.dataSource.setData(this.attributeSchemes)
        this.isLoaded = true
      }
    )
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
