using System;
using System.Collections.Generic;
using System.IO;

namespace FixedWidthExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == null || !File.Exists(args[0]))
            {
                Console.WriteLine("The program was expecting an existing data source file as argument.");
                return;
            }

            var sourcePath = args[0];

            var delimiters = new List<Tuple<int, int, string>>();

            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                var content = line.Split(',');

                try
                {
                    var a = int.Parse(content[0]);
                    var b = int.Parse(content[1]);
                    var name = content[2];

                    delimiters.Add(Tuple.Create(a - 1, b - 1, name));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            try
            {
                using (var reader = new StreamReader(sourcePath))
                {
                    var first = true;
                    foreach (var delimiter in delimiters)
                    {
                        if (!first)
                            Console.Write(',');

                        Console.Write(delimiter.Item3);
                        first = false;
                    }
                    Console.WriteLine();

                    while ((line = reader.ReadLine()) != null)
                    {
                        first = true;
                        foreach (var delimiter in delimiters)
                        {
                            if (!first)
                                Console.Write(',');

                            Console.Write(line.Substring(delimiter.Item1, delimiter.Item2 - delimiter.Item1 + 1));
                            first = false;
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
