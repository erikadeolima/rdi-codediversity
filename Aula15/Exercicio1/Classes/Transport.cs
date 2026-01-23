using System;

namespace Classes
{
  public abstract class Transport
  {
    public decimal CostPerKm { get; protected set; }

    public void Move()
    {
      Console.WriteLine("Starting transport movement ...");
    }

    public abstract decimal CalculateCost(decimal distanceInKm);
  }
}