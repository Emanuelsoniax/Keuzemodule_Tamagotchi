using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace Tama
{
    public partial class App : Application
    {
        readonly Timer timer = new Timer();
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());

            if (Preferences.Get("creatureJson", "") == null)
            {
                DependencyService.RegisterSingleton(new Creature());
            }
            else
            {
                string creatureAsString = Preferences.Get("creatureJson", "");
                Creature creature = JsonConvert.DeserializeObject<Creature>(creatureAsString);
                DependencyService.RegisterSingleton(creature);
            }

            timer.Interval = 1000.0;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
        }

        protected override void OnStart()
        {
            timer.Start();
        }

        protected override void OnSleep()
        {
            var sleepTime = DateTime.UtcNow;
            Preferences.Set("Sleeptime", sleepTime);

            string creatureAsString = JsonConvert.SerializeObject(DependencyService.Get<Creature>());
            Preferences.Set("creatureJson", creatureAsString);
        }

        protected override void OnResume()
        {
            var sleepTime = Preferences.Get("Sleeptime", DateTime.UtcNow);
            var timePassed = DateTime.UtcNow - sleepTime;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DependencyService.Get<Creature>().LowerStats();
        }
    }
}
