using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Repository;
using ProgramAplicationAPI.Repository.Interface;

namespace ProgramAplicationAPI.Repository.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly Container _questionContainer;
        //private readonly Microsoft.Azure.Cosmos.Container _questionContainer;


        public QuestionService(CosmosClient cosmosClient, string databaseName, string containerName )
        {
            _questionContainer = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<QuestionDto> CreateQuestionAsync(QuestionDto question)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionDto> GetQuestionAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionDto>> GetQuestionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionDto> UpdateQuestionAsync(QuestionDto question)
        {
            throw new NotImplementedException();
        }
    }
}
