import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-mainapplication",
  templateUrl: "./mainapplication.component.html",
  styleUrls: ["./mainapplication.component.css"]
})
export class MainapplicationComponent implements OnInit {
  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit() {}

  navigateToAddStore() {
    this.router.navigate(["storeAdd"], { relativeTo: this.route });
  }

  navigateToStores() {
    this.router.navigate(["stores"], { relativeTo: this.route });
  }

}
