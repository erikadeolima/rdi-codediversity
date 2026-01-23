using System;

namespace Classes
{
  public class Bus : Transport
  {
    public Bus()
    {
      CostPerKm = 5.00m;
    }

    public override decimal CalculateCost(decimal distanceInKm)
    {
      int passengers = 40;
      return (distanceInKm * CostPerKm) / passengers;
    }
  }
}