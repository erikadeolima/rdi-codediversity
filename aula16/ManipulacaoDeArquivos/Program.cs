using System;
using Classes;

class Program
{
  public static void Main(string[] args)
  {
    var repositry = new UserRepository();
    var menu = new MenuSystem(repositry);
    menu.Execute();
  }
}
