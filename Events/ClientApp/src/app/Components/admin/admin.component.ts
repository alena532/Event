import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import { first } from 'rxjs/operators';
import {EventsService} from "../../_services/events.service";
import {Event} from "../../_models/Event"

@Component({
  selector: 'app-admin',
  providers: [EventsService],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  events: Event[] | undefined;

  constructor(
    private router: Router,
    private eventsService: EventsService
  ){}

  ngOnInit(): void {
    this.getAll()
  }

  getAll(){
    this.eventsService.getAll().subscribe(events =>
      this.events = events
    )
  }

  deleteEvent(id: number): void{
    this.eventsService.delete(id).pipe(first()).subscribe(() => {
        this.events = this.events!.filter(x => x.id !== id)
      }
    );

    this.getAll()
  }
}
