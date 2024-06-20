using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramAplicationAPI.Core.Dtos;
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
        public async Task<ActionResult<QuestionDto>> CreateQuestion(QuestionDto questionDto)
        {
            return await _questionService.CreateQuestionAsync(questionDto);
        }

        [HttpPut]
        public async Task<ActionResult<QuestionDto>> UpdateQuestion(QuestionDto questionDto)
        {
            return await _questionService.UpdateQuestionAsync(questionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }
    }
}
