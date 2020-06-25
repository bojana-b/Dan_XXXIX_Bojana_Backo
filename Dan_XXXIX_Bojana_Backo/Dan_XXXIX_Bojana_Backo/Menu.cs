using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dan_XXXIX_Bojana_Backo
{
    static class Menu
    {
        public static List<Song> playlist = new List<Song>();
        public static string fileMusic = @"..\..\Music.txt";
        public static string fileAdvertising = @"..\..\Advertising.txt";
        public static string author;
        public static string name;
        public static string duration;
        public static DateTime time;
        public static AutoResetEvent A = new AutoResetEvent(false);
        public static bool stopAdvertising = false;
        /// <summary>
        ///  Function to be called at the begining od app
        /// </summary>
        public static void Begin()
        {
            Console.WriteLine("\n*************** Audio Player ***************");
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
                        case 1: CreateNewPlaylist();
                            break;
                        case 2: ChoseASong();
                            break;
                        default:
                            break;
                    }
                    Begin();
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
                    foreach (var item in playlist)
                    {
                        sw.WriteLine(item);
                    }
                    sw.Close();
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
                        author = input;
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
                        name = input;
                        b = false;
                    }
                } while (b);
                do
                {
                    Console.Write("\nDuration [format hh:mm:ss]: ");
                    string input = Console.ReadLine();
                    string format = "g";
                    bool valid = TimeSpan.TryParseExact(input, format, CultureInfo.InvariantCulture, out TimeSpan timeSpan);
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("\nYou must provide some input!");
                        b = true;
                    }
                    else if (!valid)
                    {
                        Console.WriteLine("\nNot valid format!!!!");
                        b = true;
                    }
                    else
                    {
                        duration = timeSpan.ToString();
                        b = false;
                    }
                } while (b);
            } while (repeat);

            Song song = new Song(author, name, duration);
            playlist.Add(song);
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
                    if (string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase))
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
            WriteToFileMusic();
        }

        /// <summary>
        /// Function that Read from file Music.txt and load to screen
        /// </summary>
        public static void ReadFromFileMusic()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(fileMusic))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] lines = line.Split(']');
                        string songAuthor = lines.ElementAt(0);
                        songAuthor = songAuthor.Replace("[", "");
                        string songName = lines.ElementAt(1);
                        songName = songName.Replace("[", "");
                        songName = songName.Replace(":", "");
                        string songDuration = lines.ElementAt(2);
                        songDuration = songDuration.Replace(" [", "");
                        playlist.Add(new Song(songAuthor, songName, songDuration));
                    }
                    Console.WriteLine("\n*********************PLAYLIST*********************\n");
                    for (int i = 0; i < playlist.Count; i++)
                    {
                        Console.WriteLine("{0}. {1}", i+1, playlist.ElementAt(i).ToString());
                    }
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
        /// Function to be called when user wants to chose a song from a playlist
        /// </summary>
        public static void ChoseASong()
        {
            ReadFromFileMusic();
            bool repeat = false;
            int num;
            do
            {
                Console.Write("\nWhich song you choose? Enter the number of the song -> ");
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
                else if (num > playlist.Count)
                {
                    Console.WriteLine("\nThere is no song with that number");
                    repeat = true;
                }
                else
                {
                    Console.WriteLine("\n" + DateTime.Now.ToShortTimeString() + " " + playlist.ElementAt(num - 1).Name);
                    time = DateTime.ParseExact(playlist.ElementAt(num - 1).Duration, "HH:mm:ss", CultureInfo.InvariantCulture);
                    SongIsPlaying(time);
                    repeat = false;
                }

            } while (repeat);
        }

        /// <summary>
        /// Function to be called by a Thread manager who plays the song
        /// </summary>
        /// <param name="obj"></param>
        public static void SongIsPlaying(object obj)
        {
            double totalTimeElapsed = 0.0;
            Thread tAdvertStarter = new Thread(new ThreadStart(Advertising));
            tAdvertStarter.Start();
            //threadAdvertSignal.Set();
            Stopwatch stopwatch = new Stopwatch();
            while (totalTimeElapsed <= time.TimeOfDay.TotalMilliseconds)
            {
                stopwatch.Start();
                Thread.Sleep(1000);
                totalTimeElapsed += 1000;
                if (totalTimeElapsed > time.TimeOfDay.TotalMilliseconds)
                {
                    Console.WriteLine("Song is over.");
                    stopAdvertising = true;
                    break;
                }
                Console.WriteLine("Song is playing (la - la - la) ...");
            }
        }

        /// <summary>
        /// Function that reads advertising from file Advertising
        /// and send random one while song is playing
        /// </summary>
        public static void Advertising()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(fileAdvertising))
                {
                    List<string> advertise = new List<string>();
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        advertise.Add(line);
                    }
                    while (!stopAdvertising)
                    {
                        string adv = advertise.OrderBy(s => Guid.NewGuid()).First();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(adv);
                        Console.ResetColor();
                        Thread.Sleep(200);
                    }
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
    }
}
