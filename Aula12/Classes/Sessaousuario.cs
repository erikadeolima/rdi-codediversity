namespace Classes;

public class SessaoUsuario
{
    public string NomeUsuarioAtivo { get; set; }

    public bool UsuarioLogado()
    {
        return !string.IsNullOrEmpty(NomeUsuarioAtivo);
    }

    public void DefinirUsuarioAtivo(string nomeUsuario)
    {
        NomeUsuarioAtivo = nomeUsuario;
    }
}
