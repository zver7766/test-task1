using System;
using System.Net;

namespace Infrastructure.Data
{
    public static class UrlValidator
    {
        public static bool ValidateUrl(string path)
        {
            var isValid = false;
            try
            {
                Uri uri = new Uri(path);
                var request = (HttpWebRequest) WebRequest.Create(uri);
                request.Timeout = 3000;
                HttpWebResponse response;
                response = (HttpWebResponse) request.GetResponse();
                if (response.StatusCode == (HttpStatusCode) 200)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
