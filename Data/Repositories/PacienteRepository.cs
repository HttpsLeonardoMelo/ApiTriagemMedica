using System.Data;
using Dapper;
using Data.Interfaces;
using Data.Models;


namespace Data.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly Func<IDbConnection> _connection;

        public PacienteRepository(Func<IDbConnection> connection)
        {
            _connection = connection;
        }

        public async Task<Paciente> Add(Paciente paciente)
        {
            paciente.PacienteId = Guid.NewGuid();

            const string sql_script = @"INSERT INTO Paciente (PacienteId, Nome, Email, Sexo) VALUES (@pacienteId, @nome, @email, @sexo)";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@pacienteId", paciente.PacienteId);
                parametros.Add("@nome", paciente.Nome);
                parametros.Add("@email", paciente.Email);
                parametros.Add("@sexo", paciente.Sexo);
                await connection.ExecuteAsync(sql_script, parametros);

                return paciente;
            }
        }

        public async Task<Paciente> Update(Paciente paciente)
        {
            const string sql_script = @"UPDATE Paciente set Nome = @nome, Email = @email, Sexo = @sexo WHERE PacienteId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", paciente.PacienteId);
                parametros.Add("@nome", paciente.Nome);
                parametros.Add("@sexo", paciente.Sexo);
                await connection.ExecuteAsync(sql_script, parametros);

                return paciente;
            }
        }

        public async Task<IEnumerable<Paciente>> GetAll()
        {

            string sql_script = @"SELECT PacienteId, Nome, Email, Sexo FROM Paciente";

            using (IDbConnection connection = _connection.Invoke())
            {
                return await connection.QueryAsync<Paciente>(sql_script);
            }
        }

		public async Task<Paciente> GetById(Guid id)
		{
			const string sql_script = @"SELECT PacienteId, Nome, Email, Sexo FROM Paciente WHERE PacienteId = @id";

			using (IDbConnection connection = _connection.Invoke())
			{
				var parametros = new DynamicParameters();
				parametros.Add("@id", id);
				return await connection.QueryFirstOrDefaultAsync<Paciente>(sql_script, parametros);
			}
		}

        public async Task<string> Delete(Guid id)
        {
            const string sql_script = @"DELETE FROM Paciente WHERE PacienteId = @id";

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
