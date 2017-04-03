using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TwitchAlertsCSharpAPI;
using System.Diagnostics;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitchAlertsAPI Api = new TwitchAlertsAPI("6nAVHJyDBzGJbvwu4bWCEXbJ9UFUvuMVvxBLlngF", "uxWMyHJjQYcZmICEYgcleQQbMe2vfou2bMbZSDt6", 
                "https://www.google.com", Scope.DonationsRead | Scope.DonationsCreate | Scope.AlertsCreate);
            Console.WriteLine(Api.GetAuthorizeString());
            Process.Start(Api.GetAuthorizeString());
            Console.Write("Authkey: "); TokenObject obj = Api.GetAccessToken(Console.ReadLine());
            Console.WriteLine(obj.AccessToken);
            Console.WriteLine(obj.RefreshToken);
            Console.WriteLine(obj.TokenType);
            Console.ReadKey();
            AuthenticatedUserObject userobj = Api.GetUserInfo();
            if (userobj.Twitch != null)
            {
                Console.WriteLine(userobj.Twitch.ID);
                Console.WriteLine(userobj.Twitch.Name);
                Console.WriteLine(userobj.Twitch.DisplayName);
            }
            else
            {
                Console.WriteLine(userobj.Youtube.ID);
                Console.WriteLine(userobj.Youtube.Title);
            }
            Console.ReadKey();
            List<DonationObject> donationobj = Api.GetDonation(50, CurrencyCode.USD);
            foreach(var v in donationobj)
            {
                Console.WriteLine("{");
                Console.WriteLine($"Amount: {v.Amount}");
                Console.WriteLine($"Created at: {v.CreatedAtUnix}");
                Console.WriteLine($"Currency: {v.Currency}");
                Console.WriteLine($"Donation ID: {v.DonationID}");
                Console.WriteLine($"Message: {v.Message}");
                Console.WriteLine($"Name: {v.Name}");
                Console.WriteLine($"CreatedAt: {v.CreatedAt}");
                Console.WriteLine("}");
            }
            Console.ReadKey();
            Api.CreateDonation("Test", "YQQNDFEF", 13.45, CurrencyCode.USD);
            Console.ReadKey();
            Api.CreateAlert(AlertType.Follow);
            Console.ReadKey();
        }
    }
}
