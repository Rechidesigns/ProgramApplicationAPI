using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Core.Model;
using ProgramAplicationAPI.Repository.Interface;

namespace ProgramAplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionDto>>> GetQuestions()
        {
            return await _questionService.GetQuestionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(string id, string questionId)
        {
            return await _questionService.GetQuestionAsync(id, questionId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestions(List<QuestionDto> questions)
        {
            var result = await _questionService.CreateQuestionsAsync(questions);
            return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult<QuestionDto>> UpdateQuestion(string id, string questionId, QuestionDto questionDto)
        {
            return await _questionService.UpdateQuestionAsync(id, questionId, questionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id, string questionId)
        {
            await _questionService.DeleteQuestionAsync(id, questionId);
            return NoContent();
        }
    }
}
