namespace Classes;

public class Usuario 
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public bool EmailValido()
    {
        return Validador.EmailValido(Email);
    }
}
