import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { SignupComponent } from "../signup/signup.component";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { AlertifyService } from "src/services/alertify.service";
import { LoginComponent } from "../login/login.component";
import { ConfirmEqualValidatorDirective } from "src/shared/confirm-equal-validator.directive";
import { JwtHelperService, JwtModule } from "@auth0/angular-jwt";
import { MainapplicationComponent } from "../dashboard/mainapplication/mainapplication.component";
import { NavbarComponent } from "../navbar/navbar.component";
import { StoreService } from "src/services/store.service";
import { AuthService } from "src/services/auth.service";
import { StoreaddComponent } from "../storeadd/storeadd.component";
import { CategoryService } from "src/services/category.service";
import { StoresComponent } from "../stores/stores.component";
import { StoreupdateComponent } from '../storeupdate/storeupdate.component';
import { ChartsModule } from 'ng2-charts';

export function tokenGetter() {
  return localStorage.getItem("Token");
}

@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    LoginComponent,
    ConfirmEqualValidatorDirective,
    MainapplicationComponent,
    NavbarComponent,
    StoreaddComponent,
    StoresComponent,
    StoreupdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ChartsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ["https://localhost:44321"],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [AlertifyService, StoreService, AuthService, CategoryService],
  bootstrap: [AppComponent]
})
export class AppModule {}
