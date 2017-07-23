using BusinessLogic.Interfaces;
using KidsapWebApi.Filters;
using SettingConstant;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ViewModel;

namespace KidsapWebApi.Controllers
{
    [PrivilegeFilter]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductServiceManager _serviceManager;

        public ProductController(IProductServiceManager _serviceManager)
        {
            this._serviceManager = _serviceManager;
        }

        [Route("list")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of products" ,Type = typeof(IList<Product>))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Access Denied")]
        public async Task<HttpResponseMessage> ListAsync()
        {
            try
            {
                var products = await _serviceManager.FindAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("name/{name}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Return Product Object" ,Type = typeof(Product))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Access Denied")]
        public async Task<HttpResponseMessage> FindByNameAsync(string name)
        {
            try
            {                
                var product = await _serviceManager.FindByNameAsync(name);
                if (product == null) return Request.CreateResponse(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Access Denied")]
        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            try
            {
                await _serviceManager.DeleteAsync(new Guid(id));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}