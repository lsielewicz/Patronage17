using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using Patronage17.Engine.Helpers;

class Program
{
    static void Main(string[] args)
    {
        if (!args.Any())
        {
            ExitApplication("Cannot process command because of missing mandatory parameter: DirectoryPath");
        }

        string directoryPath = args.First();
        if (directoryPath.Length > 260)
        {
            ExitApplication("Path is too long");
        }

        if (Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory: {directoryPath}\n");
            IEnumerable<FileInfo> files = null;
            try
            {
                files = FilesIoHelper.Instance.GetFilesFromDirectory(directoryPath, SearchOption.TopDirectoryOnly);
            }
            catch (ArgumentNullException ex){ HandleUIException("There is no given argument", ex.Message, ex.StackTrace); }
            catch (SecurityException ex) { HandleUIException("There is a security issue that prevents from further executing", ex.Message, ex.StackTrace); }
            catch (ArgumentException ex) { HandleUIException("Given argument is incorrect", ex.Message, ex.StackTrace); }
            catch (PathTooLongException ex) { HandleUIException("Given path is too long", ex.Message, ex.StackTrace); }
            catch (Exception ex) { HandleUIException("Unknown error", ex.Message, ex.StackTrace); }


            files?.ToList().ForEach(file =>
            {
                int fileLengthLimit = 50;
                var fileName = file.Name.Length < fileLengthLimit ? file.Name : $"{file.Name.Substring(0, fileLengthLimit)} ...";
                Console.WriteLine($"{"File name:",-25}{fileName,-35}");
                Console.WriteLine($"{"Creation time:",-25}{file.CreationTime}");
                Console.WriteLine($"{"Last write time:",-25}{file.LastWriteTime}");
                Console.WriteLine($"{"Extension:",-25}{file.Extension}");
                Console.WriteLine();
            });
        }
        else
        {
            Console.WriteLine("Directory doesn't exist");
        }

        Console.WriteLine("\n[Press any key to exit]\n");
        Console.ReadKey();
    }


    private static void HandleUIException(string exceptionMessage, string moreInfo = "", string stackTrace="")
    {
        Console.WriteLine($"Unable to continue\n{exceptionMessage}");
        if (!string.IsNullOrEmpty(moreInfo))
        {
            Console.WriteLine($"More information:\n{moreInfo}");
        }
        if (!string.IsNullOrEmpty(stackTrace))
        {
            Console.WriteLine($"Stack trace:\n{stackTrace}");
        }
        Console.WriteLine("\n[Press any key to exit]\n");
        Console.ReadKey();
        Environment.Exit(1);
    }

    private static void ExitApplication(string msg)
    {
        Console.WriteLine(msg);
        Console.WriteLine("\n[Press any key to exit]\n");
        Console.ReadKey();
        Environment.Exit(1);
    }
}