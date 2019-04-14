import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { AddStoreDto } from "src/model/dto/addstoredto";
import { Observable } from "rxjs";
import { Store } from "src/model/store";
import { tap } from "rxjs/operators";
import { AuthService } from "./auth.service";
import { GetStoresDto } from "src/model/dto/getStoresdto";
import { UpdateStoreDto } from "src/model/dto/updatestoredto";
import { GetStoreDt } from 'src/model/dto/getstoredt';
import { ChartReportDto } from 'src/model/dto/chartreportdto';
import { GetChartReportDto } from 'src/model/dto/getchartreportdto';

@Injectable({
  providedIn: "root"
})
export class StoreService {
  path: string = "https://localhost:44321/api/Store/";

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {}

  addStore(addStoreDto: AddStoreDto): Observable<Store> {
    addStoreDto.UserName = this.authService.getUserName();
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    };

    return this.httpClient
      .post<Store>(this.path + "addStore", addStoreDto, httpOptions)
      .pipe(
        tap(data => {
          console.log(JSON.stringify(data));
        })
      );
  }

  getStores(): Observable<GetStoresDto[]> {
    var userName = this.authService.getUserName();

    var httpParams = new HttpParams();
    httpParams = httpParams.append("userName", userName);

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      }),
      params: httpParams
    };

    return this.httpClient
      .get<GetStoresDto[]>(this.path + "getStores", httpOptions)
      .pipe(
        tap(data => {
          console.log(JSON.stringify(data));
        })
      );
  }

  getStore(id: any): Observable<GetStoreDt> {
    var httpParams = new HttpParams();
    httpParams = httpParams.append("storeId", id);

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      }),
      params: httpParams
    };

    return this.httpClient
      .get<GetStoreDt>(this.path + "getStore", httpOptions)
      .pipe(
        tap(data => {
          console.log(JSON.stringify(data));
        })
      );
  }

  updateStore(updateStoreDto: UpdateStoreDto): Observable<GetStoreDt> {

    updateStoreDto.UserName = this.authService.getUserName();

    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      }),
    };

    debugger;
    return this.httpClient
      .put<GetStoreDt>(this.path+"updateStore" , updateStoreDto, httpOptions)
      .pipe(
        tap(data => {
          console.log(JSON.stringify(data));
        })
      );
  }

   getChartReport(getChartReport:GetChartReportDto):Observable<ChartReportDto>{

    var httpParams = new HttpParams();
    httpParams = httpParams.append("storeId",getChartReport.StoreId.toString());
    httpParams = httpParams.append("userName",this.authService.getUserName());
    httpParams = httpParams.append("reportType",getChartReport.ReportType.toString());
    
    debugger;
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      }),
      params: httpParams
    };

    return this.httpClient.get<ChartReportDto>(this.path+"getChartReport",httpOptions);

  }
  
}
