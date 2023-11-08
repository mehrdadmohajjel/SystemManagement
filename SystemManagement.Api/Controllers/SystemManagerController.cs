using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SystemManagement.Api.Business;
using SystemManagement.Api.Filters;
using SystemManagement.Shared.Models.Input.SystemManager;
using SystemManagement.Shared.Models.Output.SystemManager;

namespace SystemManagement.Api.Controllers
{
    [RoutePrefix("SystemManager")]
    public class SystemManagerController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///ثبت سیستم جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddNewSystem")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddNewSystem(InputAddNewSystem input)
        {
            const string storedProcedureName = "[AA].[prc_AddNewSystem]";

            var result =
                _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);


            if(!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            result = "سیستم به شماره<b>: " + result + "  </b>با موفقیت ایجاد شد.";
            return Ok(result);

        }

        /// <summary>
        ///ثبت فرم جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AssignFormToSystem")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AssignFormToSystem(InputAssignFormToSystem input)
        {
            const string storedProcedureName = "[AA].[prc_AssignFormToSystem]";

            var result =
                _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            result = "با موفقیت انجام شد";
            return Ok(result);
        }
        
        /// <summary>
        ///ثبت رویداد جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AssignEventToForm")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AssignEventToForm(InputAssignEventToForm input)
        {
            const string storedProcedureName = "[AA].[prc_AssignEventToForm]";

            var result =
                _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            result = "با موفقیت انجام شد";
            return Ok(result);
        }

        /// <summary>
        ///ویرایش سیستم جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditSystem")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult EditSystem(InputEditSystem input)
        {
            const string storedProcedureName = "[AA].[prc_EditSystem]";


            var result =
                _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            result = "با موفقیت انجام شد";
            return Ok(result);

        }


        /// <summary>
        ///    تغییر وضعیت
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("ChangeSystemStatus")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult ChangeSystemStatus(InputChangeSystemStatus input)
        {
            const string storedProcedureName = "[AA].[prc_ChangeSystemStatus]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }
        

        /// <summary>
        ///    تغییر وضعیت
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("ChangeSystemFormStatus")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult ChangeSystemFormStatus(InputChangeSystemFormStatus input)
        {
            const string storedProcedureName = "[AA].[prc_ChangeSystemFormStatus]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     دریافت لسیت سیستم ها
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetAllSystemList")]
        [HttpPost]
        public IHttpActionResult GetAllSystemList()
        {
            const string storedProcedureName = "[AA].[prc_GetAllSystemList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetAllSystemList[]>(
                        storedProcedureName);

            return Ok(result);
        }
        /// <summary>
        ///     دریافت لسیت فرم های سیستم
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetSystemFormsBySystemId")]
        [HttpPost]
        public IHttpActionResult GetSystemFormsBySystemId(InputGetSystemFormsBySystemId input)
        {
            const string storedProcedureName = "[AA].[prc_GetSystemFormsBySystemId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetSystemFormsBySystemId, OutputGetSystemFormsBySystemId[]>(
                        storedProcedureName, input);

            return Ok(result);

        }
        
        /// <summary>
        ///     دریافت لسیت رویداد های سیستم
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetFormEventByFormId")]
        [HttpPost]
        public IHttpActionResult GetFormEventByFormId(InputGetFormEventByFormId input)
        {
            const string storedProcedureName = "[AA].[prc_GetFormEventByFormId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetFormEventByFormId, OutputGetFormEventByFormId[]>(
                        storedProcedureName, input);

            return Ok(result);

        }
        
        /// <summary>
        ///    تغییر وضعیت
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("ChangeFormEventStatus")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult ChangeFormEventStatus(InputChangeFormEventStatus input)
        {
            const string storedProcedureName = "[AA].[prc_ChangeFormEventStatus]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }
    }
}
