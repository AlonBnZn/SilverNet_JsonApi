using JsonApiSerializer;
using JsonApiSerializer.JsonApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SilveNetJsonApiAssignment.Service.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SilverNetJsonApiAssigment.Tests
{
    public abstract class ControllerSpecificationBase : SpecificationBase
    {
        protected TestWebApplicationFactory Factory = null!;

        protected HttpClient Client = null!;

        protected IConfiguration Configuration = null!;

        protected CommandDbContext DbContext = null!;

        string databaseName = "TestDb";

        protected override void Given()
        {
            Factory = new TestWebApplicationFactory();

            Client = Factory.CreateClient();

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));

            IServiceScope scope = Factory.Services.CreateScope();

            DbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();
        }

        protected DocumentRoot<TResource> SendJsonApiRequest<TResource>(HttpMethod method, string url, object? requestBody = null) where TResource : class
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, url);

            if (requestBody != null)
            {
                var json = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = false,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                });

                requestMessage.Content = new StringContent(json, Encoding.UTF8);
                requestMessage.Content.Headers.Clear();
                requestMessage.Content.Headers.Add("Content-Type", "application/vnd.api+json");
            }

            var response = Client.SendAsync(requestMessage).Result;

            var responseJson = response.Content.ReadAsStringAsync().Result;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentRoot<TResource>>(responseJson, new JsonApiSerializerSettings())!;
        }

        protected override void Cleanup()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}
