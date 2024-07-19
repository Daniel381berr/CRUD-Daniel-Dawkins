using System.Collections.Generic;
using System.Threading.Tasks;
using LexBillServices.Models;

public interface IPedidoRepository
    {
    Task<IEnumerable<Pedido>> ListarPedidosAsync();
    Task<Pedido> BuscarPedidoPorIdAsync(int pedidoId);
    Task<int> CrearPedidoAsync(Pedido pedido);
    Task ModificarPedidoAsync(Pedido pedido);
    Task EliminarPedidoAsync(int pedidoId);
}

