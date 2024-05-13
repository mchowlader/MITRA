using Mitra.Domain.IRepositories;
using Mitra.Domain.Entities;
using Mitra.Applications.IServices;

namespace Mitra.Infrastructure.Service;

internal class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateContactAsync(Contact contact)
    {
        if (contact == null) throw new InvalidOperationException("Company was not provided");
        _unitOfWork.ContactRepository.Add(contact);
       await _unitOfWork.SaveChangesAsync();
    }

    public void DataSet(Contact contact)
    {
        throw new NotImplementedException();
    }

    public void DeleteContact(int id)
    {
        throw new NotImplementedException();
    }

    public Contact GetContact(int id)
    {
        throw new NotImplementedException();
    }

    public (IList<Contact> records, int total, int totalDisplay) GetContactData(int pageIndex, int pageSize, string searchText, string sortText)
    {
        throw new NotImplementedException();
    }

    public void UpdateContact(Contact contact)
    {
        throw new NotImplementedException();
    }
}
