using App.Common.Dtos.Base;
using App.Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentsController : Controller
    {
        private readonly IResponseModel _responseModel;
        public AssessmentsController(IConfiguration config, IResponseModel responseModel)
        {
            _responseModel = responseModel;
        }


        //[HttpGet("GetAsessmentAnswers ")]
        //public async Task<ResponseModel> GetAsessmentAnswers()
        //{

        //}

    }
}
