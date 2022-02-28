import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/cart/cart.service';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product!: IProduct;
  qty = 1;

  constructor(
    private shopService: ShopService,
    private activeRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private cartService: CartService
  ) {
    this.bcService.set('@productDetails',' ');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  addItemToCart(){
    this.cartService.addItemToCart(this.product,this.qty)
  }

  incrementQty(){
    this.qty++;
  }
  decrementQty(){
    if(this.qty > 1){
      this.qty--;
    }

  }

  loadProduct() {
    this.shopService.getProduct(Number(this.activeRoute.snapshot.paramMap.get('id'))).subscribe({
      next: (product) => {
        this.product = product;
        this.bcService.set('@productDetails',product.name)
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
