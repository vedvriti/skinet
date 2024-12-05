import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/products';
import { Pagination } from './shared/models/pagination';

@Component({
    selector: 'app-root',
    standalone:true,
    imports:[HeaderComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'Skinet';
  baseurl = 'http://localhost:5001/api/';
  products:Product[]=[];

  constructor(private http:HttpClient){}

  ngOnInit(): void {
      //inside an observer object
      this.http.get<Pagination<Product>>(this.baseurl + 'products').subscribe({
        //next: data => console.log(data), //callback function
        next: response => this.products = response.data,
        error: error => console.log(error),
        complete : () => console.log('complete'),
      });
  }


}
