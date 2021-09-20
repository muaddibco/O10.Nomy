import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-unauthorized-use',
  templateUrl: './unauthorized-use.component.html',
  styleUrls: ['./unauthorized-use.component.css']
})
export class UnauthorizedUseComponent implements OnInit {

  ipaddress: string = '';
  latitude: string = '';
  longitude: string = '';
  currency: string = '';
  currencysymbol: string = '';
  isp: string = '';
  city: string = '';
  country: string = '';

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.getLocation()
    this.getIpAddress().subscribe(res => {

      this.ipaddress = res['ip'];
      this.getGEOLocation(this.ipaddress).subscribe(res => {

        this.latitude = res['latitude'];
        this.longitude = res['longitude'];
        this.currency = res['currency']['code'];
        this.currencysymbol = res['currency']['symbol'];
        this.city = res['city'];
        this.country = res['country_code3'];
        this.isp = res['isp'];
        console.log(res);
      });
      //console.log(res);

    });
  }

  getLocation(): void {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        const longitude = position.coords.longitude;
        const latitude = position.coords.latitude;
        console.info("longitude = " + longitude + "; latitude = " + latitude)
      });
    } else {
      console.error("No support for geolocation")
    }
  }

  getIpAddress() {
    return this.http
      .get('https://api.ipify.org/?format=json')
      .pipe(
        catchError(this.handleError)
      );
  }

  getGEOLocation(ip) {
    // Update your api key to get from https://ipgeolocation.io
    let url = "https://api.ipgeolocation.io/ipgeo?apiKey=11ae072767fc45ecb9ff83d13692ae2d&ip=" + ip;
    return this.http
      .get(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }

  onDismiss() {
    this.router.navigate(['joint-purchases'])
  }
}
