import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cart, ICart, ICartItem } from '../shared/models/cart';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  baseUrl = environment.apiUrl;
  // it will multiple subscriber for cartSource
  private cartSource = new BehaviorSubject<ICart>(null!); // need initial value ,so we set null initially
  cart$ = this.cartSource.asObservable();

  constructor(private http: HttpClient) {}

  getCart(id: string) {
    return this.http.get<ICart>(this.baseUrl + 'cart?id=' + id).pipe(
      map((cart: ICart) => {
        this.cartSource.next(cart);
        console.log(this.getCurrentCartValue());
      })
    );
  }

  setCart(cart: ICart) {
    return this.http.post<ICart>(this.baseUrl + 'cart', cart).subscribe(
      (response: ICart) => {
        this.cartSource.next(response);
        console.log(response);
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
  private addOrUpdateItem(
    items: ICartItem[],
    itemToAdd: ICartItem,
    qty: number
  ): ICartItem[] {
    const index = items.findIndex((i) => i.id === itemToAdd.id);
    if (index === -1) {
      itemToAdd.qty = qty;
      items.push(itemToAdd);
    }else{
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
