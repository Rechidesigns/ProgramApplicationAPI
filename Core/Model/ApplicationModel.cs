using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using Cassandra.Data.Linq;
using Microsoft.Azure.Documents;


namespace ProgramAplicationAPI.Core.Model
{
    public class ApplicationModel
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("questionid")]
        public string QuestionId { get; set; }

        [JsonProperty("answers")]
        public string Answer { get; set; }

        [PartitionKey]
        public string questionId { get; set; } = Guid.NewGuid().ToString();
    }

    public class AnswerModel
    {
        [JsonProperty("Answers")]
        public List<ApplicationModel> Answers { get; set; }
    }
}

