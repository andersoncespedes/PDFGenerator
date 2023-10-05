using System.Collections.Generic;
using Domain.Interface;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using API.Services;
namespace API.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Generador_Pdf _generador;
        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _generador = new Generador_Pdf();
        }

        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProductoDto>>> Get11([FromQuery] Params productParams)
        {

            var products = await _unitOfWork.Productos.paginacion(productParams.PageIndex, productParams.PageSize, productParams.Search);
            var lstProductDto = _mapper.Map<List<ProductoDto>>(products.registros);
            var ms = _generador.Generador<ProductoDto>(lstProductDto);
            return new FileStreamResult(ms, "application/pdf");
        }
    }
}
