using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Core.Model;
using ProgramAplicationAPI.Repository;
using ProgramAplicationAPI.Repository.Interface;
using SendGrid.Helpers.Errors.Model;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Office.Interop.Excel;
using Microsoft.Extensions.Logging;
using Cassandra;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProgramAplicationAPI.Repository.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly Container _container;
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger<QuestionService> _logger;

        //private readonly Microsoft.Azure.Cosmos.Container _container;


        public QuestionService(CosmosClient cosmosClient, string databaseName, string containerName, ILogger<QuestionService> logger )
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        //public async Task<QuestionDto> CreateQuestionAsync(QuestionDto questionDto)
        //{
        //    var questionModel = MapQuestionDtoToModel(questionDto);
        //    await _container.CreateItemAsync(questionModel);
        //    return questionDto;
        //}

        //public async Task<QuestionDto> CreateQuestionAsync(QuestionDto questionDto)
        //{
        //    var questionModel = MapQuestionDtoToModel(questionDto);

        //    var validationResult = await new QuestionModelValidator().ValidateAsync(questionModel);

        //    if (!validationResult.IsValid)
        //    {
        //        // Handle validation errors, e.g., throw an exception or return an error response
        //        throw new ValidationException(validationResult.Errors);
        //    }

        //    await _container.CreateItemAsync(questionModel);
        //    return questionDto;
        //}


        public async Task<List<QuestionDto>> CreateQuestionsAsync(List<QuestionDto> questionsDto)
        {
            var questionsModel = new List<QuestionModel>();

            foreach (var questionDto in questionsDto)
            {
                var questionModel = MapQuestionDtoToModel(questionDto);
                questionsModel.Add(questionModel);
            }

            var validationResult = await new QuestionModelValidator().ValidateAsync(questionsModel);

            if (!validationResult.IsValid)
            {
                // Handle validation errors, e.g., throw an exception or return an error response
                throw new ValidationException(validationResult.Errors);
            }

            await CreateItemsAsync(questionsModel);
            return questionsDto;
        }

        private async Task CreateItemsAsync(List<QuestionModel> questionsModel)
        {
            foreach (var question in questionsModel)
            {
                await _container.CreateItemAsync(question);
            }
        }
        public async Task DeleteQuestionAsync(string id, string questionId)
        {
            await _container.DeleteItemAsync<QuestionModel>(id, new PartitionKey(questionId));
        }

        public async Task<QuestionDto> GetQuestionAsync(string id, string questionId)
        {
            try
            {
                var question = await _container.ReadItemAsync<QuestionModel>(id, new PartitionKey(questionId));

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
        public async Task<QuestionDto> UpdateQuestionAsync(string id, string questionId, QuestionDto questionDto)
        {
            var questionResponse = await _container.ReadItemAsync<QuestionModel>(id, new PartitionKey(questionId));
            var questionModel = questionResponse.Resource;

            if (questionModel != null)
            {
                var questionModelUpdated = MapQuestionDtoToModel(questionDto);
                questionModelUpdated.id = questionModel.id;
                questionModelUpdated.questionId = questionModel.questionId;

                await _container.UpsertItemAsync(questionModelUpdated);

                return MapQuestionModelToDto(questionModelUpdated);
            }
            throw new NotFoundException("Question not found");
        }

        private QuestionDto MapQuestionModelToDto(QuestionModel questionModel)
        {
            return new QuestionDto
            {
                QuestionText = questionModel.QuestionText,
                QuestionType = questionModel.QuestionType.ToString(),
                IsRequired = questionModel.IsRequired,
                IsInternal = questionModel.IsInternal,
                //DataType = questionModel.DataType,
                Choices = questionModel.Choices.Select(c => new ChoiceDto { ChoiceText = c.ChoiceText }).ToList()
            };
        }

        private QuestionModel MapQuestionDtoToModel(QuestionDto questionDto)
        {
            return new QuestionModel
            {
                //id = questionDto.id,
                QuestionText = questionDto.QuestionText,
                QuestionType = (QuestionType)Enum.Parse(typeof(QuestionType), questionDto.QuestionType),
                IsRequired = questionDto.IsRequired,
                IsInternal = questionDto.IsInternal,
               // DataType = questionDto.DataType,
                Choices = questionDto.Choices.Select(c => new ChoiceModel { ChoiceText = c.ChoiceText }).ToList()
            };
        }
    }
}
