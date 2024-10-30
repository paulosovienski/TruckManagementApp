import { Component, ElementRef, OnInit, ViewChild, AfterViewInit, ViewChildren, QueryList} from '@angular/core';
import { TruckService } from '../services/truck.service';
import { Truck } from '../types/truck';
import { DatePipe } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
//import $ from 'jquery';



import { empty } from 'rxjs';



@Component({
  selector: 'app-truck',
  templateUrl: './truck.component.html',
  styleUrls: ['./truck.component.css'],
  providers: [DatePipe]
})
export class TruckComponent implements OnInit {

  @ViewChild('errorModal') errorModal: any;
     
  dataTable: any;
  trucks : any[] = []; 
  truck : Truck; 
  currentDate = new Date();  
  currentId = 0;
  trucksModel : any[] = [];  
  sites : any[] = [];  
  errorMessage : string = "";  
 
  
  constructor(private service: TruckService, private datePipe: DatePipe, private modal: NgbModal) 
  {
    this.truck = new Truck(0, "", "", this.currentDate, "", "", "");    
  }

  ngOnInit(): void {
    console.log("opa passei pelo ngOnInit;");
   
  }

  ngAfterViewInit(): void {
    this.TesteTrucksReceived();
    this.ClearTruck();
    this.GetAllTRucks();   
    this.GetTruckModels();  
    this.GetSites();
    console.log(this.trucksModel);
    //this.initializeDataTable();
  }  

 
  // initializeDataTable(): void {
  //   console.log("this.truckTable");
  
  //   this.dataTable = $('#truckTable').DataTable({
  //     data: this.trucks,
  //     columns: [
  //       { title: 'Modelo', data: '' },
  //       { title: 'Data de Fabricação', data: 'TruckManufacturingDateF' },
  //       { title: 'Chassis', data: 'tiTruckChassistle' },
  //       { title: 'Cor', data: 'tiTruckColortle' },
  //       { title: 'Planta de Fabricação', data: 'TruckManufacturingPlant' },        
  //     ]
      
  //   });
  // }

  ngOnDestroy(): void {
    if (this.dataTable) {
      this.dataTable.destroy();
    }
  }

  GetAllTRucks() : void{
    this.service.getAll().subscribe(
      (response) => {   
            this.ClearTruck();
            this.SetTrucksIncoming(response);
            console.log(this.trucks);                      
      },   
      (error) => {
        this.ClearTruck();
        this.modal.dismissAll();   
        this.errorMessage = error.message;
        this.OpenModalerrorModal();
      }    
    ); 
  }

  OpenModalSaveTruck(content: any) : void{
    this.modal.open(content);
  }  

  SaveTruck() : void {
    console.log(this.truck);
    this.truck.TruckManufacturingDate = this.SetTruckManufacturingDateF(this.truck.TruckManufacturingDateF);
    if(!this.ValidateTruck()){

      this.service.postTruck(this.truck).subscribe(
        response => {        
          console.log(response);
          this.ClearTruck();
          this.modal.dismissAll();     
          this.GetAllTRucks(); 
        },
        error => {        
          console.log(error.message);
          this.modal.dismissAll();
          this.errorMessage = error.message;
          this.OpenModalerrorModal();

        }
      )

    }else{
      this.modal.dismissAll();
      this.OpenModalerrorModal()
    } 
  }

  OpenModalEditTruck(content: any, event: any) : void{
    this.currentId = event.target.id;    
    this.service.getTruck(this.currentId).subscribe(
      response => {  
        this.SetTruckIncoming(response); 
                
      },
      (error) => {        
        this.ClearTruck();
        this.modal.dismissAll();   
        this.errorMessage = error.message;
        this.OpenModalerrorModal();
      }
    )
    this.modal.open(content);
  }

  OpenModalerrorModal() : void{  
    this.modal.open(this.errorModal);
  }

  OpenModalDeleteTruck(confirmDelete: any, event: any) : void{
    this.currentId = event.target.id;
    this.modal.open(confirmDelete);
  }

  DeleteTruck() : void{
    console.log(this.currentId);
    this.service.deleteTruck(this.currentId).subscribe(
      (response) => {        
        console.log(response);
        this.modal.dismissAll();
        this.GetAllTRucks(); 
        this.ClearTruck();

      },
      (error) => {
        this.ClearTruck();
        this.modal.dismissAll();   
        this.errorMessage = error.message;
        this.OpenModalerrorModal();
      }
      
    )
  } 

  GetTruckModels() : void {   
    this.service.getTruckModels().subscribe(
      response => {   
        console.log(response);
        for(let i = 0; i< response.length; i++){
          this.trucksModel.push(response[i].model);
        }       
      }
      
    )
  }

  GetSites() : void {   
    this.service.getSites().subscribe(
      response => {   
        console.log(response);
        for(let i = 0; i< response.length; i++){
          this.sites.push(response[i].site);
        }       
      }
      
    )
  }


 TesteTrucksReceived(): void{
  console.log("TesteTrucksReceived");
  console.log("size of trucks "+this.trucks.length);
  console.log(this.trucks);  
 }

 SetTrucksIncoming(t: any[]) : void {
  for(let i = 0; i < t.length; i++ ){

    this.trucks.push(new Truck(t[i].id, t[i].truckChassis, t[i].truckColor, t[i].truckManufacturingDate, t[i].truckManufacturingPlant,  t[i].truckModel, this.GetTruckManufacturingDateF(t[i].truckManufacturingDate)));      
  } 

  }

  SetTruckIncoming(t: any) : void {      
      this.truck.Id = t.data.id;
      this.truck.TruckChassis = t.data.truckChassis;
      this.truck.TruckColor = t.data.truckColor;
      this.truck.TruckManufacturingDate = t.data.truckManufacturingDate;
      this.truck.TruckManufacturingPlant = t.data.truckManufacturingPlant;
      this.truck.TruckModel = t.data.truckModel;     
      this.truck.TruckManufacturingDateF  = this.GetTruckManufacturingDateF(t.data.truckManufacturingDate);
    }

  ClearTruck() : void {
    this.truck.Id = 0;
    this.truck.TruckChassis = "";
    this.truck.TruckColor = "";
    this.truck.TruckManufacturingDate = this.currentDate;
    this.truck.TruckManufacturingPlant = "";
    this.truck.TruckModel = "";
    this.currentId = 0;
    this.trucks = [];
    this.errorMessage = "";
  }   

  GetTruckManufacturingDateF(dt: any) : string{
    console.log(dt);
    const dttp : string = dt.substring(0, 10);
    console.log(dttp);
    var dtsplitted = dttp.split("-");
    return dtsplitted[2]+"/"+dtsplitted[1]+"/"+dtsplitted[0];  
  }

  SetTruckManufacturingDateF(dtf : string) : Date{
    var dtfSplitted = dtf.split("/");
    let dateString = dtfSplitted[2]+"-"+dtfSplitted[1]+"-"+dtfSplitted[0]+"T00:00:00";
    let dateAngularFormat = new Date(dateString);
    return dateAngularFormat;
   

  }

  ValidateTruck() : boolean{

    let isInvalid = false; 

    if(!(this.truck.TruckModel == "FH" || this.truck.TruckModel == "FM" || this.truck.TruckModel == "VM")){
      isInvalid = true; 
      this.errorMessage = "Modelo Inválido";
      return isInvalid;
    }
   
    if(this.truck.TruckManufacturingDate?.toString() == "Invalid Date"){
      isInvalid = true; 
      this.errorMessage = "Data Inválida";
      return isInvalid;
    }

    if(this.truck.TruckManufacturingDate?.toString() == "Invalid Date"){
      isInvalid = true; 
      this.errorMessage = "Data Inválida";
      return isInvalid;
    }

    if(this.truck.TruckColor == "" || this.truck.TruckManufacturingPlant == "" || this.truck.TruckChassis == "" ){
      isInvalid = true; 
      this.errorMessage = "É necessário preencher todos os dados!";
      return isInvalid;
    }

    return isInvalid;

  }

}
