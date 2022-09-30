using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tama
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void StartButton_Pressed(object sender, EventArgs e)
        {
            ScaleButton();
            Navigation.PushAsync(new NavPage());
        }

        private async void ScaleButton()
        {
            await start.ScaleTo(1.3, 100);
            await start.ScaleTo(1, 100);
        }
    }
}
