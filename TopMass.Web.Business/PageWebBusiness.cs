using Topmass.Core.Model.Support;
using Topmass.Core.Repository;
using TopMass.Web.Business;
using TopMass.Web.Business.Model;


namespace Topmass.Web.Business
{
    public partial class PageWebBusiness : IPageWebBusiness
    {
        private readonly IContactItemModelRepository _repository;

        public PageWebBusiness(IContactItemModelRepository userRepository)
        {

            _repository = userRepository;
        }

        public async Task<dynamic> AddCustomerRequest(ContactCustomerRequest request)
        {
            var itemInsert = new ContactItemModel()
            {
                Content = request.Content,
                CreateAt = DateTime.Now,
                CreatedBy = -1,
                Deleted = false,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.phoneNumber,
                Status = 1,
                Title = request.Title,
                UpdateAt = DateTime.Now,
                UpdatedBy = 1
            };
            return await _repository.AddOrUPdate(itemInsert);

        }






    }
}
