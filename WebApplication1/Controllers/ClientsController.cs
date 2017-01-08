using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApi170108.Controllers
{
    [RoutePrefix("clients")]
    public class ClientsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public ClientsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        [Route("")]
        [ResponseType(typeof(IQueryable<Client>))]
        public IHttpActionResult GetClient()
        {
            if (!Request.IsLocal())
            {
                return NotFound();
            }
            return Ok(db.Client);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [Route("{id:int}/orders")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetClientOrders(int id)
        {
            var orders = db.Order.Where(p => p.ClientId == id);

            return Ok(orders.ToList());
        }

        [Route("{id:int}/orders/{*date:datetime}")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetClientOrdersByDate(int id, DateTime date)
        {
            var date_begin = date.Date;
            var date_end = date.AddDays(1);

            var orders = db.Order.Where(p =>
            p.ClientId == id &&
            (
            p.OrderDate.Value > date_begin &&
            p.OrderDate.Value < date_end
            ));

            return Ok(orders.ToList());
        }

        [Route("{id:int}/orders/top10")]
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetClientOrdersByTop10(int id)
        {
            var orders = db.Order
            .Where(p => p.ClientId == id)
            .OrderByDescending(p => p.OrderDate)
            .Take(10);

            return Ok(orders.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}