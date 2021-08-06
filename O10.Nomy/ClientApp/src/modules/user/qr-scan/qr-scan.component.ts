import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-qr-scan',
  templateUrl: './qr-scan.component.html',
  styleUrls: ['./qr-scan.component.css']
})
export class QrScanComponent implements OnInit {

  public isLoaded = false
  public isQrCodeSet = false
  public qrContent: string | null
  public userId: number

  constructor(
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))
    this.isLoaded = true
  }

  public getDataFromClipBoard(event: ClipboardEvent): void {
    navigator['clipboard'].readText().then((data) => {
      this.qrContent = data
      this.isQrCodeSet = true
    });
  }

  public clearQr() {
    this.isQrCodeSet = false
    this.qrContent = null
  }

  public proceed() {

  }

  public cancel() {
    this.router.navigate(['user-details', this.userId])
  }
}
