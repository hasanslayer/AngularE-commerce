import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {
  constructor(
    private cartService: CartService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  createPaymentIntent() {
    return this.cartService.createPaymentIntent().subscribe({
      next: (response: any) => {
        this.toastr.success('payment intent created');
      },
      error: (error) => {
        console.log(error);
        this.toastr.error(error.message);
      },
    });
  }
}
