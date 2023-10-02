using System.Diagnostics;

namespace cdk.commun.Helpers;

public static class PublishHelper
{
    private const string Framework = "net6.0";
    private const string Runtime = "linux-x64";

    private const string UserServiceFunction = "../../../../../src/Lambda/QuickSlot.UserService/QuickSlot.UserService.Api/QuickSlot.UserService.Lambda.Api.csproj";

    public static void DotnetPublish(string configuration, string projectPath, string outputDirectory)
    {
        // Convert relative paths to absolute paths
        projectPath = Path.GetFullPath(projectPath);
        outputDirectory = Path.GetFullPath(outputDirectory);

        // Ensure the output directory exists, if not, create it.
        Directory.CreateDirectory(outputDirectory);

        var process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = @$"publish {projectPath} -c {configuration} -o {outputDirectory}";
        process.StartInfo.CreateNoWindow = false;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        process.WaitForExit();

        string versionTextFilePath = Path.Combine(outputDirectory, "version.txt");
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