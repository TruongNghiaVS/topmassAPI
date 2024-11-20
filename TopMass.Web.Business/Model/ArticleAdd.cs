using Topmass.Core.Model;
using Topmass.Web.Repository.Model;

namespace TopMass.Web.Business.Model
{
    public class ArticleRequestAdd : ArticleAdd
    {
        public int? Id { get; set; }
        public ArticleRequestAdd() : base()
        {
            Id = -1;
        }
    }

    public class CategoryRequestAdd : CategoryAritcle
    {

        public CategoryRequestAdd() : base()
        {
            Id = -1;
        }
    }
    public class DeleteRequest
    {
        public int Id { get; set; }

    }


    public class ContactCustomerRequest
    {

        public string? Name { get; set; }

        public string? Content { get; set; }

        public string? phoneNumber { get; set; }

        public string? Title { get; set; }

        public string? Email { get; set; }
        public ContactCustomerRequest() : base()
        {

        }
    }



}
