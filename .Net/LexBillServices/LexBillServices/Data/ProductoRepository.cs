using Dapper;
using System.Data;

public class ProductoRepository : IProductoRepository
{
    private readonly IDbConnection _dbConnection;

    public ProductoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Producto>> ListarProductosAsync()
    {
        return await _dbConnection.QueryAsync<Producto>("PA_Productos_ListarProductos", commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Producto>> BuscarProductoAsync(string nombre, string codigoProducto, string codigoBarras, string sku)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nombre", nombre);
        parameters.Add("@CodigoProducto", codigoProducto);
        parameters.Add("@CodigoBarras", codigoBarras);
        parameters.Add("@SKU", sku);

        return await _dbConnection.QueryAsync<Producto>("PA_Productos_BuscarProducto", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CrearProductoAsync(Producto producto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Nombre", producto.Nombre);
        parameters.Add("@CodigoProducto", producto.CodigoProducto);
        parameters.Add("@CodigoBarras", producto.CodigoBarras);
        parameters.Add("@SKU", producto.SKU);
        parameters.Add("@Precio", producto.Precio);
        parameters.Add("@Stock", producto.Stock);

        return await _dbConnection.QuerySingleAsync<int>("PA_Productos_CrearProducto", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task ModificarProductoAsync(Producto producto)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ProductoId", producto.ProductoId);
        parameters.Add("@Nombre", producto.Nombre);
        parameters.Add("@CodigoProducto", producto.CodigoProducto);
        parameters.Add("@CodigoBarras", producto.CodigoBarras);
        parameters.Add("@SKU", producto.SKU);
        parameters.Add("@Precio", producto.Precio);
        parameters.Add("@Stock", producto.Stock);

        await _dbConnection.ExecuteAsync("PA_Productos_ModificarProducto", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task EliminarProductoAsync(int productoId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ProductoId", productoId);

        await _dbConnection.ExecuteAsync("PA_Productos_EliminarProducto", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task BuscarProductoPorIdAsync(int productoId) 
    {
                 using (var connection = _dbConnection)
{
     await connection.QueryFirstOrDefaultAsync<Producto>("PA_Productos_BuscarProductoPorId", new { ProductoId = productoId }, commandType: CommandType.StoredProcedure);
}
    }


}