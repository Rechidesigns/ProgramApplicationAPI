using Newtonsoft.Json;

namespace ProgramAplicationAPI.Core.Model
{
    public class ApplicationModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("candidateName")]
        public string CandidateName { get; set; }

        [JsonProperty("candidateEmail")]
        public string CandidateEmail { get; set; }

        [JsonProperty("answers")]
        public List<AnswerModel> Answers { get; set; }
    }



    public class AnswerModel
    {
        [JsonProperty("questionId")]
        public string QuestionId { get; set; }

        [JsonProperty("answer")]
        public object Answer { get; set; }
    }
}

