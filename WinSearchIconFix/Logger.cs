using System.IO;
using System;

class Logger
{
    private static Logger _instance;
    private StreamWriter writer;

    // Private constructor to prevent instantiation from outside
    private Logger()
    {
        writer = new StreamWriter(FileManager.logLocation);
    }

    // Access the single instance of Logger
    public static Logger GetInstance()
    {
        if (_instance == null)
            _instance = new Logger();
        return _instance;
    }

    public void WriteLog(string message)
    {
        Console.WriteLine(message);
        writer.WriteLine(message);
    }
    public void Close()
    {
        writer.Close();
    }
}
