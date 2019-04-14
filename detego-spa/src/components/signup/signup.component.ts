import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/services/auth.service';
import { AlertifyService } from 'src/services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserForRegisterDto } from 'src/model/dto/userforregisterdto';
import { Router } from "@angular/router";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  model: UserForRegisterDto = new UserForRegisterDto();
  passwordRepeat:string;

  errors: string[] = [];

  constructor(private authService: AuthService,
    private alertifyService: AlertifyService,
    private router: Router) { }
    
  ngOnInit() {
  }

  onSubmit(form: NgForm){
    this.authService.register(this.model).subscribe((data)  => 
    {
      this.alertifyService.success("You have signed up successfully!");
      this.router.navigate(['/login']);
    },
    (err)=>
    {
      this.errors = [];
      if (err.status === 400) {
        this.errors = err.error;
      } else {
          this.errors.push("something went wrong!");
      }
    });
  }

}
