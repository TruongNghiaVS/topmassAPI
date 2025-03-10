﻿using Microsoft.Extensions.Configuration;
using Topmass.Core.Repository.IndexModel;
using Topmass.Core.Repository.Report;

namespace Topmass.Core.Repository
{
    public partial class JobOverViewCounterRepository : RepositoryBase<JobOverViewCounterModel>, IJobOverViewCounterRepository
    {

        private readonly IJobRepository _jobRepository;
        public JobOverViewCounterRepository(IConfiguration configuration, IJobRepository jobRepository) : base(configuration)
        {
            tableName = "JobOverViewCounter";
            _jobRepository = jobRepository;
        }

        public async Task<JobOverViewCounterReponse> GetAll(JobOverViewCounterRequest request)
        {
            var jobItem = await _jobRepository.ExecuteSqlProcedure<JobOverviewDisplay>("sp_getInfoDetailOverview", new
            {
                JobId = request.JobId
            });
            var dataReports = await this.ExecuteSqlProcerduceToList<JobOverViewCounterDisplay>("sp_getInfoOverview", request);
            var listData = new List<JobOverViewCounterDisplay>();



            var dateRequest = request.From;
            while (dateRequest <= request.To)
            {
                var tempTotalCounterView = 0;
                var tempTotalCounterApply = 0;
                dateRequest = dateRequest.Value.AddDays(1);
                foreach (var item in dataReports)
                {
                    if (item.DayReport.Year == dateRequest.Value.Year &&
                        item.DayReport.Month == dateRequest.Value.Month &&
                        item.DayReport.Day == dateRequest.Value.Day
                        )
                    {
                        tempTotalCounterView += item.TotalViewer;
                        tempTotalCounterApply += item.TotalApply;
                    }
                }
                var itemDisaply = new JobOverViewCounterDisplay()
                {
                    TotalApply = tempTotalCounterApply,
                    TotalViewer = tempTotalCounterView,
                    DayReport = dateRequest.Value
                };
                listData.Add(itemDisaply);
            }
            var itemReponse = new JobOverViewCounterReponse();
            itemReponse.From = request.From;
            itemReponse.To = request.To;
            itemReponse.JobName = jobItem.JobName;
            itemReponse.TotalViewer = listData.Sum(x => x.TotalViewer);
            itemReponse.TotalApply = listData.Sum(x => x.TotalApply);
            itemReponse.StatusText = jobItem.StatusText;
            itemReponse.Status = jobItem.StatusCode;

            itemReponse.Data = listData;
            return itemReponse;
        }
    }
}
