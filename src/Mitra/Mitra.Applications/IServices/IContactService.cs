using Mitra.Applications.Response;
using Mitra.Domain.Entities;
using Mitra.Domain.Model;
using System.Text.RegularExpressions;

namespace Mitra.Applications.IServices;

public interface IContactService
{
    Task<ServiceResponse<ContactDTO>> CreateAsync(ContactDTO contact);
    ServiceResponse<IList<Contact>> GetContact(int pageIndex, int pageSize,
        string searchText, string sortText);
    Contact GetContact(int id);
    void UpdateContact(Contact contact);
    void DeleteContact(int id);
    void DataSet(Contact contact);
}
