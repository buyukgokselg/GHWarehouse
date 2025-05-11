using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace GHWarehouseApp.Views
{
    public partial class DashCriticalPage : ContentPage
    {
        private bool _isMarqueeRunning = false;

        public DashCriticalPage()
        {
            InitializeComponent();
            MarqueeLabel.SizeChanged += MarqueeLabel_SizeChanged;
        }

        private void MarqueeLabel_SizeChanged(object sender, EventArgs e)
        {
            if (!_isMarqueeRunning && MarqueeLabel.Width > 0 && ((Grid)MarqueeLabel.Parent).Width > 0)
            {
                MarqueeLabel.SizeChanged -= MarqueeLabel_SizeChanged;
                StartMarquee();
            }
        }

        private async void StartMarquee()
        {
            _isMarqueeRunning = true;

            double containerWidth = ((Grid)MarqueeLabel.Parent).Width;
            double labelWidth = MarqueeLabel.Width;

            while (true)
            {
                MarqueeLabel.TranslationX = containerWidth;

                await MarqueeLabel.TranslateTo(-labelWidth, 0, 10000u, Easing.Linear);

                await Task.Delay(500);
            }
        }

        private async void OnBorderTapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is string param)
            {
                await DisplayAlert("Týklama Algýlandý", $"Seçilen Kategori: {param}", "Tamam");
            }
        }
    }
}
