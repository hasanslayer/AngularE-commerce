import { v4 as uuidv4 } from "uuid";

export interface ICart {
  id: string;
  items: ICartItem[];
}

export interface ICartItem {
  id: number;
  productName: string;
  price: number;
  qty: number;
  imgUrl: string;
  brand: string;
  type: string;
}

export class Cart implements ICart{
  id = uuidv4();
  items: ICartItem[] = [];
}


