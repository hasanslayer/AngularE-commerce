import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:7094/api/';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<IPagination> {
    return this.http.get<IPagination>(this.baseUrl + 'products?pageSize=50');
  }
}
