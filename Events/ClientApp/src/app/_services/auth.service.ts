import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {map} from "rxjs";
import {User} from "../_models/User";

@Injectable()
export class AuthService {

  private accessPointUrl: string = 'https://localhost:7292/api/Auth';

  currentUser: User | undefined;
  constructor(private http: HttpClient) {

  }

  login(username: string, password: string) {
    return this.http.post<any>(this.accessPointUrl+'/Login', { username, password }).
      pipe(map(data => {
        let userInfo = data["user"];
        this.currentUser = new User(userInfo["id"],userInfo["role"],userInfo["userName"],userInfo["firstName"],userInfo["lastName"],data["token"]);
        localStorage.setItem('auth_user',JSON.stringify(this.currentUser));

      }))
  }

  isLoggedIn(): boolean{
    if(this.currentUser) return true;
    return false;
  }

  logout(){
    localStorage.removeItem('auth_user');
  }

  getRole(){
    if (this.currentUser) return this.currentUser.role;
    return undefined;
  }


}
