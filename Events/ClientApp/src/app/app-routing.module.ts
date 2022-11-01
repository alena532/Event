import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./Components/login/login.component";
import {AuthGuard} from "./_helpers/auth.guard";

const routes: Routes = [
  {path: 'admin',loadChildren: ()=> import('./Components/admin/admin.module').then(m=>m.AdminModule),canActivate: [AuthGuard]},
  { path: '', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
