using System;

namespace CalculateFinalPayment
{
    public class Program
    {
        private const double validValue = 0;

        public enum CustomerType
        {
            ClientRegular = 1,
            ClientPremium = 2,
            ClientVip = 3
        }

        private const double DiscountClientRegular = 0.05;
        private const double DiscountClientPremium = 0.1;
        private const double DiscountClientVip = 0.15;

        public static double CalculateDiscount(CustomerType clientType) => clientType switch
        {
            CustomerType.ClientRegular => DiscountClientRegular,
            CustomerType.ClientPremium => DiscountClientPremium,
            CustomerType.ClientVip => DiscountClientVip,
            _ => 0
        };

        public static double CalculateFinalPayment(int quantity, double unitPriceItem, CustomerType clientType, bool hasDiscount)
        {
            if (quantity <= validValue || unitPriceItem <= validValue) return 0;

            double totalValue = quantity * unitPriceItem;
            
            if (hasDiscount)
            {
                totalValue -= totalValue * CalculateDiscount(clientType);
            }

            return totalValue;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Discount calculation for Customer type Regular: " + CalculateFinalPayment(1, 100, CustomerType.ClientRegular, true));

            Console.WriteLine("Discount calculation for Customer type Premium: " + CalculateFinalPayment(1, 100, CustomerType.ClientPremium, true));
            
            Console.WriteLine("Discount calculation for Customer type VIP: " + CalculateFinalPayment(1, 100, CustomerType.ClientVip, true));
        }
    }
}