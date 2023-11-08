using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemManagement.Client.Api;
using SystemManagement.Shared.Models.Input.SystemManager;

namespace SystemManagement.Client.Controllers
{
    public class SystemManagerController : BaseController
    {
        // GET: SystemManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddNewSystem(InputAddNewSystem values)
        {
            var token = GetUserToken();
            var apiResult = ApiList.AddNewSystem(values, token);
            return Content(apiResult);
        }
        public ActionResult AssignFormToSystem(InputAssignFormToSystem input)
        {
            var token = GetUserToken();
            var apiResult = ApiList.AssignFormToSystem(input, token);
            return Content(apiResult);

        }
        public ActionResult AssignEventToForm(InputAssignEventToForm input)
        {
            var token = GetUserToken();
            var apiResult = ApiList.AssignEventToForm(input, token);
            return Content(apiResult);

        }

        
        public ActionResult Grid()
        {
            const string partialViewUrl = "~/Views/SystemManager/Grid/Grid.cshtml";

            var apiResult = ApiList.GetAllSystemList();
            return PartialView(partialViewUrl, apiResult);

        }

        public ActionResult SystemFormManagement(long systemId)
        {
            const string partialViewUrl = "~/Views/SystemManager/Grid/FormManagement/AddNewFormModal.cshtml";

            ViewData["systemId"] = systemId;
            return PartialView(partialViewUrl);
        }

        public ActionResult SystemFormGrid(long systemId)
        {
            const string partialViewUrl = "~/Views/SystemManager/Grid/FormManagement/FormGird.cshtml";

            var input = new InputGetSystemFormsBySystemId { SystemId = systemId };
            var apiResult = ApiList.GetSystemFormsBySystemId(input);
            return PartialView(partialViewUrl, apiResult);

        }
        public ActionResult ChangeSystemStatus(InputChangeSystemStatus value)
        {
            var token = GetUserToken();
            var apiResult = ApiList.ChangeSystemStatus(value, token);
            return Content(apiResult);
        }
        public ActionResult ChangeSystemFormStatus(InputChangeSystemFormStatus value)
        {
            var token = GetUserToken();
            var apiResult = ApiList.ChangeSystemFormStatus(value, token);
            return Content(apiResult);
        }

        public ActionResult SystemFormEventManagement(long formId)
        {
            const string partialViewUrl = "~/Views/SystemManager/Grid/EventManagement/AddNewEventModal.cshtml";

            ViewData["formId"] = formId;
            return PartialView(partialViewUrl);
        }
        public ActionResult SystemFormEventGird(long formId)
        {
            const string partialViewUrl = "~/Views/SystemManager/Grid/EventManagement/EventGird.cshtml";

            var input = new InputGetFormEventByFormId { FormId = formId };
            var apiResult = ApiList.GetFormEventByFormId(input);
            return PartialView(partialViewUrl, apiResult);

        }
        public ActionResult ChangeFormEventStatus(InputChangeFormEventStatus value)
        {
            var token = GetUserToken();
            var apiResult = ApiList.ChangeFormEventStatus(value, token);
            return Content(apiResult);
        }
        
    }
}