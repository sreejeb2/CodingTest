using System.Threading.Tasks;

namespace AGLCodingTest.Application.Common.Interfaces
{
    public interface ITestableHttpClient
    {
        Task<string> GetAsync(string resource);
    }
}
