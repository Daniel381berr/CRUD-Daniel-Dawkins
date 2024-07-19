using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LexBillServices.Models;
public class PedidoRepository : IPedidoRepository
{
    private readonly string _connectionString;

    public PedidoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("CadenaSql");
    }

    public async Task<IEnumerable<Pedido>> ListarPedidosAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.QueryAsync<Pedido>("PA_Pedidos_ListarPedidos", commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<Pedido> BuscarPedidoPorIdAsync(int pedidoId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PedidoId", pedidoId);

            return await connection.QueryFirstOrDefaultAsync<Pedido>("PA_Pedidos_BuscarPedidoPorId", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<int> CrearPedidoAsync(Pedido pedido)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ClienteId", pedido.ClienteId);
            parameters.Add("@Fecha", pedido.Fecha);
            parameters.Add("@ITBMS", pedido.ITBMS);
            parameters.Add("@Total", pedido.Total);

            return await connection.ExecuteScalarAsync<int>("PA_Pedidos_CrearPedido", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task ModificarPedidoAsync(Pedido pedido)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PedidoId", pedido.PedidoId);
            parameters.Add("@ClienteId", pedido.ClienteId);
            parameters.Add("@Fecha", pedido.Fecha);
            parameters.Add("@ITBMS", pedido.ITBMS);
            parameters.Add("@Total", pedido.Total);

            await connection.ExecuteAsync("PA_Pedidos_ModificarPedido", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task EliminarPedidoAsync(int pedidoId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PedidoId", pedidoId);

            await connection.ExecuteAsync("PA_Pedidos_EliminarPedido", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}


