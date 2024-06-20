﻿using Newtonsoft.Json;

namespace ProgramAplicationAPI.Core.Dtos
{
    public class ApplicationDto
    {
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }


    public class AnswerDto
    {
        public string QuestionId { get; set; }
        public object Answer { get; set; }
    }
}
