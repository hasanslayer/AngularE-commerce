import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cart, ICart, ICartItem, ICartTotal } from '../shared/models/cart';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = environment.apiUrl;
  // it will multiple subscriber for cartSource
  private cartSource = new BehaviorSubject<ICart>(null!); // need initial value ,so we set null initially
  cart$ = this.cartSource.asObservable();

  private cartTotalSource = new BehaviorSubject<ICartTotal>(null!);
  cartTotal$ = this.cartTotalSource.asObservable();
  shipping = 0;

  constructor(private http: HttpClient) {}

  createPaymentIntent() {
    return this.http
      .post<ICart>(
        this.baseUrl + 'payments/' + this.getCurrentCartValue().id,
        {}
      )
      .pipe(
        map((cart: ICart) => {
          this.cartSource.next(cart);
        })
      );
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.shipping = deliveryMethod.price;
    const cart = this.getCurrentCartValue();
    cart.deliveryMethodId = deliveryMethod.id;
    cart.shippingPrice = deliveryMethod.price;
    this.calculateTotals();
    this.setCart(cart);
  }

  getCart(id: string) {
    return this.http.get<ICart>(this.baseUrl + 'cart?id=' + id).pipe(
      map((cart: ICart) => {
        this.cartSource.next(cart);
        this.shipping = cart.shippingPrice!
        this.calculateTotals();
      })
    );
  }

  setCart(cart: ICart) {
    return this.http.post<ICart>(this.baseUrl + 'cart', cart).subscribe(
      (response: ICart) => {
        this.cartSource.next(response);
        this.calculateTotals();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getCurrentCartValue() {
    return this.cartSource.value;
  }

  addItemToCart(item: IProduct, qty = 1) {
    const itemToAdd: ICartItem = this.mapProductItemToCartItem(item, qty);
    const cart = this.getCurrentCartValue() ?? this.createCart();
    cart.items = this.addOrUpdateItem(cart.items, itemToAdd, qty);
    this.setCart(cart);
  }

  incrementItemQty(item: ICartItem) {
    const cart = this.getCurrentCartValue();
    const foundItemIndex = cart.items.findIndex((x) => x.id === item.id);
    cart.items[foundItemIndex].qty++;
    this.setCart(cart);
  }
  decrementItemQty(item: ICartItem) {
    const cart = this.getCurrentCartValue();
    const foundItemIndex = cart.items.findIndex((x) => x.id === item.id);
    if (cart.items[foundItemIndex].qty > 1) {
      cart.items[foundItemIndex].qty--;
      this.setCart(cart);
    } else {
      this.removeItemFromCart(item);
    }
  }
  removeItemFromCart(item: ICartItem) {
    const cart = this.getCurrentCartValue();
    if (cart.items.some((x) => x.id === item.id)) {
      cart.items = cart.items.filter((i) => i.id !== item.id); // filter all items that not match the condition
      if (cart.items.length > 0) {
        this.setCart(cart);
      } else {
        this.deleteCart(cart);
      }
    }
  }

  deleteLocalCart(id: string) {
    this.cartSource.next(null!);
    this.cartTotalSource.next(null!);
    localStorage.removeItem('cart_id');
  }

  deleteCart(cart: ICart) {
    return this.http.delete(this.baseUrl + 'cart?id=' + cart.id).subscribe({
      next: () => {
        this.cartSource.next(null!);
        this.cartTotalSource.next(null!);
        localStorage.removeItem('cart_id');
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  private calculateTotals() {
    const cart = this.getCurrentCartValue();
    const shipping = this.shipping;
    const subtotal = cart.items.reduce((a, b) => b.price * b.qty + a, 0);
    const total = subtotal + shipping;
    this.cartTotalSource.next({
      shipping,
      subtotal,
      total,
    });
  }

  private addOrUpdateItem(
    items: ICartItem[],
    itemToAdd: ICartItem,
    qty: number
  ): ICartItem[] {
    const index = items.findIndex((i) => i.id === itemToAdd.id);
    if (index === -1) {
      itemToAdd.qty = qty;
      items.push(itemToAdd);
    } else {
      items[index].qty += qty;
    }

    return items;
  }

  private createCart(): ICart {
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }

  private mapProductItemToCartItem(item: IProduct, qty: number): ICartItem {
    return {
      id: item.id,
      productName: item.name,
      brand: item.productBrand,
      type: item.productType,
      qty: qty,
      imgUrl: item.imgUrl,
      price: item.price,
    };
  }
}
