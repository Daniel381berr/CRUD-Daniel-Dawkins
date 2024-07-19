using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using LexBillServices.Models;

public class PedidosController : Controller
    {

    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProductoRepository _productoRepository;

    public PedidosController(IPedidoRepository pedidoRepository, IProductoRepository productoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _productoRepository = productoRepository;
    }

    public async Task<IActionResult> Index()
    {
        var pedidos = await _pedidoRepository.ListarPedidosAsync();
        return View(pedidos);
    }

    [HttpGet]
    public async Task<IActionResult> Crear()
    {
        var productos = await _productoRepository.ListarProductosAsync();
        ViewBag.Productos = productos;
        return PartialView("_Crear");
    }

    //[HttpGet]
    //public async Task<IActionResult> Crear()
    //{
    //    var viewModel = new CrearPedidoViewModel
    //    {
    //        Pedido = new Pedido(),
    //        Productos = await _productoRepository.ListarProductosAsync()
    //    };
    //    return PartialView("_Crear", viewModel);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Crear(Pedido pedido)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var pedidoId = await _pedidoRepository.CrearPedidoAsync(pedido);
    //        return Json(new { success = true, pedidoId = pedidoId });
    //    }
    //    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
    //}

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var pedido = await _pedidoRepository.BuscarPedidoPorIdAsync(id);
        if (pedido == null)
        {
            return NotFound();
        }
        var productos = await _productoRepository.ListarProductosAsync();
        ViewBag.Productos = productos;
        return PartialView("_Editar", pedido);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Pedido pedido)
    {
        if (ModelState.IsValid)
        {
            await _pedidoRepository.ModificarPedidoAsync(pedido);
            return Json(new { success = true });
        }
        return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
    }

    [HttpGet]
    public async Task<IActionResult> Eliminar(int id)
    {
        var pedido = await _pedidoRepository.BuscarPedidoPorIdAsync(id);
        if (pedido == null)
        {
            return NotFound();
        }
        return PartialView("_Eliminar", pedido);
    }

    [HttpPost]
    public async Task<IActionResult> EliminarConfirmado(int id)
    {
        await _pedidoRepository.EliminarPedidoAsync(id);
        return Json(new { success = true });
    }
}
