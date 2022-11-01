import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule, HttpHandler, HttpHeaders} from "@angular/common/http";
import {AdminComponent} from "./admin.component";
import {UpdateEventComponent} from "./event/updateEvent/update.event.component";
import {CreateEventComponent} from "./event/createEvent/create.event.component";
import {JwtInterceptor} from "../../_helpers/jwt.interceptor";
import {AuthService} from "../../_services/auth.service";
import {SpeakersService} from "../../_services/speakers.service";
import {CompaniesService} from "../../_services/companies.service";
import {EventsService} from "../../_services/events.service";
import {AdminRoutingModule} from "./admin-routing.module";
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [
    AdminComponent,
    CreateEventComponent,
    UpdateEventComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    AuthService,
    EventsService,
    CompaniesService,
    SpeakersService
  ]

})
export class AdminModule { }
