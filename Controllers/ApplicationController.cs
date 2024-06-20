using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramAplicationAPI.Core.Dtos;
using ProgramAplicationAPI.Repository.Interface;
using ProgramAplicationAPI.Repository.Services;
using SendGrid.Helpers.Errors.Model;

namespace ProgramAplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [Route("submit-answer")]
        public async Task<IActionResult> SubmitAnswer([FromBody] ApplicationModelDto application)
        {
            try
            {
                await _applicationService.SubmitApplicationAnswer(application.QuestionId, application);
                return Ok("Answer submitted successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
