import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TruckRoutingModule } from './truck-routing.module';
import { TruckComponent } from './truck.component';
import { FormsModule }   from '@angular/forms';




@NgModule({
  declarations: [
    TruckComponent
  ],
  imports: [
    CommonModule,
    TruckRoutingModule, 
    FormsModule
  ]
})
export class TruckModule { }
