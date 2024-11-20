namespace Topmass.Admin.Business
{


    public class NTDRequestAdd
    {
        public string FirstName { get; set; }

        public int LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public NTDRequestAdd()
        {

        }
    }

    public class  ArticleRequestAdd
    {

            public int? Id { get; set; }

            public string Title{ get; set; }

            public string ShortDes { get; set; }

            public string Keyword { get; set; }

            public string LinkImage { get; set; }

            public string CategryIdLink { get; set; }

            public string Content{ get; set; }
      
    }
}
