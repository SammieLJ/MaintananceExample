using System;
using System.IO;
using System.Text.Json;

#nullable enable

class Program
{
    static void Main(string[] args)
    {
        var baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        var maintenanceFile = Path.Combine(baseDirectory ?? throw new InvalidOperationException("Base directory not found"), "WebApp", "maintenance.lock");

        Console.WriteLine($"Maintenance file path: {maintenanceFile}");
        // Check if the file exists
        if (File.Exists(maintenanceFile))
        {
            Console.WriteLine("Maintenance file already exists.");
        }
        else
        {
            Console.WriteLine("Maintenance file does not exist.");
        }
        // Check if the directory exists
        var maintenanceDir = Path.GetDirectoryName(maintenanceFile);
        if (maintenanceDir != null && Directory.Exists(maintenanceDir))
        {
            Console.WriteLine("Maintenance directory exists.");
        }
        else
        {
            Console.WriteLine("Maintenance directory does not exist.");
        }
        // Check if the directory is writable
        if (maintenanceDir != null && Directory.Exists(maintenanceDir))
        {
            var isWritable = false;
            try
            {
                var testFile = Path.Combine(maintenanceDir, "test.txt");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
                isWritable = true;
            }
            catch
            {
                isWritable = false;
            }
            Console.WriteLine($"Maintenance directory is writable: {isWritable}");
        }
        else
        {
            Console.WriteLine("Maintenance directory is not writable.");
        }
        // Check if the file is writable
        if (File.Exists(maintenanceFile))
        {
            var isWritable = false;
            try
            {
                File.AppendAllText(maintenanceFile, "test");
                isWritable = true;
            }
            catch
            {
                isWritable = false;
            }
            Console.WriteLine($"Maintenance file is writable: {isWritable}");
        }
        else
        {
            Console.WriteLine("Maintenance file is not writable.");
        }

        // Ensure the directory exists
        /*var maintenanceDir = Path.GetDirectoryName(maintenanceFile);
        if (!Directory.Exists(maintenanceDir))
        {
            _ = Directory.CreateDirectory(maintenanceDir);
        }*/

        if (args.Length == 0)
        {
            Console.WriteLine("Usage: MaintenanceManager [down|up] [--message \"your message\"]");
            return;
        }

        var command = args[0].ToLower();
        var message = "Maintenance mode enabled.";

        if (args.Length > 2 && args[1] == "--message")
        {
            message = args[2];
        }

        if (command == "down")
        {
            var info = new MaintenanceInfo { Message = message };
            var json = JsonSerializer.Serialize(info);
            File.WriteAllText(maintenanceFile, json);
            Console.WriteLine("App is now in maintenance mode.");
        }
        else if (command == "up")
        {
            if (File.Exists(maintenanceFile))
            {
                File.Delete(maintenanceFile);
                Console.WriteLine("App is now live.");
            }
            else
            {
                Console.WriteLine("App was not in maintenance mode.");
            }
        }
        else
        {
            Console.WriteLine("Unknown command.");
        }
    }
}

public class MaintenanceInfo
{
    public string? Message { get; set; }
}