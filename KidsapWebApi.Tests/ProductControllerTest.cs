using BusinessLogic.Interfaces;
using BusinessLogic.TestMock;
using DependencyResolver;
using KidsapWebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using ViewModel;

namespace KidsapWebApi.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public async Task ListAsync_ReturnListOfProduct()
        {
            ProductServiceManagerMock _service = new ProductServiceManagerMock();
            ProductController controller = new ProductController(_service) {
                Request = new HttpRequestMessage() { Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
            };
            // Act

            var response = await controller.ListAsync();
            var actualResult = response.Content.ReadAsAsync<IList<Product>>().Result;
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(4, actualResult.Count);
            Assert.IsInstanceOfType(actualResult, typeof(IList<Product>));
        }

        [TestMethod]
        public async Task FindByNameAsync_WithProductName_ShouldReturnNull()
        {
            ProductServiceManagerMock _service = new ProductServiceManagerMock();
            ProductController controller = new ProductController(_service)
            {
                Request = new HttpRequestMessage() { Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
            };

            var response = await controller.FindByNameAsync("Product5");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public async Task FindByNameAsync_WithProductName_ShouldReturnProduct()
        {
            ProductServiceManagerMock _service = new ProductServiceManagerMock();
            ProductController controller = new ProductController(_service)
            {
                Request = new HttpRequestMessage() { Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
            };

            var response = await controller.FindByNameAsync("Product1");
            var actualResult = response.Content.ReadAsAsync<Product>().Result;
            var expectResult = new Product() { Id = new Guid("1D9701B6-94D2-47B0-B09C-E3865383CB37"), Name = "Product1", ExpirayDate = new DateTime(2017, 7, 20), isDeleted = false };
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectResult.Id, actualResult.Id);
            Assert.IsInstanceOfType(actualResult, typeof(Product));
        }

        [TestMethod]
        public async Task DeleteAsync_WithId_ShouldDeleteProduct()
        {
            ProductServiceManagerMock _service = new ProductServiceManagerMock();
            ProductController controller = new ProductController(_service)
            {
                Request = new HttpRequestMessage() { Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
            };

            await controller.DeleteAsync("1D9701B6-94D2-47B0-B09C-E3865383CB37");
            var response = await controller.FindByNameAsync("Product1");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsNull(response.Content);
        }      
    }
}
