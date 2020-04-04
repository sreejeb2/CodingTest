import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';


@Injectable()
export class PetService {

  constructor(@Inject(HttpClient) private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  private petsApiUrl = '/api/person/pet/';

  public getPets(petType: string) {
    return this.httpClient.get(this.baseUrl + this.petsApiUrl + petType);
  }
};
