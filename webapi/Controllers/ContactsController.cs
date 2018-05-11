using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactContext _context;
        private readonly IOptions<EmailSettings> _emailSettings;
        public ContactsController(ContactContext context, IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _emailSettings = emailSettings;
            if (_context.Contacts.Count() == 0)
            {
                _context.Contacts.Add(new Contact { FirstName = "Jun", LastName = "Wang", Email = "demo@andmap.co", Message = "this is testing" });
                _context.SaveChanges();
            }
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            return _context.Contacts;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.SingleOrDefaultAsync(m => m.ContactId == id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.ContactId)
            {
                return BadRequest();
            }

            if (contact.Created == null)
                contact.Created = DateTime.Now;
            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (contact.Created == null)
                contact.Created = DateTime.Now;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            Helper.MailService mailService = new Helper.MailService(_emailSettings);
            /*
            var jsonResult = Json(contact);
            string jsonString = JsonConvert.SerializeObject(jsonResult.Value);
            */
            string jsonString = "Email: " +  contact.Email +"\n";
            if (!string.IsNullOrEmpty(contact.FirstName))
                jsonString += "First Name: " + contact.FirstName + "\n";

            if (!string.IsNullOrEmpty(contact.LastName))
                jsonString += "Last Name: " + contact.LastName + "\n";

            if (!string.IsNullOrEmpty(contact.Message))
                jsonString += "Message: " + contact.Message + "\n";

            mailService.SendEmail("Contact US", jsonString);
            return CreatedAtAction("GetContact", new { id = contact.ContactId }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.SingleOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(contact);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactId == id);
        }
    }
}