using System;
using System.Collections.Generic;


namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
           var book = new Book("Scott's Grade Book"); // new ile constructor invoke edilir.
           book.AddGrade(89.1);
           book.AddGrade(90.5);
           book.AddGrade(77.5);
           book.ShowStatistics();
        }
    }
}
