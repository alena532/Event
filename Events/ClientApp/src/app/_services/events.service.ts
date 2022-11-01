import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {map} from "rxjs";
import {CreateEvent} from "../_models/Create-event";
import {Event} from "../_models/Event"
import {UpdateEvent} from "../_models/Update-event";

@Injectable()
export class EventsService {

  private accessPointUrl: string = 'https://localhost:7292/api/admin/Events';

  constructor(private http: HttpClient) {
  }

  getAll() {
    return this.http.get<Event[]>(this.accessPointUrl).pipe(map(data=>{
          return data.map(function(event:any): Event{
            let timeString = String(event.time);
            let times=timeString.split("T");
            let date = times[0];
            let time = times[1];

            return new Event(event.id,event.title,event.description,event.plan,date,time,event.place,event.company,event.speaker);
          })
        }
      )
    )
  }

  create(event:CreateEvent){
    return this.http.post<any>(this.accessPointUrl,event);
  }

  getById(id:number){
    return this.http.get<any>(`https://localhost:7292/api/admin/Events/${id}`).pipe(map(data=>{
            let timeString = String(data.time);
            let times=timeString.split("T");
            let date = times[0];
            let time = times[1];

            let ev = new Event(data.id,data.title,data.description,data.plan,date,time,data.place,data.company,data.speaker);
            console.log(ev);
            return ev;
        }
      )
    )
  }

  update(id:number,event:UpdateEvent){
    return this.http.put<any>(`https://localhost:7292/api/admin/Events/${id}`,event);
  }

  delete(id: number){
    return this.http.delete(`https://localhost:7292/api/admin/Events/${id}`);
  }
}
