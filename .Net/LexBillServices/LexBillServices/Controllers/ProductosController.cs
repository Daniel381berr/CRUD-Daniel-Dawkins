using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ProductosController : Controller
{
    private readonly IProductoRepository _productoRepository;

    public ProductosController(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<IActionResult> Index()
    {
        var productos = await _productoRepository.ListarProductosAsync();
        return View(productos);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return PartialView("_Crear");
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            await _productoRepository.CrearProductoAsync(producto);
            return Json(new { success = true });
        }
        return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var productos = await _productoRepository.BuscarProductoAsync(null, null, null, null);
        var producto = productos.FirstOrDefault(p => p.ProductoId == id);
        if (producto == null)
        {
            return NotFound();
        }
        return PartialView("_Editar", producto);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Producto producto)
    {
        if (ModelState.IsValid)
        {
            await _productoRepository.ModificarProductoAsync(producto);
            return Json(new { success = true });
        }
        return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
    }

 
    [HttpGet]
    public async Task<IActionResult> Eliminar(int id)
    {

        var productos = await _productoRepository.BuscarProductoAsync(null, null, null, null);
        var producto = productos.FirstOrDefault(p => p.ProductoId == id);

        if (producto == null)
        {
            return NotFound();
        }
        return PartialView("_Eliminar", producto);
    }


    [HttpPost]
    public async Task<IActionResult> EliminarConfirmado([FromForm] int ProductoId)
    {
        await _productoRepository.EliminarProductoAsync(ProductoId);
        return Json(new { success = true });
    }
}