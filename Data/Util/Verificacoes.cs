using Data.Interfaces.Util;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Data.Util
{
	public class Verificacoes : IVerificacoes
	{
		public bool EmailValido(string email)
		{
			try
			{
				var mailAddress = new MailAddress(email);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		public bool SenhaValida(string senha)
		{
			if (string.IsNullOrEmpty(senha))
			{
				return false;
			}

			// Verifica se o email tem mais de 8 caracteres
			if (senha.Length <= 8)
			{
				return false;
			}

			// Verifica se há pelo menos uma letra maiúscula
			if (!Regex.IsMatch(senha, "[A-Z]"))
			{
				return false;
			}

			return true;
		}
	}
}
