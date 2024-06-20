using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Core.Model;
using ProgramAplicationAPI.Repository.Interface;
using SendGrid.Helpers.Errors.Model;

namespace ProgramAplicationAPI.Repository.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly Container _container;
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<ApplicationService> _logger;

        //private readonly Microsoft.Azure.Cosmos.Container _container;


        public ApplicationService(CosmosClient cosmosClient, string databaseName, string containerName, ILogger<ApplicationService> logger)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task DeleteApplicationAnswer(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationModelDto> GetApplicationAnswerById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task SubmitApplicationAnswer(string QuestionId, ApplicationModelDto application)
        {
            try
            {
                // Check if the question exists
                var questionQuery = _container.GetItemQueryIterator<QuestionModel>(new QueryDefinition("SELECT * FROM c WHERE c.QuestionId = @QuestionId")
                    .WithParameter("@QuestionId", QuestionId));
                var question = await questionQuery.ReadNextAsync();
                {
                    throw new NotFoundException("Question not found");
                }

                // Create a new answer entity
                var answer = new ApplicationModel
                {
                    Id = Guid.NewGuid().ToString(),
                    QuestionId = QuestionId,
                    Answer = application.Answer
                };

                // Save the answer to the database
                await _container.CreateItemAsync(answer);
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex, "Error submitting application answer");
                throw;
            }
        }

        public async Task UpdateApplicationAnswer(string id, ApplicationModelDto application)
        {
            throw new NotImplementedException();
        }
    }
}
