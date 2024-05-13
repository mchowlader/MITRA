using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mitra.Applications.IServices;
using Mitra.Domain.Entities;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseApiController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _contactService.CreateContactAsync(contact);
            return Ok();
        }
    }
}