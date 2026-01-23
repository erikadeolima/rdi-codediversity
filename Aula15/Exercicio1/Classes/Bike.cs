using System;

namespace Classes
{
  public class Bike : Transport
  {
    public Bike()
    {
      CostPerKm = 0.1m;
    }

    public override decimal CalculateCost(decimal distanceInKm)
    {
      return distanceInKm * CostPerKm;
    }
  }
}