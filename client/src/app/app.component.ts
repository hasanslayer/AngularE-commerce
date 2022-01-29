import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products: IProduct[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<IPagination>('https://localhost:7094/api/products?pageSize=50').subscribe({
      next: (res: IPagination) => this.products = res.data,
      error: (e) => console.log(e)
    });
  }
}
