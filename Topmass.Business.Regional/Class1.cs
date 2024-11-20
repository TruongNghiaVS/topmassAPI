using Topmass.Core.Repository;

namespace Topmass.Business.Regional
{
    public class RegionalBsRequest : RegionalRequest
    {

        public RegionalBsRequest()
        {
            this.Type = 1;
        }

    }


    public class GetAllDistrictRequest
    {
        public string Code { get; set; }
        public GetAllDistrictRequest()
        {

        }

    }



    public class RegionalBsReponse : RegionalReponse
    {


    }
}
