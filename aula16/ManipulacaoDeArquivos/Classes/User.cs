using System;

namespace Classes
{
  public class User
  {
    public string name { get; set; }
    public int age { get; set; }

    public User(string name, int age)
    {
      this.name = name;
      this.age = age;
    }
  }
}