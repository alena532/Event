import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {LoginComponent} from "./Components/login/login.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AuthService} from "./_services/auth.service";
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule, HttpHandler, HttpHeaders} from "@angular/common/http";
import {JwtInterceptor} from "./_helpers/jwt.interceptor";
import {AdminComponent} from "./Components/admin/admin.component";
import {EventsService} from "./_services/events.service";
import {CreateEventComponent} from "./Components/admin/event/createEvent/create.event.component";
import {CompaniesService} from "./_services/companies.service";
import {SpeakersService} from "./_services/speakers.service";
import {UpdateEventComponent} from "./Components/admin/event/updateEvent/update.event.component";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    AuthService,
    EventsService,
    CompaniesService,
    SpeakersService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
