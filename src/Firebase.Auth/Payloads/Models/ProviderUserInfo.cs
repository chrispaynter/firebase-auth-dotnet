using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.Auth.Payloads.Models
{
    public class ProviderUserInfo
    {
        public string ProviderId { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }
        public string FederatedId { get; set; }
        public string Email { get; set; }
        public string RawId { get; set; }
        public string ScreenName { get; set; }
    }
}
