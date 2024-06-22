using Mitra.Applications.IServices;
using Mitra.Applications.Response;
using Mitra.Domain.Entities;
using Mitra.Domain.IRepositories;
using Mitra.Domain.Model;

namespace Mitra.Applications.Services;
public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse<ContactDTO>> CreateAsync(ContactDTO contact)
    {
        try
        {
            _unitOfWork.ContactRepository.Add(
                new Contact()
                  {
                      Name = contact.Name,
                      Email = contact.Email,
                      Phone = contact.Phone
                  });
            await _unitOfWork.SaveChangesAsync();
            return ServiceResponse<ContactDTO>.AddSuccessfully(contact);
        }
        catch (Exception ex)
        {
            return ServiceResponse<ContactDTO>.Error(ex.Message);
        }
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

    public ServiceResponse<IList<Contact>> GetContact(int pageIndex, int pageSize, string searchText, string sortText)
    {
        try
        {
            var result = _unitOfWork.ContactRepository.Get(null, null, string.Empty, pageIndex, pageSize, false);
            return ServiceResponse<IList<Contact>>.RetriveSuccessfully(result.data, "Data retrive successfully");
        }
        catch (Exception ex)
        {
            return ServiceResponse<IList<Contact>>.Error(ex.Message);
        }
     
    }

    public void UpdateContact(Contact contact)
    {
        throw new NotImplementedException();
    }
}
