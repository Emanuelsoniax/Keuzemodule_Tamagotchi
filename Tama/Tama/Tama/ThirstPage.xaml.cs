﻿using System;
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
    public partial class ThirstPage : ContentPage
    {
        public Creature pageCreature;

        public ThirstPage()
        {
            InitializeComponent();

            pageCreature = DependencyService.Get<Creature>();
            pageCreature.PropertyChanged += PageCreature_PropertyChanged;

            AnimateResource();
        }

        private void PageCreature_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                BackgroundImage.Source = pageCreature.BGImageName;
                ProgressBar.Progress = pageCreature.Thirst;
                ProgressBar.ProgressColor = Color.FromHex(pageCreature.StatColor(pageCreature.Thirst));
            });
        }

        private async void AnimateResource()
        {
            await Resource.TranslateTo(-300, -245, 1000);
            await Resource.TranslateTo(300, -245, 1000);
            AnimateResource();
        }

        private void Resource_Clicked(object sender, EventArgs e)
        {
            pageCreature.Thirst += 0.01f;
            ScaleButton();
        }

        private async void ScaleButton()
        {
            await Resource.ScaleTo(1.3, 100);
            await Resource.ScaleTo(1, 100);
        }
    }
}