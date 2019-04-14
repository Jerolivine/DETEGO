import { Component, OnInit } from '@angular/core';
import { GetStoresDto } from 'src/model/dto/getStoresdto';
import { StoreService } from 'src/services/store.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-stores',
  templateUrl: './stores.component.html',
  styleUrls: ['./stores.component.css']
})
export class StoresComponent implements OnInit {

  headerList:string[] = [
    "Name","Email","Category","CountryCode",""
  ];

  stores: GetStoresDto[] = [];

  constructor(private storeService:StoreService,
    private router: Router) { }

  ngOnInit() {
    this.storeService.getStores().subscribe(data => {
      this.stores = data;
    });
  }

  navigateToStore(id:number){
    debugger;
    this.router.navigate(['../storeUpdate'], { queryParams: { storeId: id } });
  }


}
