using AgainUWP.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgainUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalInformation : Page
    {
        private static string API_MEMBER_INFOR_URL = "https://1-dot-backup-server-002.appspot.com/member/infor";
        public PersonalInformation()
        {
            this.InitializeComponent();
            LoadMemberInformation();
        }

        private async void LoadMemberInformation() {
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(API_MEMBER_INFOR_URL + "?id=" + SplitView.currentMemberId);
            Member member = JsonConvert.DeserializeObject<Member>(response);
            this.Name.Text =  member.firtsName + " " + member.lastName;

            Uri uri = new Uri(member.avatar, UriKind.Absolute);
            BitmapImage bmImage = new BitmapImage(uri);
            this.Avatar.Source = bmImage;
            Debug.WriteLine(response);
        }
    }
}
