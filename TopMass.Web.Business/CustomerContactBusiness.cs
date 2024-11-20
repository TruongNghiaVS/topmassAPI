using Topmass.Core.Repository;
using TopMass.Core.Result;


namespace Topmass.Web.Business
{
    public partial class CustomerContactBusiness : ICustomerContactBusiness
    {
        private readonly IContactItemModelRepository _contactItemModelRepository;
        public CustomerContactBusiness(IContactItemModelRepository contactItemModelRepository)
        {

            _contactItemModelRepository = contactItemModelRepository;
        }
        public async Task<BaseResult> AddRequest(string name,
            string email,
            string phone,
            string title,
            string content,
            int typeData = 0
            )
        {
            var reponse = new BaseResult();

            if (string.IsNullOrEmpty(phone))
            {
                reponse.AddError(nameof(phone), "thiếu thông tin số điện thoại");
            }

            if (string.IsNullOrEmpty(title))
            {
                reponse.AddError(nameof(title), "thiếu nội dung tiêu đề");
            }

            if (string.IsNullOrEmpty(content))
            {
                reponse.AddError(nameof(content), "thiếu nội dung");
            }
            await _contactItemModelRepository.AddOrUPdate(new Core.Model.Support.ContactItemModel()
            {
                Content = content,
                Name = name,
                PhoneNumber = phone,
                Email = email,
                Type = typeData,
                Title = title,
                Status = 1,
                CreateAt = DateTime.Now,
                CreatedBy = -1,
                UpdateAt = DateTime.Now,
                UpdatedBy = -1
            });

            return reponse;
        }
    }
}
