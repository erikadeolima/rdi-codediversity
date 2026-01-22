using System;

// Dupla: Erika Lima e Marília Lima

public class Program
{
    private const double validValue = 0;

    /* 

    Camile Nós haviamos declarado o enum mas por algum motivo não compilou não entendemos o porque....


    public enum CustomerType
        {
            ClientRegular = 1,
            ClientPremium = 2,
            ClientVip= 3
        } 
    */

    private const int ClientRegular = 1;
    private const int ClientPremium = 2;
    private const int ClientVip= 3;

    
    private const double DiscountClientRegular = 0.05;
    private const double DiscountClientPremium = 0.1;
    private const double DiscountClientVip = 0.15;
        

    public static double CalculateFinalPayment(int quantity, double unitPriceItem, int clientType, bool hasDiscount){
        double totalValue = 0;

        if (quantity <= validValue || unitPriceItem <= validValue){ return 0; }


        if (clientType == ClientRegular){
            totalValue = quantity * unitPriceItem;
            if (hasDiscount){
                totalValue = totalValue - (totalValue * DiscountClientRegular);
            }
        }
        else if (clientType == ClientPremium){
            totalValue = quantity * unitPriceItem;
            if (hasDiscount)
            {
                totalValue = totalValue - (totalValue * DiscountClientPremium);
            }
        }
        else if (clientType == DiscountClientVip){
            totalValue = quantity * unitPriceItem;
            if (hasDiscount){ totalValue = totalValue - (totalValue * DiscountClientVip); }
        }
        else{
            totalValue = quantity * unitPriceItem;
        }


        return totalValue;
    }

    public static void Main(string[] args)
        {   
            Console.Write("Discount calculation for Customer type Regular: ");
            Console.WriteLine(CalculateFinalPayment(1,100,1,true));

            
        }
}
