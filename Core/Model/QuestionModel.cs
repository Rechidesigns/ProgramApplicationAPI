using Microsoft.Azure.Cosmos;
using Cassandra.Data.Linq;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;


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

        [JsonProperty("dataType")]
        public DataType DataType { get; set; }

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

    public enum DataType
    {
        String,
        Boolean,
        Number,
        Date
    }

    public class ChoiceModel
    {
        [JsonProperty("choiceText")]
        public string ChoiceText { get; set; }
    }
}