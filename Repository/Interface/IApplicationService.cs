using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Core.Model;

namespace ProgramAplicationAPI.Repository.Interface
{
    public interface IApplicationService
    {

        Task SubmitApplicationAnswer(string QuestionId, ApplicationModelDto application);
        Task<ApplicationModelDto> GetApplicationAnswerById(string id);
        Task UpdateApplicationAnswer(string id, ApplicationModelDto application);
        Task DeleteApplicationAnswer(string id);
    }
}
