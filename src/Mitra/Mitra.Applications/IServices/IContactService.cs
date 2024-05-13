using Mitra.Domain.Entities;
using System.Text.RegularExpressions;

namespace Mitra.Applications.IServices;

public interface IContactService
{
    Task CreateContactAsync(Contact contact);
    (IList<Contact> records, int total, int totalDisplay) GetContactData(int pageIndex, int pageSize,
        string searchText, string sortText);
    Contact GetContact(int id);
    void UpdateContact(Contact contact);
    void DeleteContact(int id);
    void DataSet(Contact contact);
}
