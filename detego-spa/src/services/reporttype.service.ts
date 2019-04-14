import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { ReportType } from "src/model/reporttype";
import { tap } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class ReporttypeService {
  path: string = "https://localhost:44321/api/ReportType/";

  constructor(private httpClient: HttpClient) {}

  getReportTypes(): Observable<ReportType[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    };

    return this.httpClient
      .get<ReportType[]>(this.path + "getReportTypes", httpOptions)
      .pipe(tap(data => console.log(JSON.stringify(data))));
  }
}
