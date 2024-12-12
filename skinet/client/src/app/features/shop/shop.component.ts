import { Component, OnInit } from '@angular/core';
import { Product } from '../../shared/models/products';
import { ShopService } from '../../core/services/shop.service';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone:true,
  imports: [
    MatCard,
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  baseurl = 'http://localhost:5001/api/';
  // products:Product[]=[];
  products? : Pagination<Product>
  // selectedBrands:string[]=[];
  // selectedTypes:string[]=[];
  // selectedSort: string = 'name';
  sortOptions = [
    {name: 'Alphabetical' , value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'}
  ]

  constructor(private shopService: ShopService,
              private dialogService:MatDialog){}

  ngOnInit(): void {
      //inside an observer object
      //this.http.get<Pagination<Product>>(this.baseurl + 'products').subscribe({
        //next: data => console.log(data), //callback function
        // this.shopService.getProducts().subscribe({
        // next: response => this.products = response.data,
        // error: error => console.log(error),
        // complete : () => console.log('complete'),
        this.initializeShop()
      }
  shopParams = new ShopParams();
  pageSizeOptions = [5,10,15,20]

  initializeShop()
  {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts()
  }

  getProducts()
  {
    // this.shopService.getProducts(this.selectedBrands , this.selectedTypes,this.selectedSort).subscribe({
      this.shopService.getProducts(this.shopParams).subscribe({
      // next: response => this.products = response.data,
      next: response => this.products = response,
      error: error => console.log(error),
      complete : () => console.log('complete'),
    });
  }

  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  handlePageEvent(event:PageEvent)
  {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }
  onSortChange(event: MatSelectionListChange){
    const selectedOption = event.options[0];
    if (selectedOption){
      // this.selectedSort = selectedOption.value;
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts()
    }
  }
 
  //method to open the dialog
  openFiltersDialog(){
    const dialogRef = this.dialogService.open(FiltersDialogComponent,{
      minWidth: '500px',
      data:{
        selectedBrands: this.shopParams.brands,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        selectedTypes:this.shopParams.types
      }
    });
    dialogRef.afterClosed().subscribe({
      next: result => {
        if (result){
          // this.selectedBrands = result.selectedBrands;
          // this.selectedTypes = result.selectedTypes;
          this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.shopParams.pageNumber = 1;
          this.getProducts()
          //apply filters
          // this.shopService.getProducts(this.selectedBrands, this.selectedTypes).subscribe({
          //   next: response => this.products = response.data,
          //   error: error => console.log(error)
          // })
        }
      }
    })
  }

}
