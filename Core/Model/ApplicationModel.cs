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

        [JsonProperty("answers")]
        public List<AnswerModel> Answers { get; set; }

        [PartitionKey]
        public string answerId { get; set; } = Guid.NewGuid().ToString();

        
    }



    public class AnswerModel
    {
        [JsonProperty("questionId")]
        public string QuestionId { get; set; }

        [JsonProperty("answer")]
        public object Answer { get; set; }
    }
}

