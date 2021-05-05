using CSharpFundamental1;
using System;
using System.IO;

namespace Grades
{
    //Can I set this program to public?
    public class Program
    {
        //Can I set this main to public?
        public static void Main(string[] args)
        {
            SpeechSpeaker.Speaker();

            IGradeTracker book = CreateGradeBook();

            try
            {
                using (FileStream stream = File.Open("grades.txt", FileMode.Open))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        float grade = float.Parse(line);
                        book.AddGrade(grade);
                        line = reader.ReadLine();
                    }
                }

                //book.AddGrade(91f);
                //book.AddGrade(89.1f);
                //book.AddGrade(75f);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Could not locate the file grades.txt");
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("No access");
                return;
            }

            foreach (float grade in book)
            {
                Console.WriteLine(grade);
            }

            try
            {
                Console.WriteLine("Please enter a name for the book");
                book.Name = Console.ReadLine();
                //Delegate playground.
                //Book call deledgate Namgechanged. If namechanged is not null, then
                book.NameChanged += OnNameChanged;
                book.NameChanged += OnNameChanged;
                book.NameChanged += OnNameChanged2;
                book.NameChanged -= OnNameChanged;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid name");
            }


            book.Name = "Allen's Book";
            WriteNames(book.Name);
            GradeStatistics stats = book.ComputeStatistics();
            Console.WriteLine(stats.AverageGrade);
            Console.WriteLine(stats.LowestGrade);
            Console.WriteLine(stats.HighestGrade);
            Console.WriteLine("{0} {1}", stats.LetterGrade, stats.Description);

            Console.WriteLine("Press any key to exist");
            Console.ReadLine();
        }

        private static void OnNameChanged2(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine("***");
        }

        private static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine("Name changed from {0} to {1}",
                args.OldValue, args.NewValue);
        }

        private static void WriteNames(params string[] names)
        {
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }

        private static IGradeTracker CreateGradeBook()
        {
            IGradeTracker book = new ThrowAwayGradeBook("Scott's Book");
            return book;
        }
    }
}
