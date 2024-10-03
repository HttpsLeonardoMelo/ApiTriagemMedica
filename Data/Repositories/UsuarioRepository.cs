using System.Data;
using Dapper;
using Data.Interfaces;
using Data.Interfaces.Util;
using Data.Models;
using Data.Models.Enums;
using Data.Models.Filtros;
using Data.Models.Listas;


namespace Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Func<IDbConnection> _connection;
        private readonly IHashSenha _hashSenha;

        public UsuarioRepository(Func<IDbConnection> connection, IHashSenha hashSenha)
        {
            _connection = connection;
            _hashSenha = hashSenha;
        }

        public async Task<Usuario> Add(Usuario usuario)
        {

            usuario.UsuarioId = Guid.NewGuid();

            const string sql_script = @"INSERT INTO Usuario (UsuarioId, Nome, Email, Senha, DataCriacao, Ativo) VALUES (@usuarioId, @nome, @email, @senha, @dataCriacao, @ativo)";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@usuarioId", usuario.UsuarioId);
                parametros.Add("@nome", usuario.Nome);
                parametros.Add("@email", usuario.Email);
                parametros.Add("@senha", _hashSenha.Hash(usuario.Senha));
                parametros.Add("@dataCriacao", DateTime.Now);
                parametros.Add("@ativo", usuario.Ativo);
                await connection.ExecuteAsync(sql_script, parametros);

                return usuario;
            }
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            const string sql_script = @"UPDATE Usuario set Nome = @nome, Email = @email, Ativo = @ativo, DataUltimoLogin = @dataUltimoLogin WHERE UsuarioId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", usuario.UsuarioId);
                parametros.Add("@nome", usuario.Nome);
                parametros.Add("@ativo", usuario.Ativo);
                parametros.Add("@dataUltimoLogin", usuario.DataUltimoLogin);
                await connection.ExecuteAsync(sql_script, parametros);

                return usuario;
            }
        }

        public async Task<Usuario> UpdateSenha(Usuario usuario)
        {
            const string sql_script = @"UPDATE Usuario set Senha = @senha WHERE UsuarioId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", usuario.UsuarioId);
                parametros.Add("@senha", _hashSenha.Hash(usuario.Senha));

                await connection.ExecuteAsync(sql_script, parametros);

                return usuario;
            }
        }

        public async Task<IEnumerable<Usuario>> GetAll(bool listaCompleta = false)
        {

            string sql_script = @"SELECT UsuarioId, Nome, Email, Senha, DataCriacao, DataUltimoLogin, Ativo, DataUltimoLogin FROM Usuario";

            if (listaCompleta == false)
            {
                sql_script += " WHERE Ativo = 1";
            }

            using (IDbConnection connection = _connection.Invoke())
            {
                return await connection.QueryAsync<Usuario>(sql_script);
            }
        }

        public async Task<Usuario> GetById(Guid id)
        {
            const string sql_script = @"SELECT UsuarioId, Nome, Email, Foto, Senha, DataCriacao, DataUltimoLogin, Ativo, DataUltimoLogin FROM Usuario WHERE UsuarioId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                return await connection.QueryFirstOrDefaultAsync<Usuario>(sql_script, parametros);
            }
        }

        public async Task<Usuario> GetByEmail(string email, bool somenteAtivos = false)
        {
            string sql_script = @"SELECT UsuarioId, Nome, Email, Senha, DataCriacao, DataUltimoLogin, Ativo, DataUltimoLogin FROM Usuario WHERE Email = @email";
            if (somenteAtivos == true)
            {
                sql_script += " and Ativo = 1";
            }
            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@email", email);
                return await connection.QueryFirstOrDefaultAsync<Usuario>(sql_script, parametros);
            }
        }

        public async Task<IEnumerable<Usuario>> GetByNome(string nome, bool somenteAtivos = false)
        {
            string sql_script = @"SELECT UsuarioId, Nome, Email, Senha, DataCriacao, DataUltimoLogin, Ativo, DataUltimoLogin FROM Usuario WHERE Nome LIKE '%' + @nome + '%'  AND Ativo = 1";
            if (somenteAtivos == true)
            {
                sql_script += " and Ativo = 1";
            }
            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@nome", nome);
                return await connection.QueryAsync<Usuario>(sql_script, parametros);
            }
        }
        public async Task<ListaPaginada<ListaUsuario>> ListarUsuariosAsync(FiltroUsuario filtro, bool paginarRegistros = true)
        {
            var query = @"SELECT * FROM (
                            SELECT U.UsuarioId, U.Nome, U.Email, U.Ativo, U.DataUltimoLogin, u.DataCriacao FROM Usuario AS U
                          ) AS Resultado";

            var parametros = new DynamicParameters();

            var where = " WHERE ";
            var and = " AND ";
            var whereInsert = false;

            if (!string.IsNullOrEmpty(filtro.Busca))
            {
                if (whereInsert == false) { query += where; whereInsert = true; }
                else query += and;
                query += @" (
                                Nome LIKE '%' + @busca + '%' OR 
                                Email LIKE '%' + @busca + '%'
                            )";
                
                parametros.Add("@busca", filtro.Busca.Trim().ToUpper());
            }

            var queryCount = query;

            query += @" ORDER BY";

            switch (filtro.OrdenarPor)
            {
                case UsuarioEnum.Nome:
                    query += @" Nome ";
                    break;
                case UsuarioEnum.Email:
                    query += @" Email ";
                    break;
                case UsuarioEnum.Ativo:
                    query += @" Ativo ";
                    break;
                default:
                    query += @" Nome, Departamento";
                    break;
            }

            query += filtro.Ordem == AscDescEnum.Asc ? @" ASC " : @" DESC ";

            if (paginarRegistros)
            {
                query += @"OFFSET (@pagina - 1) * @itensPagina ROWS 
                        FETCH NEXT @itensPagina ROWS ONLY";

                parametros.Add("@pagina", filtro.Pagina);
                parametros.Add("@itensPagina", filtro.ItensPagina);
            }

            using (IDbConnection connection = _connection.Invoke())
            {
                var result = await connection.QueryAsync<ListaUsuario>(query, parametros);

                queryCount = queryCount.Replace("SELECT *", "SELECT COUNT(*)");
                var resultCount = await connection.QueryFirstOrDefaultAsync<int>(queryCount, parametros);

                var paginas = resultCount % filtro.ItensPagina > 0 ? (resultCount / filtro.ItensPagina) + 1 : resultCount / filtro.ItensPagina;
                if (paginas == 0)
                    paginas = 1;

                var response = new ListaPaginada<ListaUsuario>()
                {
                    Lista = result.ToList(),
                    Paginas = paginas,
                    TotalItens = resultCount
                };

                return response;
            }
        }

        public async Task<string> Delete(Guid id)
        {
            const string sql_script = @"DELETE FROM Usuario WHERE UsuarioId = @id";

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", id);
                await connection.ExecuteAsync(sql_script, parametros);
                return "Item excluido com sucesso";
            }
        }

        public async Task<bool> EmailExiste(Guid? id, string email)
        {
            string sql_script = @"SELECT Email FROM Usuario WHERE Email LIKE @email";

            if (id != null)
            {
                sql_script += " and UsuarioId <> @id";
            }

            using (IDbConnection connection = _connection.Invoke())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@email", email);
                parametros.Add("@id", id);
                return (await connection.QueryAsync<Usuario>(sql_script, parametros)).Any();
            }
        }
    }
}
