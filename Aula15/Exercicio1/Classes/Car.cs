using System;

namespace Classes
{
  public class Car : Transport
  {
    public Car()
    {
      CostPerKm = 1.50m;
    }

    public override decimal CalculateCost(decimal distanceInKm)
    {
      return distanceInKm * CostPerKm;
    }
  }
}