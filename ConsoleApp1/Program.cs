using SC.BL;
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
        MethodInfo huidigeMethode = (MethodInfo)methodInfos[j];
        Console.WriteLine(huidigeMethode.Name + " \t\treturnt:  " + huidigeMethode.ReturnParameter);
      }

      Console.WriteLine("****************************");
      Type myTicketManagerCon = (typeof(TicketManager));
      ConstructorInfo[] Consinfo = myTicketManagerCon.GetConstructors();
      for (int j = 0; j < Consinfo.Length; j++)
      {
        ConstructorInfo huidigeCon = (ConstructorInfo)Consinfo[j];
        Console.WriteLine(huidigeCon.Name);
      }

      Console.WriteLine("****************************");
      Type myTicketManagerProp = (typeof(TicketManager));
      PropertyInfo[] PropInfo = myTicketManagerProp.GetProperties();
      for (int j = 0; j < PropInfo.Length; j++)
      {
        PropertyInfo huidigeInfo = (PropertyInfo)PropInfo[j];
        Console.WriteLine(huidigeInfo);
      }


        Console.ReadLine();
    }
  }
}
