import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent implements OnInit {
  @Input() product: IProduct = {
    id: 0,
    description: '',
    imgUrl: '',
    name: '',
    price: 0,
    productBrand: '',
    productType: '',
  };

  constructor() {}

  ngOnInit(): void {}
}
