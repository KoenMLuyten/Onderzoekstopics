using SC.BL;
using SC.BL.Domain;
using System;
using System.Reflection;
using System.Reflection.Emit;


namespace ConsoleApp1
{
  class Program
  {
    static void Main(string[] args)
    {
      double i = 56.6;
      System.Type type = i.GetType();
      Console.WriteLine("i is een " + type);


      Type myTicketManager = (typeof(TicketManager));
      MethodInfo[] methodInfos = myTicketManager.GetMethods();
      for (int j = 0; j < methodInfos.Length; j++)
      {
        MethodInfo huidigeMethode = methodInfos[j];
        Console.WriteLine(huidigeMethode.Name + " \t\treturnt:  " + huidigeMethode.ReturnParameter);
      }

      Console.WriteLine("****************************");
      Type myTicketManagerCon = (typeof(TicketManager));
      ConstructorInfo[] Consinfo = myTicketManagerCon.GetConstructors();
      for (int j = 0; j < Consinfo.Length; j++)
      {
        ConstructorInfo huidigeCon = Consinfo[j];
        Console.WriteLine(huidigeCon.Name);
      }
      //verder uitgewerkt in onderstaande code
      /*
      Console.WriteLine("****************************");
      var TickType = typeof(Ticket);
      var TickProps = TickType.GetProperties();
      foreach (var TickInf in TickProps)
      {
        Console.WriteLine(TickInf.Name);
      }*/

      Console.WriteLine("****************************");
      Type TickType = typeof(Ticket);
      PropertyInfo[] TickProps = TickType.GetProperties();
      foreach (var TickInf in TickProps)
      {
        Console.Write("{0,-20}", TickInf.Name);
        var accessors = TickInf.GetAccessors();
        foreach (var accessor in accessors)
        {
          Console.Write(accessor.Name + "; ");
        }
        Console.WriteLine();
      }

      Ticket honderd = new Ticket();
      Type honderdType = honderd.GetType();
      PropertyInfo prInfo = honderdType.GetProperty("AccountId");
      Console.WriteLine("Huidige waarde: " + prInfo.GetValue(honderd, null));
      prInfo.SetValue(honderd, 100, null);
      Console.WriteLine("Huidige waarde: " + prInfo.GetValue(honderd, null));

      Console.ReadKey();
    }
  }
}
