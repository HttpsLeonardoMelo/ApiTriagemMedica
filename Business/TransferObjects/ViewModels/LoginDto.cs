using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.TransferObjects.ViewModels
{
	public class LoginDto
	{
		[MaxLength(255)]
		public string Email { get; set; }
		[MaxLength(255)]
		public string Senha { get; set; }
	}
}
