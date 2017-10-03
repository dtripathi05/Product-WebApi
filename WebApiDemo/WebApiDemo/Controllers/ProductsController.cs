using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsData;
namespace WebApiDemo.Controllers
{
    public class ProductsController : ApiController
    {
        ProductDBEntities entity;
        public ProductsController()
        {
            entity = new ProductDBEntities();
        }
        public IEnumerable<ProductsTable> Get()
        {
            //using (ProductDBEntities entities = new ProductDBEntities())
            //{
            //    return entities.ProductsTables.ToList();
            //}
            return entity.ProductsTables.ToList();
        }
        public HttpResponseMessage Get(int id)
        {
            var en = entity.ProductsTables.FirstOrDefault(p => p.ID == id);
            if (entity == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with the Id " + id.ToString() + "not found in the database");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        public void Post([FromBody]ProductsTable product)
        {
            entity.ProductsTables.Add(product);
            entity.SaveChanges();
        }

        public void Delete(int id)
        {
           var en = entity.ProductsTables.FirstOrDefault(p => p.ID == id);
            entity.ProductsTables.Remove(en);
            entity.SaveChanges();
        }
       public void Put(int id,[FromBody]ProductsTable product)
        {
            var en = entity.ProductsTables.FirstOrDefault(p => p.ID == id);
            en.Name = product.Name;
            en.Price = product.Price;
            en.Quantity = product.Quantity;
            entity.SaveChanges();
        }
    }
}
