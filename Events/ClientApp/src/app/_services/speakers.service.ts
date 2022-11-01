import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SpeakersService {

  private accessPointUrl: string = 'https://localhost:7292/api/admin/Speakers';

  constructor(private http: HttpClient) {
  }

  getAll() {
    return this.http.get<any>(this.accessPointUrl);
  }
}




