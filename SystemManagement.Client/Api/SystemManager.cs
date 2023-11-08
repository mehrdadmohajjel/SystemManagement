using System.Threading.Tasks;
using SystemManagement.Shared.Models.Input.SystemManager;
using SystemManagement.Shared.Models.Output.SystemManager;

namespace SystemManagement.Client.Api
{
    public static partial class ApiList
    {
        public static string AddNewSystem(InputAddNewSystem values, string token)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(AddNewSystem);

            var task = Task.Run(
                    async () =>
                        await ApiConnector<string>.Post(
                            url,
                            methodName, parameters: values, token: token)
                );

            return task.GetAwaiter().GetResult();

        }
        public static string AssignFormToSystem(InputAssignFormToSystem values, string token)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(AssignFormToSystem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }
        public static string AssignEventToForm(InputAssignEventToForm values, string token)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(AssignEventToForm);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }

        public static string EditSystem(InputEditSystem values, string token)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(EditSystem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }
        public static string ChangeSystemStatus(InputChangeSystemStatus values, string token)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(ChangeSystemStatus);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }

        public static string ChangeSystemFormStatus(InputChangeSystemFormStatus values, string token)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(ChangeSystemFormStatus);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }
        
              public static string ChangeFormEventStatus(InputChangeFormEventStatus values, string token)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(ChangeFormEventStatus);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }
        public static OutputGetAllSystemList[] GetAllSystemList()
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(GetAllSystemList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetAllSystemList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }
        public static OutputGetSystemFormsBySystemId[] GetSystemFormsBySystemId(InputGetSystemFormsBySystemId input)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(GetSystemFormsBySystemId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetSystemFormsBySystemId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }
        public static OutputGetFormEventByFormId[] GetFormEventByFormId(InputGetFormEventByFormId input)
        {
            var url = $"{BaseUrl}/SystemManager/";
            const string methodName = nameof(GetFormEventByFormId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetFormEventByFormId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }
        
    }
}