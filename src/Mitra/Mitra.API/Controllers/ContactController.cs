using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mitra.Applications.Helper;
using Mitra.Applications.IServices;
using Mitra.Applications.Response;
using Mitra.Domain.Entities;
using Mitra.Domain.Model;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly string requestTime = Utilities.GetRequestResponseTime();
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(Contact), 201)]
        public async Task<ActionResult> Create(ContactDTO contact)
        {
            if (ModelState.IsValid)
            {
               var result = await _contactService.CreateAsync(contact);
                if (result.IsSuccess) 
                {
                    var response = new PayloadResponse<ContactDTO>
                    {
                        Message = result.Message != null ? result.Message : new List<string> { "Internal Error" },
                        Payload = result.Data,
                        PayloadType = "Add Contact",
                        RequestTime = requestTime,
                        ResponseTime = Utilities.GetRequestResponseTime(),
                        Success = result != null ? result.IsSuccess : false
                    };
                    var created_obj_id = response.Payload != null ? response.Payload.Id : null;
                    return Created(response.RequestURL + "/" + created_obj_id, response);
                }
            }
            else
            {
                return new BadRequestObjectResult(new
                {
                    errors = new
                    {
                        contact = new[] {"Invalid contact"}
                    },
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    titel = "One or more validation errors occurred.",
                    status = 400,
                    traceId = HttpContext != null ? HttpContext.TraceIdentifier : ""
                });
            }
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(List<Contact>), 200)]
        public IActionResult Get(string? searchText, string? sortText, int page = 1, int limit = 10)
        {
            var result = _contactService.GetContact(page, limit, searchText, sortText);
            if(result.IsSuccess == true)
            {
                var response = new PayloadResponse<IList<Contact>>
                {
                    Message = result.Message != null ? result.Message : new List<string> {"Internal Error"},
                    Payload = result.Data,
                    PayloadType = "Contact List",
                    RequestTime = requestTime,
                    ResponseTime = Utilities.GetRequestResponseTime(),
                    Success = result != null ? result.IsSuccess : false,
                };
                return Ok(response);
            }
            else
            {
                return new BadRequestObjectResult(new
                {
                    errors = new
                    {
                        contact = new[] { "Invalid contact" }
                    },
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    titel = "One or more validation errors occurred.",
                    status = 400,
                    traceId = HttpContext != null ? HttpContext.TraceIdentifier : ""
                });
            }
        }
    }
}