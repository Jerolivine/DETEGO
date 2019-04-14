import { Component, OnInit } from "@angular/core";
import { StoreService } from "src/services/store.service";
import { Router, ActivatedRoute } from "@angular/router";
import { NgForm } from "@angular/forms";
import { Category } from "src/model/category";
import { CountryCode } from "src/model/dto/countrycode";
import { CategoryService } from "src/services/category.service";
import { CountrycodeService } from "src/services/countrycode.service";
import { UpdateStoreDto } from "src/model/dto/updatestoredto";
import { AlertifyService } from "src/services/alertify.service";
import { GetStoreDt } from "src/model/dto/getstoredt";
import { ReportType } from "src/model/reporttype";
import { ReporttypeService } from "src/services/reporttype.service";
import { GetChartReportDto } from "src/model/dto/getchartreportdto";

@Component({
  selector: "app-storeupdate",
  templateUrl: "./storeupdate.component.html",
  styleUrls: ["./storeupdate.component.css"]
})
export class StoreupdateComponent implements OnInit {
  constructor(
    private storeService: StoreService,
    private activatedRoute: ActivatedRoute,
    private categoryService: CategoryService,
    private countryCodeService: CountrycodeService,
    private alertifyService: AlertifyService,
    private reportTypeService: ReporttypeService
  ) {}

  public model: GetStoreDt = new GetStoreDt();
  public selectedReportType: number;
  public displayBars = true;
  categories: Category[] = [];
  countryCodes: CountryCode[] = [];
  reportTypes: ReportType[] = [];

  public barChartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  /******* chart ********/
  public barChartType = "bar";
  public barChartLegend = true;

  // chart labels
  public barChartTotalStockLabels = ["Total Stock"];
  public barDecimalLabels = ["Accuracy", "On Floor Availability"];
  public barMeanAgeInDaysLabels = ["Mean Age In Days"];

  // chart datas
  public barChartTotalStockData = [
    { data: [0], label: "This Store" },
    { data: [0], label: "All Store" }
  ];

  public barChartDecimalData = [
    { data: [0, 0], label: "This Store" },
    { data: [0, 0], label: "All Store" }
  ];

  public barMeanAgeInDaysData = [
    { data: [0], label: "This Store" },
    { data: [0], label: "All Store" }
  ];

  /***********************/
  errors: string[] = [];

  ngOnInit() {
    // get categories
    this.categoryService.getCategories().subscribe(data => {
      this.categories = data;
    });

    // get country codes
    this.countryCodeService.getCountryCodes().subscribe(data => {
      this.countryCodes = data;
    });

    // get store info via query params
    this.activatedRoute.params.subscribe(params => {
      this.storeService.getStore(params["storeId"]).subscribe(data => {
        this.model = data;

        // get report types
        this.reportTypeService.getReportTypes().subscribe(data => {
        debugger;
        this.reportTypes = data;
        this.selectedReportType = 3;
        this.getReport();
    });
      });
    });

    

  }

  onSubmit(ngForm: NgForm) {
    var updateStoreDto = new UpdateStoreDto();
    updateStoreDto.BackStore = this.model.backStore;
    updateStoreDto.Category = this.model.category;
    updateStoreDto.CountryCode = this.model.countryCode;
    updateStoreDto.Email = this.model.email;
    updateStoreDto.FrontStore = this.model.frontStore;
    updateStoreDto.Name = this.model.name;
    updateStoreDto.ShoppingWindow = this.model.shoppingWindow;
    updateStoreDto.StoreId = this.model.id;

    this.storeService.updateStore(updateStoreDto).subscribe(
      data => {
        this.model = data;
        this.alertifyService.success("Store Has Updated Successfully");
        this.getReport();
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

  getReport() {
    var getChartReportDto = new GetChartReportDto();
    getChartReportDto.ReportType = this.selectedReportType;
    getChartReportDto.StoreId = this.model.id;

    this.storeService.getChartReport(getChartReportDto).subscribe(
      data => {
        this.barChartTotalStockData = [
          { data: [data.storeReport.totalStock], label: "This Store" },
          { data: [data.generalReport.totalStock], label: "All Store" }
       ];

       this.barChartDecimalData = [
        { data: [data.storeReport.accuracy, data.storeReport.onFloorAvailability], label: "This Store" },
        { data: [data.generalReport.accuracy, data.generalReport.accuracy], label: "All Store" }
      ];

      this.barMeanAgeInDaysData = [
        { data: [data.storeReport.meanAgeInDays], label: "This Store" },
        { data: [data.generalReport.meanAgeInDays], label: "All Store" }
      ];

      this.displayBars = true;

      },
      err => {
        this.displayBars = false;
      }
    );
  }
}
