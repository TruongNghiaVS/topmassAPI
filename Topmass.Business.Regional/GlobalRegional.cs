using Topmass.Core.Model.location;

namespace Topmass.Business.Regional
{
    public class GlobalRegional
    {

        public static GlobalRegional _globalRegional = null;
        public List<RegionalModel> DataGlobal { get; set; }

        public GlobalRegional()
        {
            DataGlobal = new List<RegionalModel>();
        }
        public static GlobalRegional GetRegional()
        {
            if (_globalRegional == null)
            {
                _globalRegional = new GlobalRegional();
            }

            return _globalRegional;
        }


        public RegionalModel GetRegionalById(int id)
        {

            var itemfind = DataGlobal.Where(x => x.Id == id).FirstOrDefault();
            if (itemfind == null)
            {
                return new RegionalModel()
                {
                    Id = -1,
                    Name = "",
                    Code = ""
                };
            }
            return itemfind;
        }
        public RegionalModel GetRegionalById(string id)
        {
            if (id == "-1")
            {
                return new RegionalModel()
                {
                    Id = -1,
                    Name = "Toàn quốc",
                    Code = ""
                };
            }
            var reponse = new RegionalModel()
            {
                Id = -1,
                Name = "",
                Code = ""
            };
            if (string.IsNullOrEmpty(id))
            {
                return reponse;
            }

            var itemfind = DataGlobal.Where(x => x.Code == id).FirstOrDefault();
            if (itemfind == null)
            {
                return new RegionalModel()
                {
                    Id = -1,
                    Name = "",
                    Code = ""
                };
            }
            return itemfind;
        }



    }
}
