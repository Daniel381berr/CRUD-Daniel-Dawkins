using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class ClienteRepository
{
    private readonly Database _database;

    public ClienteRepository(Database database)
    {
        _database = database;
    }

    public async Task<IEnumerable<Cliente>> ListarClientesAsync()
    {
        using (var connection = _database.GetConnection())
        {
            return await connection.QueryAsync<Cliente>("PA_Clientes_ListarClientes", commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<Cliente> BuscarClientePorIdAsync(int clienteId)
    {
        using (var connection = _database.GetConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Cliente>("PA_Clientes_BuscarClientePorId", new { ClienteId = clienteId }, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<int> CrearClienteAsync(Cliente cliente)
    {
        using (var connection = _database.GetConnection())
        {
            var parameters = new DynamicParameters();
            parameters.Add("Nombre", cliente.Nombre);
            parameters.Add("Apellido", cliente.Apellido);
            parameters.Add("Email", cliente.Email);
            parameters.Add("Telefono", cliente.Telefono);
            parameters.Add("Direccion", cliente.Direccion);

            return await connection.ExecuteScalarAsync<int>("PA_Clientes_CrearCliente", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task ModificarClienteAsync(Cliente cliente)
    {
        using (var connection = _database.GetConnection())
        {
            var parameters = new DynamicParameters();
            parameters.Add("ClienteId", cliente.ClienteId);
            parameters.Add("Nombre", cliente.Nombre);
            parameters.Add("Apellido", cliente.Apellido);
            parameters.Add("Email", cliente.Email);
            parameters.Add("Telefono", cliente.Telefono);
            parameters.Add("Direccion", cliente.Direccion);

            await connection.ExecuteAsync("PA_Clientes_ModificarCliente", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task EliminarClienteAsync(int clienteId)
    {
        using (var connection = _database.GetConnection())
        {
            await connection.ExecuteAsync("PA_Clientes_EliminarCliente", new { ClienteId = clienteId }, commandType: CommandType.StoredProcedure);
        }
    }
}
