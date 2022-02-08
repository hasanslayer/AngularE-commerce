import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';

const routes: Routes = [
  { path: '', component: ShopComponent }, // we don't need 'shop' path in shop-routing
  { path: ':id', component: ProductDetailsComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports:[RouterModule] // becuse we want to use RouterModule in shop module
})
export class ShopRoutingModule {}
