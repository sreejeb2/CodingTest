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
  petsByOwnerGender = {};
  ownerGender: string[];

  public constructor(@Inject(PetService) private petService: PetService) {
  }

  ngOnInit() {
    this.getCatsAndOwners();
  }

  public getCatsAndOwners() {
    this.petService.getPets('Cat').subscribe((result: any) => {
      // Result can be get into a model if required. Not doing this as we are interested in only one property
      this.petsByOwnerGender = result.petsByOwnerGender;
      // Key represent owner gender
      this.ownerGender = Object.keys(this.petsByOwnerGender);
    });
  }
}
