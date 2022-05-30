import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CartService } from 'src/app/cart/cart.service';
import { ICart } from 'src/app/shared/models/cart';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() checkoutForm!: FormGroup;

  constructor(
    private cartService: CartService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  submitOrder() {
    const cart = this.cartService.getCurrentCartValue();
    const orderToCreate = this.getOrderToCreate(cart);
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order: IOrder) => {
        this.toastr.success('Order created successfully');
        this.cartService.deleteLocalCart(cart.id);
        const navigationExtras: NavigationExtras = { state: order };
        this.router.navigate(['checkout/success'], navigationExtras);
      },
      error: (error) => {
        this.toastr.error(error.message);
        console.log(error);
      },
    });
  }
  private getOrderToCreate(cart: ICart) {
    return {
      cartId: cart.id,
      deliveryMethodId: +this.checkoutForm
        .get('deliveryForm')
        ?.get('deliveryMethod')?.value,
      shipToAddress: this.checkoutForm.get('addressForm')?.value,
    };
  }
}
