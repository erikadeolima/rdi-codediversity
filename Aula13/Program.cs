using System;

namespace Program
{
    public static class Program
    {
        private const double fivePercentOfDiscount = 0.95;

        private const double tenPercentOfDiscount = 0.90;

        /* 
        Troquei essas declaraçoes pelo Enum
        private const int clientTypeOne = 1;
        private const int clientTypeTwo = 2;
        private const int clientTypeTree = 3; 
        
        */

            public enum CustomerType
        {
            ClientTypeOne = 1,
            ClientTypeTwo = 2,
            ClientTypeThree = 3
        }

        public static double CalculeDiscount(double totalOrderValue, int custumerType)
        {   
            // C# não suporta object desestructuring?
            // const { ClientTypeOne, ClientTypeTwo, ClientTypeTree } = CustomerType;
            // não existe
            if (custumerType == CustomerType.ClientTypeOne) return totalOrderValue * fivePercentOfDiscount;
            if (custumerType == CustomerType.ClientTypeTwo) return totalOrderValue * tenPercentOfDiscount;
            return totalOrderValue;
        }

        
        public static void Main(string[] args)
        {   
            Console.Write("Discount calculation for Customer type 1 ");
            Console.WriteLine(CalculeDiscount(100, clientTypeOne));

            Console.Write("Discount calculation for Customer type 2 ");
            Console.WriteLine(CalculeDiscount(100, clientTypeTwo));

            Console.Write("Discount calculation for Customer type 3 ");
            Console.WriteLine(CalculeDiscount(100, clientTypeTree));
        }
    }
}