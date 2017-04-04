using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using System.Drawing;
using RestSharp.Authenticators;
using System.Threading;
using System.Diagnostics;

namespace TwitchAlertsCSharpAPI
{
    /// <summary>
    /// Represents an application
    /// </summary>
    public class TwitchAlertsAPI
    {
        /// <summary>
        /// Unix Epoch date in LOCAL time
        /// </summary>
        public static readonly DateTime LocalZeroUnixTime = FromUnixTime(0);

        /// <summary>
        /// Twitch Alerts API Uri
        /// </summary>
        public static readonly Uri RESTUri = new Uri("https://streamlabs.com/api/v1.0/");
        
        public string ClientID;
        public string ClientSecret;
        public Uri RedirectUri;
        public TwitchScope AvailableScope;
        public string AccessToken;
        public string RefreshToken;

        RestClient Client;

        public TwitchAlertsAPI(string clientid, string clientsecret, string redirecturi, TwitchScope scope)
        {
            ClientID = clientid;
            ClientSecret = clientsecret;
            RedirectUri = new Uri(redirecturi);
            AvailableScope = scope;
            Client = new RestClient();
        }

        public string GetAuthorizeString()
        {
            var request = new RestRequest();
            request.AddParameter("response_type", "code");
            request.AddParameter("client_id", ClientID);
            request.AddParameter("redirect_uri", RedirectUri.OriginalString);
            string scope = string.Empty;
            if (AvailableScope.HasFlag(TwitchScope.AlertsCreate))
            {
                if (scope != string.Empty) scope += " ";
                scope += "alerts.create";
            }
            if (AvailableScope.HasFlag(TwitchScope.DonationsCreate))
            {
                if (scope != string.Empty) scope += " ";
                scope += "donations.create";
            }
            if (AvailableScope.HasFlag(TwitchScope.DonationsRead))
            {
                if (scope != string.Empty) scope += " ";
                scope += "donations.read";
            }
            if (AvailableScope.HasFlag(TwitchScope.LegacyToken))
            {
                if (scope != string.Empty) scope += " ";
                scope += "legacy.token";
            }
            request.AddParameter("scope", scope);
            Client.BaseUrl = GetSpecificUri("authorize?");
            return Client.BuildUri(request).OriginalString;
        }

        /// <summary>
        /// Sets the <see cref="AccessToken"/>, use <see cref="GetAuthorizeString"/>
        /// </summary>
        /// <param name="authcode">Authcode from <see cref="GetAuthorizeString"/></param>
        /// <returns></returns>
        public TokenObject GetAccessToken(string authcode)
        {
            Client.BaseUrl = GetSpecificUri("token");
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri.OriginalString },
                { "code", authcode },
            };
            request.AddBody(serialize);
            
            TokenObject obj = Execute<TokenObject>(request);
            AccessToken = obj.AccessToken;
            RefreshToken = obj.RefreshToken;
            return obj;
        }

        public async Task<TokenObject> GetAccessTokenAsync(string authcode)
        {
            Client.BaseUrl = GetSpecificUri("token");
            var request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri.OriginalString },
                { "code", authcode },
            };
            request.AddBody(serialize);

            TokenObject token = (await ExecuteAsync<TokenObject>(request)).Data;
            AccessToken = token.AccessToken;
            RefreshToken = token.RefreshToken;
            return token;
        }

        /// <summary>
        /// Refreshes the <see cref="AccessToken"/>
        /// </summary>
        /// <returns></returns>
        public TokenObject RefreshAccessToken()
        {
            Client.BaseUrl = GetSpecificUri("token");
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                { "grant_type", "refresh_token" },
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri.OriginalString },
                { "refresh_token", RefreshToken },
            };
            request.AddBody(serialize);
            TokenObject obj = Execute<TokenObject>(request);
            AccessToken = obj.AccessToken;
            RefreshToken = obj.RefreshToken;
            return obj;
        }

        /// <summary>
        /// Refreshes the <see cref="AccessToken"/>
        /// </summary>
        /// <returns></returns>
        public async Task<TokenObject> RefreshAccessTokenAsync()
        {
            Client.BaseUrl = GetSpecificUri("token");
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                { "grant_type", "refresh_token" },
                { "client_id", ClientID },
                { "client_secret", ClientSecret },
                { "redirect_uri", RedirectUri.OriginalString },
                { "refresh_token", RefreshToken },
            };
            request.AddBody(serialize);
            IRestResponse<TokenObject> obj = await ExecuteAsync<TokenObject>(request);
            AccessToken = obj.Data.AccessToken;
            RefreshToken = obj.Data.RefreshToken;
            return obj.Data;
        }

        /// <summary>
        /// Get the user info
        /// </summary>
        /// <returns></returns>
        public AuthenticatedUserObject GetUserInfo()
        {
            if (string.IsNullOrEmpty(AccessToken)) return default(AuthenticatedUserObject);
            Client.BaseUrl = GetSpecificUri("user?");
            RestRequest request = new RestRequest();
            request.AddParameter("access_token", AccessToken);

            return Execute<AuthenticatedUserObject>(request);
        }

        /// <summary>
        /// Get the user info async
        /// </summary>
        /// <returns></returns>
        public async Task<AuthenticatedUserObject> GetUserInfoAsync()
        {
            if (string.IsNullOrEmpty(AccessToken)) return default(AuthenticatedUserObject);
            Client.BaseUrl = GetSpecificUri("user?");
            RestRequest request = new RestRequest();
            request.AddParameter("access_token", AccessToken);

            return (await ExecuteAsync<AuthenticatedUserObject>(request)).Data;
        }

        public List<DonationObject> GetAllDonations(CurrencyCode? currency = CurrencyCode.USD, int verified = -1)
        {
            List<DonationObject> list = new List<DonationObject>();
            List<DonationObject> curlist = new List<DonationObject>();
            int id = 0;
            while ((curlist = GetDonation(limit: 100, currency: currency, verified: verified, before: id)).Count != 0)
            { list = list.Concat(curlist).ToList(); id = Convert.ToInt32(list[list.Count - 1].DonationID); }
            return list;
        }

        public async Task<List<DonationObject>> GetAllDonationsAsync(CurrencyCode? currency = CurrencyCode.USD, int verified = -1)
        {
            List<DonationObject> list = new List<DonationObject>();
            List<DonationObject> curlist = new List<DonationObject>();
            int id = 0;
            while ((curlist = await GetDonationAsync(limit: 100, currency: currency, verified: verified, before: id)).Count != 0)
            { list = list.Concat(curlist).ToList(); id = Convert.ToInt32(list[list.Count - 1].DonationID); }
            return list;
        }

        /// <summary>
        /// Get donation information
        /// </summary>
        /// <param name="limit">How many results to return</param>
        /// <param name="currency">The currency to use, null if you want the currency the donation was paid in</param>
        /// <param name="before">Before an ID</param>
        /// <param name="after">after an ID</param>
        /// <param name="verified">wheather to include verified sources of donations, 0 for added, 1 for verified, -1 for both</param>
        /// <returns></returns>
        public List<DonationObject> GetDonation(int? limit = 50, CurrencyCode? currency = CurrencyCode.USD, int before = 0, int after = 0, int verified = -1)
        {
            if (string.IsNullOrEmpty(AccessToken)) return default(List<DonationObject>);
            Client.BaseUrl = GetSpecificUri("donations");
            RestRequest request = new RestRequest();
            request.AddParameter("access_token", AccessToken);
            if (limit != null && limit <= 100) request.AddParameter("limit", limit.ToString());
            if (currency != null) request.AddParameter("currency", currency.ToString());
            if (before > 0) request.AddParameter("before", before.ToString());
            if (after > 0) request.AddParameter("after", after.ToString());
            if (verified > -1) request.AddParameter("verified", verified.ToString());

            return Execute<DonationListObject>(request).List;
        }


        /// <summary>
        /// Get donation information
        /// </summary>
        /// <param name="limit">How many results to return</param>
        /// <param name="currency">The currency to use, null if you want the currency the donation was paid in</param>
        /// <param name="before">Before an ID</param>
        /// <param name="after">after an ID</param>
        /// <param name="verified">wheather to include verified sources of donations, 0 for added, 1 for verified, -1 for both</param>
        /// <returns></returns>
        public async Task<List<DonationObject>> GetDonationAsync(int? limit = null, CurrencyCode? currency = null, int before = 0, int after = 0, int verified = -1)
        {
            if (string.IsNullOrEmpty(AccessToken)) return default(List<DonationObject>);
            Client.BaseUrl = GetSpecificUri("donations");
            RestRequest request = new RestRequest();
            request.AddParameter("access_token", AccessToken);
            if (limit != null && limit < 100) request.AddParameter("limit", limit.ToString());
            if (currency != null) request.AddParameter("currency", currency.ToString());
            if (before > 0) request.AddParameter("before", before.ToString());
            if (after > 0) request.AddParameter("after", after.ToString());
            if (verified > -1) request.AddParameter("verified", verified.ToString());

            return (await ExecuteAsync<DonationListObject>(request)).Data.List;
        }
        
        /// <summary>
        /// Creates a donation
        /// </summary>
        /// <param name="name">The name of the donation</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="donationamount">The amount</param>
        /// <param name="currency">The currency type</param>
        /// <param name="message">The message</param>
        /// <param name="datetime">The time it was created</param>
        /// <returns>The created donation ID</returns>
        public int CreateDonation(string name, string identifier, double donationamount, CurrencyCode currency, string message = "", DateTime datetime = default(DateTime))
        {
            if (string.IsNullOrEmpty(AccessToken)) return 0;
            Client.BaseUrl = GetSpecificUri("donations");
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                { "access_token", AccessToken },
                { "name", name },
                { "identifier", identifier },
                { "amount", donationamount.ToString() },
                { "currency", currency.ToString() },
            };
            if (message != "") serialize.Add("message", message);
            if (datetime != default(DateTime)) serialize.Add("datetime", ToUnixTime(datetime).ToString());
            request.AddBody(serialize);
            
            return Execute<DonationIDObject>(request).DonationID;
        }

        /// <summary>
        /// Creates a donation async
        /// </summary>
        /// <param name="name">The name of the donation</param>
        /// <param name="identifier">The identifier</param>
        /// <param name="donationamount">The amount</param>
        /// <param name="currency">The currency type</param>
        /// <param name="message">The message</param>
        /// <param name="datetime">The time it was created</param>
        /// <returns>The created donation ID</returns>
        public async Task<int> CreateDonationAsync(string name, string identifier, double donationamount, CurrencyCode currency, string message = "", DateTime datetime = default(DateTime))
        {
            if (string.IsNullOrEmpty(AccessToken)) return 0;
            Client.BaseUrl = GetSpecificUri("donations");
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                { "access_token", AccessToken },
                { "name", name },
                { "identifier", identifier },
                { "amount", donationamount.ToString() },
                { "currency", currency.ToString() },
            };
            if (message != "") serialize.Add("message", message);
            string str = datetime.ToString("MM/dd/yyyy h:mm t");
            Debug.Print(str);
            if (datetime != default(DateTime)) serialize.Add("created_at", str); // MM/DD/YYYY h:mm A format is wrong
            request.AddBody(serialize);

            return (await ExecuteAsync<DonationIDObject>(request)).Data.DonationID;
        }

        /// <summary>
        /// Creates an alert
        /// </summary>
        /// <param name="type">The type of alert</param>
        /// <param name="imagehref">The image source</param>
        /// <param name="soundhref">The sound source</param>
        /// <param name="message">The message</param>
        /// <param name="duration">The duration</param>
        /// <param name="color">The color</param>
        /// <returns>A bool wheather it was successful or not</returns>
        public bool CreateAlert(AlertType type, string imagehref = "", string soundhref = "", string message = "", TimeSpan duration = default(TimeSpan), Color color = default(Color))
        {
            if (string.IsNullOrEmpty(AccessToken)) return false;
            Client.BaseUrl = GetSpecificUri("alerts");
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                {"access_token", AccessToken },
                {"type", type.ToString() },
            };
            if (imagehref != "") serialize.Add("image_href", imagehref);
            if (soundhref != "") serialize.Add("sound_href", soundhref);
            if (message != "") serialize.Add("message", message);
            Debug.WriteLine(duration.Seconds.ToString());
            if (duration != default(TimeSpan)) serialize.Add("duration", duration.Seconds.ToString());
            if (color != default(Color)) serialize.Add("special_text_color", ColorTranslator.ToHtml(color));
            request.AddBody(serialize);

            return Execute<SuccessObject>(request).Success;
        }

        /// <summary>
        /// Creates an alert async
        /// </summary>
        /// <param name="type">The type of alert</param>
        /// <param name="imagehref">The image source</param>
        /// <param name="soundhref">The sound source</param>
        /// <param name="message">The message</param>
        /// <param name="duration">The duration</param>
        /// <param name="color">The color</param>
        /// <returns>A bool wheather it was successful or not</returns>
        public async Task<bool> CreateAlertAsync(AlertType type, string imagehref = "", string soundhref = "", string message = "", TimeSpan duration = default(TimeSpan), Color color = default(Color))
        {
            if (string.IsNullOrEmpty(AccessToken)) return false;
            Client.BaseUrl = GetSpecificUri("alerts");
            RestRequest request = new RestRequest();
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            var serialize = new Dictionary<string, string>()
            {
                {"access_token", AccessToken },
                {"type", type.ToString() },
            };
            if (imagehref != "") serialize.Add("image_href", imagehref);
            if (soundhref != "") serialize.Add("sound_href", soundhref);
            if (message != "") serialize.Add("message", message);
            if (duration != default(TimeSpan)) serialize.Add("duration", duration.Seconds.ToString());
            if (color != default(Color)) serialize.Add("special_text_color", ColorTranslator.ToHtml(color));
            request.AddBody(serialize);

            return (await ExecuteAsync<SuccessObject>(request)).Data.Success;
        }

        public string GetLegacyToken()
        {
            if (string.IsNullOrEmpty(AccessToken)) return "";
            Client.BaseUrl = GetSpecificUri("legacy/token");
            RestRequest request = new RestRequest();
            request.AddParameter("access_token", AccessToken);

            return Execute<LegacyTokenObject>(request).Token;
        }

        public async Task<string> GetLegacyTokenAsync()
        {
            if (string.IsNullOrEmpty(AccessToken)) return "";
            Client.BaseUrl = GetSpecificUri("legacy/token");
            RestRequest request = new RestRequest();
            request.AddParameter("access_token", AccessToken);

            return (await ExecuteAsync<LegacyTokenObject>(request)).Data.Token;
        }

        T Execute<T>(RestRequest request) where T : new()
        {
            var response = Client.Execute<T>(request);

            Check(response);
            return response.Data;
        }

        async Task<IRestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var CancellationToken = new CancellationTokenSource();
            var response = await Client.ExecuteTaskAsync<T>(request, CancellationToken.Token);

            Check(response);
            return response;
        }

        string ExecuteAsString(RestRequest request)
        {
            var response = Client.Execute(request);

            Check(response);
            return response.Content;
        }

        void Check(IRestResponse response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK || response.ErrorException != null)
            {
                throw new TwitchAlertsException(response.StatusCode.ToString(), response.Content, response.Request, response.ErrorException);
            }
        }

        Uri GetSpecificUri(string section)
        {
            return new Uri(RESTUri.OriginalString + section);
        }

        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTime(epoch.AddSeconds(unixTime), TimeZoneInfo.Local);
        }

        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }

        public static string CurrencyCodeToString(CurrencyCode code)
        {
            switch (code)
            {
                case CurrencyCode.AUD:
                    return "Austrailian Dollar";
                case CurrencyCode.BRL:
                    return "Brazilian Real";
                case CurrencyCode.CAD:
                    return "Canadian Dollar";
                case CurrencyCode.CZK:
                    return "Czech Koruna";
                case CurrencyCode.DKK:
                    return "Danish Krone";
                case CurrencyCode.EUR:
                    return "Euro";
                case CurrencyCode.HKD:
                    return "Hong Kong Dollar";
                case CurrencyCode.ILS:
                    return "Israeli New Sheqel";
                case CurrencyCode.MYR:
                    return "Malaysian Ringgit";
                case CurrencyCode.MXN:
                    return "Mexican Peso";
                case CurrencyCode.NOK:
                    return "Norwegian Krone";
                case CurrencyCode.NZD:
                    return "New Zealand Dollar";
                case CurrencyCode.PHP:
                    return "Philippine Peso";
                case CurrencyCode.PLN:
                    return "Polish Zloty";
                case CurrencyCode.GBP:
                    return "Pound Sterling";
                case CurrencyCode.RUB:
                    return "Russian Ruble";
                case CurrencyCode.SGD:
                    return "Singapore Dollar";
                case CurrencyCode.SEK:
                    return "Swedish Krona";
                case CurrencyCode.CHF:
                    return "Swiss Franc";
                case CurrencyCode.THB:
                    return "Thai Baht";
                case CurrencyCode.TRY:
                    return "Turkish Lira";
                case CurrencyCode.USD:
                    return "US Dollar";
                default:
                    return string.Empty;
            }
        }
    }

    /// <summary>
    /// Various scopes of accessibility
    /// </summary>
    [Flags]
    public enum TwitchScope
    {
        DonationsRead = 1,
        DonationsCreate = 2,
        AlertsCreate = 4,
        LegacyToken = 8
    }


    public class TokenObject
    {
        [DeserializeAs(Name = "access_token")]
        public string AccessToken { get; set; }

        [DeserializeAs(Name = "token_type")]
        public string TokenType { get; set; }

        [DeserializeAs(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }

    /// <summary>
    /// A fill class -- ignore
    /// </summary>
    public class TwitchObject
    {
        [DeserializeAs(Name = "id")]
        public string ID { get; set; }

        [DeserializeAs(Name = "display_name")]
        public string DisplayName { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// A fill class -- ignore
    /// </summary>
    public class YoutubeObject
    {
        [DeserializeAs(Name = "id")]
        public string ID { get; set; }

        [DeserializeAs(Name = "title")]
        public string Title { get; set; }
    }

    /// <summary>
    /// An object gained from <see cref="TwitchAlertsAPI.GetUserInfo"/>
    /// </summary>
    /// <remarks>One of the Objects inside this class is going to be null, you will have to manually check</remarks>
    public class AuthenticatedUserObject
    {
        [DeserializeAs(Name = "twitch")]
        public TwitchObject Twitch { get; set; }

        [DeserializeAs(Name = "youtube")]
        public YoutubeObject Youtube { get; set; }
    }

    /// <summary>
    /// A donation
    /// </summary>
    public class DonationObject
    {
        [DeserializeAs(Name = "donation_id")]
        public string DonationID { get; set; }

        [DeserializeAs(Name = "created_at")]
        public string CreatedAtUnix { get; set; }

        [DeserializeAs(Name = "currency")]
        public string Currency { get; set; }

        [DeserializeAs(Name = "amount")]
        public string Amount { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "message")]
        public string Message { get; set; }

        public DateTime CreatedAt
        {
            get
            {
                return TwitchAlertsAPI.FromUnixTime(Convert.ToInt64(CreatedAtUnix));
            }
        }
    }

    /// <summary>
    /// A returned DonationListObject
    /// </summary>
    public class DonationListObject
    {
        [DeserializeAs(Name = "data")]
        public List<DonationObject> List { get; set; }
    }

    public class DonationIDObject
    {
        [DeserializeAs(Name = "donation_id")]
        public int DonationID { get; set; }
    }

    public class SuccessObject
    {
        [DeserializeAs(Name = "success")]
        public bool Success { get; set; }
    }

    public class LegacyTokenObject
    {
        [DeserializeAs(Name = "token")]
        public string Token { get; set; }
    }

    public class ErrorObject
    {
        [DeserializeAs(Name = "error")]
        public string Error { get; set; }

        [DeserializeAs(Name = "error_description")]
        public string Message { get; set; }

        [DeserializeAs(Name = "message")]
        public string Message2 { get; set; }
    }
    
    public enum CurrencyCode
    {
        /// <summary>
        /// Austrailian Dollar
        /// </summary>
        AUD,

        /// <summary>
        /// Brazilian Real
        /// </summary>
        BRL,

        /// <summary>
        /// Canadian Dollar
        /// </summary>
        CAD,

        /// <summary>
        /// Czech Koruna
        /// </summary>
        CZK,

        /// <summary>
        /// Danish Krone
        /// </summary>
        DKK,

        /// <summary>
        /// Euro
        /// </summary>
        EUR,

        /// <summary>
        /// Hong Kong Dollar
        /// </summary>
        HKD,

        /// <summary>
        /// Israeli New Sheqel
        /// </summary>
        ILS,

        /// <summary>
        /// Malaysian Ringgit
        /// </summary>
        MYR,

        /// <summary>
        /// Mexican Peso
        /// </summary>
        MXN,

        /// <summary>
        /// Norwegian Krone
        /// </summary>
        NOK,

        /// <summary>
        /// New Zealand Dollar
        /// </summary>
        NZD,

        /// <summary>
        /// Philippine Peso
        /// </summary>
        PHP,

        /// <summary>
        /// Polish Zloty
        /// </summary>
        PLN,

        /// <summary>
        /// Pound Sterling
        /// </summary>
        GBP,

        /// <summary>
        /// Russian Ruble
        /// </summary>
        RUB,

        /// <summary>
        /// Singapore Dollar
        /// </summary>
        SGD,

        /// <summary>
        /// Swedish Krona
        /// </summary>
        SEK,

        /// <summary>
        /// Swiss Franc
        /// </summary>
        CHF,

        /// <summary>
        /// Thai Baht
        /// </summary>
        THB,

        /// <summary>
        /// Turkish Lira
        /// </summary>
        TRY,

        /// <summary>
        /// US Dollar
        /// </summary>
        USD
    }

    public enum AlertType
    {
        Follow,
        Subscription,
        Donation,
        Host
    }

    /// <summary>
    /// To distinguish Twitch Alert REST errors
    /// </summary>
    public class TwitchAlertsException : Exception
    {
        /// <summary>
        /// The content returned from TwitchAlertsAPI
        /// </summary>
        public string RESTResult;

        /// <summary>
        /// The error returned from TwitchAlertsAPI
        /// </summary>
        public string RESTError;

        /// <summary>
        /// The message returned from TwitchAlertsAPI
        /// </summary>
        public string RESTMessage;

        /// <summary>
        /// The request attempted
        /// </summary>
        public IRestRequest RESTRequest;

        public TwitchAlertsException(string msg, string restresult, IRestRequest restrequest, Exception innerException) : base (msg, innerException)
        {
            RESTResult = restresult;
            RESTRequest = restrequest;
            JsonDeserializer de = new JsonDeserializer();
            var obj = de.Deserialize<ErrorObject>(new RestResponse { Content = RESTResult });
            RESTError = obj.Error;
            RESTMessage = obj.Message ?? obj.Message2;
        }
    }
}
