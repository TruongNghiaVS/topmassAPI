﻿namespace Topmass.Core.Model.Page
{
    public class PageContentModel : BaseModel
    {
        public string? TitlePage { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public string? KeyWord { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public int TypeData { get; set; }
        public string AuthorPost { get; set; }

        public int Source { get; set; }



    }
}
