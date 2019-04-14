import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SignupComponent } from "../signup/signup.component";
import { LoginComponent } from "../login/login.component";
import { MainapplicationComponent } from "../dashboard/mainapplication/mainapplication.component";
import { StoreaddComponent } from "../storeadd/storeadd.component";
import { StoresComponent } from "../stores/stores.component";
import { StoreupdateComponent } from "../storeupdate/storeupdate.component";

const routes: Routes = [
  { path: "signUp", component: SignupComponent },
  { path: "login", component: LoginComponent },
  {
    path: "mainApplication",
    component: MainapplicationComponent,
    children: [
      { path: "storeUpdate/:storeId", component: StoreupdateComponent },
      { path: "storeAdd", component: StoreaddComponent },
      { path: "stores", component: StoresComponent }, 
      // { path: "", component: StoresComponent }, 
    ]
  },
  { path: "", redirectTo: "login", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
