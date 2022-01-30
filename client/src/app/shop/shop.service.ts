import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:7094/api/';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<IPagination> {
    return this.http.get<IPagination>(this.baseUrl + 'products?pageSize=50');
  }
  getTypes(): Observable<IType[]> {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
  getBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }
}
