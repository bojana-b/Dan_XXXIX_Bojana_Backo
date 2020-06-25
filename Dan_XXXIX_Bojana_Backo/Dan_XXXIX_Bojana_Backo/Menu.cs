using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan_XXXIX_Bojana_Backo
{
    static class Menu
    {
        public static List<string> playlist = new List<string>();
        public static string fileMusic = @"..\..\Music.txt";
        /// <summary>
        ///  Function to be called at the begining od app
        /// </summary>
        public static void Begin()
        {
            Console.WriteLine("*************** Audio Player ***************");
            Console.WriteLine("You can chose one of the following options:");
            Console.WriteLine("[1] Add new playlist" +
                "\n[2] See your playlist");
            bool repeat = false;
            int num;
            do
            {
                Console.Write("\nYour choice (1 or 2) -> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nYou must provide some input!");
                    repeat = true;
                }
                else if (!Int32.TryParse(input, out num))
                {
                    Console.WriteLine("\nRead instructions!!!!");
                    repeat = true;
                }
                else
                {
                    num = Int32.Parse(input);
                    switch (num)
                    {
                        case 2: CreateNewPlaylist();
                            break;
                        default:
                            break;
                    }
                    repeat = false;
                }
            } while (repeat);
        }
        
        /// <summary>
        /// Function that write playlist to the file Music.txt
        /// </summary>
        public static void WriteToFileMusic()
        {
            try
            {
                File.Delete(fileMusic);
                using (StreamWriter sw = File.CreateText(fileMusic))
                {
                    //foreach (var item in routesToDestination)
                    //{
                    //    sw.WriteLine(item);
                    //}
                    sw.Close();
                    //Console.WriteLine(Thread.CurrentThread.Name + " has finished writing to the file!");
                }

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
            catch (IOException e)
            {
                Console.WriteLine($"The file could not be opened: '{e}'");
            }
        }

        /// <summary>
        /// Function to be called when user wants to add a new playlist
        /// </summary>
        public static void CreateNewPlaylist()
        {
            bool repeat = false;
            bool b = false;
            do
            {
                do
                {
                    Console.Write("\nAdd Author: ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("\nYou must provide some input!");
                        b = true;
                    }
                    else
                    {
                        playlist.Add(input);
                        b = false;
                    }
                } while (b);
                do
                {
                    Console.Write("\nAdd Name of song: ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("\nYou must provide some input!");
                        b = true;
                    }
                    else
                    {
                        playlist.Add(input);
                        b = false;
                    }
                } while (b);
                do
                {
                    Console.Write("\nDuration [format 00:00:00]: ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("\nYou must provide some input!");
                        b = true;
                    }
                    else
                    {
                        playlist.Add(input);
                        b = false;
                    }
                } while (b);
            } while (repeat);

            do
            {
                Console.Write("\nAdd another one: Y / N  ->  ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nYou must provide some input!");
                    repeat = true;
                }
                else
                {
                    if (input.Equals("Y"))
                    {
                        CreateNewPlaylist();
                        repeat = false;
                    }
                    else
                    {
                        repeat = false;
                    }
                }
            } while (repeat);
        }

    }
}
