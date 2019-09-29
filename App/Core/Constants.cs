namespace AzureDevops
{
    public static class Constants
    {
        public const string APPCENTER_IOS = "##APPCENTER_IOS##";
        public const string APPCENTER_ANDROID = "##APPCENTER_ANDROID##";

        public static string LABEL_AZURE_DEVOPS = "Azure Devops";
        public static string LABEL_LOADING = "Loading";
        public static string LABEL_ORGANIZATION = "Organization";
        public static string LABEL_PERSONAL_ACCESS_TOKEN = "Personal Access Token";
        public static string LABEL_LOGIN = "Login";
        public static string LABEL_PIPELINES = "Pipelines";
        public static string LABEL_REPOS = "Repos";
        public static string LABEL_TESTS = "Tests";
        public static string LABEL_QUEUE = "Queue";
        public static string LABEL_BUILD_DEFINITIONS = "Build Definitions";
        public static string LABEL_SUMMARY = "Summary";
        public static string LABEL_JOBS = "Jobs";
        public static string LABEL_BOARD = "Board";
        public static string LABEL_PROJECTS = "Projects";
        public static string LABEL_BUILD = "Build";
        public static string LABEL_BUILD_VERSION = "Build {0}";

        public static string ERROR_MSG_PAT_UNABLE_TO_CONNECT_TO_AZURE_DEVOPS = "Unable to connect to Azure DevOps with these informations";
        public static string ERROR_MSG_UNABLE_TO_QUEUE_A_BUILD = "Unable to queue a build";
        public static string ERROR_MSG_UNABLE_TO_LOAD_BUILD_DEFINITIONS = "Unable to load build definitions";
        public static string ERROR_MSG_UNABLE_TO_LOAD_BUILDS = "Unable to load builds";
        public static string ERROR_MSG_UNABLE_TO_LOAD_JOBS = "Unable to load jobs";
        public static string ERROR_MSG_UNABLE_TO_LOAD_LOGS = "Unable to load logs";
        public static string ERROR_MSG_NO_BUILDS_LOGS = "No logs were generated for the running build";


        public static string URL_PERSONAL_ACCESS_TOKEN = "https://docs.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=azure-devops";
    }
}