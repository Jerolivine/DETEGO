import { Component, OnInit } from '@angular/core';
import { UserForLoginDto } from 'src/model/dto/userforlogindto';
import { AuthService } from 'src/services/auth.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: UserForLoginDto = new UserForLoginDto();
  errors: string[] = [];

  constructor(private authService:AuthService,
    private router:Router) { }

  ngOnInit() {

  }

  onSubmit(ngForm:NgForm){
    this.authService.login(this.model).subscribe(
    (res) => {
      localStorage.setItem("Token",res.toString());
      this.router.navigate(["/mainApplication/stores"]);
    },
    (err) => {
      this.errors = [];
      if (err.status === 400) {
        this.errors = err.error;
      } else if(err.status === 401){
        this.errors.push("UserName or Password is wrong");
      }
      else {
          this.errors.push("something went wrong!");
      }

    })
  }

}
