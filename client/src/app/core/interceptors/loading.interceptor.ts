import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { delay, finalize, Observable } from 'rxjs';
import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private busyService: BusyService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if(request.method == 'POST' && request.url.includes('orders')){
      return next.handle(request);
    }
    if(request.method == 'DELETE'){
      return next.handle(request);
    }
    if (request.url.includes('emailExists')) {
      return next.handle(request);
    }
    this.busyService.busy();
    return next.handle(request).pipe(
      finalize(() => {
        this.busyService.idle();
      })
    );
  }
}
