using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using topmass.Controllers;
using Topmass.core.Business;

namespace topmass.Model
{
    [ApiController]
    [Authorize]
    public class ProjectUserController : BaseController
    {

        private readonly ILogger<ProjectUserController> _logger;

        private readonly IProfileBusiness _profileBusiness;

        public ProjectUserController(ILogger<ProjectUserController> logger,

            IProfileBusiness profileBusiness
            ) : base(logger)
        {
            _logger = logger;

            _profileBusiness = profileBusiness;
        }
        [HttpPost]
        public async Task<ActionResult> AddProjectUser(InputProjectUserRequestAdd requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();

            if (string.IsNullOrEmpty(requestAdd.ProjectName))
            {
                baseReult.AddError(nameof(requestAdd.ProjectName), "Thiếu thông tin trường");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }

            var request = new ProjectUserInfoRequest()
            {
                FromMonth = requestAdd.FromMonth,
                FromYear = requestAdd.FromYear,
                Introduction = requestAdd.Introduction,
                LinkFile = requestAdd.LinkFile,

                IsEnd = requestAdd.IsEnd,
                Position = requestAdd.Position,
                ToMonth = requestAdd.ToMonth,
                ToYear = requestAdd.ToYear,
                UserId = int.Parse(resultUser.Id),
                CustomerName = requestAdd.CustomerName,
                ProjectName = requestAdd.ProjectName,
                NumOfMember = requestAdd.NumOfMember,
                Techology = requestAdd.Technology

            };
            var data = await _profileBusiness.AddProject(request);
            baseReult.Data = data;

            return StatusCode(baseReult.StatusCode, baseReult);

        }
        [HttpPost]
        public async Task<ActionResult> SaveProjectUsers(List<InputProjectUserRequestAdd> requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();


            foreach (var item in requestAdd)
            {
                if (string.IsNullOrEmpty(item.ProjectName))
                {
                    baseReult.AddError(nameof(item.ProjectName), "Thiếu thông tin trường");
                }
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }

            var request = new ProjectUserInfoRequest()
            {

                UserId = int.Parse(resultUser.Id)
            };


            var allIdExited = await _profileBusiness.GetAllId(resultUser.UserId, 0);


            foreach (var item in requestAdd)
            {
                if (item.Id < 1)
                {

                    var requestInsert = new ProjectUserInfoUpdateRequest()
                    {
                        FromMonth = item.FromMonth,
                        FromYear = item.FromYear,
                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,

                        IsEnd = item.IsEnd,
                        CustomerName = item.CustomerName,
                        ProjectName = item.ProjectName,
                        NumOfMember = item.NumOfMember,
                        Techology = item.Technology,
                        Position = item.Position,
                        ToMonth = item.ToMonth,
                        ToYear = item.ToYear,


                        UserId = int.Parse(resultUser.Id)
                    };
                    await _profileBusiness.AddProject(requestInsert);

                }
                else
                {
                    var requestUpdate = new ProjectUserInfoUpdateRequest()
                    {
                        FromMonth = item.FromMonth,
                        FromYear = item.FromYear,
                        Introduction = item.Introduction,
                        LinkFile = item.LinkFile,

                        IsEnd = item.IsEnd,
                        CustomerName = item.CustomerName,
                        ProjectName = item.ProjectName,
                        NumOfMember = item.NumOfMember,
                        Techology = item.Technology,
                        Position = item.Position,
                        ToMonth = item.ToMonth,
                        ToYear = item.ToYear,

                        UserId = int.Parse(resultUser.Id),
                        Id = item.Id
                    };
                    await _profileBusiness.UpdateProject(requestUpdate);
                }
            }
            var deleteAll = allIdExited.Select(a => a.Id).Except(requestAdd.Select(b => b.Id)).ToList();

            foreach (var item in deleteAll)
            {
                await _profileBusiness.DeleteProjectUser(new ProjectUserInfoDeleteRequest()
                {
                    Id = item
                });
            }



            baseReult.Data = true;
            await _profileBusiness.ReloadGenFileCV(resultUser.UserId);
            return StatusCode(baseReult.StatusCode, baseReult);

        }


        [HttpPost]
        public async Task<ActionResult> UpdateProjectUser(InputProjectUserInfoUpdateRequest requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();

            if (string.IsNullOrEmpty(requestAdd.ProjectName))
            {
                baseReult.AddError(nameof(requestAdd.ProjectName), "Thiếu thông tin trường");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }
            var request = new ProjectUserInfoUpdateRequest()
            {
                FromMonth = requestAdd.FromMonth,
                FromYear = requestAdd.FromYear,
                Introduction = requestAdd.Introduction,
                LinkFile = requestAdd.LinkFile,

                IsEnd = requestAdd.IsEnd,

                Position = requestAdd.Position,
                ToMonth = requestAdd.ToMonth,
                ToYear = requestAdd.ToYear,
                UserId = int.Parse(resultUser.Id),
                Id = requestAdd.Id,
                CustomerName = requestAdd.CustomerName,
                ProjectName = requestAdd.ProjectName,
                NumOfMember = requestAdd.NumOfMember,
                Techology = requestAdd.Technology
            };
            var data = await _profileBusiness.UpdateProject(request);
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpPost]
        public async Task<ActionResult> DeleteProjectUser(InputProfileDelete requestAdd)
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            if (requestAdd.Id < 1)
            {
                baseReult.AddError(nameof(requestAdd.Id), "Thiếu thông tin trường Id");
            }

            if (!baseReult.Success)
            {
                return StatusCode(baseReult.StatusCode, baseReult);
            }
            var data = await _profileBusiness.DeleteProjectUser(new ProjectUserInfoDeleteRequest()
            {
                Id = requestAdd.Id
            });
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }

        [HttpGet]
        public async Task<ActionResult> GetAllProjectUser()
        {
            var resultUser = await GetCurrentUser();
            var baseReult = new BaseResult();
            var data = await _profileBusiness.GetAllProjectUser(int.Parse(resultUser.Id));
            baseReult.Data = data;
            return StatusCode(baseReult.StatusCode, baseReult);

        }
    }
}
