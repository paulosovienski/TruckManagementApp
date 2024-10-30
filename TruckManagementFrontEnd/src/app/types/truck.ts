
export class Truck {    
    Id?: number;    
    TruckChassis!: string;   
    TruckColor!: string; 
    TruckManufacturingDate?: Date;    
    TruckManufacturingPlant!: string;
    TruckModel!: string;  
    TruckManufacturingDateF: string;    
   
   

    constructor(id:number, truckChassis:string, truckColor:string, truckManufacturingDate: Date, TruckManufacturingPlant: string, truckModel: string, truckManufacturingDateF: string)   
    {
        this.Id = id;
        this.TruckChassis = truckChassis;
        this.TruckColor = truckColor;
        this.TruckManufacturingDate = truckManufacturingDate;
        this.TruckManufacturingPlant = TruckManufacturingPlant;
        this.TruckModel = truckModel;
        this.TruckManufacturingDateF = truckManufacturingDateF; 
    }
}

