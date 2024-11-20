using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace washdata
{
    public class CallApi
    {
        private string Token { get; set; }
        private DataAccess dataAccess;
        private DateTime? DateExprise { get; set; }
        public CallApi()
        {
            Token = "";
            DateExprise = null;
        }

        public CreateTokenReponse CreateToken()
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                id = "vietstaru",
                password = "vietstarP@ssw0rd",

            }));

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151";
            var accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE3Mjg2MzgxOTYsImV4cCI6MTcyODY0MTc5NiwiaXNzIjoieW91cl9pc3N1ZXIiLCJkYXRhIjp7InVzZXJJZCI6InZpZXRzdGFydSIsInVzZXJOYW1lIjoidmlldHN0YXJ1In19.6bhpQIgKGY19IsXZKuCmP0kD-SReEcZDWmMwtLvNGTs";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = client.PostAsync("autocall/create_token", data).GetAwaiter().GetResult();

                if (reponse.IsSuccessStatusCode)
                {
                    var responseContent = reponse.Content;
                    var ressult = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();

                    return JsonConvert.DeserializeObject<CreateTokenReponse>(ressult);

                }


            }
            return new CreateTokenReponse() { Token = "" };

        }
        public void MakeCall(string phoneNumber, WashDataItem item)
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phone_number = phoneNumber,
                wash = true

            }));
            if (string.IsNullOrEmpty(Token) || DateTime.Now > DateExprise.Value.AddMinutes(-10))
            {
                var resultToken = CreateToken();
                Token = resultToken.Token;
                DateExprise = resultToken.ExpiresAt;
            }
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151";
            var accessToken = Token;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = client.PostAsync("autocall/make_call", data).GetAwaiter().GetResult();
                if (reponse.IsSuccessStatusCode)
                {
                    var responseContent = reponse.Content;
                    var ressult = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (dataAccess == null)
                    {

                        dataAccess = new DataAccess();
                    }
                    //dataAccess.AddLogCall(item.Phone, item.NoAgree);

                };

            }

        }


        private async Task<bool> GetStatusList(string phoneNumber, bool wash = true)
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phone_number = phoneNumber

            }));
            if (string.IsNullOrEmpty(Token) || DateTime.Now > DateExprise.Value.AddMinutes(-10))
            {
                var resultToken = CreateToken();
                Token = resultToken.Token;
                DateExprise = resultToken.ExpiresAt;
            }
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151";
            var accessToken = Token;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = await client.PostAsync("autocall/get_pjsip_status", data);
                var result = await reponse.Content.ReadAsStringAsync();
            }
            return true;
        }
    }
}
