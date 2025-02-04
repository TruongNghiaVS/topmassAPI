﻿using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness

{
    public partial interface IRewardBusiness
    {
        public Task<BaseResult> ExchangePointToOpenCV(int
            searchId, int point, int userId, int? campaignId = -1);
        public Task<BaseResult> ExchangePointToOpenCVNoSearchCV(int
        searchId, int point, int userId, int identify, string fileName);
        public Task<BaseResult> ExchangePointToOpenViewer(int userId, int point, int logviewerId);


    }
}
