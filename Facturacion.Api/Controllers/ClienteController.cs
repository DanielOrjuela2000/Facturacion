using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Facturacion.Api.Models;
using Facturacion.Core.DTOs;
using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
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
                var productos = _clienteRepository.Select();
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
                var cliente = _clienteRepository.SelectById(id);
                response.Data = cliente;
                response.Description = "Cliente obtenido";
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
        public ActionResult<Response> Post(ClienteDto clienteDto)
        {
            var response = new Response();
            try
            {
                var AddCliente = _mapper.Map<Cliente>(clienteDto);
                _clienteRepository.Insert(AddCliente);

                response.Data = AddCliente;
                response.Description = "Cliente agregado";
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
        public ActionResult<Response> Put(int id, ClienteDto productoDto)
        {
            var response = new Response();
            try
            {
                var updateCliente = _mapper.Map<Cliente>(productoDto);
                updateCliente.IdCliente = id;

                _clienteRepository.Update(updateCliente);
                response.Data = updateCliente;
                response.Description = "Cliente actualizado";
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
                _clienteRepository.Delete(id);
                response.Data = null;
                response.Description = "Cliente eliminado";
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
