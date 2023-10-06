
using System.Collections.Generic;
using AutoMapper;
using Domain.Interface;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;
using API.Dtos;
namespace API.Controllers;

public class MarcaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private Generador_Pdf _generador;
    public MarcaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _generador = new Generador_Pdf();
    }
    [HttpGet("Pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MarcaDto>>> Get([FromQuery] Params marcaParams ){
        var pag = await _unitOfWork.Marca.paginacion(marcaParams.PageIndex, marcaParams.PageSize, marcaParams.Search);
        var dto = _mapper.Map<List<MarcaDto>>(pag.registros);
        var ms = _generador.Generador<MarcaDto>(dto);
        return new FileStreamResult(ms, "application/pdf");

    }
}
