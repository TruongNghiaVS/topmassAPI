namespace Topmass.Core.Repository
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyRegister
    {
        public static void ConfigRep(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IForgetPasswordRepository, ForgetPasswordRepository>();
            services.AddSingleton<ICandidateInfoRepository, CandidateInfoRepository>();
            services.AddSingleton<ICandidateRepository, CandidateRepository>();
            services.AddSingleton<IRegionalRepository, RegionalRepository>();
            services.AddSingleton<IMasterDataRepository, MasterDataRepository>();
            services.AddSingleton<ICampagnRepository, CampagnRepository>();
            services.AddSingleton<IJobRepository, JobItemRepository>();
            services.AddSingleton<IJobApplyRepository, JobApplyRepository>();
            services.AddSingleton<IResumeRepository, ResumeRepository>();
            services.AddSingleton<IResumeUIRepository, ResumeUIRepository>();
            services.AddSingleton<IJobApplyStatusRepository, JobApplyStatusRepository>();
            services.AddSingleton<IJobLogViewRepository, JobLogViewRepository>();
            services.AddSingleton<ITicketItemRepository, TicketItemRepository>();
            services.AddSingleton<IJobOverViewCounterRepository, JobOverViewCounterRepository>();
            services.AddSingleton<IJobsaveRepository, JobsaveRepository>();
            services.AddSingleton<IJobInfoRepository, JobInfoRepository>();
            services.AddSingleton<IEducationUserRepository, EducationUserRepository>();
            services.AddSingleton<IExperienceUserRepository, ExperienceRepository>();
            services.AddSingleton<ICertifyUserRepository, CertifyUserRepository>();
            services.AddSingleton<IOtherProfileUserRepository, OtherProfileUserRepository>();
            services.AddSingleton<IProjectUserRepository, ProjectUserRepository>();
            services.AddSingleton<IJobSettingUserRepository, JobSettingUserRepository>();
            services.AddSingleton<ICompanyInfoRepository, CompanyInfoRepository>();
            services.AddSingleton<IJobDisplayItemRepository, JobDisplayItemRepository>();
            services.AddSingleton<IProfileCVUserRepository, ProfileCVUserRepository>();
            services.AddSingleton<ICompanyFollowModelRepository, CompanyFollowModelRepository>();
            services.AddSingleton<ICompanyFavoriteModelRepository, CompanyFavoriteRepository>();
            services.AddSingleton<ILogActionModelRepository, LogActionModelRepository>();
            services.AddSingleton<IPageContentRepository, PageContentRepository>();
            services.AddSingleton<ISearchCVExtraRepository, SearchCVExtraRepository>();
            services.AddSingleton<ISearchCVRepository, SearchCVRepository>();
            services.AddSingleton<ISearchCVResultRepository, SearchCVResultRepository>();
            services.AddSingleton<IMetaDataRepository, MetaDataRepository>();
            services.AddSingleton<IOpenCVResultRepository, OpenCVResultRepository>();
            services.AddSingleton<IActiveCodeMemberRepository, ActiveCodeMemberRepository>();
            services.AddSingleton<IMailModelRepository, MailModelRepository>();
            services.AddSingleton<IOpenViewerResultRepository, OpenViewerResultRepository>();
            services.AddSingleton<IcandidateViewStatusRepository, CandidateViewStatusRepository>();
        }
    }
}
