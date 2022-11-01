import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class CompaniesService {

  private accessPointUrl: string = 'https://localhost:7292/api/admin/Companies';

  constructor(private http: HttpClient) {
  }

  getAll() {
    return this.http.get<any>(this.accessPointUrl);
  }
}
