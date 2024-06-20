using Microsoft.Azure.Cosmos;
using Cassandra.Data.Linq;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using FluentValidation;



namespace ProgramAplicationAPI.Core.Model
{
    public class QuestionModel
    {
        [JsonProperty("id")] 
        public string id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("questionText")]
        public string QuestionText { get; set; }

        [JsonProperty("questionType")]
        public QuestionType QuestionType { get; set; }

        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }

        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; }

        [JsonProperty("choices")]
        public List<ChoiceModel> Choices { get; set; }

        [PartitionKey]
        public string questionId { get; set; } = Guid.NewGuid().ToString();
    }

    public enum QuestionType
    {
        Paragraph,
        YesNo,
        Dropdown,
        MultipleChoice,
        Date,
        Number
    }

    public class ChoiceModel
    {
        [JsonProperty("choiceText")]
        public string ChoiceText { get; set; }
    }

    //public class QuestionModelValidator : AbstractValidator<QuestionModel>
    //{
    //    public QuestionModelValidator()
    //    {
    //        RuleFor(q => q.QuestionText).NotEmpty();
    //        RuleFor(q => q.QuestionType).IsInEnum();

    //        When(q => q.QuestionType == QuestionType.MultipleChoice || q.QuestionType == QuestionType.Dropdown, () =>
    //        {
    //            RuleFor(q => q.Choices).Must(choices => choices != null && choices.Count >= 2);
    //        });
    //    }
    //}

    public class QuestionModelValidator : AbstractValidator<List<QuestionModel>>
    {
        public QuestionModelValidator()
        {
            RuleForEach(q => q).SetValidator(new QuestionValidator());
        }
    }

    public class QuestionValidator : AbstractValidator<QuestionModel>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.QuestionText).NotEmpty();
            RuleFor(q => q.QuestionType).IsInEnum();

            When(q => q.QuestionType == QuestionType.MultipleChoice || q.QuestionType == QuestionType.Dropdown, () =>
            {
                RuleFor(q => q.Choices).Must(choices => choices != null && choices.Count >= 2);
            });
        }
    }
}
