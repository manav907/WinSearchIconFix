using System;
using System.IO;
using Windows.ApplicationModel;

public class FileManager
{
    static string compareLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comparasion");
    static public string logLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
    public FileManager()
    {
        if (!Directory.Exists(compareLocation))
        {
            Directory.CreateDirectory(compareLocation);
        }
    }
    public void CreateComparasion(string soruce, string target, string name)
    {
        Console.WriteLine("Source: " + soruce + " Target: " + target + " Name: " + name);
        try
        {
            File.Copy(soruce, Path.Combine(compareLocation, name + ".soruce.png"), true);
        }
        catch (FileNotFoundException)
        {
            throw new LogoLocationUnavailable();
        }
        try
        {
            File.Copy(target, Path.Combine(compareLocation, name + ".target.png"), true);
        }
        catch (FileNotFoundException)
        {
            using (File.Create(Path.Combine(compareLocation, name + ".target.MissingFile.png")))
            {
                // This will be a blank file
            }
        }
    }
    public void ReplaceFile(string soruce, string target)
    {
        File.Copy(soruce, target, true);
    }
    public static void OpenFolder(string openLocation)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = openLocation,
            UseShellExecute = true // Use the OS shell to open the file
        });
    }
    public static void OpenComparasion()
    {
        OpenFolder(compareLocation);
    }
    public static void OpenLogFile()
    {
        OpenFolder(logLocation);
    }
}

