/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.systemManager = (function () {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod",
        gridFocusedRowIndex: 0,
        systemId: 0,
        formId: 0,
        eventId:0

    };

    var tools = motorsazanClient.tools;
    var controllerName = "/SystemManager/";



    async function addNewSystem() {
        const canContinue = isAddFormValid();
        if (!canContinue) return false;

        const url = controllerName + "AddNewSystem";

        const apiParam = {
            SystemName: dom.addFormSystemName.GetValue(),
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);


        filterSystemList();
        motorsazanClient.messageModal.success(apiResult);
        resetAddForm();
    }


    function resetAddForm() {
        dom.addFormSystemName.SetText('');

    }

    async function filterSystemList() {


        const url = controllerName + "Grid";

        const apiParam = {
            
        };

        const groupGridSource = await motorsazanClient.connector.post(url, apiParam);
        dom.gridParent.html(groupGridSource);
        setDom();
    }

    async function filterFormList() {

        const url = controllerName + "SystemFormGrid";

        const apiParam = {
            systemId: state.systemId
        };

        const groupGridSource = await motorsazanClient.connector.post(url, apiParam);
        dom.formGridParent.html(groupGridSource);
        setDom();
        assignFormToSystemDOM();
    }
    async function filterFormEventList() {

        const url = controllerName + "SystemFormEventGird";

        const apiParam = {
            formId: state.formId
        };

        const groupGridSource = await motorsazanClient.connector.post(url, apiParam);
        dom.formEventGridParent.html(groupGridSource);
        setDom();
        assignFormToSystemDOM();
        assignEventoFormDOM();
    }
    function handleGridCallbackUrl(source) {

        const url = controllerName + "Grid";

        source.callbackUrl = url;
    }

    function handleAddFormSystemNameValueChanged() {
        tools.hideItem(dom.addFormFormSystemNameError);
    }



    function isAddFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormFormSystemNameError);
        var systemName = dom.addFormSystemName.GetValue();
        var isSystemNameValid = !tools.isNullOrEmpty(systemName);
        if (!isSystemNameValid) {
            isValid = false;
            tools.showItem(dom.addFormFormSystemNameError);
        }


        return isValid;
    }




    function setDom() {
        dom.addFormSystemNameParent = $("#addFormSystemNameParent");
        dom.addFormFormSystemNameError = $("#addFormFormSystemNameError");
        dom.addFormSystemName = ASPxClientTextBox.Cast("addFormSystemName");

       

        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");


    }
    function girdCustomButtonClick(source, event) {
        setDom();
        assignFormToSystemDOM();
        state.systemId =
            dom.grid.GetRowKey(event.visibleIndex);
        const customBtnId = event.buttonID;
        if (customBtnId === "addFormBTN") return showAddNewFormTosystemPopup();
        if (customBtnId === "changeSystemStatusBtn") return showRemoveConfirmation();

        

    }
    function showRemoveConfirmation() {
        var title = "تاییدیه تغییر وضعیت سیستم";
        var content = "آیا از تغییر وضعیت سیستم مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, proveChangeStatus, title);

    }
    async function proveChangeStatus() {
        const url = controllerName + "ChangeSystemStatus";

        const systemId = state.systemId;

        const apiParam = {
            systemId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);


        filterSystemList();
        motorsazanClient.messageModal.hide();
        motorsazanClient.messageModal.success(apiResult);
    }

    function showAddNewFormTosystemPopup() {
        const url = controllerName + "SystemFormManagement";
        const apiParam = {
            systemId: state.systemId
        };

        const title = "مدیریت فرم های سیستم";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, assignFormToSystemDOM);

    }
    
    function assignFormToSystemDOM() {
        dom.addFormTitle = ASPxClientTextBox.Cast("addFormTitle");
        dom.addFormTitleError = $("#addFormTitleError");

        dom.addFormCode = ASPxClientTextBox.Cast("addFormCode");
        dom.addFormCodeError = $("#addFormCodeError");



        dom.formGridParent = $("#formGridParent");
        dom.formGrid = ASPxClientGridView.Cast("formGrid");

    }


    function handleFormGridCallback(source) {

        const url = controllerName + "SystemFormGrid";

        source.callbackUrl = url + "?SystemId=" + state.systemId;
    }
    async function handleAssignFormToSystemBtn() {

        if (!isAssignFormToSystemValid()) return false;
        const url = controllerName + "AssignFormToSystem";
        const apiParam = {
            FormName: dom.addFormTitle.GetValue(),
            FormCode: dom.addFormCode.GetValue(),
            SystemId: state.systemId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        await filterFormList();
        motorsazanClient.messageModal.success(apiResult);
        dom.addFormTitle.SetText('');
        dom.addFormCode.SetText('');
        return false;
    }
    
    function isAssignFormToSystemValid() {
        var isValid = true;

        tools.hideItem(dom.addFormTitleError);
        const formTitle = dom.addFormTitle.GetValue();
        const isFormTitleValid = !tools.isNullOrEmpty(formTitle);
        if (!isFormTitleValid) {
            isValid = false;
            tools.showItem(dom.addFormTitleError);
        }

        tools.hideItem(dom.addFormCodeError);
        const formCode = dom.addFormCode.GetValue();
        const isFormCodeValid = !tools.isNullOrEmpty(formCode);
        if (!isFormCodeValid) {
            isValid = false;
            tools.showItem(dom.addFormCodeError);
        }

        return isValid;
    }


    function formGridCustomButtonClick(source, event) {
        setDom();
        assignFormToSystemDOM();
        state.formId =
            dom.formGrid.GetRowKey(event.visibleIndex);
        const customBtnId = event.buttonID;
        if (customBtnId === "addEventBTN") return showAddNewEventToFormPopup();
        if (customBtnId === "changeSystemFormStatusBtn") return showRemovePageConfirmation();
    }
    function showAddNewEventToFormPopup() {
        const url = controllerName + "SystemFormEventManagement";
        const apiParam = {
            formId: state.formId
        };

        const title = "مدیریت رویدادهای فرم";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, assignEventoFormDOM);
    }
    function showRemovePageConfirmation() {
        var title = "تاییدیه تغییر وضعیت فرم";
        var content = "آیا از تغییر وضعیت فرم مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, provePageChangeStatus, title);

    }
    async function provePageChangeStatus() {
        const url = controllerName + "ChangeSystemFormStatus";

        const formId = state.formId;

        const apiParam = {
            formId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);


        filterFormList();
        motorsazanClient.messageModal.hide();
        motorsazanClient.messageModal.success(apiResult);
    }

    function assignEventoFormDOM() {
        dom.addEventTitle = ASPxClientTextBox.Cast("addEventTitle");
        dom.addEventTitleError = $("#addEventTitleError");

        dom.addEventCode = ASPxClientTextBox.Cast("addEventCode");
        dom.addEventCodeError = $("#addEventCodeError");



        dom.formEventGridParent = $("#formEventGridParent");
        dom.eventGrid = ASPxClientGridView.Cast("eventGrid");

    }


    function handleEventGridCallback(source) {

        const url = controllerName + "SystemFormEventGird";

        source.callbackUrl = url + "?Formid=" + state.formId;
    }
    function eventGridCustomButtonClick(source, event) {
        setDom();
        assignEventoFormDOM();
        state.eventId =
            dom.eventGrid.GetRowKey(event.visibleIndex);
        const customBtnId = event.buttonID;
        if (customBtnId === "changeSystemFormEventStatusBtn") return showRemoveEventConfirmation();
    }
    function showRemoveEventConfirmation() {
        var title = "تاییدیه تغییر وضعیت رویداد";
        var content = "آیا از تغییر وضعیت رویداد مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, proveChangeEventStatus, title);

    }
    async function proveChangeEventStatus() {
        const url = controllerName + "ChangeFormEventStatus";

        const eventId = state.eventId;

        const apiParam = {
            eventId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);


        filterFormEventList();
        motorsazanClient.messageModal.hide();
        motorsazanClient.messageModal.success(apiResult);
    }

    function handleAddEventTitleValueChanged() {
        tools.hideItem(dom.addEventTitleError);
    }
    function handleAddEventCodeValueChanged() {
        tools.hideItem(dom.addEventCodeError);
    }
    async function handleAssignEventToFormBtn() {

        if (!isAssignEventToFormValid()) return false;
        const url = controllerName + "AssignEventToForm";
        const apiParam = {
            EventName: dom.addEventTitle.GetValue(),
            EventCode: dom.addEventCode.GetValue(),
           FormId: state.formId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        await filterFormEventList();
        motorsazanClient.messageModal.success(apiResult);
        dom.addEventTitle.SetText('');
        dom.addEventCode.SetText('');
        return false;
    }
    
    function isAssignEventToFormValid() {
        var isValid = true;

        tools.hideItem(dom.addEventTitleError);
        const EventTitle = dom.addEventTitle.GetValue();
        const isEventTitleValid = !tools.isNullOrEmpty(EventTitle);
        if (!isEventTitleValid) {
            isValid = false;
            tools.showItem(dom.addEventTitleError);
        }

        tools.hideItem(dom.addEventCodeError);
        const EventCode = dom.addEventCode.GetValue();
        const isEventCodeValid = !tools.isNullOrEmpty(EventCode);
        if (!isEventCodeValid) {
            isValid = false;
            tools.showItem(dom.addEventCodeError);
        }

        return isValid;
    }

    function init() {
        setDom();
    }


    $(document).ready(init);

    return {
        addNewSystem: addNewSystem,
        girdCustomButtonClick: girdCustomButtonClick,
        handleGridCallbackUrl: handleGridCallbackUrl,
        handleAddFormSystemNameValueChanged: handleAddFormSystemNameValueChanged,
        girdCustomButtonClick: girdCustomButtonClick,
        handleFormGridCallback: handleFormGridCallback,
        handleAssignFormToSystemBtn: handleAssignFormToSystemBtn,
        formGridCustomButtonClick: formGridCustomButtonClick,
        handleEventGridCallback: handleEventGridCallback,
        eventGridCustomButtonClick: eventGridCustomButtonClick,
        handleAddEventTitleValueChanged: handleAddEventTitleValueChanged,
        handleAddEventCodeValueChanged: handleAddEventCodeValueChanged,
        handleAssignEventToFormBtn: handleAssignEventToFormBtn
    };

})();