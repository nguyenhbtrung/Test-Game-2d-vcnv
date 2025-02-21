using UnityEngine;
using System.IO;

public static class TestLogger
{
    private static string logFilePath = Path.Combine(Application.dataPath, "TestLogs", "TrapTestResults.txt");
    private static string logDir = "TestLogs";
    public static string GetLogFilePath()
    {
        return logFilePath;
    }

    public static void Log(string message)
    {
        // Đảm bảo thư mục tồn tại
        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

        // Ghi thông điệp vào tệp tin (thêm mới dòng)
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine(message);
        }
    }

    public static void Log(string message, string fileName)
    {
        string logFilePath = Path.Combine(Application.dataPath, logDir, fileName);
        // Đảm bảo thư mục tồn tại
        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

        // Ghi thông điệp vào tệp tin (thêm mới dòng)
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine(message);
        }
    }

    public static void ClearLog()
    {
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }
    }

    public static void ClearLog(string fileName)
    {
        string logFilePath = Path.Combine(Application.dataPath, logDir, fileName);
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }
    }
}
