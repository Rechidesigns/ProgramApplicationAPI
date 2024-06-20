using ProgramAplicationAPI.Core.Model;
using ProgramAplicationAPI.Repository.Interface;

namespace ProgramAplicationAPI.Repository.Services
{
    public class ApplicationService
    {
        private readonly IApplicationService _repository;

        public ApplicationService(IApplicationService repository)
        {
            _repository = repository;
        }

        public async Task<QuestionModel> GetQuestionAsync(string questionId)
        {
            return await _repository.GetQuestionAsync(questionId);
        }

        public async Task SubmitApplicationAsync(string questionId, ApplicationModel application)
        {
            var question = await GetQuestionAsync(questionId);
            if (question != null)
            {
                await _repository.SubmitApplicationAsync(questionId, application);
            }
            else
            {
                
            }
        }
    }
}
