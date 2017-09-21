using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ConsoleApp01
{
    class Program
    {
        public static string FileInput(string path)
        {
            string document = "";
            try
            {
                document = File.ReadAllText(path);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Path to file is not valid.");
            }
            catch (PathTooLongException e)
            {
                Console.WriteLine("Path to file is too long.");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("No such directory.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No such file.");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("No access to file.");
            }

            return document;
        }

        public static string ConsoleInput()
        {
            string document = "";
            document = Console.ReadLine();
            return document;
        }

        static void Main(string[] args)
        {
            string[] sentenceEnd = {".", "!", "?"};
            string document = "";
            if (ConfigurationManager.AppSettings.Get("ConsoleInput").Equals("true"))
            {
                Console.WriteLine("[Enter your string line..]");
                document = ConsoleInput();;
            }
            else
            {
                Console.WriteLine("[Reading from file..]");
                document = FileInput(ConfigurationManager.AppSettings.Get("FilePath"));
            }
            Console.WriteLine(document);
            document = document.ToLower();
            string pattern = "(.+?)([*.!?])\\s+";
            string replacement = $"[{DateTime.Now}:" +$"{DateTime.Now.Millisecond}" + "] $1$2\n";
            Regex reg = new Regex(pattern);
            document = reg.Replace(document, replacement);
            Console.WriteLine(document);
        }
    }
}
