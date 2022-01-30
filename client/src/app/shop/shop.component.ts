import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts().subscribe({
      next: (res) => {
        this.products = res.data;
        console.log(res);
      },
      error: (err) => console.log(err),
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (res) => {
        this.brands = res;
        console.log(res);
      },
      error: (err) => console.log(err),
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (res) => {
        this.types = res;
        console.log(res);
      },
      error: (err) => console.log(err),
    });
  }
}
