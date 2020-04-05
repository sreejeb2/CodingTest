import { Injectable, Inject, Optional } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class PetService {

  public constructor(@Inject(HttpClient) private httpClient: HttpClient, @Optional() @Inject('BASE_URL') private baseUrl?: string) {
  }

  private petsApiUrl = '/api/person/pet/';

  public getPets(petType: string) {
    return this.httpClient.get(`${this.baseUrl}/${this.petsApiUrl}/${petType}`);
  }
};
