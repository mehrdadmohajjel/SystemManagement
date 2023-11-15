using System.Threading.Tasks;
using SystemManagement.Shared.Models.Input.AssignFormToUser;
using SystemManagement.Shared.Models.Output.AssignFormToUser;

namespace SystemManagement.Client.Api
{
    public static partial class ApiList
    {
        public static string AddUserAccessList(InputAddUserAccessList values, string token)
        {
            var url = $"{BaseUrl}/AssignFormToUser/";
            const string methodName = nameof(AddUserAccessList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }
        public static OutputGetAllMainDepartment[] GetAllMainDepartment()
        {
            var url = $"{BaseUrl}/AssignFormToUser/";
            const string methodName = nameof(GetAllMainDepartment);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetAllMainDepartment[]>.Post(
                        url,
                        methodName, parameters: null)
            );
            return task.GetAwaiter().GetResult();
        }

        public static OutputGetSubDepartmentListByMainDepartmentId[] GetSubDepartmentListByMainDepartmentId(
            InputGetSubDepartmentListByMainDepartmentId input)
        {
            var url = $"{BaseUrl}/AssignFormToUser/";
            const string methodName = nameof(GetSubDepartmentListByMainDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetSubDepartmentListByMainDepartmentId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetEmployeeListByDepartmentId[] GetEmployeeListByDepartmentId(
            InputGetEmployeeListByDepartmentId input)
        {
            var url = $"{BaseUrl}/AssignFormToUser/";
            const string methodName = nameof(GetEmployeeListByDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetEmployeeListByDepartmentId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();

        }

    }
}