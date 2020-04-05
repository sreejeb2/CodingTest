using System.Collections.Generic;
using AGLCodingTest.Application.Models;

namespace AGLCodingTest.Application.ViewModels
{
    /// <summary>
    /// Used to pass presentable form of data
    /// </summary>
    public class PetsViewModel
    {
        public PetsViewModel(IEnumerable<Pet> petsWithMaleOwners, IEnumerable<Pet> petsWithFemaleOwners)
        {
            this.PetsByOwnerGender = new Dictionary<string, IEnumerable<Pet>>();
            this.PetsByOwnerGender.Add("Male", petsWithMaleOwners);
            this.PetsByOwnerGender.Add("Female", petsWithFemaleOwners);
        }

        /// <summary>
        /// Holds all the pets using owner gender as the key
        /// </summary>
        public Dictionary<string, IEnumerable<Pet>> PetsByOwnerGender { get; }
    }
}
