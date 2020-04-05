using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGLCodingTest.Application.Common.Interfaces;
using AGLCodingTest.Application.Common.Models;
using AGLCodingTest.Application.Interfaces;
using AGLCodingTest.Application.Models;
using AGLCodingTest.Application.ViewModels;
using Newtonsoft.Json;

namespace AGLCodingTest.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        private readonly ITestableHttpClient client;

        public PersonService(ITestableHttpClient client)
        {
            this.client = client;
        }

        public async Task<PetsViewModel> GetPetsByOwnerGenderAsync(AnimalType petType)
        {
            var responseText = await client.GetAsync("people.json");
            var people = JsonConvert.DeserializeObject<IList<Person>>(responseText);

            var petsWithMaleOwners = people.Where(person => person.Gender == Gender.Male && person.Pets != null && person.Pets.Any(pet => pet.Type == petType))
                .SelectMany(owner => owner.Pets.Where(pet => pet.Type == petType))
                .OrderBy(pet => pet.Name);
            var petsWithFemaleOwners = people.Where(person => person.Gender == Gender.Female && person.Pets != null && person.Pets.Any(pet => pet.Type == petType))
                .SelectMany(owner => owner.Pets.Where(pet => pet.Type == petType))
                .OrderBy(pet => pet.Name);

            var model = new PetsViewModel(petsWithMaleOwners, petsWithFemaleOwners);

            return model;
        }
    }
}
