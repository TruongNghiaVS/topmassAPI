using Topmass.Admin.Repository;
using Topmass.Bussiness.Mail;
using Topmass.Core.Model;

namespace Topmass.Admin.Business
{


    public class DataDetailBasicInfo
    {
        public string Phone { get; set; }
        public string Name { get; set; }

        public int Gender { get; set; }

        public string UserName { get; set; }
        public string DateTimeCreateText { get; set; }

        public int LevelAuthen1 { get; set; }

        public int StatusCompany { get; set; }
        public string LevelLevelAuthen1Text { get; set; }
    }

    public class DocumentDetailInfo
    {
        public string LinkFile { get; set; }

        public int Status { get; set; }

        public string CurrentStatus { get; set; }

        public string Message { get; set; }
    }
    public class DataDetailInfo
    {

        public DataDetailBasicInfo DataBasic { get; set; }

        public DocumentDetailInfo DocumentDetailInfo { get; set; }

        public dynamic CompanyInfo { get; set; }

        public DataDetailInfo()
        {

        }

    }

    public class NTDBusiness : BaseBusiness, INTDBusiness
    {

        private readonly IAdminRepository _repository;
        private readonly IRecruitmentMailBussiness _recruitmentMailBussiness;

        public NTDBusiness(IAdminRepository _adminRepository,

            IRecruitmentMailBussiness recruitmentMailBussiness
            ) : base(_adminRepository)
        {
            _repository = _adminRepository;
            _recruitmentMailBussiness = recruitmentMailBussiness;

        }

        public async Task<SearchNTDReponse> GetAllNTD(SearchNTDRequest request)
        {
            var reponse = new SearchNTDReponse();
            var data = await _repository.NTDRepository.GetAll(new
            {
                    request.Token,
                    request.OrderBy,
                    request.CbDocumnetStatus,
                    request.Page,
                    Status = request.Status,
                    request.Limit,
                    request.AuthenLevel,
                    request.From,
                    request.To
            });
            reponse.Data = data.Data;
            return reponse;
        }


        public async Task<List<NTDLogInfo>> GetAllLog(int id)
        {
            var reponse = new SearchNTDReponse();
            var data = await _repository.NTDRepository.GetAllLog(id);
            return data;
        }
        public async Task<List<NTDAccountLogInfo>> GetALlLogAccount(int id)
        {
            var reponse = new SearchNTDReponse();
            var data = await _repository.NTDRepository.GetALlLogAccount(id);
            return data;
        }
      

        public async Task<List<NTDShortInfo>> GetAllShortNTD()
        {
            return await _repository.NTDRepository.GetAllShortNTD();
        }


        public async Task<bool> AddOrUpdateDocumnet(UpdateDocumnetRequest request)
        {
            var status = request.StatusChange;
            var documnetInfo = await _repository.NTDRepository.GetDocumentNTD(request.Id);
            var content = "Chúng tôi chưa ghi nhận thông tin chứng từ liên quan, vui lòng vào cập nhật chứng từ ";
            if (status == "1")
            {
                content = "Chúng tôi đã ghi nhận thông tin chứng từ liên quan, Vui  lòng chờ chúng tôi xem xét ";
            }
            if (status == "2")
            {
                content = "Sau khi đánh giá chứng tư, chúng tôi từ chối chứng từ, Vui lòng cập nhật chứng từ khác ";
            }

            if (status == "3")
            {
                content = "Chứng từ đã được duyệt thành công, bây giờ bạn có thể đăng tin.";
            }
            if (documnetInfo == null || string.IsNullOrEmpty(documnetInfo.Email))
            {
                await _repository.NTDRepository.AddDocumentNTD(request.StatusChange,
                    request.NotedChange, request.Id, content, request.LinkFile, request.ReasonReject );

            }
            else
            {
                await _repository.NTDRepository.UpdateDocumentNTD(request.StatusChange,
                   request.NotedChange, request.Id, content, request.ReasonReject);
            }

            return true;
        }

        public async Task<bool> UpdateInfoHuman(int statusAccout, int statusConfirm, int id, int reasonCode = -1, string noted = "")
        {

            return await _repository.NTDRepository.UpdateInfoHuman(statusAccout, statusConfirm, id,
                reasonCode ,  noted );


        }
        public async Task<bool> UpdatePersonalPerson(string FullName, int Gender, string phoneNumber, int id)
        {
            return await _repository.NTDRepository.UpdatePersonalPerson(FullName, Gender, phoneNumber,
               id);

        }
        public async Task<bool> SendMailActiveAccount(int id)
        {

            var itemInfo = await _repository.FindOneByStatementSql<RecruiterModel>("select * from Recruiter where id = @id", new
            {
                id
            });
            //check 
            if (itemInfo == null)
            {
                return false;
            }
            // push event send email password

            var forgetPasswordRequest = new ForgetPasswordModel()
            {
                CreateAt = DateTime.Now,
                CreatedBy = 1,
                TypeUser = 1,
                UserId = itemInfo.Id,
                Status = 0,
                SendMailStatus = 0,
                Deleted = false,
                UpdateAt = DateTime.Now,
                Email = itemInfo.Email,
                UpdatedBy = 1,
            };
            var randomCode = "" + new Random().Next(1000, 10000) + DateTime.Now.Ticks + "";
            forgetPasswordRequest.Code = randomCode;

            await adminRepository.ForgetPasswordRepository.AddOrUPdate(forgetPasswordRequest);

            await _recruitmentMailBussiness.RecruitmentCheckMailPassword(itemInfo.Email, randomCode);

            //var item = new ActiveCodeRecruiter();
            //var randomCode = "" + new Random().Next(1000, 10000) + DateTime.Now.Ticks + "";
            //item.Code = randomCode;
            //item.Email = itemInsert.Email;
            //item.Status = 1;
            //item.CreateAt = DateTime.Now;
            //await _activeCodeRecruiterRepository.AddOrUPdate(item);





            //await _mailBussiness.RecruitmentSuccessRegister(item.Email);
            return true;



        }
        public async Task<bool> ResetPasswordAccount(int id)
        {

            var itemInfo = await _repository.FindOneByStatementSql<RecruiterModel>("select * from Recruiter where id = @id", new
            {
                id
            });
            //check 
            if (itemInfo == null)
            {
                return false;
            }
            // push event send email password

            var forgetPasswordRequest = new ForgetPasswordModel()
            {
                CreateAt = DateTime.Now,
                CreatedBy = 1,
                TypeUser = 1,
                UserId = itemInfo.Id,
                Status = 0,
                SendMailStatus = 0,
                Deleted = false,
                UpdateAt = DateTime.Now,
                Email = itemInfo.Email,
                UpdatedBy = 1,
            };
            var randomCode = "" + new Random().Next(1000, 10000) + DateTime.Now.Ticks + "";
            forgetPasswordRequest.Code = randomCode;

            await adminRepository.ForgetPasswordRepository.AddOrUPdate(forgetPasswordRequest);

            await _recruitmentMailBussiness.RecruitmentCheckMailPassword(itemInfo.Email, randomCode);


            return true;



        }

        public async Task<DataDetailInfo> GetDetail(int id)
        {
            var basicInfo = new DataDetailBasicInfo();

            var detailBasic = await _repository.NTDRepository.GetDetailBasic(id);
            basicInfo.Phone = detailBasic.Phone;
            basicInfo.Name = detailBasic.Name;
            basicInfo.Gender = detailBasic.Gender == true ? 1 : 0;
            basicInfo.DateTimeCreateText = detailBasic.DateTimeCreateText;
            basicInfo.UserName = detailBasic.UserName;
            basicInfo.LevelAuthen1 = detailBasic.LevelAuthen1;
            basicInfo.StatusCompany = detailBasic.StatusCompany;
            basicInfo.LevelLevelAuthen1Text = detailBasic.LevelLevelAuthen1Text;
            var resultInfo = await _repository.NTDRepository.GetDocumentNTD(id);
            var documentInfo = new DocumentDetailInfo();
            if (resultInfo.Status == 0)
            {
                documentInfo.CurrentStatus = "Chưa ghi nhận thông tin";
            }
            else if (resultInfo.Status == 1)
            {
                documentInfo.CurrentStatus = "Chờ duyệt";
            }
            else if (resultInfo.Status == 2)
            {
                documentInfo.CurrentStatus = "Từ chối";
            }
            else if (resultInfo.Status == 3)
            {
                documentInfo.CurrentStatus = "Đã duyệt";
            }
            documentInfo.Status = resultInfo.Status;
            if (string.IsNullOrEmpty(resultInfo.LinkFile))
            {
                documentInfo.LinkFile = "";
                documentInfo.Status = 0;
                documentInfo.CurrentStatus = "Chưa nhận thông tin file";
            }
            else
            {
                documentInfo.LinkFile = "https://www.cdn.topmass.vn/static/" + resultInfo.LinkFile;
            }
            var companyInfo = await _repository.NTDRepository.GetCompanyInfo(id);
            var dataReponse = new DataDetailInfo
            {
                DataBasic = basicInfo,
                DocumentDetailInfo = documentInfo,
                CompanyInfo = companyInfo
            };
            return dataReponse;
        }

    }
}
