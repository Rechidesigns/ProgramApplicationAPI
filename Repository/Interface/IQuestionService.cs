using Microsoft.AspNetCore.Mvc;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Core.Model;

namespace ProgramAplicationAPI.Repository.Interface
{
    public interface IQuestionService
    {
        Task<QuestionDto> GetQuestionAsync(string id, string questionId);
        Task<List<QuestionDto>> GetQuestionsAsync();
        //Task<QuestionDto> CreateQuestionAsync(QuestionDto question);
        Task<List<QuestionDto>> CreateQuestionsAsync(List<QuestionDto> questionsDto);
        Task<QuestionDto> UpdateQuestionAsync(string id, string questionId, QuestionDto question);
        Task DeleteQuestionAsync(string id, string questionId);

    }
}
