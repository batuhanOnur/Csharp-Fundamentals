using System;
using System.Collections.Generic;


namespace GradeBook
{
   class Program
   {
      static void Main(string[] args)
      {
            IBook book = new DiskBook("Scott's Grade Book"); // new ile constructor invoke edilir.
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);
            var stats = book.GetStatistics();
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highsest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter is {stats.Letter}");
      }
      private static void EnterGrades(IBook book)
      {
            while (true)
            {

               Console.WriteLine("Enter a grade or 'q' to quit.");
               var input = Console.ReadLine(); //string

               if (input == "q")
               {
                  break;
               }

               try
               {
                  var grade = double.Parse(input); // type conversion
                  book.AddGrade(grade);
               }
               catch (ArgumentException ex)
               {
                  Console.WriteLine(ex.Message);
               }
               catch (FormatException ex)
               {
                  Console.WriteLine(ex.Message);
               }
               finally
               {
                  Console.WriteLine("**");
               }
            }
      }

      static void OnGradeAdded(object sender, EventArgs e)
      {
         Console.WriteLine("Grade is added");
      }
   }
}
