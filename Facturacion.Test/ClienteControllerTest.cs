using AutoMapper;
using Facturacion.Api.Controllers;
using Facturacion.Api.Models;
using Facturacion.Core.DTOs;
using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Facturacion.Test
{
    [TestClass]
    public class ClienteControllerTest
    {
        [TestMethod]
        public void Get()
        {
            var response = new Response();
            var cliente = new Cliente
            {
                IdCliente = 1,
                Nombre = "Juan",
                Apellidos = "Ruiz",
                Identificacion = "1234567890",
                FechaNacimiento = Convert.ToDateTime("1990-04-27")
            };

            response.Data = cliente;
            response.Description = "Cliente obtenido";
            response.Result = true;

            Mock<IClienteRepository> clienteRepo = new Mock<IClienteRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            clienteRepo.Setup(metodo => metodo.SelectById(1))
                .Returns(cliente);

            var clienteController = new ClienteController(clienteRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = clienteController.Get(1);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Put()
        {
            var response = new Response();
            var cliente = new Cliente
            {
                IdCliente = 1,
                Nombre = "Juan",
                Apellidos = "Ruiz",
                Identificacion = "1234567890",
                FechaNacimiento = Convert.ToDateTime("1990-04-27")
            };

            var clienteDto = new ClienteDto
            {
                Nombre = "Juan",
                Apellidos = "Ruiz",
                Identificacion = "1234567890",
                FechaNacimiento = Convert.ToDateTime("1990-04-27")
            };

            response.Data = cliente;
            response.Description = "Cliente actualizado";
            response.Result = true;

            Mock<IClienteRepository> clienteRepo = new Mock<IClienteRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            mapper.Setup(metodo => metodo.Map<Cliente>(clienteDto))
                .Returns(cliente);
            clienteRepo.Setup(metodo => metodo.Update(cliente));

            var clienteController = new ClienteController(clienteRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = clienteController.Put(1, clienteDto);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Post()
        {
            var response = new Response();
            var clienteDto = new ClienteDto
            {
                Nombre = "Juan",
                Apellidos = "Ruiz",
                Identificacion = "1234567890",
                FechaNacimiento = Convert.ToDateTime("1990-04-27")
            };

            var AddCliente = new Cliente
            {
                IdCliente = 4,
                Nombre = "Juan",
                Apellidos = "Ruiz",
                Identificacion = "1234567890",
                FechaNacimiento = Convert.ToDateTime("1990-04-27")
            };

            response.Data = AddCliente;
            response.Description = "Cliente agregado";
            response.Result = true;

            Mock<IClienteRepository> clienteRepo = new Mock<IClienteRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            mapper.Setup(metodo => metodo.Map<Cliente>(clienteDto))
                .Returns(AddCliente);
            clienteRepo.Setup(metodo => metodo.Insert(AddCliente));

            var clienteController = new ClienteController(clienteRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = clienteController.Post(clienteDto);

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
            response.Description = "Cliente eliminado";
            response.Result = true;

            Mock<IClienteRepository> clienteRepo = new Mock<IClienteRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            clienteRepo.Setup(metodo => metodo.Delete(id));

            var clienteController = new ClienteController(clienteRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = clienteController.Delete(id);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);

        }
    }
}
