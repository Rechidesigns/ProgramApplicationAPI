using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Repository.Interface;

namespace ProgramAplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _service;

        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetQuestionAsync(string questionId)
        {
            var question = await _service.GetQuestionAsync(questionId);
            return Ok(question);
        }

        [HttpPost("{questionId}")]
        public async Task<IActionResult> SubmitApplicationAsync(string questionId, ApplicationDto application)
        {
            await _service.SubmitApplicationAsync(questionId, application);
            return Ok("Application submitted successfully");
        }
    }
}
