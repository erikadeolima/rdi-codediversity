namespace Classes;

public class Pedido
{    
    public decimal ValorTotal { get; set; }
  
    public bool ValorValido(decimal valor)
    {
        return valor > 0;
    }
}