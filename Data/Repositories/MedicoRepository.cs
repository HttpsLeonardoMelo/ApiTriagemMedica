using System.Data;
using Dapper;
using Data.Interfaces;
using Data.Models;


namespace Data.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly Func<IDbConnection> _connection;

        public MedicoRepository(Func<IDbConnection> connection)
        {
            _connection = connection;
        }

        public async Task<Medico> Add(Medico medico)
        {

            medico.MedicoId = Guid.NewGuid();

            const string sql_script = @"INSERT INTO Medico (MedicoId, EspecialidadeId, Nome, Disponivel, Ativo, CrmUf) VALUES (@medicoId, @especialidadeId, @nome, @disponivel, @ativo, @crmUf)";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@medicoId", medico.MedicoId);
                parametros.Add("@especialidadeId", medico.EspecialidadeId);
                parametros.Add("@nome", medico.Nome);
                parametros.Add("@disponivel", medico.Disponivel);
                parametros.Add("@ativo", medico.Ativo);
                parametros.Add("@crmUf", medico.CrmUf);
                await connection.ExecuteAsync(sql_script, parametros);

                return medico;
            }
        }

        public async Task<Medico> Update(Medico medico)
        {
            const string sql_script = @"UPDATE Medico set EspecialidadeId = @especialidadeId, Nome = @nome, Disponivel = @disponivel, Ativo = @ativo, CrmUf = @crmUf WHERE MedicoId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", medico.MedicoId);
				parametros.Add("@especialidadeId", medico.EspecialidadeId);
				parametros.Add("@nome", medico.Nome);
				parametros.Add("@disponivel", medico.Disponivel);
				parametros.Add("@ativo", medico.Ativo);
				parametros.Add("@crmUf", medico.CrmUf);
				await connection.ExecuteAsync(sql_script, parametros);

                return medico;
            }
        }

        public async Task<IEnumerable<Medico>> GetAll()
        {

            string sql_script = @"SELECT MedicoId, EspecialidadeId, Nome, Disponivel, Ativo, CrmUf FROM Medico";

            using (IDbConnection connection = _connection.Invoke())
            {
                return await connection.QueryAsync<Medico>(sql_script);
            }
        }

		public async Task<Medico> GetById(Guid id)
		{
			const string sql_script = @"SELECT MedicoId, EspecialidadeId, Nome, Disponivel, Ativo, CrmUf FROM Medico WHERE MedicoId = @id";

			using (IDbConnection connection = _connection.Invoke())
			{
				var parametros = new DynamicParameters();
				parametros.Add("@id", id);
				return await connection.QueryFirstOrDefaultAsync<Medico>(sql_script, parametros);
			}
		}

        public async Task<string> Delete(Guid id)
        {
            const string sql_script = @"DELETE FROM Medico WHERE MedicoId = @id";

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
