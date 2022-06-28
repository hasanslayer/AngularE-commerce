import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {

  @Input() appStepper!: CdkStepper

  constructor(
    private cartService: CartService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  createPaymentIntent() {
    return this.cartService.createPaymentIntent().subscribe({
      next: (response: any) => {
        this.appStepper.next();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
