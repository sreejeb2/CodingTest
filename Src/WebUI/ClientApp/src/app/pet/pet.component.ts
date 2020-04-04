import { Component, OnInit, Inject } from '@angular/core';
import { PetService } from './pet.service';

@Component({
  selector: 'pet-list',
  templateUrl: './pet.component.html',
  providers: [
    PetService
  ]
})
export class PetComponent implements OnInit {

  title = 'List of cats in alphabetical order under a heading of the gender of their owner';
  pets = {};
  ownerGender: string[];

  constructor(@Inject(PetService) private petService: PetService) { }

  ngOnInit() {
    this.getCatsAndOwners();
  }

  public getCatsAndOwners() {
    this.petService.getPets('Cat').subscribe((result: any)=> {
      this.pets = result.petsByOwnerGender;
      this.ownerGender = Object.keys(this.pets);
    });
  }
}
