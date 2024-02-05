using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using TestNetProsegur.Api.Controllers;
using TestNetProsegur.Application.Dtos;
using TestNetProsegur.Application.Implements;
using TestNetProsegur.Application.Interfaces;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;
using TestNetProsegur.Infrastructure.DBContexts;
using TestNetProsegur.Infrastructure.Repositories;

namespace TestNetProsegur.Test
{
    public class ProductTest
    {
        private Mock<IProductService> MockProductService = new Mock<IProductService>();
        private Mock<IStockService> MockStockService = new Mock<IStockService>();
        private Mock<IRepository<Product>> MockProductRepository = new Mock<IRepository<Product>>();

        private const string AUTHAPP = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMkBleGFtcGxlLmNvbSIsImp0aSI6ImFiZDE4NzhhLTU1OWItNDZhNy1iYzUzLTE1MWE5ZjFjZTdhMSIsInJvbGUiOlsiQURNSU5JU1RSQVRPUiIsIlNVUEVSVklTT1IiXSwiZXhwIjoxNzA2OTM5NTExLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTI3LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMjcvIn0.zmu3VySC5sTl3CB8OWrLyQIhLrlhs70JOjXaigzPAiM";

        [Fact]
        public void GetAllControllerSuccessfull()
        {
            //RESULTADO = new IEnumerable<Product>();

            MockProductService.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new ServiceResponseDto<IEnumerable<Product>> { IsSuccess = true }));
            ProductController ProductController = new ProductController(MockProductService.Object, MockStockService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext { }
                }
            };

            ProductController.ControllerContext.HttpContext.Request.Headers["Authorization"] = AUTHAPP;
            IActionResult result = ProductController.GetAll().Result;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllControllerBarRequest()
        {
            MockProductService.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new ServiceResponseDto<IEnumerable<Product>>()));
            ProductController ProductController = new ProductController(MockProductService.Object, MockStockService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext { }
                }
            };

            ProductController.ControllerContext.HttpContext.Request.Headers["Authorization"] = AUTHAPP;
            IActionResult result = ProductController.GetAll().Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetAllServiceSuccessfull()
        {
            var dummyQuery = (new List<Product>() { new Product { Id = 1, Name = "Product1", Unit = "Unit1", Stock = 1 } }).AsQueryable(); ;
            MockProductRepository.Setup(x => x.GetAll()).Returns(dummyQuery);
            
            IProductService ProductService = new ProductService(MockProductRepository.Object);
            var result = ProductService.GetAll();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

    }

}