﻿using TopMass.Core.Result;

namespace Topmass.Recruiter.Bussiness
{

    public class LoginResult : BaseResult
    {
        public string Token { get; set; }

        public int AuthenLevel { get; set; }
        public LoginResult()
        {


        }
    }
}
