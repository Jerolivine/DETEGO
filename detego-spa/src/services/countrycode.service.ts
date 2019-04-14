import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { CountryCode } from 'src/model/dto/countrycode';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CountrycodeService {

  constructor(private httpClient: HttpClient) {}

  path: string = "https://localhost:44321/api/CountryCode";

  getCountryCodes(): Observable<CountryCode[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    };

    return this.httpClient
      .get<CountryCode[]>(this.path, httpOptions)
      .pipe(tap(data => console.log(JSON.stringify(data))));
  }

}
