using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tama
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavPage : ContentPage, INotifyPropertyChanged
    {
        public Creature pageCreature;
        public string happinesstostring;

        NotificationRequest notification = new NotificationRequest
        {
            Title = "Fret-otchi!",
            Description = "Help ik ben hardstikke ongelukkig :("
        };

        public NavPage()
        {
            BindingContext = this;

            InitializeComponent();
            pageCreature = DependencyService.Get<Creature>();
            pageCreature.PropertyChanged += PageCreature_PropertyChanged;
        }

        private void PageCreature_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(pageCreature.CalculateOverallHappiness < 2.5f && pageCreature.CalculateOverallHappiness > 2.4f)
            {
                LocalNotificationCenter.Current.Show(notification);
            }
            happinesstostring = pageCreature.CalculateOverallHappiness.ToString();
            UpdateUI();
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                BackgroundImage.Source = pageCreature.BGImageName;
                HappinessBar.Progress = pageCreature.CalculateOverallHappiness / 6;
                HappinessBar.ProgressColor = Color.FromHex(pageCreature.StatColor(pageCreature.CalculateOverallHappiness / 6));
            });
        }

        private void Hunger_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HungerPage());
        }
        private void Thirst_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ThirstPage());
        }
        private void Boredom_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BoredomPage());
        }
        private void Loneliness_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LonelinessPage());
        }
        private void Stimulated_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StimulatedPage());
        }
        private void Sleep_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SleepPage());
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            pageCreature.ResetCreature();
        }
    }
}