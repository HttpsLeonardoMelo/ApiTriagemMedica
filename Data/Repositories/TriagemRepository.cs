using System.Data;
using Dapper;
using Data.Interfaces;
using Data.Models;


namespace Data.Repositories
{
    public class TriagemRepository : ITriagemRepository
    {
        private readonly Func<IDbConnection> _connection;

        public TriagemRepository(Func<IDbConnection> connection)
        {
            _connection = connection;
        }

        public async Task<Triagem> Add(Triagem triagem)
        {

            triagem.TriagemId = Guid.NewGuid();

            const string sql_script = @"INSERT INTO Triagem (TriagemId, AtendimentoId, Sintomas, PressaoSistolica, PressaoDiastolica, Peso, Altura, EspecialidadeId) VALUES (@triagemId, @atendimentoId, @sintomas, @pressaoSistolica, @pressaoDiastolica, @peso, @altura, @EspecialidadeId)";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@triagemId", triagem.TriagemId);
                parametros.Add("@atendimentoId", triagem.AtendimentoId);
                parametros.Add("@sintomas", triagem.Sintomas);
                parametros.Add("@pressaoSistolica", triagem.PressaoSistolica);
                parametros.Add("@pressaoDiastolica", triagem.PressaoDiastolica);
                parametros.Add("@peso", triagem.Peso);
                parametros.Add("@altura", triagem.Altura);
                parametros.Add("@EspecialidadeId", triagem.EspecialidadeId);
                await connection.ExecuteAsync(sql_script, parametros);

                return triagem;
            }
        }

        public async Task<Triagem> Update(Triagem triagem)
        {
            const string sql_script = @"UPDATE Triagem set Sintomas, PressaoSistolica, PressaoDiastolica, Peso, Altura, EspecialidadeId WHERE TriagemId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", triagem.TriagemId);
				parametros.Add("@sintomas", triagem.Sintomas);
				parametros.Add("@pressaoSistolica", triagem.PressaoSistolica);
				parametros.Add("@pressaoDiastolica", triagem.PressaoDiastolica);
				parametros.Add("@peso", triagem.Peso);
				parametros.Add("@altura", triagem.Altura);
				parametros.Add("@EspecialidadeId", triagem.EspecialidadeId);
				await connection.ExecuteAsync(sql_script, parametros);

                return triagem;
            }
        }

        public async Task<IEnumerable<Triagem>> GetAll()
        {

            string sql_script = @"SELECT TriagemId, AtendimentoId, Sintomas, PressaoSistolica, PressaoDiastolica, Peso, Altura, EspecialidadeId FROM Triagem";

            using (IDbConnection connection = _connection.Invoke())
            {
                return await connection.QueryAsync<Triagem>(sql_script);
            }
        }

		public async Task<Triagem> GetById(Guid id)
		{
			const string sql_script = @"SELECT TriagemId, AtendimentoId, Sintomas, PressaoSistolica, PressaoDiastolica, Peso, Altura, EspecialidadeId FROM Triagem WHERE TriagemId = @id";

			using (IDbConnection connection = _connection.Invoke())
			{
				var parametros = new DynamicParameters();
				parametros.Add("@id", id);
				return await connection.QueryFirstOrDefaultAsync<Triagem>(sql_script, parametros);
			}
		}

        public async Task<string> Delete(Guid id)
        {
            const string sql_script = @"DELETE FROM Triagem WHERE TriagemId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                await connection.ExecuteAsync(sql_script, parametros);
                return "Item excluido com sucesso";
            }
        }
    }
}
