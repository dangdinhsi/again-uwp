using AgainUWP.DataAccessLibrary;
using AgainUWP.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgainUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PivotDemoWithListView : Page
    {
        private bool _isPlaying = false;

        private int _currentIndex = 0;

        private ObservableCollection<Song> listSong;

        internal ObservableCollection<Song> ListSong { get => listSong; set => listSong = value; }

        public PivotDemoWithListView()
        {
            this.ListSong = new ObservableCollection<Song>();
            this.ListSong.Add(new Song()
            {
                name = "Chưa bao giờ",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://file.tinnhac.com/resize/600x-/music/2017/07/04/19554480101556946929-b89c.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui963/ChuaBaoGioSEESINGSHARE2-HaAnhTuan-5111026.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Tình thôi xót xa",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/XyjhXzsVdiI/maxresdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui963/TinhThoiXotXaSEESINGSHARE1-HaAnhTuan-4652191.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Tháng tư là tháng nói dối của em",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://sky.vn/wp-content/uploads/2018/05/0-30.jpg",
                link = "https://od.lk/s/NjFfMjM4MzQ1OThf/ThangTuLaLoiNoiDoiCuaEm-HaAnhTuan-4609544.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Nơi ấy bình yên",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/A8u_fOetSQc/hqdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui946/NoiAyBinhYenSeeSingShare2-HaAnhTuan-5085337.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Giấc mơ chỉ là giấc mơ",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/J_VuNwxSEi0/maxresdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui945/GiacMoChiLaGiacMoSeeSingShare2-HaAnhTuan-5082049.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Người tình mùa đông",
                singer = "Hà Anh Tuấn",
                thumbnail = "https://i.ytimg.com/vi/EXAmxBxpZEM/maxresdefault.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui963/NguoiTinhMuaDongSEESINGSHARE2-HaAnhTuan-5104816.mp3"
            }); ;
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Song song = new Song()
            {
                name = this.SongName.Text,
                thumbnail = this.SongThumbnail.Text
            };
            this.ListSong.Add(song);
            this.SongName.Text = string.Empty;
            this.SongThumbnail.Text = string.Empty;
            SongModel.Add(song);
        }

        private void Choosed_Song(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            Song choosed = panel.Tag as Song;
            _currentIndex = this.MyListSong.SelectedIndex;
            Uri mp3Link = new Uri(choosed.link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.ListSong[_currentIndex].name + " - " + this.ListSong[_currentIndex].singer;
            Play_Song();
        }

        private void Play_Song() {
            _isPlaying = true;
            this.Player_Status.Text = "Now Playing: ";
            this.MyPlayer.Play();
            this.Play_Button.Symbol = Symbol.Pause;
        }

        private void Pause_Song()
        {
            _isPlaying = false;
            this.Player_Status.Text = "Paused: ";
            this.MyPlayer.Pause();
            this.Play_Button.Symbol = Symbol.Play;
        }

        private void Click_Play(object sender, RoutedEventArgs e)
        {
            if (_isPlaying){
                Pause_Song();
            }
            else {                
                Play_Song();
            }
        }

        private void Do_Next(object sender, RoutedEventArgs e)
        {
            Pause_Song();
            _currentIndex += 1;
            if (_currentIndex >= this.ListSong.Count)
            {
                _currentIndex = 0;
            }
            Uri mp3Link = new Uri(this.ListSong[_currentIndex].link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.ListSong[_currentIndex].name + " - " + this.ListSong[_currentIndex].singer;
            this.MyListSong.SelectedIndex = _currentIndex;
            Play_Song();
        }

        private void Do_Previous(object sender, RoutedEventArgs e)
        {
            Pause_Song();
            _currentIndex -= 1;
            if (_currentIndex < 0)
            {
                _currentIndex = this.ListSong.Count - 1;
            }
            Uri mp3Link = new Uri(this.ListSong[_currentIndex].link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.ListSong[_currentIndex].name + " - " + this.ListSong[_currentIndex].singer;
            this.MyListSong.SelectedIndex = _currentIndex;
            Play_Song();
        }
    }
}
