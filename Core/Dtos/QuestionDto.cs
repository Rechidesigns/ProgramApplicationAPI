using ProgramAplicationAPI.Core.Model;

namespace ProgramAplicationAPI.Core.Dtos
{
    public class QuestionDto
    {
        public string QuestionText { get; set; }
        //public QuestionType QuestionType { get; set; }
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public bool IsInternal { get; set; }
        public List<ChoiceDto> Choices { get; set; }
    }

    public class ChoiceDto
    {
        public string ChoiceText { get; set; }
    }
}
