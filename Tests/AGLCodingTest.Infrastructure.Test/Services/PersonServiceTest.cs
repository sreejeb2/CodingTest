using System;
using System.Linq;
using System.Threading.Tasks;
using AGLCodingTest.Application.Common.Interfaces;
using AGLCodingTest.Application.Models;
using AGLCodingTest.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace AGLCodingTest.Infrastructure.Test.Services
{
    public class PersonServiceTest
    {
        private Mock<ITestableHttpClient> mockHttpClient;

        [SetUp]
        public void Setup()
        {
            mockHttpClient = new Mock<ITestableHttpClient>(MockBehavior.Strict);
        }

        [Test]
        public async Task ShouldAlwaysHaveMaleAndFemaleResult()
        {
            // Arrange
            mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>())).Returns(Task.FromResult("[]"));
            var personService = new PersonService(mockHttpClient.Object);

            // Act
            var catsByGenderOwner = await personService.GetPetsByOwnerGenderAsync(AnimalType.Cat);

            // Assert
            mockHttpClient.Verify(mock => mock.GetAsync("people.json"), Times.Once());
            Assert.IsNotNull(catsByGenderOwner);
            Assert.AreEqual(2, catsByGenderOwner.PetsByOwnerGender.Count);
            Assert.IsEmpty(catsByGenderOwner.PetsByOwnerGender["Male"]);
            Assert.IsEmpty(catsByGenderOwner.PetsByOwnerGender["Female"]);
        }

        [Test]
        public async Task ShouldReturnCorrectMaleCountEvenIfFemaleIsMissing()
        {
            // Arrange
            mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(@"[{
            ""gender"": ""Male"",
            ""pets"": [
                {
                    ""name"": ""Garfield"",
                    ""type"": ""Cat""
                },
                {
                    ""name"": ""Fido"",
                    ""type"": ""Dog""
                }
            ]
            }]"));

            var personService = new PersonService(mockHttpClient.Object);

            // Act
            var catsByGenderOwner = await personService.GetPetsByOwnerGenderAsync(AnimalType.Cat);

            // Asserts
            mockHttpClient.Verify(mock => mock.GetAsync("people.json"), Times.Once());
            Assert.IsNotNull(catsByGenderOwner);
            Assert.AreEqual(2, catsByGenderOwner.PetsByOwnerGender.Count);
            Assert.AreEqual(1, catsByGenderOwner.PetsByOwnerGender["Male"].Count());
            Assert.IsEmpty(catsByGenderOwner.PetsByOwnerGender["Female"]);
        }

        [Test]
        public async Task ShouldReturnCorrectCountWhenSamePersonHaveMultipleCats()
        {
            mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(@"[{
            ""gender"": ""Female"",
            ""pets"": [
                {
                    ""name"": ""Garfield"",
                    ""type"": ""Cat""
                },
                {
                    ""name"": ""Fido"",
                    ""type"": ""Cat""
                }
            ]
            }]"));

            var personService = new PersonService(mockHttpClient.Object);

            var catsByGenderOwner = await personService.GetPetsByOwnerGenderAsync(AnimalType.Cat);

            mockHttpClient.Verify(mock => mock.GetAsync("people.json"), Times.Once());
            Assert.IsNotNull(catsByGenderOwner);
            Assert.AreEqual(2, catsByGenderOwner.PetsByOwnerGender.Count);
            Assert.AreEqual(2, catsByGenderOwner.PetsByOwnerGender["Female"].Count());
            Assert.IsEmpty(catsByGenderOwner.PetsByOwnerGender["Male"]);
        }

        [Test]
        public async Task ShouldReturnCorrectCountWhenOwnersDontHaveCats()
        {
            mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(@"[{
            ""gender"": ""Female"",
            ""pets"": [
                {
                    ""name"": ""Garfield"",
                    ""type"": ""Dog""
                },
                {
                    ""name"": ""Fido"",
                    ""type"": ""Dog""
                }
            ]
            }]"));

            var personService = new PersonService(mockHttpClient.Object);

            var catsByGenderOwner = await personService.GetPetsByOwnerGenderAsync(AnimalType.Cat);

            mockHttpClient.Verify(mock => mock.GetAsync("people.json"), Times.Once());
            Assert.IsNotNull(catsByGenderOwner);
            Assert.AreEqual(2, catsByGenderOwner.PetsByOwnerGender.Count);
            Assert.IsEmpty(catsByGenderOwner.PetsByOwnerGender["Male"]);
            Assert.IsEmpty(catsByGenderOwner.PetsByOwnerGender["Female"]);
        }

        [Test]
        public async Task ShouldReturnCorrectCatsCountEvenIfPetsHaveSameName()
        {
            mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(@"[{
            ""gender"": ""Female"",
            ""pets"": [
                {
                    ""name"": ""Garfield"",
                    ""type"": ""Dog""
                }
            ]
            },{
            ""gender"": ""Female"",
            ""pets"": [
                {
                    ""name"": ""Simba"",
                    ""type"": ""Cat""
                }
            ]
            },{
            ""gender"": ""Male"",
            ""pets"": [
                {
                    ""name"": ""Nemo"",
                    ""type"": ""Fish""
                }
            ]
            },{
            ""gender"": ""Male"",
            ""pets"": [
                {
                    ""name"": ""Simba"",
                    ""type"": ""Cat""
                }
            ]
            }]"));

            var personService = new PersonService(mockHttpClient.Object);

            var catsByGenderOwner = await personService.GetPetsByOwnerGenderAsync(AnimalType.Cat);

            mockHttpClient.Verify(mock => mock.GetAsync("people.json"), Times.Once());
            Assert.IsNotNull(catsByGenderOwner);
            Assert.AreEqual(2, catsByGenderOwner.PetsByOwnerGender.Count);
            Assert.AreEqual(1, catsByGenderOwner.PetsByOwnerGender["Male"].Count());
            Assert.AreEqual(1, catsByGenderOwner.PetsByOwnerGender["Female"].Count());
        }
    }
}