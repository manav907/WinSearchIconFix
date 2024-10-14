using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

public class PackageParser
{

    PackageManager packageManager;
    List<Package> packages;
    public PackageParser()
    {
        packageManager = new PackageManager();
        packages = new List<Package>(packageManager.FindPackagesForUser(string.Empty));
    }

    public List<Package> GetListOfPackages()
    {
        return packages;
    }
    public int GetNumberOfPackages()
    {
        return packages.Count;
    }
    public string GetBasicPackageInfo(Package package)
    {
        string listedApp = package.GetAppListEntries().Count != 0 ? "Listed" : "Unlisted";
        string basicPackageInfo = ($"{listedApp} package: '{package.DisplayName}', {package.Id.FullName}");
        return basicPackageInfo;
    }
    public string PrintPackageData(Package package)    //This dumps data about the package
    {
        Logger logger = Logger.GetInstance();
        string packageData = "";

        AppendString($"Package Name: {package.Id.Name}");
        AppendString($"Application Name: {package.DisplayName}");
        AppendString($"Family Name: {package.Id.FamilyName}");
        AppendString($"Package Full Name: {package.Id.FullName}");
        AppendString($"Version: {package.Id.Version}");
        AppendString($"Publisher: {package.Id.Publisher}");
        AppendString($"App List Entries: {package.GetAppListEntries().Count}");
        return packageData;
        void AppendString(string str) { packageData += "\n" + str; }
    }
    public string GenerateIconPath(string familyName, string appID)
    {
        string iconPath = "\\Packages\\Microsoft.Windows.Search_cw5n1h2txyewy\\LocalState\\AppIconCache\\100";
        string basicLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + iconPath + "\\";
        string iconFileName = familyName + "!" + appID;
        return basicLocation + iconFileName.Replace(".", "_");
    }
    //This reads the manifisto xml and dumps some data in a list of strings. currently it only dumps the appid
    public string ParseAppID(string appxManifestPath)
    {
        string manifestPath = Path.Combine(appxManifestPath, "AppxManifest.xml");

        if (File.Exists(manifestPath))
        {
            XDocument manifest = XDocument.Load(manifestPath);// Load the XML

            var applicationId = manifest.Descendants("{http://schemas.microsoft.com/appx/manifest/foundation/windows10}Application").Select(app => app.Attribute("Id")?.Value).FirstOrDefault();// Get the first Application Id
            if (applicationId != null)
                return applicationId;
            else
                throw new AppIdNullException();
        }
        else
            throw new AppxManifestXmlNotFound();
    }

    public string GetLogoPath(Package package)
    {
        try
        {
            return package.Logo.LocalPath;
        }
        catch (Exception ex)
        {
            if (ex is FileNotFoundException)
                throw new LogoLocationUnavailable();
            throw;
        }
    }
    public string GetInstalledLocationPath(Package package)
    {
        try
        {
            return package.InstalledLocation.Path;
        }
        catch (Exception ex)
        {
            if (ex is FileNotFoundException)
                throw new InstallLocationUnavailable();
            throw;
        }
    }
}
