import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { CartService } from './cart/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  constructor(private cartService: CartService,private accountService:AccountService) {}

  ngOnInit(): void {
    this.loadCard();
    this.loadCurrentUser();
  }

  loadCurrentUser(){
    const token = localStorage.getItem('token');
    if(token){
      this.accountService.loadCurrentUser(token).subscribe({
        next:() => console.log('loaded user'),
        error:(error) => console.log(error)
      })
    }
  }

  loadCard() {
    const cartId = localStorage.getItem('cart_id');
    if (cartId) {
      this.cartService.getCart(cartId).subscribe({
        next: () => console.log('cart initialized'),
        error: (error) => console.log(error),
      });
    }
  }
}
