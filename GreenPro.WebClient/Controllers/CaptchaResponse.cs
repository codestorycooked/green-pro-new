using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
namespace GreenPro.WebClient.Controllers
{
    public class CaptchaResponse
    {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

        public bool ValidateCaptcha(string response)
        {
            const string secret = "6LeLSwcTAAAAAEMgHBZddE7xUwm_EkE2uUPIh";
            var client = new System.Net.WebClient();
            var reply =
               client.DownloadString(
                   string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            if (captchaResponse.Success)
            {
                return true;
            }
            else
                return false;
        }
    }
}
