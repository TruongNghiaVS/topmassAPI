using Dapper;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace TopMass.Import
{
    public class BlogItem
    {


        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string KeyWord { get; set; }
        public string Content { get; set; }
        public string ShortDes { get; set; }
        public string Linked { get; set; }
        public string Slug { get; set; }

        public string AuthorPost { get; set; }
    }

    public class BlogItemIndex
    {
        public string Heading { get; set; }
        public string Slug { get; set; }
        public string Cate_id { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Featured { get; set; }
        public string Meta_title { get; set; }

        public string Meta_keywords { get; set; }

        public string meta_descriptions { get; set; }
        public string TypePost { get; set; }
        public string AuthorPost { get; set; }
    }
    public class BlogImport
    {
        public List<BlogItemIndex> BlogItems { get; set; }
        public List<BlogItem> BlogItem { get; set; }
        public BlogImport()
        {
            BlogItem = new List<BlogItem>();
            BlogItems = new List<BlogItemIndex>();
        }

        public void AddItem(BlogItem itemInsert)
        {
            if (string.IsNullOrEmpty(itemInsert.Title))
            {
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=jobvieclam;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;"))
                {
                    conn.Execute("sp_articles_insert", itemInsert, commandType: System.Data.CommandType.StoredProcedure);
                    conn.Close();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occre while creating table:" + e.Message + "\t" + e.GetType());
            }
        }

        public void GetData()
        {



            using (var conn = new MySqlConnection("Server=192.168.1.2,3306; Initial Catalog=jobportaldb;User ID=demo2"))
            {
                try
                {

                    var dataList = conn.Query<BlogItemIndex>("SELECT * FROM blogs", new
                    { }, commandType: System.Data.CommandType.Text);
                    foreach (var item in dataList)
                    {
                        var linked2 = "";
                        try
                        {
                            linked2 = item.Cate_id;
                        }
                        catch (Exception)
                        {

                        }
                        var newcontent = item.Content.Replace("http://jobvieclam.com", "http://42.115.94.180:8584/static");
                        newcontent = newcontent.Replace("https://jobvieclam.com", "http://42.115.94.180:8584/static");
                        var categoryId = "";
                        if (linked2 == "5")
                        {
                            categoryId = "2";
                        }
                        if (linked2 == "7")
                        {
                            categoryId = "3";
                        }
                        if (linked2 == "8")
                        {
                            categoryId = "4";
                        }
                        if (linked2 == "11")
                        {
                            categoryId = "5";
                        }
                        if (linked2 == "12")
                        {
                            categoryId = "6";
                        }

                        if (linked2 == "16")
                        {
                            categoryId = "7";
                        }
                        if (linked2 == "17")
                        {
                            categoryId = "8";
                        }
                        if (linked2 == "18")
                        {
                            categoryId = "9";
                        }
                        if (linked2 == "21")
                        {
                            categoryId = "12";
                        }

                        if (linked2 == "5,17")
                        {
                            categoryId = "2,8";
                        }
                        var iteminsert = new BlogItem()
                        {
                            Content = newcontent,
                            AuthorPost = item.AuthorPost,
                            CoverImage = item.Image,
                            KeyWord = item.Meta_keywords,
                            Linked = categoryId,
                            ShortDes = item.meta_descriptions,
                            Slug = item.Slug,
                            Title = item.Heading
                        };
                        BlogItem.Add(iteminsert);
                    }
                    conn.Close();


                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception Occre while creating table:" + e.Message + "\t" + e.GetType());
                }

            }




        }

        public void Migrate()
        {
            GetData();


            foreach (var item in BlogItem)
            {

                AddItem(item);
            }
        }
    }
}
