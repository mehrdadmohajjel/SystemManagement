using System.Web.Http;
using SystemManagement.Api.Business;
using SystemManagement.Api.Filters;
using SystemManagement.Shared.Models.Input.AssignFormToUser;
using SystemManagement.Shared.Models.Input.SystemManager;
using SystemManagement.Shared.Models.Output.AssignFormToUser;

namespace SystemManagement.Api.Controllers
{
    [RoutePrefix("AssignFormToUser")]
    public class AssignFormToUserController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///ثبت مجوز
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddUserAccessList")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddUserAccessList(InputAddUserAccessList input)
        {
            const string storedProcedureName = "[AA].[prc_AddUserAccessList]";

            var result =
                _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);


            if(!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            result = string.Empty;
            return Ok(result);

        }
        

        /// <summary>
        ///   دریافت لیست امورها
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetAllMainDepartment")]
        [HttpPost]
        public IHttpActionResult GetAllMainDepartment()
        {
            const string storedProcedureName = "[HRS].[prc_GetAllMainDepartment]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetAllMainDepartment[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///    دریافت لسیت زیر دپارتمان ها بر اساس شناسه دپارتمان اصلی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetSubDepartmentListByMainDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetSubDepartmentListByMainDepartmentId(InputGetSubDepartmentListByMainDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetSubDepartmentListByMainDepartmentId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetSubDepartmentListByMainDepartmentId, OutputGetSubDepartmentListByMainDepartmentId[]>(
                        storedProcedureName, input);

            return Ok(result);

        }
        /// <summary>
        ///    دریافت لسیت پرسنل دپارتمان
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetEmployeeListByDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetEmployeeListByDepartmentId(InputGetEmployeeListByDepartmentId input)
        {
            const string storedProcedureName = "[HRS].[prc_GetEmployeeListByDepartmentId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetEmployeeListByDepartmentId, OutputGetEmployeeListByDepartmentId[]>(
                        storedProcedureName, input);

            return Ok(result);

        }

        /// <summary>
        ///    دریافت لسیت پرسنل دپارتمان
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetUserAccessList")]
        [HttpPost]
        public IHttpActionResult GetUserAccessList(InputGetUserAccessList input)
        {
            const string storedProcedureName = "[AA].[prc_GetUserAccessList]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetUserAccessList, OutputGetUserAccessList[]>(
                        storedProcedureName, input);

            return Ok(result);

        }
        

    }
}
