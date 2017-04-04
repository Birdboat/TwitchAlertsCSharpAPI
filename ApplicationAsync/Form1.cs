using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchAlertsCSharpAPI;

namespace ApplicationAsync
{
    public partial class Form1 : Form
    {
        TwitchAlertsAPI Api;
        AuthenticatedUserObject userinfo;

        public Form1()
        {
            InitializeComponent();
            Api = new TwitchAlertsAPI("6nAVHJyDBzGJbvwu4bWCEXbJ9UFUvuMVvxBLlngF", "uxWMyHJjQYcZmICEYgcleQQbMe2vfou2bMbZSDt6",
                "https://www.google.com", TwitchScope.AlertsCreate | TwitchScope.DonationsCreate | TwitchScope.DonationsRead | TwitchScope.LegacyToken);

            TreeNode twitch = new TreeNode("Twitch"); twitch.Nodes.Add("ID"); twitch.Nodes.Add("DisplayName"); twitch.Nodes.Add("Name");
            TreeNode youtube = new TreeNode("Youtube"); youtube.Nodes.Add("ID"); youtube.Nodes.Add("Name");
            userinfotree.Nodes.Add(twitch); userinfotree.Nodes.Add(youtube); userinfotree.ExpandAll();

            FillComboBox<CurrencyCode>(donationcombobox);
            FillComboBox<AlertType>(alerttype);

            donationtimepicker.Value = TwitchAlertsAPI.LocalZeroUnixTime;

            colorDialog1.Color = default(Color);
        }

        void FillComboBox<T>(ComboBox combobox)
        {
            foreach (var v in Enum.GetValues(typeof(T)))
                combobox.Items.Add(v.ToString());
            combobox.SelectedIndex = 0; // force value
        }

        private async void setauthkey_Click(object sender, EventArgs e)
        {
            await Api.GetAccessTokenAsync(authCodeBox.Text);
            Debug.WriteLine(Api.AccessToken);
            accesstokenlabel.Text = "Access Token: " + Api.AccessToken;
        }

        private void accessUrlButton_Click(object sender, EventArgs e)
        {
            Process.Start(Api.GetAuthorizeString());
        }

        private async void getuserinfobutton_Click(object sender, EventArgs e)
        {
            userinfo = await Api.GetUserInfoAsync();
            if (userinfo.Twitch != null)
            {
                var node = userinfotree.Nodes[0];
                node.Nodes[0].Text = userinfo.Twitch.ID + "(ID)";
                node.Nodes[1].Text = userinfo.Twitch.DisplayName + "(DisplayName)";
                node.Nodes[2].Text = userinfo.Twitch.Name + "(Name)";
            }
            else
            {
                var node = userinfotree.Nodes[1];
                node.Nodes[0].Text = userinfo.Youtube.ID + "(ID)";
                node.Nodes[1].Text = userinfo.Youtube.Title + "(Name)";
            }
        }

        private async void createdonationbutton_Click(object sender, EventArgs e)
        {
            await Api.CreateDonationAsync(donationnametext.Text, donationidentifiertext.Text, Convert.ToDouble(donationamounttext.Text), (CurrencyCode)donationcombobox.SelectedIndex, string.IsNullOrEmpty(donationmessagetext.Text) ? "" : donationmessagetext.Text, donationtimepicker.Value != TwitchAlertsAPI.LocalZeroUnixTime ? donationtimepicker.Value : default(DateTime));
        }

        private async void getdonationsbutton_Click(object sender, EventArgs e)
        {
            (await Api.GetAllDonationsAsync()).ForEach(v =>
            {
                TreeNode donationnode = new TreeNode();
                donationnode.Nodes.Add(v.Amount);
                donationnode.Nodes.Add(v.CreatedAt.ToShortDateString());
                donationnode.Nodes.Add(v.Currency);
                donationnode.Nodes.Add(v.DonationID);
                donationnode.Nodes.Add(v.Message);
                donationnode.Nodes.Add(v.Name);
                donationnode.Text = v.DonationID;
                treeView1.Nodes.Add(donationnode);
            });
        }

        private async void createalertbutton_Click(object sender, EventArgs e)
        {
            await Api.CreateAlertAsync((AlertType)alerttype.SelectedIndex, alertimagehref.Text, alertsoundhref.Text, alertmessage.Text, alertduration.Text == string.Empty ? default(TimeSpan) : new TimeSpan(0, 0, Convert.ToInt32(alertduration.Text)), colorDialog1.Color);
        }

        private void setcolorbutton_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            getlegacytokentext.Text = await Api.GetLegacyTokenAsync();
        }
    }
}
