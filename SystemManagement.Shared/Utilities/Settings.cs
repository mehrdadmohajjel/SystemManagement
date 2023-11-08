
namespace SystemManagement.Shared.Utilities
{
    public static class Settings
    {
        public static string SystemCode { get; } = "%SYSTEM_CODE%";

        // If you changed this path, remember to add it into ../robocopy-ignored-directories.txt 
        public static string UploaderFilesLocations { get; } = "~/UploadedFiles";
    }
}
