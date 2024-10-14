using Windows.ApplicationModel;

class Program
{
    static bool pauseAtException = true; //This controls wether to pause at handled exceptions occur
    static bool createComparasion = false;
    static void Main()
    {
        OpenProgramDisclaimer();

        TryReplaceAllIcons();

        Logger.GetInstance().Close();
        CloseProgram();
    }
    static void OpenProgramDisclaimer()
    {
        Console.WriteLine("This program scans all your listed UWP packages and attempts to fix their icons when searched from the Windows Start menu by copying icon files.");
        Console.WriteLine("Continue at your own risk.");
        Console.WriteLine("To create comparison, type Y.");
        Console.WriteLine("To not create comparison, type N.");
        Console.WriteLine("To close the program, type C.");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.Y:
                createComparasion = true;
                break;
            case ConsoleKey.N:
                break;
            case ConsoleKey.C:
                Environment.Exit(0);
                break;
            default:
                OpenProgramDisclaimer();
                break;
        }
    }
    static void CloseProgram()
    {
        Console.WriteLine("");
        Console.WriteLine(new string('-', 40));
        if (createComparasion)
            Console.WriteLine("To Open Comparasion Press O");
        Console.WriteLine("To Open Logs Press L");
        Console.WriteLine("To Open Close Press C");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        switch (keyInfo.Key)
        {
            case ConsoleKey.O:
                FileManager.OpenComparasion();
                break;
            case ConsoleKey.L:
                FileManager.OpenLogFile();
                break;
            case ConsoleKey.C:
                Environment.Exit(0);
                break;
            default:
                break;
        }
        CloseProgram();
    }

    static void TryReplaceAllIcons()
    {
        Logger logger = Logger.GetInstance();
        FileManager fileManager = new FileManager();
        PackageParser parser = new PackageParser();

        string createComparasionString = createComparasion ? " create " : " not create ";
        logger.WriteLog($"Will{createComparasionString}a comparasion folder as instructed");

        int packageCount = parser.GetNumberOfPackages();
        int successCount = 0;
        int failedCount = 0;
        int skippedCount = 0;

        foreach (var package in parser.GetListOfPackages())// List all installed packages
        {
            string basicPackageInfo = parser.GetBasicPackageInfo(package);

            logger.WriteLog($"Checking {basicPackageInfo} ");
            try
            {
                if (package.GetAppListEntries().Count != 0)// Ignore packages that are dont have/need icons.
                {
                    string familyName = package.Id.FamilyName;
                    string sourceIconPath = parser.GetLogoPath(package);
                    string packageInstallLocation = parser.GetInstalledLocationPath(package);
                    string packageAppId = parser.ParseAppID(packageInstallLocation);
                    string targetIconPath = parser.GenerateIconPath(familyName, packageAppId);

                    if (createComparasion)
                        fileManager.CreateComparasion(sourceIconPath, targetIconPath, package.DisplayName);
                    fileManager.ReplaceFile(sourceIconPath, targetIconPath);

                    logger.WriteLog($"Icon copied succesfully for{basicPackageInfo}");
                    successCount++;
                }
                else
                {
                    logger.WriteLog($"Skipping{basicPackageInfo}");
                    skippedCount++;
                }
            }
            catch (Exception ex)
            {
                failedCount++;
                logger.WriteLog(new string('-', 40)); // Separator

                logger.WriteLog($"Exception occured for{basicPackageInfo}");
                logger.WriteLog(ex.ToString());
                logger.WriteLog(parser.PrintPackageData(package));//Dumps data for debugging purposes

                ExceptionPause(parser.GetInstalledLocationPath(package));
            }
            logger.WriteLog(new string('-', 40)); // Separator
        }
        ResultScreen();

        //Functions
        void ResultScreen()
        {
            float sucessRate = (successCount / (failedCount + successCount)) * 100;
            float entropyRate = 100 - (failedCount + successCount + skippedCount) * 100 / packageCount;
            if (entropyRate != 0)
                logger.WriteLog($"Entropy rate is {entropyRate}%. Please Report");
            logger.WriteLog($"Sucess Rate: {sucessRate}%");
            logger.WriteLog($"Total Packages: {packageCount}, Skipped Packages: {skippedCount}, Sucessfully Processed: {successCount}, Failed: {failedCount}");
        }
    }
    static void ExceptionPause(string openLocation)
    {
        Console.WriteLine("");
        Console.WriteLine($"Pause at Exception: {pauseAtException}");
        if (pauseAtException)
        {
            Console.WriteLine("Report if this happens with Listed package");
            Console.WriteLine("Type N to not pause at exceptions");
            Console.WriteLine("Type O to open file location");
            Console.WriteLine("");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.N:
                    pauseAtException = false;
                    break;
                case ConsoleKey.O:
                    FileManager.OpenFolder(openLocation);
                    break;
                default:
                    Console.WriteLine("Other key pressed");
                    break;
            }
        }
    }
}
