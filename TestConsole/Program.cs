using DAL.UOW;
using System;
using DAL.Models.CommonModels;
namespace TestConsole
{
    class Program
    {
      static public  Random r=new Random();
       
        static void Main(string[] args)
        {
            var unit = new UnitOfWork(new DAL.Context.ApplicationContext());




            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
