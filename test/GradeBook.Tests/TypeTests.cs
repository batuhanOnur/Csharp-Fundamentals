using System;
using Xunit;

namespace GradeBook.Tests
{ 
    public delegate string WriteLogDelegate(string LogMessage);

    public class TypeTests
    {
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
        }

        string ReturnMessage(string message)
        {
          return message;
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
          var x = GetInt();
          SetInt(ref x);

          Assert.Equal(42, x);
        }
        private void SetInt(ref int z)
        {
            z = 42;
        }
        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
          var book1 = GetBook("Book 1");
          GetBookSetName(out book1,"New Name");

          Assert.Equal("New Name", book1.Name);
        }
        private void GetBookSetName(out InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }


        [Fact]
        public void CSharpIsPassByValue()
        {
          var book1 = GetBook("Book 1");
          GetBookSetName(book1,"New Name");

          Assert.Equal("Book 1", book1.Name);
        }
        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }


        [Fact]
        public void CanSetNameFromReference()
        {
          var book1 = GetBook("Book 1");
          SetName(book1,"New Name");

          Assert.Equal("New Name", book1.Name);
        }
        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }
          
        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
          string name = "Scott";
          var upper = MakeUpperCase(name);

          Assert.Equal("SCOTT", upper);
        }
        private string MakeUpperCase(string parameter)
        {
              return parameter.ToUpper();
        }

        [Fact] //attribute
        public void GetBookReturnsDiffirentObjects()
        {
          var book1 = GetBook("Book 1");
          var book2 = GetBook("Book 2");
          
          Assert.Equal("Book 1", book1.Name);
          Assert.Equal("Book 2", book2.Name);
          Assert.NotSame(book1,book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
          var book1 = GetBook("Book 1");
          var book2 = book1;
          
          Assert.Same(book1, book2);
          Assert.True(Object.ReferenceEquals(book1, book2));
        }
        InMemoryBook GetBook(string name)
        {
          return new InMemoryBook(name);
        }
        
    }
}
