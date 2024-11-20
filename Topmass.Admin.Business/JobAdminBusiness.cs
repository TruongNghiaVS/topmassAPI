using System.Text.Json;
using Topmass.Admin.Business.Model;
using Topmass.Admin.Repository;
using Topmass.Business.Regional;
using Topmass.Bussiness.Mail;

namespace Topmass.Admin.Business
{



    public class JobAdminBusiness : BaseBusiness, IJobAdminBusiness
    {

        private readonly IAdminRepository _repository;
        private readonly IRecruitmentMailBussiness _recruitmentMailBussiness;
        private readonly IRegionalBusiness _regionalbussiness;
        public JobAdminBusiness(IAdminRepository _adminRepository,
            IRegionalBusiness regionalBusiness,
            IRecruitmentMailBussiness recruitmentMailBussiness
            ) : base(_adminRepository)
        {
            _repository = _adminRepository;
            _recruitmentMailBussiness = recruitmentMailBussiness;
            _regionalbussiness = regionalBusiness;

        }

        public async Task<SearchJobAdminReponse> GetAll(SearchJobAdminRequest request)
        {
            var reponse = new SearchJobAdminReponse();
            var data = await _repository.JobAdminRep.GetAll(request);
            reponse.Data = data.Data;
            return reponse;
        }


        public async Task<bool> UpdateConfirmStatus(UpdateJobAdmin request)
        {
            var status = request.StatusChange;
            var content = "Chúng tôi đang xem xét tin đăng của bạn, vui lòng theo dõi  ";
            if (status == 1)
            {
                content = "Chúng tôi đang xét duyệt tin đăng của quý khách, vui lòng theo dõi và chờ đợi. ";
            }
            if (status == 2)
            {
                content = "Sau khi xem xét, Tin của bạn sẽ được hiển thị và được tìm kiếm trên trang topmmass.vn";
            }

            if (status == 3)
            {
                content = "Sau khi xem xét, Rất tiếc khi tin của quý khách đã bị tự chối.";
            }
            if (status == 4)
            {
                content = "Tin của bạn đã bị khoá.";
            }
            return await _repository.NTDRepository.UpdateConfirmStatus(request.Id, request.StatusChange, request.NotedChange, content);
        }

        public async Task<bool> UpdateStatusDisplay(int id, int statusChange, string noted = "", string content = "")
        {
            content = "Chúng tôi đã thay trạng thái hiển thị của tin đăng, quý khách vui lòng kiểm tra và  thay đổi lại trong phần chiến dịch tuyển dụng";
            return await _repository.NTDRepository.UpdateStatusDisplay(id, statusChange, noted, content);

        }
        public async Task<List<JobLogAdminItemDisplay>> GetAllLog(int id)
        {

            return await _repository.JobAdminRep.GetAllLog(id);

        }
        public async Task<JobAdminDetail> GetDetail(int id)
        {

            var detailBasic = await _repository.JobAdminRep.GetDetailBasic(id);

            var infoBasic = await _repository.JobAdminRep.GetInfoJobAdmin(id);
            var locationsArray = new List<LocationsJob>();
            var itemGlobal = GlobalRegional.GetRegional();
            if (itemGlobal.DataGlobal == null || itemGlobal.DataGlobal.Count < 1)
            {
                await _regionalbussiness.LoadAllData();

            }
            if (!string.IsNullOrEmpty(infoBasic.Locations))
            {
                locationsArray = JsonSerializer.Deserialize<List<LocationsJob>>(infoBasic.Locations);
            }
            var timeWorkings = new List<TimeWorking>();
            if (!string.IsNullOrEmpty(infoBasic.Time_workings))
            {
                timeWorkings = JsonSerializer.Deserialize<List<TimeWorking>>(infoBasic.Time_workings);
            }
            var textTimeWorking = "";
            foreach (var item in timeWorkings)
            {
                if(!string.IsNullOrEmpty( item.Day_from))
                {
                    textTimeWorking += "<p> Từ "  + item.Day_from;
                    
                }

                if (!string.IsNullOrEmpty(item.Day_to))
                {
                    textTimeWorking += " Đến " + item.Day_to;

                }

                if (!string.IsNullOrEmpty(item.Time_from))
                {
                    textTimeWorking += "  từ khung giờ  " + item.Time_from;
                }

                if (!string.IsNullOrEmpty(item.Time_from))
                {
                    textTimeWorking += " đến " + item.Time_to;
                }
                textTimeWorking += "</p>";
            }   
            var addressDetail = "";
            foreach (var item in locationsArray)
            {
                var locationtext1 =  itemGlobal.GetRegionalById(item.Location).Name;
                var provincecity = item.Location;
                var city = item.Districts.FirstOrDefault();
                if( city != null)
                {
                    var citycode = city.District;
                    var cityText = itemGlobal.GetRegionalById(city.District).Name;
                    var detailAdress = city.Detail_location;
                    if( !string.IsNullOrEmpty(detailAdress))
                    {
                        addressDetail += detailAdress;
                    }
                    if (!string.IsNullOrEmpty(detailAdress))
                    {
                        addressDetail += "," + cityText + ", ";
                    }
                }
                addressDetail += locationtext1;
            }
            var salaryText = "";
            var unitText = "";
            if (infoBasic.Aggrement == false)
            {
                if (infoBasic.Salary_from > 0)
                {
                    salaryText += "Từ " + infoBasic.Salary_from + " ";
                }

                if (infoBasic.Salary_to > 0)
                {
                    salaryText += "Đến " + infoBasic.Salary_to;
                }
                if (infoBasic.Type_money == "0")
                {
                    unitText = " VNĐ";
                }

                if (infoBasic.Type_money == "1")
                {
                    unitText = " USD";
                }
            }
            else
            {
                salaryText = "Thỏa thuận";
            }
            
            
            return new JobAdminDetail()
            {
                DataBasic = detailBasic,
                InfoBasic = infoBasic,
                SalaryText = salaryText,
                UnitText = unitText,
                AddressDetail = addressDetail,
                TextTimeWorking = textTimeWorking

            };
        }

    }
}
