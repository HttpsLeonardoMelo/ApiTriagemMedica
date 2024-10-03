namespace Data.Interfaces.Util
{
    public interface IVerificacoes
    {
        bool EmailValido(string email);
        bool SenhaValida(string senha);
    }
}
