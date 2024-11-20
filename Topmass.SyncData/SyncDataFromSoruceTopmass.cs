using HandleSyncImportCV;

namespace Topmass.SyncData
{
    public class SyncDataFromSoruceTopmass
    {
        public List<CVInfoImport> DataImport { get; set; }
        private DataAccess dataAccess { get; set; }

        public SyncDataFromSoruceTopmass(List<CVInfoImport> _dataImport)
        {
            DataImport = _dataImport;
            dataAccess = new DataAccess();
        }
        public string GetLocatoinCode(string location)
        {
            location = "79";
            if (location == "Long An")
            {
                location = "80";
            }
            if (location == "Hà Nội")
            {
                location = "01";
            }

            if (location == "Huế")
            {
                location = "46";
            }
            if (location == "Nha Trang")
            {
                location = "56";
            }
            return location;
        }

        public int GetRankLevel(string location)
        {
            int result = -1;
            if (location == "Trung cấp")
            {
                result = 1;
            }
            if (location == "THPT")
            {
                result = 2;
            }

            if (location == "Cao đẳng")
            {
                result = 3;
            }
            if (location == "Đại học")
            {
                result = 4;
            }
            if (location == "Sau đại học ")
            {
                result = 5;
            }
            return result;
        }
        public async void SyncData()
        {


            foreach (var item in DataImport)
            {
                var searchcv = new SearchCVAdd()
                {
                    Phone = item.Phone,
                    FullName = item.Name,
                    Email = item.Email,
                    Position = item.Postion,
                    IntroductionText = item.Introduction,
                    DobYear = item.Dob.HasValue ? item.Dob.Value.Year : -1,
                    ContentCV = item.Content,
                    ContentCV2 = item.Content2,
                    Gender = item.Gender == "Nam" ? 1 : 2,
                    LocationText = item.Location,
                    LocationCode = GetLocatoinCode(item.Location),
                    LinkCV = item.LinkFileCV,
                    RandEducation = GetRankLevel(item.EducationLevel)

                };
                await dataAccess.CreateSearchCV(searchcv);
            }

        }
        public void CreateAccout(string email, string phone, string Name)
        {


        }

        public void CreateSearchCV(CVInfoImport itemInsert)
        {

        }
    }
}
