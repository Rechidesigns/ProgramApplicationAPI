using ProgramAplicationAPI.Core.Dtos;

namespace ProgramAplicationAPI.Repository.Interface
{
    public interface IQuestionService
    {
        Task<QuestionDto> GetQuestionAsync(string id, string questionId);
        Task<List<QuestionDto>> GetQuestionsAsync();
        Task<QuestionDto> CreateQuestionAsync(QuestionDto question);
        Task<QuestionDto> UpdateQuestionAsync(QuestionDto question);
        Task DeleteQuestionAsync(string id, string questionId);

    }
}
