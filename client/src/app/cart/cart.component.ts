import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ICart, ICartItem } from '../shared/models/cart';
import { CartService } from './cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  cart$!: Observable<ICart>;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
  }

  removeCartItem(item: ICartItem) {
    this.cartService.removeItemFromCart(item);
  }

  incrementQty(item: ICartItem) {
    this.cartService.incrementItemQty(item);
  }

  decrementQty(item: ICartItem) {
    this.cartService.decrementItemQty(item);
  }
}
