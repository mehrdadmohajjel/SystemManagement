/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.assignFormToUser = (function () {

    var dom = {};
    var state = {
        GridFocusedRowIndex: 0,
        GridMemberFocusedRowIndex: 0,
        mainDepartmentId: 0,
        subDepartmentId: 0,
        systemId: 0,
        formId: 0,
        eventValue: '',
        userId:''
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/AssignFormToUser/";

    async function addAccess() {
        var canContinue = isAddFormValid();
        if (!canContinue) return false;

        const url = controllerName + "AddUserAccessList";

        const userIdList = dom.addFormDepartmentEmployeeCheckComboBox.GetSelectedValues();
        const eventIdList = dom.addFormEventFormCheckComboBox.GetSelectedValues();

        for (let i = 0; i < userIdList.length; i++) {
            for (let j = 0; i < eventIdList.length; i++) {
                const apiParam = {
                    userId: userIdList[i],
                    eventId: eventIdList[i]
                };
                const gridSource = await motorsazanClient.connector.post(url, apiParam);
                if (gridSource !== "") {
                    motorsazanClient.messageModal.success(gridSource);
                }
                filterList();

            }
        }
        resetAddForm();
        motorsazanClient.messageModal.success('عملیات انجام شد');
    }
   async  function handleAddFormGetAllMainDepartmentComboSelectedIndexChanged() {

        const url = controllerName + "GetSubDepartmentList";
        tools.hideItem(dom.addFormMainDepartmentComboError);


        const departmentId = dom.addFormGetAllMainDepartmentCombo.GetValue();
        state.mainDepartmentId = dom.addFormGetAllMainDepartmentCombo.GetValue();

        const apiParam = {
            departmentId: departmentId
        };

        const machineList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormSubDepartmentComboParent.html(machineList);
        setDom();

    }
    
    async function handleAddFormGetAllSubDepartmentComboSelectedIndexChanged() {
        tools.hideItem(dom.addFormSubDepartmentComboError);
        const url = controllerName + "GetDepartmentEmployeeCheckComboBox";

        const departmentId = dom.addFormGetAllSubDepartmentCombo.GetValue();
        state.subDepartmentId = dom.addFormGetAllSubDepartmentCombo.GetValue();

        const apiParam = {
            departmentId: departmentId
        };

        const machineList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormDepartmentEmployeeComboParent.html(machineList);
        setDom();


    }
    function handleAddFormGetAllSubDepartmentComboBeginCallBack(command) {
        var departmentId = dom.addFormGetAllMainDepartmentCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "GetSubDepartmentList?departmentId=" + departmentId;
    }

    function handleDepartmentEmployeeCheckComboBoxBeginCallback(command) {
        var departmentId = dom.addFormGetAllSubDepartmentCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "GetDepartmentEmployeeCheckComboBox?departmentId=" + departmentId;
    }

    function handleDepartmentEmployeeCheckComboBoxSelectedIndexChange() {
        tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxError);

        tools.showItem(dom.addFormEmployeeCheckDetailParent);

        const employee = dom.addFormDepartmentEmployeeCheckComboBox.GetSelectedValues();
        const isEmployeeSelected = !tools.isNullOrEmpty(employee);
        if (!isEmployeeSelected) {
            tools.hideItem(dom.addFormDepartmentEmployeeCombInfo);
            tools.showItem(dom.addFormDepartmentEmployeeComboError);
        } else {
            const len = employee.length;
            dom.addFormDepartmentEmployeeCombInfo.html(`تعداد ${len} کارمند انتخاب شده است`);
            tools.showItem(dom.addFormDepartmentEmployeeCombInfo);
        }
    }


    async function handleAddFormGetAllSystemComboSelectedIndexChanged() {
        tools.hideItem(dom.addFormSystemListComboError);
        const url = controllerName + "GetSystemFormList";
        const systemId = dom.addFormGetAllSystemListCombo.GetValue();
        state.systemId = dom.addFormGetAllSystemListCombo.GetValue();

        const apiParam = {
            systemId: systemId
        };

        const formList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormSystemFormListComboParent.html(formList);
        setDom();
    }

  async  function handleAddFormGetSystemFormComboSelectedIndexChanged() {
        tools.hideItem(dom.addFormSystemFormListComboError);

        const url = controllerName + "GetFormEventList";
        const formId = dom.addFormSystemFormListCombo.GetValue();
        state.formId = dom.addFormSystemFormListCombo.GetValue();

        const apiParam = {
            formId: formId
        };

        const eventList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormSystemFormEventListComboParent.html(eventList);
        setDom();

    }
    function handleAddFormGetSystemFormComboBeginCallback(command) {
        var systemId = dom.addFormGetAllSystemListCombo.GetValue();

        if (systemId == null) systemId = -1;

        command.callbackUrl =
            controllerName + "GetSystemFormList?systemId=" + systemId;
    }

    function handleFormEventCheckComboBoxBeginCallback(command) {
        var formId = dom.addFormSystemFormListCombo.GetValue();

        if (formId == null) formId = -1;

        command.callbackUrl =
            controllerName + "GetFormEventList?formId=" + formId;
    }

    function handleFormEventCheckComboBoxSelectedIndexChange() {
        tools.hideItem(dom.addFormEventListCheckComboBoxError);

        tools.showItem(dom.addFormEventListCheckComboBoxParent);

        const events = dom.addFormEventListCheckComboBox.GetSelectedValues();
        const isEventSelected = !tools.isNullOrEmpty(employee);
        if (!isEventSelected) {
            tools.hideItem(dom.addFormEventListCheckComboBoxInfo);
            tools.showItem(dom.addFormEventListCheckComboBoxError);
        } else {
            const len = events.length;
            dom.addFormEventListCheckComboBoxInfo.html(`تعداد ${len} رویداد انتخاب شده است`);
            tools.showItem(dom.addFormEventListCheckComboBoxInfo);
        }

    }

    
    async function filterList() {
        const url = controllerName + "Grid";
        state.eventValue = dom.addFormEventFormCheckComboBox.GetSelectedValues();

        const apiParam = {
            eventList: state.eventValue,
        };

        const grid = await motorsazanClient.connector.post(url, apiParam);
        dom.gridParent.html(grid);
        setDom();

    }

    function getWorkOrderGridActiveRowKey() {
        const activeIndex = dom.grid.GetFocusedRowIndex();
        return dom.grid.GetRowKey(activeIndex);
    }

    function init() {
        setDom();
    }


    function isAddFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormEmployeeDepartmentComboError);
        const department = dom.addFormEmployeeDepartmentCombo.GetValue();
        const isDepartmentValid = !tools.isNullOrEmpty(department);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.addFormEmployeeDepartmentComboError);
        }

        tools.hideItem(dom.addFormMachineComboError);
        const machine = dom.addFormMachineCombo.GetValue();
        const isMachineValid = !tools.isNullOrEmpty(machine);
        if (!isMachineValid) {
            isValid = false;
            tools.showItem(dom.addFormMachineComboError);
        }

        tools.hideItem(dom.addFormScrapRadioError);
        const scrap = dom.addFormScrapRadio.GetValue();
        const isScrapValid = !tools.isNullOrEmpty(scrap);
        if (!isScrapValid) {
            isValid = false;
            tools.showItem(dom.addFormScrapRadioError);
        }


        tools.hideItem(dom.addFormMaintenanceGroupComboError);
        const maintenanceGroup = dom.addFormMaintenanceGroupListCombo.GetValue();
        const isMaintenanceGroupValid = !tools.isNullOrEmpty(maintenanceGroup);
        if (!isMaintenanceGroupValid) {
            isValid = false;
            tools.showItem(dom.addFormMaintenanceGroupComboError);
        }

        tools.hideItem(dom.addFormStopTypeListComboError);
        const stopType = dom.addFormStopTypeListCombo.GetValue();
        const isStopTypeValid = !tools.isNullOrEmpty(stopType);
        if (!isStopTypeValid) {
            isValid = false;
            tools.showItem(dom.addFormStopTypeListComboError);
        }

        tools.hideItem(dom.addFormFaultDescriptionError);
        const faultDescription = dom.addFormFaultDescription.GetValue();
        const isFaultDescription = !tools.isNullOrEmpty(faultDescription);
        if (!isFaultDescription) {
            isValid = false;
            tools.showItem(dom.addFormFaultDescriptionError);
        }

        return isValid;
    }

    function resetAddForm() {
        dom.addFormGetAllMainDepartmentCombo.SetSelectedIndex(-1);
        dom.addFormGetAllSubDepartmentCombo.SetSelectedIndex(-1);
        dom.addFormDepartmentEmployeeCheckComboBox.SetSelectedIndex(-1);
        dom.addFormGetAllSystemListCombo.SetSelectedIndex(-1);
        dom.addFormSystemFormListCombo.SetSelectedIndex(-1);
        dom.addFormEventListCheckComboBox.SetSelectedIndex(-1);
    }

    function setDom() {
        dom.addFormMainDepartmentComboError = $("#addFormMainDepartmentComboError");
        dom.addFormGetAllMainDepartmentCombo = ASPxClientTextBox.Cast("addFormGetAllMainDepartmentCombo");

        dom.addFormSubDepartmentComboParent = $("#addFormSubDepartmentComboParent");
        dom.addFormSubDepartmentComboError = $("#addFormSubDepartmentComboError");
        dom.addFormGetAllSubDepartmentCombo = ASPxClientTextBox.Cast("addFormGetAllSubDepartmentCombo");

        dom.addFormDepartmentEmployeeComboParent = $("#addFormDepartmentEmployeeComboParent");
        dom.addFormDepartmentEmployeeComboError = $("#addFormDepartmentEmployeeComboError");
        dom.addFormDepartmentEmployeeCombInfo = $("#addFormDepartmentEmployeeCombInfo");
        dom.addFormDepartmentEmployeeCheckComboBox = ASPxClientListBox.Cast("addFormDepartmentEmployeeCheckComboBox");
        dom.checkComboBox = ASPxClientDropDownEdit.Cast("checkComboBox");





        dom.addFormSystemListComboParent = $("#addFormSystemListComboParent");
        dom.addFormSystemListComboError = $("#addFormSystemListComboError");
        dom.addFormGetAllSystemListCombo = ASPxClientTextBox.Cast("addFormGetAllSystemListCombo");

        dom.addFormSystemFormListComboParent = $("#addFormSystemFormListComboParent");
        dom.addFormSystemFormListComboError = $("#addFormSystemFormListComboError");
        dom.addFormSystemFormListCombo = ASPxClientTextBox.Cast("addFormSystemFormListCombo");

        dom.addFormEventListCheckComboBoxParent = $("#addFormEventListCheckComboBoxParent");
        dom.addFormEventListCheckComboBoxError = $("#addFormEventListCheckComboBoxError");
        dom.addFormEventListCheckComboBoxInfo = $("#addFormEventListCheckComboBoxInfo");
        dom.addFormEventListCheckComboBox = ASPxClientListBox.Cast("addFormEventListCheckComboBox");
        dom.eventCheckComboBox = ASPxClientDropDownEdit.Cast("eventCheckComboBox");


        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");

       

    }
    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormEventListCheckComboBoxError);
        const employee = dom.addFormDepartmentEmployeeCheckComboBox.GetSelectedValues();
        const isEmployeeSelected = !tools.isNullOrEmpty(employee);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.addFormEventListCheckComboBoxError);
        }

        return isValid;
    }
    function handleGridCallbackUrl(source) {
        var url = "/AssignFormToUser/Grid?";
        const events = state.eventValue;
        for (let i = 0; i < events.length; i++) {
            url += "&eventId=" + events[i];
        }
        source.callbackUrl = url;
    }
   

    $(document).ready(init);

    return {
        handleAddFormGetAllMainDepartmentComboSelectedIndexChanged: handleAddFormGetAllMainDepartmentComboSelectedIndexChanged,
        handleAddFormGetAllSubDepartmentComboSelectedIndexChanged: handleAddFormGetAllSubDepartmentComboSelectedIndexChanged,
        handleAddFormGetAllSubDepartmentComboBeginCallBack: handleAddFormGetAllSubDepartmentComboBeginCallBack,
        handleDepartmentEmployeeCheckComboBoxSelectedIndexChange: handleDepartmentEmployeeCheckComboBoxSelectedIndexChange,
        handleDepartmentEmployeeCheckComboBoxBeginCallback: handleDepartmentEmployeeCheckComboBoxBeginCallback,
        handleAddFormGetAllSystemComboSelectedIndexChanged: handleAddFormGetAllSystemComboSelectedIndexChanged,
        handleAddFormGetSystemFormComboSelectedIndexChanged: handleAddFormGetSystemFormComboSelectedIndexChanged,
        handleAddFormGetSystemFormComboBeginCallback: handleAddFormGetSystemFormComboBeginCallback,
        handleFormEventCheckComboBoxSelectedIndexChange: handleFormEventCheckComboBoxSelectedIndexChange,
        handleFormEventCheckComboBoxBeginCallback: handleFormEventCheckComboBoxBeginCallback,
        addAccess: addAccess,
        handleGridCallbackUrl: handleGridCallbackUrl
    };

})();

