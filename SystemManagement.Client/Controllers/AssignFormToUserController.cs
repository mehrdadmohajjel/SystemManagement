using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SystemManagement.Client.Api;
using SystemManagement.Shared.Models.Input.AssignFormToUser;
using SystemManagement.Shared.Models.Input.SystemManager;
using SystemManagement.Shared.Models.Output.AssignFormToUser;
using SystemManagement.Shared.Utilities;

namespace SystemManagement.Client.Controllers
{
    public class AssignFormToUserController : BaseController
    {
        public ActionResult AddUserAccessList(InputAddUserAccessList values)
        {
            var token = GetUserToken();
            var apiResult = ApiList.AddUserAccessList(values, token);
            return Content(apiResult);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMainDepartmentList()
        {
            const string partialViewUrl = "~/Views/AssignFormToUser/AddForm/AddFormGetMainDepartmentList.cshtml";

            var apiResult = ApiList.GetAllMainDepartment();
            return PartialView(partialViewUrl, apiResult);

        }

        public ActionResult GetSubDepartmentList(InputGetSubDepartmentListByMainDepartmentId input)
        {
            const string partialViewUrl = "~/Views/AssignFormToUser/AddForm/AddFormGetSubDepartmentList.cshtml";

            var apiResult = ApiList.GetSubDepartmentListByMainDepartmentId(input);
            return PartialView(partialViewUrl, apiResult);

        }

        public ActionResult GetDepartmentEmployeeCheckComboBox(InputGetEmployeeListByDepartmentId input)
        {
            const string partialViewUrl = "~/Views/AssignFormToUser/AddForm/AddFormDepartmentEmployeeCheckComboBox.cshtml";
            var employeeList = ApiList.GetEmployeeListByDepartmentId(input);

            return PartialView(partialViewUrl, employeeList);
        }

        public ActionResult GetAllSystemList()
        {
            const string partialViewUrl = "~/Views/AssignFormToUser/AddForm/AddFormGetAllSystemList.cshtml";
            var systemList = ApiList.GetAllSystemList();

            return PartialView(partialViewUrl, systemList);

        }
        public ActionResult GetSystemFormList(InputGetSystemFormsBySystemId input)
        {
            const string partialViewUrl = "~/Views/AssignFormToUser/AddForm/AddFormGetSystemFormList.cshtml";
            var formList = ApiList.GetSystemFormsBySystemId(input);

            return PartialView(partialViewUrl, formList);

        }

        public ActionResult GetFormEventList(InputGetFormEventByFormId input)
        {
            const string partialViewUrl = "~/Views/AssignFormToUser/AddForm/AddFormGetFormEventList.cshtml";
            var eventList = ApiList.GetFormEventByFormId(input);

            return PartialView(partialViewUrl, eventList);

        }

        public ActionResult Grid([FromUri] long[] eventValues)
        {
            const string partialViewUrl = "~/Views/AssignFormToUser/Grid/Grid.cshtml";
            var dataSource = GetReport(eventValues);
            return PartialView(partialViewUrl, dataSource);


        }

        public OutputGetUserAccessList[] GetReport(long[] eventValues)
        {
            var stringArray = new EventIdList[eventValues != null ? eventValues.Length : 0];
            if(eventValues == null)
            {
                return new OutputGetUserAccessList[] { };
            }

            for(var i = 0; i < eventValues.Length; i++)
            {
                stringArray[i] = new EventIdList
                {
                    EventId = Convert.ToInt64(eventValues[i])
                };
            }

            var apiParam = new InputGetUserAccessList
            {
                EventList = stringArray,
            };
            var dataSource = ApiList.GetUserAccessList(apiParam);
            return dataSource;
        }
    }
}