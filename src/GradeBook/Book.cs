using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
   // reference null exception hatası field alanının 
    // doğru tanımlanmamasından kaynaklanabilir.
    public delegate void GradeAddDelegate(object sender, EventArgs args);
    public class NamedObject   // kendi ayrı dosyası olması
    {
        public NamedObject(string name)
        {
          Name = name;
        }
        public string Name { get; set; }
    }

    public interface IBook
    {
      void AddGrade(double grade);
      Statistics GetStatistics();
      string Name { get;}
      event GradeAddDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
      public Book(string name) : base(name)
      {
      }
      public abstract event GradeAddDelegate GradeAdded; // override edilebilmesi için virtual kullandık.
      public abstract void AddGrade(double grade);
      public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book 
    {
      public DiskBook(string name): base(name)
      {
        
      }

      public override event GradeAddDelegate GradeAdded;

      public override void AddGrade(double grade)
      {
        using(var writer = File.AppendText($"{Name}.text"))
        {
          //using { } kullanılırsa bitince dispose çağırılır.
           writer.WriteLine(grade);
           if(GradeAdded != null)
           {
             GradeAdded(this, new EventArgs());
           }
        }
        //writer.Dispose(); // close file, clean up
      }

      public override Statistics GetStatistics()
      {
        var result = new Statistics();

        using(var reader = File.OpenText($"{Name}.txt"))
        {
          var line = reader.ReadLine();
          while(line != null)
          {
            var number = double.Parse(line);
            result.Add(number);
            line = reader.ReadLine();
          }
        }
        return result;
      }
    }
    public class InMemoryBook : Book
    {
      // base ile inherit edilen base class constructor'a erişilebilir. chaining
      public InMemoryBook(string name) : base(name) 
      {
        grades = new List<double>(); //initialize
        Name = name;
      }

      // polymorphisim
      public override void AddGrade(double grade)
      {
        
      if(grade <= 100 && grade >= 0)
      {
        grades.Add(grade);
        if( GradeAdded != null )
        {
          GradeAdded(this, new EventArgs());
        } 
      }
      else
      {
        throw new ArgumentException($"Invalid {nameof(grade)}");
      }
        
      }

      public override event GradeAddDelegate GradeAdded;
      public override Statistics GetStatistics()
      {
        var result = new Statistics();
        

          for(var index=0; index<grades.Count; index++)
          {
            result.Add(grades[index]);
          }
          return result;
      }
        // fields
        private List<double> grades;
        
    }
}