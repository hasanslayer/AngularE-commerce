import { IAddress } from "./address";

export interface IOrderToCreate {
  cartId: string;
  deliveryMethodId: number;
  shipToAddress: IAddress;
}

export interface IOrderItem {
  productItemId: number;
  productNameAr: string;
  productNameEn: string;
  imgUrl: string;
  price: number;
  qty: number;
}

export interface IOrder {
  id: number;
  buyerEmail: string;
  dateOrder: Date;
  shipToAddress: IAddress;
  deliveryMethod: string;
  shippingPrice: number;
  orderItems: IOrderItem[];
  subtotal: number;
  total: number;
  status: string;
}
