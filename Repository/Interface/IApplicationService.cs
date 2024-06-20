using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Core.Model;

namespace ProgramAplicationAPI.Repository.Interface
{
    public interface IApplicationService
    {
        Task<QuestionModel> GetQuestionAsync(string questionId);
        Task<List<QuestionModel>> GetQuestionsAsync();
        Task SubmitApplicationAsync(string questionId, ApplicationModel application);
        Task SubmitApplicationAsync(string questionId, ApplicationDto application);
    }
}
