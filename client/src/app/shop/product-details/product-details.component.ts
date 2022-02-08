import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct = {
    id: 0,
    description: '',
    imgUrl: '',
    name: '',
    price: 0,
    productBrand: '',
    productType: '',
  };

  constructor(
    private shopService: ShopService,
    private activeRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    this.shopService.getProduct(Number(this.activeRoute.snapshot.paramMap.get('id'))).subscribe({
      next: (product) => {
        this.product = product;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
