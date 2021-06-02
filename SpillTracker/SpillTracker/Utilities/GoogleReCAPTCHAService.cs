using Newtonsoft.Json;
using SpillTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpillTracker.Utilities
{
    public class GoogleReCAPTCHAService
    {
        public static GoogleReCAPTCHAResponse VerifyToken(string token, string secret)
        {
            string url = $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                string jsonResult = null;
                // TODO: You should handle exceptions here
                using (WebResponse response = request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    jsonResult = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                }

                GoogleReCAPTCHAResponse captchaRespnse = JsonConvert.DeserializeObject<GoogleReCAPTCHAResponse>(jsonResult);

                return captchaRespnse;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                return null;
            }
        }
    }
}

