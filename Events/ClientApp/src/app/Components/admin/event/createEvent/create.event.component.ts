import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {EventsService} from "../../../../_services/events.service";
import {formatDate, Time} from "@angular/common";
import {CreateEvent} from "../../../../_models/Create-event";
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Speaker} from "../../../../_models/Speaker";
import {Company} from "../../../../_models/Company";
import {CompaniesService} from "../../../../_services/companies.service";
import {SpeakersService} from "../../../../_services/speakers.service";

@Component({
  selector: 'app-adm',
  providers: [EventsService,CompaniesService,SpeakersService],
  templateUrl: './create.event.component.html',
  styleUrls: ['./create.event.component.css']
})
export class CreateEventComponent implements OnInit {
  eventCreateForm: FormGroup;
  submitted = false;
  speakers: Speaker[] | undefined ;
  companies: Company[] | undefined;
  currentTime: Date = new Date(Date.now())

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private eventsService: EventsService,
    private companiesService: CompaniesService,
    private speakersService: SpeakersService
  )

  {
    this.speakersService.getAll().subscribe(speakers =>
    this.speakers = speakers);

    this.companiesService.getAll().subscribe(companies =>
      this.companies = companies);

    this.eventCreateForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      plan: ['', Validators.required],
      speakerId: ['', Validators.required],
      companyId: ['', Validators.required],
      date: [formatDate(this.currentTime, 'yyyy-MM-dd', 'en'), [Validators.required]],
      time: [new Date().toLocaleTimeString([],{hour: '2-digit', minute:'2-digit'}), [Validators.required]],
      place: ['', Validators.required],
    });
  }

  get f() { return this.eventCreateForm.controls; }

  ngOnInit(): void {
  }

  onSubmit() {
    this.submitted = true;

    if (this.eventCreateForm.invalid) {
      return;
    }

    let currentEvent = new CreateEvent(this.f['title'].value,this.f['description'].value,this.f['plan'].value,this.f['speakerId'].value,this.f['companyId'].value,this.f['date'].value+'T'+this.f['time'].value,this.f['place'].value);

    this.eventsService.create(currentEvent).subscribe();
      this.router.navigate(['admin']);
  }
}
