using System;
using System.Net;
using AutoMapper;
using Facturacion.Api.Models;
using Facturacion.Core.DTOs;
using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        public ProductoController(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Response> Get()
        {
            var response = new Response();
            try
            {
                var productos = _productoRepository.Select();
                response.Data = productos;
                response.Description = "Productos obtenidos";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Description = ex.Message;
                return StatusCode(500, response);
            }

            return response;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Response> Get(int id)
        {
            var response = new Response();
            try
            {
                var producto = _productoRepository.SelectById(id);
                response.Data = producto;
                response.Description = "Producto obtenido";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Description = ex.Message;
                return StatusCode(500, response);
            }

            return response;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Response> Post(ProductoDto productoDto)
        {
            var response = new Response();
            try
            {
                var AddProducto = _mapper.Map<Producto>(productoDto);
                _productoRepository.Insert(AddProducto);

                response.Data = AddProducto;
                response.Description = "Producto agregado";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Description = ex.Message;
                return StatusCode(500, response);
            }
            return response;
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Response> Put(int id, ProductoDto productoDto)
        {
            var response = new Response();
            try
            {
                var updateProducto = _mapper.Map<Producto>(productoDto);
                updateProducto.IdProducto = id;

                _productoRepository.Update(updateProducto);
                response.Data = updateProducto;
                response.Description = "Producto actualizado";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Description = ex.Message;
                return StatusCode(500, response);
            }

            return response;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Response> Delete(int id)
        {
            var response = new Response();
            try
            {
                _productoRepository.Delete(id);
                response.Data = null;
                response.Description = "Producto eliminado";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Description = ex.Message;
                return StatusCode(500, response);
            }
            return response;
        }
    }
}
