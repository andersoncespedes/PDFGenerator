
using Domain.Interface;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using AutoMapper;
namespace API.Controllers;

public class ProductoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public ProductoController(IUnitOfWork unitOfWork){
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<Producto>>> Get11([FromQuery] Params productParams)
    {
        var products = await _unitOfWork.Productos.paginacion(productParams.PageIndex,productParams.PageSize,productParams.Search);
        var lstProductDto = _mapper.Map<List<Producto>>(products.registros);
        return new Pager<Producto>(lstProductDto,products.totalRegistros,productParams.PageIndex,productParams.PageSize,productParams.Search);
    }
}
