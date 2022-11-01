import {Time} from "@angular/common";

export class Event {
  constructor (
    public id: number,
    public title: string,
    public description: string,
    public plan: string,
    public date: string,
    public time: string,
    public place: string,
    public company: string,
    public speaker: string,
  ) {}
}



