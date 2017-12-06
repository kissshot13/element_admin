using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using element_backstage.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MySql.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace element_backstage.Controllers
{
    public class CustomersController : ApiController
    {
        private elementDB db = new elementDB();

        //GET： api/Customers
        public IQueryable<Customers> GetCustomers()
        {
            return db.Customers;
        }

        //GET:  api/Customers/5
        [ResponseType(typeof(Customers))]
        public async Task<IHttpActionResult> GetCustomers(Guid id)
        {
            Customers custoemrs = await db.Customers.FindAsync(id);
            if (custoemrs == null)
            {
                return NotFound();
            }
            return Ok(custoemrs);
        }

        //put: api/Customers/5
        [ResponseType(typeof(Customers))]
        public async Task<IHttpActionResult> PutCustomers(Guid id, Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customers.ID)
            {
                return BadRequest();
            }
            db.Entry(customers).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExits(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        //post: api/Customers
        [ResponseType(typeof(Customers))]
        public  async Task<IHttpActionResult> PostCustomers(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Customers.Add(customers);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExits(customers.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = customers.ID }, customers);
        }        
        
        // delete: api/customers/5
        [ResponseType(typeof(Customers))]
        public async  Task<IHttpActionResult> DeleteCustomers(Guid id)
        {
            Customers custoemrs = await db.Customers.FindAsync(id);
            if (custoemrs == null)
            {
                return NotFound();
            }
            db.Customers.Remove(custoemrs);
            await db.SaveChangesAsync();

            return Ok(custoemrs);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExits(Guid id)
        {
            return db.Customers.Count(o => o.ID == id) > 0;
        }
    }
}
