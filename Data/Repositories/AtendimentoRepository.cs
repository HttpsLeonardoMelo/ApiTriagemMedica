using System.Data;
using Dapper;
using Data.Interfaces;
using Data.Models;
using Data.Models.ViewModels;


namespace Data.Repositories
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        private readonly Func<IDbConnection> _connection;

        public AtendimentoRepository(Func<IDbConnection> connection)
        {
            _connection = connection;
        }

        public async Task<Atendimento> Add(Atendimento atendimento)
        {

            atendimento.AtendimentoId = Guid.NewGuid();

            const string sql_script = @"INSERT INTO Atendimento (AtendimentoId, PacienteId, DataHoraChegada, Status) VALUES (@atendimentoId, @pacienteId, @dataHoraChegada, @status)";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@atendimentoId", atendimento.AtendimentoId);
                parametros.Add("@pacienteId", atendimento.PacienteId);
                parametros.Add("@dataHoraChegada", DateTime.Now);
                parametros.Add("@status", "Aguardando Atendimento");
                await connection.ExecuteAsync(sql_script, parametros);

                return atendimento;
            }
        }

		public async Task<string> AtualizarStatus(Guid id, string status)
		{
			const string sql_script = @"UPDATE Atendimento set Status = @status WHERE AtendimentoId = @id";

			using (IDbConnection connection = _connection.Invoke())
			{
				var parametros = new DynamicParameters();
				parametros.Add("@id", id);
				parametros.Add("@status", status);

				await connection.ExecuteAsync(sql_script, parametros);

				return "Status alterado com sucesso: " + status;
			}
		}

		public async Task<IEnumerable<Atendimento>> GetAll()
        {
            string sql_script = @"SELECT AtendimentoId, PacienteId, Token, DataHoraChegada, Status FROM Atendimento";

            using (IDbConnection connection = _connection.Invoke())
            {
                return await connection.QueryAsync<Atendimento>(sql_script);
            }
        }

		public async Task<IEnumerable<ChamadaPaciente>> Fila()
		{
			string sql_script = @"SELECT A.AtendimentoId, A.Token, P.Nome, P.PacienteId FROM Atendimento AS A LEFT JOIN Paciente AS P ON A.PacienteId = P.PacienteId WHERE A.Status = 'Aguardando Atendimento' Order By A.Token";

			using (IDbConnection connection = _connection.Invoke())
			{
				return await connection.QueryAsync<ChamadaPaciente>(sql_script);
			}
		}

		public async Task<Atendimento> GetById(Guid id)
		{
			const string sql_script = @"SELECT AtendimentoId, PacienteId, Token, DataHoraChegada, Status FROM Atendimento WHERE AtendimentoId = @id";

			using (IDbConnection connection = _connection.Invoke())
			{
				var parametros = new DynamicParameters();
				parametros.Add("@id", id);
				return await connection.QueryFirstOrDefaultAsync<Atendimento>(sql_script, parametros);
			}
		}

        public async Task<string> Delete(Guid id)
        {
            const string sql_script = @"DELETE FROM Atendimento WHERE AtendimentoId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                await connection.ExecuteAsync(sql_script, parametros);
                return "Item excluido com sucesso";
            }
        }

		public async Task<ChamadaPaciente> ChamarProximo(string status)
		{
			const string sql_script = @"SELECT A.AtendimentoId, A.Token, P.Nome, P.PacienteId
                                        FROM Atendimento AS A
                                        LEFT JOIN Paciente AS P ON A.PacienteId = P.PacienteId
                                        WHERE A.Token = (
                                            SELECT MIN(A1.Token)
                                            FROM Atendimento AS A1
                                            WHERE A1.Status = @status
                                        );";

			using (IDbConnection connection = _connection.Invoke())
			{
				var parametros = new DynamicParameters();
				parametros.Add("@status", status);

				ChamadaPaciente chamada = await connection.QueryFirstOrDefaultAsync<ChamadaPaciente>(sql_script, parametros);

				return chamada;
			}
		}
	}
}
