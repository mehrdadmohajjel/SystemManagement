using System.Web.Http;
using SystemManagement.Api.Business;
using SystemManagement.Api.Filters;
using SystemManagement.Shared.Models.Input.Sample;
using SystemManagement.Shared.Models.Output.Sample;

namespace SystemManagement.Api.Controllers
{
    public class SampleController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        /// ثبت به همراه بررسی پیغام خطای اعتبارسنجی سمت پروسیجر
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddWithReturnMessage")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddSomething(InputAddWithReturnMessage input)
        {
            const string storedProcedureName = "[Schema].[prc_AddSomething]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if (!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///  ثبت بدون ورودی 
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("ChangeSomethingWithoutInput")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult ChangeSomethingWithoutInput()
        {
            const string storedProcedureName = "[Schema].[prc_ChangeSomethingWithoutInput]";

            _ = _businessManager.CallStoredProcedure(storedProcedureName);

            return Ok("با موفقیت ثبت شد");
        }

        /// <summary>
        /// دریافت لیست همه
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAll")]
        [RequestModelNullValidation]
        [RequestModelValidation]
        public IHttpActionResult GetAll()
        {
            const string storedProcedureName = "[Schema].[prc_GetAll]";

            var result = _businessManager.CallStoredProcedure<OutputGetAll[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// دریافت با ورودی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetByInput")]
        [HttpPost]
        public IHttpActionResult GetByInput(InputGetByInput input)
        {
            const string storedProcedureName = "[Schema].[prc_GetByInput]";

            var result =
                _businessManager.CallStoredProcedure<InputGetByInput, OutputGetByInput[]>
                    (storedProcedureName, input);

            return Ok(result);
        }
    }
}
