using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace ConsoleApp01
{
    class Program
    {
        public static string FileInput(string path)
        {
            string document = "";
            document = File.ReadAllText(path);
            return document;
        }

        public static string ConsoleInput()
        {
            string document = "";
            document = Console.ReadLine();
            return document;
        }

        public static string FindSentencesEnd(string doc)
        {
            List <int> indexesList = new List<int>();
            int[] indexes = new int[doc.Length];
            for (int i = 0; i < doc.Length; i++)
            {
                if (doc[i].Equals('!') || doc[i].Equals('?'))
                {
                    indexesList.Add(i);
                }
                else if(doc[i].Equals('.'))
                {
                    try
                    {
                        if (Char.IsLower(doc[i + 2]))
                        {
                            continue;
                        }
                        else
                        {
                            indexesList.Add(i);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        indexesList.Add(i);
                    }
                }
            }
            foreach (int index in indexesList)
            {
                Console.WriteLine($"{index} ");
            }
            return doc;
        }

        static void Main(string[] args)
        {
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
            FindSentencesEnd(document);

        }
    }
}
