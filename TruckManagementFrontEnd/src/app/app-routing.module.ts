import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./truck/truck.module').then((m) => m.TruckModule)
    
  },
  {
    path: 'truck',
    loadChildren: () =>
      import('./truck/truck.module').then((m) => m.TruckModule)
    
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
