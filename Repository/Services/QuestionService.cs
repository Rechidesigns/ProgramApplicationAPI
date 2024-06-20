using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Core.Model;
using ProgramAplicationAPI.Repository;
using ProgramAplicationAPI.Repository.Interface;

namespace ProgramAplicationAPI.Repository.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly Container _container;
        private readonly CosmosClient _cosmosClient;
        //private readonly Microsoft.Azure.Cosmos.Container _container;


        public QuestionService(CosmosClient cosmosClient, string databaseName, string containerName )
        {
            _cosmosClient = cosmosClient;
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<QuestionDto> CreateQuestionAsync(QuestionDto questionDto)
        {
            var questionModel = MapQuestionDtoToModel(questionDto);
            await _container.CreateItemAsync(questionModel);
            return questionDto;
        }

        public async Task DeleteQuestionAsync(string id)
        {
            await _container.DeleteItemAsync<QuestionModel>(id, new PartitionKey(id));
        }

        public async Task<QuestionDto> GetQuestionAsync(string id, string questionId)
        {
            try
            {
                var question = await _container.ReadItemAsync<QuestionModel>(id, new PartitionKey(questionId));
                //var question = await _container.ReadItemAsync<QuestionModel>(id, new PartitionKey(question.QuestionId));

                return MapQuestionModelToDto(question);
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error getting question: {ex.Message}");
            }
        }

        public async Task<List<QuestionDto>> GetQuestionsAsync()
        {
            var questions = new List<QuestionModel>();
            var iterator = _container.GetItemQueryIterator<QuestionModel>();
            while (iterator.HasMoreResults)
            {
                var questionModel = await iterator.ReadNextAsync();
                questions.AddRange(questionModel);
            }
            return questions.Select(MapQuestionModelToDto).ToList();
        }
        public async Task<QuestionDto> UpdateQuestionAsync(QuestionDto questionDto)
        {
            var questionModel = MapQuestionDtoToModel(questionDto);
            await _container.UpsertItemAsync(questionModel);
            return questionDto;
        }

        private QuestionDto MapQuestionModelToDto(QuestionModel questionModel)
        {
            return new QuestionDto
            {
                QuestionText = questionModel.QuestionText,
                QuestionType = questionModel.QuestionType,
                IsRequired = questionModel.IsRequired,
                IsInternal = questionModel.IsInternal,
                DataType = questionModel.DataType,
                Choices = questionModel.Choices.Select(c => new ChoiceDto { ChoiceText = c.ChoiceText }).ToList()
            };
        }

        private QuestionModel MapQuestionDtoToModel(QuestionDto questionDto)
        {
            return new QuestionModel
            {
                //id = questionDto.id,
                QuestionText = questionDto.QuestionText,
                QuestionType = questionDto.QuestionType,
                IsRequired = questionDto.IsRequired,
                IsInternal = questionDto.IsInternal,
                DataType = questionDto.DataType,
                Choices = questionDto.Choices.Select(c => new ChoiceModel { ChoiceText = c.ChoiceText }).ToList()
            };
        }
    }
}
