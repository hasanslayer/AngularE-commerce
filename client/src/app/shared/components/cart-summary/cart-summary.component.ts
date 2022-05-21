import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { CartService } from 'src/app/cart/cart.service';
import { ICart, ICartItem } from '../../models/cart';

@Component({
  selector: 'app-cart-summary',
  templateUrl: './cart-summary.component.html',
  styleUrls: ['./cart-summary.component.scss'],
})
export class CartSummaryComponent implements OnInit {
  cart$!: Observable<ICart>;
  @Output() decrement: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
  @Output() increment: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
  @Output() remove: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
  @Input() isCart = true;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
  }

  decrementQty(item:ICartItem){
    this.decrement.emit(item);
  }

  incrementQty(item:ICartItem){
    this.increment.emit(item);
  }

  removeCartItem(item:ICartItem){
    this.remove.emit(item);
  }
}
