using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.TransferObjects
{
	public class AtendimentoDto
	{
		public Guid Id { get; set; }  // uniqueidentifier
		public Guid PacienteId { get; set; }  // uniqueidentifier
		public int? Token { get; set; }  // int
		public DateTime? DataHoraChegada { get; set; }  // datetime
		public string? Status { get; set; }  // bit
	}
}
