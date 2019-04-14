import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { SystemUser } from 'src/model/systemuser';
import { UserForRegisterDto } from 'src/model/dto/userforregisterdto';
import { LoginComponent } from 'src/components/login/login.component';
import { UserForLoginDto } from 'src/model/dto/userforlogindto';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router) { }
  private path: string = "https://localhost:44321/api/Auth/";

  register(systemUser: UserForRegisterDto) : Observable<SystemUser>{
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.post<SystemUser>(this.path+"register", systemUser, httpOptions).pipe(
      tap(data => console.log(JSON.stringify(data)))
    );

  }

  isAuthenticated(): boolean{
    const token = localStorage.getItem("Token");

    return !this.jwtHelper.isTokenExpired(token);

  }

  getUserName():string{
    const token = localStorage.getItem("Token");

    var userName = this.jwtHelper.decodeToken(token).unique_name;
    return userName;
  }

  signOut(){
    localStorage.removeItem("Token");
    this.router.navigate(["/login"]);
  }

  login(userToBeLoggedIn: UserForLoginDto):Observable<String>{

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.post<String>(this.path+"login",userToBeLoggedIn,httpOptions).pipe(
      tap(data => {
        console.log(JSON.stringify(data))
      })
    );


  }

  // _handleError(err: HttpErrorResponse) {
  //   let errorMessage = '';
  //   if (err.error instanceof ErrorEvent) {
  //     errorMessage = 'An Error ' + err.error.message;
  //   }
  //   else {
  //     errorMessage = 'System Error';
  //   }
  //   return throwError(errorMessage);
  // }


}
