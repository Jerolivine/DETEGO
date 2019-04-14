import { Component, OnInit } from "@angular/core";
import { AddStoreDto } from "src/model/dto/addstoredto";
import { StoreService } from "src/services/store.service";
import { NgForm } from "@angular/forms";
import { AlertifyService } from "src/services/alertify.service";
import { CategoryService } from 'src/services/category.service';
import { Category } from 'src/model/category';
import { CountryCode } from 'src/model/dto/countrycode';
import { CountrycodeService } from 'src/services/countrycode.service';

@Component({
  selector: "app-storeadd",
  templateUrl: "./storeadd.component.html",
  styleUrls: ["./storeadd.component.css"]
})
export class StoreaddComponent implements OnInit {
  model: AddStoreDto = new AddStoreDto();
  categories:Category[] = [];
  countryCodes: CountryCode[] = [];

  errors: string[] = [];

  constructor(
    private storeService: StoreService,
    private alertifyService: AlertifyService,
    private categoryService: CategoryService,
    private countryCodeService: CountrycodeService
  ) {}

  ngOnInit() {

    this.categoryService.getCategories().subscribe((data) =>{
      this.categories = data;
    });
    
    this.countryCodeService.getCountryCodes().subscribe((data) =>{
      this.countryCodes = data;
    });
    
  }

  onSubmit(ngForm: NgForm) {
    this.storeService.addStore(this.model).subscribe(
      res => {
        this.alertifyService.success("Store Has Successfully Added");
        // store detayına navigate
      },
      err => {
        this.errors = [];
        if (err.status === 400) {
          this.errors = err.error;
        } else {
          this.errors.push("something went wrong!");
        }
      }
    );
  }
  
}
