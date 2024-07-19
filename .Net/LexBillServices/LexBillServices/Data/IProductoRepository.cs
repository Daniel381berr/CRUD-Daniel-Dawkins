public interface IProductoRepository
{
    Task<IEnumerable<Producto>> ListarProductosAsync();
    Task<IEnumerable<Producto>> BuscarProductoAsync(string nombre, string codigoProducto, string codigoBarras, string sku);
    Task<int> CrearProductoAsync(Producto producto);
    Task ModificarProductoAsync(Producto producto);
    Task EliminarProductoAsync(int productoId);
    Task BuscarProductoPorIdAsync(int productoId);
}