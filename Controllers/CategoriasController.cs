using Microsoft.AspNetCore.Mvc;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Services;
using TuristeiRV.API.Repositories;

namespace TuristeiRV.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategorias()
    {
        var categorias = await _categoriaService.GetCategoriasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoriaById(string id)
    {
        var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        return Ok(categoria);
    }
}