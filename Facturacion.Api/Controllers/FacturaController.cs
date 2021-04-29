using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Facturacion.Api.Models;
using Facturacion.Core.DTOs;
using Facturacion.Core.DTOs.Commands;
using Facturacion.Core.Entities;
using Facturacion.Core.Entities.Commands;
using Facturacion.Core.Interfaces;
using Facturacion.Core.Interfaces.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaCommand _facturaCommand;
        private readonly IFacturaRepository _facturaRepository;
        private readonly IMapper _mapper;
        public FacturaController(IFacturaCommand facturaCommand, IFacturaRepository facturaRepository, IMapper mapper)
        {
            _facturaCommand = facturaCommand;
            _mapper = mapper;
            _facturaRepository = facturaRepository;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Response> Get()
        {
            var response = new Response();
            try
            {
                var facturas = _facturaCommand.Select();
                response.Data = facturas;
                response.Description = "Facturas obtenidas";
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
                var factura = _facturaCommand.SelectById(id);
                response.Data = factura;
                response.Description = "Factura obtenida";
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
        public ActionResult<Response> Post(FacturaCommandDto facturaCommandDto)
        {
            var response = new Response();
            try
            {
                var newFacturaCommand = _mapper.Map<FacturaCommand>(facturaCommandDto);
                _facturaCommand.Insert(newFacturaCommand);

                response.Data = null;
                response.Description = "Factura creada";
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
        public ActionResult<Response> Put(int idFactura, FacturaDto facturaDto)
        {
            var response = new Response();
            try
            {
                var facturaFind = _facturaRepository.SelectById(idFactura);
                if (facturaFind == null)
                    throw new Exception("Factura no encontrada.");

                facturaFind.IdCliente = facturaDto.IdCliente;
                _facturaRepository.Update(facturaFind);

                response.Data = facturaFind;
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
                _facturaCommand.Delete(id);
                response.Data = null;
                response.Description = "Factura eliminada";
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
