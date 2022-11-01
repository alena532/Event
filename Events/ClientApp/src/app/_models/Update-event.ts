import {Time} from "@angular/common";

export class UpdateEvent {
  constructor (
    public title: string,
    public description: string,
    public plan: string,
    public time: string,
    public place: string,
  ) {}
}

