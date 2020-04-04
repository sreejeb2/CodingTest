using System.Threading.Tasks;
using AGLCodingTest.Application.Models;
using AGLCodingTest.Application.ViewModels;

namespace AGLCodingTest.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PetsViewModel> GetPetsByOwnerGenderAsync(AnimalType petType);
    }
}
