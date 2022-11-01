import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CreateEventComponent} from "./event/createEvent/create.event.component";
import {AuthGuard} from "../../_helpers/auth.guard";
import {UpdateEventComponent} from "./event/updateEvent/update.event.component";
import {AdminComponent} from "./admin.component";

const routes: Routes = [
  {path: '',component:AdminComponent,canActivate: [AuthGuard]},
  {path: 'create',component:CreateEventComponent,canActivate: [AuthGuard]},
  {path: 'update/:id',component:UpdateEventComponent,canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
