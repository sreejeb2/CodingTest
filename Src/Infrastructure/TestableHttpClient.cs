using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AGLCodingTest.Application.Common.Interfaces;

namespace AGLCodingTest.Infrastructure
{
    public class TestableHttpClient : ITestableHttpClient
    {
        HttpClient client;
        public TestableHttpClient(string baseUri)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUri),
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<string> GetAsync(string resource)
        {
            return await client.GetStringAsync(resource);
        }
    }
}
