using System.Diagnostics;

namespace cdk.commun.Helpers;

public static class PublishHelper
{
    private const string Framework = "net6.0";
    private const string Runtime = "linux-x64";

    private const string UserServiceFunction = "../Lambda/QuickSlot.UserService/QuickSlot.UserService.Api";

    public static void DotnetPublish(string configuration, string projectPath)
    {
        DirectoryInfo di = new DirectoryInfo($@"{projectPath}/bin/{configuration}");

        if (di.Exists)
        {
            di.Delete(true);
        }

        var process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = @$"publish {projectPath} --configuration {configuration} --framework {Framework} /p:GenerateRuntimeConfigurationFiles=true --runtime {Runtime} --no-self-contained";
        process.StartInfo.CreateNoWindow = false;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        process.WaitForExit();

        string versionTextFilePath = $@"{projectPath}/version.txt";
        File.AppendAllLines(versionTextFilePath, new[] { $"Commit = [{Environment.GetEnvironmentVariable("CODEBUILD_RESOLVED_SOURCE_VERSION")}]", $"Deployment Guid = [{Guid.NewGuid()}]", $"Deployment Date = [{DateTime.UtcNow.ToString("yyyyMMdd-HHmmss")}]" });
    }

    public static string GetPublishCodePath(string configuration, string projectPath)
    {
        return $"{projectPath}/bin/{configuration}/{Framework}/{Runtime}/publish";
    }

    public static string GetPublishUserFunctionCodePath(string configuration)
    {
        return GetPublishCodePath(configuration, UserServiceFunction);
    }
}