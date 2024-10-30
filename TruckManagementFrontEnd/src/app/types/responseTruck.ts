import {Truck } from '../types/truck'  

export class ResponseTruck {
    data!: Truck[];
    message?: string;
    success!: boolean;
    updated!: Date;
}