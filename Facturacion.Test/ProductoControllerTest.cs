using AutoMapper;
using Facturacion.Api.Controllers;
using Facturacion.Api.Models;
using Facturacion.Core.DTOs;
using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Facturacion.Test
{
    [TestClass]
    public class ProductoControllerTest
    {
        [TestMethod]
        public void Get()
        {
            var response = new Response();
            var producto = new Producto
            {
                IdProducto = 1,
                Nombre = "Producto 1",
                Precio = 5500,
                Existencia = 85
            };

            response.Data = producto;
            response.Description = "Producto obtenido";
            response.Result = true;

            Mock<IProductoRepository> productoRepo = new Mock<IProductoRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            productoRepo.Setup(metodo => metodo.SelectById(1))
                .Returns(producto);

            ProductoController productoController = new ProductoController(productoRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = productoController.Get(1);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Put()
        {
            var response = new Response();
            var producto = new Producto
            {
                IdProducto = 1,
                Nombre = "Producto 100",
                Precio = 500,
                Existencia = 15
            };

            var productoDto = new ProductoDto
            {
                Nombre = "Producto 100",
                Precio = 500,
                Existencia = 15
            };

            response.Data = producto;
            response.Description = "Producto actualizado";
            response.Result = true;

            Mock<IProductoRepository> productoRepo = new Mock<IProductoRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            mapper.Setup(metodo => metodo.Map<Producto>(productoDto))
                .Returns(producto);
            productoRepo.Setup(metodo => metodo.Update(producto));

            ProductoController productoController = new ProductoController(productoRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = productoController.Put(1, productoDto);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Post()
        {
            var response = new Response();
            var productoDto = new ProductoDto
            {
                Nombre = "Producto 100",
                Precio = 500,
                Existencia = 15
            };

            var AddProducto = new Producto
            {
                IdProducto = 25,
                Nombre = "Producto 100",
                Precio = 500,
                Existencia = 15
            };

            response.Data = AddProducto;
            response.Description = "Producto agregado";
            response.Result = true;

            Mock<IProductoRepository> productoRepo = new Mock<IProductoRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            mapper.Setup(metodo => metodo.Map<Producto>(productoDto))
                .Returns(AddProducto);
            productoRepo.Setup(metodo => metodo.Insert(AddProducto));

            ProductoController productoController = new ProductoController(productoRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = productoController.Post(productoDto);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);
        }

        [TestMethod]
        public void Delete()
        {
            int id = 24;
            var response = new Response();
            response.Data = null;
            response.Description = "Producto eliminado";
            response.Result = true;

            Mock<IProductoRepository> productoRepo = new Mock<IProductoRepository>();
            Mock<IMapper> mapper = new Mock<IMapper>();

            productoRepo.Setup(metodo => metodo.Delete(id));

            ProductoController productoController = new ProductoController(productoRepo.Object, mapper.Object);

            ActionResult<Response> actionResult = productoController.Delete(id);

            Assert.AreEqual(response.Description, actionResult.Value.Description);
            Assert.AreEqual(response.Result, actionResult.Value.Result);
            Assert.AreEqual(response.Data, actionResult.Value.Data);

        }
    }
}
