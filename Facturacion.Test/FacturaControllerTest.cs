using AutoMapper;
using Facturacion.Api.Controllers;
using Facturacion.Api.Models;
using Facturacion.Core.DTOs;
using Facturacion.Core.DTOs.Commands;
using Facturacion.Core.Entities;
using Facturacion.Core.Entities.Commands;
using Facturacion.Core.Interfaces;
using Facturacion.Core.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Test
{
    [TestClass]
    public class FacturaControllerTest
    {
        [TestMethod]
        public void Get()
        {
            var response = new Response();
            var facturasSimulation = new FacturaCommand
            {
                Factura = new Core.Entities.Factura
                {
                    IdFactura = 1,
                    IdCliente = 1,
                    Fecha = Convert.ToDateTime("2021-10-11 00:00:00.000"),
                    Total = null
                },
                FacturaDetalles = new List<Core.Entities.FacturaDetalle>
                {
                    new Core.Entities.FacturaDetalle
                    {
                        IdFactura = 1,
                        IdProducto = 1,
                        IdFacturaDetalle = 1,
                        Cantidad = 5000,
                        Precio = 100
                    },
                    new Core.Entities.FacturaDetalle
                    {
                        IdFactura = 1,
                        IdProducto = 1,
                        IdFacturaDetalle = 2,
                        Cantidad = 5000,
                        Precio = 100
                    },
                }
            };

            response.Data = facturasSimulation;
            response.Description = "Factura obtenida";
            response.Result = true;

            Mock<IFacturaCommand> facturaCommand = new Mock<IFacturaCommand>();
            Mock<IFacturaRepository> facturaRepository = new Mock<IFacturaRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            facturaCommand.Setup(metodo => metodo.SelectById(1))
                .Returns(facturasSimulation);

            var facturaController = new FacturaController(facturaCommand.Object,
                facturaRepository.Object, mapper.Object);

            ActionResult<Response> actionResult = facturaController.Get(1);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Put()
        {
            var response = new Response();

            var factura = new Factura
            {
                IdFactura = 1,
                IdCliente = 2,
                Fecha = Convert.ToDateTime("2021-10-11 00:00:00.000"),
                Total = null
            };

            var facturaFind = new Factura
            {
                IdFactura = 1,
                IdCliente = 1,
                Fecha = Convert.ToDateTime("2021-10-11 00:00:00.000"),
                Total = null
            };

            var facturaDto = new FacturaDto
            {
                IdCliente = 2
            };

            response.Data = factura;
            response.Description = "Producto actualizado";
            response.Result = true;

            Mock<IFacturaCommand> facturaCommand = new Mock<IFacturaCommand>();
            Mock<IFacturaRepository> facturaRepository = new Mock<IFacturaRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            facturaRepository.Setup(metodo => metodo.SelectById(1))
                .Returns(facturaFind);

            facturaFind.IdCliente = facturaDto.IdCliente;
            facturaRepository.Setup(metodo => metodo.Update(facturaFind));

            var facturaController = new FacturaController(facturaCommand.Object,
                facturaRepository.Object, mapper.Object);

            ActionResult<Response> actionResult = facturaController.Put(1, facturaDto);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            //Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Post()
        {
            var response = new Response();

            var facturaCommand = new FacturaCommand
            {
                Factura = new Factura
                {
                    IdFactura = 3,
                    IdCliente = 1,
                    Fecha = DateTime.Now,
                    Total = 500
                },
                FacturaDetalles = new List<FacturaDetalle>
                {
                    new FacturaDetalle
                    {
                        IdFactura = 3,
                        IdProducto = 1,
                        IdFacturaDetalle = 1,
                        Precio = 20,
                        Cantidad = 20
                    },
                    new FacturaDetalle
                    {
                        IdFactura = 3,
                        IdProducto = 2,
                        IdFacturaDetalle = 2,
                        Precio = 10,
                        Cantidad = 10
                    }
                }
            };

            var facturaCommandDto = new FacturaCommandDto
            {
                Factura = new FacturaDto
                {
                    IdCliente = 1
                },
                FacturaDetalles = new List<FacturaDetalleDto>
                {
                    new FacturaDetalleDto
                    {
                        IdProducto = 1,
                        Precio = 20,
                        Cantidad = 20
                    },
                    new FacturaDetalleDto
                    {
                        IdProducto = 2,
                        Precio = 10,
                        Cantidad = 10
                    }
                }
            };

            response.Data = null;
            response.Description = "Factura creada";
            response.Result = true;

            Mock<IFacturaCommand> _facturaCommand = new Mock<IFacturaCommand>();
            Mock<IFacturaRepository> facturaRepository = new Mock<IFacturaRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            mapper.Setup(metodo => metodo.Map<FacturaCommand>(facturaCommandDto))
                .Returns(facturaCommand);
            
            _facturaCommand.Setup(metodo => metodo.Insert(facturaCommand));

            var facturaController = new FacturaController(_facturaCommand.Object,
                facturaRepository.Object, mapper.Object);

            ActionResult<Response> actionResult = facturaController.Post(facturaCommandDto);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Delete()
        {
            int id = 1;
            var response = new Response();
            response.Data = null;
            response.Description = "Factura eliminada";
            response.Result = true;

            Mock<IFacturaCommand> facturaCommand = new Mock<IFacturaCommand>();
            Mock<IFacturaRepository> facturaRepository = new Mock<IFacturaRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            facturaCommand.Setup(metodo => metodo.Delete(id));

            var facturaController = new FacturaController(facturaCommand.Object,
                facturaRepository.Object, mapper.Object);

            ActionResult<Response> actionResult = facturaController.Delete(id);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }
    }
}
