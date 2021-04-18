using System;
using System.Collections.Generic;

namespace GradeBook
{
    // reference null exception hatası field alanının 
    // doğru tanımlanmamasından kaynaklanabilir.
    public class Book
    {
      public Book(string name)
      {
        grades = new List<double>(); //initialize
        this.name = name;
      }
      public void AddGrade(double grade)
      {
        grades.Add(grade); 
      }
      public Statistics GetStatistics()
      {
         var result = new Statistics();
         result.Average = 0.0;
         result.High = double.MinValue;
         result.Low = double.MaxValue;

           foreach(var grade in grades)
           {
             result.Low = Math.Min(grade, result.Low);
             result.High = Math.Max(grade, result.High);
             result.Average += grade;
           }
           result.Average /= grades.Count;

           return result;
      }

        // fields
        private List<double> grades;
        private string name; 
    }
}