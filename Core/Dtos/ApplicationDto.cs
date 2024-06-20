using Newtonsoft.Json;

namespace ProgramAplicationAPI.Core.Dtos
{
    public class ApplicationModelDto
    {
        public string QuestionId { get; set; }
        public string Answer { get; set; }

    }
    public class AnswerModelDto
    {
        public List<ApplicationModelDto> Answers { get; set; }
    }
}
