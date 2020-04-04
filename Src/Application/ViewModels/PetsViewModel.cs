using System;
using System.Collections.Generic;
using System.Text;
using AGLCodingTest.Application.Models;

namespace AGLCodingTest.Application.ViewModels
{
    public class PetsViewModel
    {
        public PetsViewModel()
        {
            //this.PetsWithMaleOwners = new List<Pet>();
            //this.PetsWithFemaleOwners = new List<Pet>();
        }

        public PetsViewModel(IEnumerable<Pet> petsWithMaleOwners, IEnumerable<Pet> petsWithFemaleOwners)
        {
            //this.PetsWithMaleOwners = petsWithMaleOwners;
            //this.PetsWithFemaleOwners = petsWithFemaleOwners;
            this.PetsByOwnerGender = new Dictionary<string, IEnumerable<Pet>>();
            this.PetsByOwnerGender.Add("Male", petsWithMaleOwners);
            this.PetsByOwnerGender.Add("Female", petsWithFemaleOwners);
        }

        //public IEnumerable<Pet> PetsWithMaleOwners { get; }

        //public IEnumerable<Pet> PetsWithFemaleOwners { get; }


        public Dictionary<string, IEnumerable<Pet>> PetsByOwnerGender { get; }
    }
}
