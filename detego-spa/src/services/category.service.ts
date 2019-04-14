import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Category } from "src/model/category";
import { tap } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class CategoryService {
  constructor(private httpClient: HttpClient) {}

  path: string = "https://localhost:44321/api/Category";

  getCategories(): Observable<Category[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    };

    return this.httpClient
      .get<Category[]>(this.path, httpOptions)
      .pipe(tap(data => console.log(JSON.stringify(data))));
  }
}
