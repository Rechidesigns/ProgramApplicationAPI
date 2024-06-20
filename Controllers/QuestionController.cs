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

        //[HttpPost]
        //public async Task<ActionResult<QuestionDto>> CreateQuestion(List<QuestionDto> questionDtos)
        //{
        //    return await _questionService.CreateQuestionsAsync(questionDtos);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(QuestionDto questionDto)
        {
            try
            {
                var questionTypeEnum = (QuestionType)Enum.Parse(typeof(QuestionType), questionDto.QuestionType);
                if (!Enum.IsDefined(typeof(QuestionType), questionTypeEnum))
                {
                    return BadRequest("Enum doesn't exist");
                }
                var result = await _questionService.CreateQuestionAsync(questionDto);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
