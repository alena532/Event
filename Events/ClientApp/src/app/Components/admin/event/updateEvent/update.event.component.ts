import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {EventsService} from "../../../../_services/events.service";
import {formatDate, Time} from "@angular/common";
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {CompaniesService} from "../../../../_services/companies.service";
import {SpeakersService} from "../../../../_services/speakers.service";
import {UpdateEvent} from "../../../../_models/Update-event";

@Component({
  selector: 'app-adm',
  providers: [EventsService,CompaniesService,SpeakersService],
  templateUrl: './update.event.component.html',
  styleUrls: ['./update.event.component.css']
})
export class UpdateEventComponent implements OnInit {
  id!: number;
  eventUpdateForm: FormGroup;
  submitted = false;
  currentTime: Date = new Date(Date.now())

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private eventsService: EventsService,
  )
  {
    this.eventUpdateForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      plan: ['', Validators.required],
      date: [formatDate(this.currentTime, 'yyyy-MM-dd', 'en'), [Validators.required]],
      time: [new Date().toLocaleTimeString([],{hour: '2-digit', minute:'2-digit'}), [Validators.required]],
      place: ['', Validators.required],
    });
  }

  get f() { return this.eventUpdateForm.controls; }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.eventsService.getById(this.id).subscribe(event => {
        this.eventUpdateForm.patchValue(event)
      }
    )
  }

  onSubmit() {
    this.submitted = true;

    if (this.eventUpdateForm.invalid) {
      return;
    }

    let eventForUpdate = new UpdateEvent(this.f['title'].value,this.f['description'].value,this.f['plan'].value,this.f['date'].value+'T'+this.f['time'].value,this.f['place'].value);

    this.eventsService.update(this.id,eventForUpdate).subscribe();

    this.router.navigate(['admin'])
  }
}
