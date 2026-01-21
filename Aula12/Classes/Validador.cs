namespace Classes
{
    public static class Validador
    {
        public static bool EhMaiorDeIdade(int idade)
        {
            return idade >= 18;
        }

        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Contains("@") && email.Contains(".com");
        }
    }
}
