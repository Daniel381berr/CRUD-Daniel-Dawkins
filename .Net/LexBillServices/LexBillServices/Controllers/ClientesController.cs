using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
public class ClientesController : Controller
{
    private readonly ClienteRepository _clienteRepository;

    public ClientesController(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

   
    public async Task<IActionResult> Index()
    {
        var clientes = await _clienteRepository.ListarClientesAsync();
        return View(clientes);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return PartialView("_Crear", new Cliente());
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            await _clienteRepository.CrearClienteAsync(cliente);
            //return RedirectToAction(nameof(Index));
            return Json(new { success = true });
            //return RedirectToAction("Index");
            //return View("Index");
        }
        //return View(cliente);
        return Json(new { success = false });
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var cliente = await _clienteRepository.BuscarClientePorIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return PartialView("_Editar", cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            await _clienteRepository.ModificarClienteAsync(cliente);
            return Json(new { success = true });
        }
        return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
    }

    [HttpGet]
    public async Task<IActionResult> Eliminar(int id)
    {
        var cliente = await _clienteRepository.BuscarClientePorIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return PartialView("_Eliminar", cliente);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> EliminarConfirmado(int id)
    {
        await _clienteRepository.EliminarClienteAsync(id);
        return Json(new { success = true });
    }
}
