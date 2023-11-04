string sourceDirectory = @"C:\SourceFolder";
string targetDirectory = @"D:\DestinationFolder";

CopyDirectory(sourceDirectory, targetDirectory);

Console.WriteLine("Completed");
Console.Read();


static void CopyDirectory(string sourceDir, string targetDir)
{
    // Create the target directory if it doesn't exist
    if (!Directory.Exists(targetDir))
    {
        Directory.CreateDirectory(targetDir);
    }

    // Copy files from the source directory to the target directory
    foreach (string sourceFile in Directory.GetFiles(sourceDir))
    {
        string targetFile = Path.Combine(targetDir, Path.GetFileName(sourceFile));

        if (!File.Exists(targetFile) || IsNewer(sourceFile, targetFile))
        {
            File.Copy(sourceFile, targetFile, true);
            Console.WriteLine($"Copied {sourceFile} to {targetFile}");
        }
    }

    // Recursively copy subdirectories
    foreach (string subDir in Directory.GetDirectories(sourceDir))
    {
        string targetSubDir = Path.Combine(targetDir, Path.GetFileName(subDir));
        CopyDirectory(subDir, targetSubDir);
    }
}

static bool IsNewer(string sourceFile, string targetFile)
{
    var sourceLastWriteTime = File.GetLastWriteTime(sourceFile);
    var targetLastWriteTime = File.GetLastWriteTime(targetFile);

    return sourceLastWriteTime > targetLastWriteTime;
}