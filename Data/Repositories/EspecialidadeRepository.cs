using System.Data;
using Dapper;
using Data.Interfaces;
using Data.Models;


namespace Data.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        private readonly Func<IDbConnection> _connection;

        public EspecialidadeRepository(Func<IDbConnection> connection)
        {
            _connection = connection;
        }

        public async Task<Especialidade> Add(Especialidade especialidade)
        {

            especialidade.EspecialidadeId = Guid.NewGuid();

            const string sql_script = @"INSERT INTO Especialidade (EspecialidadeId, Nome, Descricao, Ativo) VALUES (@especialidadeId, @nome, @descricao, @ativo)";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@especialidadeId", especialidade.EspecialidadeId);
                parametros.Add("@nome", especialidade.Nome);
                parametros.Add("@descricao", especialidade.Descricao);
                parametros.Add("@ativo", especialidade.Ativo);
                await connection.ExecuteAsync(sql_script, parametros);

                return especialidade;
            }
        }

        public async Task<Especialidade> Update(Especialidade especialidade)
        {
            const string sql_script = @"UPDATE Especialidade set Nome = @nome, Descricao = @descricao, Ativo = @ativo WHERE EspecialidadeId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", especialidade.EspecialidadeId);
                parametros.Add("@nome", especialidade.Nome);
                parametros.Add("@ativo", especialidade.Ativo);
                await connection.ExecuteAsync(sql_script, parametros);

                return especialidade;
            }
        }

        public async Task<IEnumerable<Especialidade>> GetAll()
        {

            string sql_script = @"SELECT EspecialidadeId, Nome, Descricao, Ativo FROM Especialidade";

            using (IDbConnection connection = _connection.Invoke())
            {
                return await connection.QueryAsync<Especialidade>(sql_script);
            }
        }

		public async Task<Especialidade> GetById(Guid id)
		{
			const string sql_script = @"SELECT EspecialidadeId, Nome, Descricao, Ativo FROM Especialidade WHERE EspecialidadeId = @id";

			using (IDbConnection connection = _connection.Invoke())
			{
				var parametros = new DynamicParameters();
				parametros.Add("@id", id);
				return await connection.QueryFirstOrDefaultAsync<Especialidade>(sql_script, parametros);
			}
		}

        public async Task<string> Delete(Guid id)
        {
            const string sql_script = @"DELETE FROM Especialidade WHERE EspecialidadeId = @id";

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
