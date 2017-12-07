using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MySql.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace element_backstage.Controllers
{
    public class CustomerController : ApiController
    {
        private ELementDB db = new ELementDB();

        //GET： api/Customer
        public IQueryable<Customer> GetCustomer()
        {
            return db.Customers;
        }

        //GET:  api/Customer/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(Guid id)
        {
            Customer custoemrs = await db.Customers.FindAsync(id);
            if (custoemrs == null)
            {
                return NotFound();
            }
            return Ok(custoemrs);
        }

        //put: api/Customer/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PutCustomer(Guid id, Customer Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != Customer.ID)
            {
                return BadRequest();
            }
            db.Entry(Customer).State = EntityState.Modified;

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
        
        //post: api/Customer
        [ResponseType(typeof(Customer))]
        public  async Task<IHttpActionResult> PostCustomer(Customer Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Customers.Add(Customer);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExits(Customer.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = Customer.ID }, Customer);
        }        
        
        // delete: api/Customer/5
        [ResponseType(typeof(Customer))]
        public async  Task<IHttpActionResult> DeleteCustomer(Guid id)
        {
            Customer custoemrs = await db.Customers.FindAsync(id);
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
