import {Time} from "@angular/common";

export class CreateEvent {
  constructor (
    public title: string,
    public description: string,
    public plan: string,
    public speakerId: number,
    public companyId: number,
    public time: string,
    public place: string,
  ) {}
}



