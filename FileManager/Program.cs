using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FileManager
{
    internal class Program
    {
        private static List<string> options = new List<string>();
        private static List<string> foptions = new List<string>();
        static int famount;
        static string currentpath = "";
        static int damount;
        static string backpath;
        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 60);
            Console.WriteLine("2 to go back. D to delete, A to make file, f to create directory");
            Console.CursorVisible = false;
            drivelist();
        }
        public static void arrows(int amount)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("1");
            bool run = false;
            int selector = 0;
            int selected = 0;
            while (run != true)
            {
                ConsoleKeyInfo menuchoosekey = Console.ReadKey();
                string choosekey = (menuchoosekey.Key.ToString());
                switch (choosekey)
                {
                    case "UpArrow":
                        Console.SetCursorPosition(0, selector);
                        selector--;
                        break;
                    case "DownArrow":
                        Console.SetCursorPosition(0, selector);
                        selector++;
                        break;
                    case "Enter":
                        selected = selector;
                        currentpath = currentpath + options[selected];
                        run = true;
                        try
                        {
                            folder(currentpath);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            currentpath = currentpath + "/";
                            string filepath = currentpath + foptions[famount];
                            Console.WriteLine(filepath);
                            Thread.Sleep(10000);
                            Console.ReadKey();
                        }
                        break;
                    case "D2":
                        run = true;
                        backpath = currentpath;
                        currentpath = Path.GetDirectoryName(currentpath);
                        try
                        {
                            folder(currentpath);
                        }
                        catch (System.Exception)
                        {
                            drivelist();
                        }
                        break;
                    case "D":
                        {
                            try
                            {
                                Directory.Delete(currentpath, true);
                            }
                            catch (System.IO.IOException)
                            {
                                try
                                {
                                    System.IO.File.Delete(currentpath);
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Ошибка");
                                }
                            }
                            folder(currentpath);
                            break;
                        }
                    case "A":
                        {
                            Console.CursorVisible = true;
                            Console.SetCursorPosition(85, 10);
                            Console.WriteLine("Enter file name: ");
                            Console.SetCursorPosition(85, 11);
                            string filename = Console.ReadLine();
                            Console.CursorVisible = false;

                            try
                            {
                                FileStream fileStream = System.IO.File.Create(currentpath + "\\" + filename);
                                fileStream.Dispose();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Ошибка");
                            }
                            folder(currentpath);
                            break;
                            
                        }
                    case "F":
                        {
                            Console.CursorVisible = true;
                            Console.SetCursorPosition(85, 10);
                            Console.WriteLine("Enter Folder name: ");
                            Console.SetCursorPosition(85, 11);
                            string dirName = Console.ReadLine();
                            Console.CursorVisible = false;
                            try
                            {
                                Directory.CreateDirectory(currentpath + "\\" + dirName);
                            }
                            catch (UnauthorizedAccessException)
                            {
                                
                            }
                            folder(currentpath);
                            break;
                        }
                }
                if (selector < 0)
                    selector = amount - 1;
                if (selector > amount)
                    selector = amount + 1;
                Console.Write("  ");
                Console.SetCursorPosition(0, selector);
                Console.Write(selector + 1);
            }
            Console.Clear();
        }

        public static void drivelist()
        {
            DriveInfo[] AllDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in AllDrives)
            if (d.IsReady == true)
            {
                if (d.IsReady == true)
                {
                    double condition = 0;
                    condition = Convert.ToDouble(d.TotalFreeSpace) / Convert.ToDouble(d.TotalSize);
                    condition = (Math.Round(condition, 1));
                        string bar = "";
                        switch (condition)
                        {
                            case 0:
                                {
                                    bar = ("         ████████████████████████████████████████ ");
                                    break;
                                }
                            case 0.1:
                                {
                                    bar = ("         ████████████████████████████████████░░░░ ");
                                    break;
                                }
                            case 0.2:
                                {
                                    bar = ("         ████████████████████████████████░░░░░░░░ ");
                                    break;
                                }
                            case 0.3:
                                {
                                    bar = ("         ████████████████████████████░░░░░░░░░░░░ ");
                                    break;
                                }
                            case 0.4:
                                {
                                    bar = ("         ████████████████████████░░░░░░░░░░░░░░░░ ");
                                    break;
                                }
                            case 0.5:
                                {
                                    bar = ("         ████████████████████░░░░░░░░░░░░░░░░░░░░ ");
                                    break;
                                }
                            case 0.6:
                                {
                                    bar = ("         ████████████████░░░░░░░░░░░░░░░░░░░░░░░░ ");
                                    break;
                                }
                            case 0.7:
                                {
                                    bar = ("         ████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ");
                                    break;
                                }
                            case 0.8:
                                {
                                    bar = ("         ████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ");
                                    break;
                                }
                            case 0.9:
                                {
                                    bar = ("         ████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ");
                                    break;
                                }
                            case 1:
                                {
                                    bar = ("         ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ");
                                    break;
                                }
                        }
                        Console.WriteLine($"    {d.VolumeLabel}   {bar}");
                        options.Add(d.Name);
                    famount += 1;
                }
            }
            arrows(famount);
        }
        public static void folder(string folderpath)
        {
            Console.Clear();
            options.Clear();
            famount = 0;
            damount = 0;
            foreach (string s in Directory.GetDirectories(folderpath))
            {
                string folderout = (s.Remove(0, folderpath.Length));
                Console.WriteLine("   {0}", slashremover(folderout));
                options.Add(folderout);
                damount++;
            }
            DirectoryInfo d = new DirectoryInfo(folderpath);
            FileInfo[] Files = d.GetFiles();
            foreach (FileInfo file in Files)
            {
                Console.WriteLine("   {0}", file.Name);
                options.Add(file.Name);
                foptions.Add(file.Name);
                famount++;
                damount++;
            }
            arrows(damount);
        }
        public static string slashremover(string str)
        {
            var chara = new string[] { "/", @"\" };
            foreach (var c in chara)
            {
                str = str.Replace(c, string.Empty);
            }
            return str;
        }
    }
}
