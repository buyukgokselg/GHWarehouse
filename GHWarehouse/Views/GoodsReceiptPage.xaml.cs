using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace GHWarehouseApp.Views
{
    public partial class GoodsReceiptPage : ContentPage
    {
        private bool _isMarqueeRunning = false;

        public GoodsReceiptPage()
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
                switch (param)
                {
                    //case "CriticalStock":
                    //    await Shell.Current.GoToAsync("dashcriticalpage");
                    //    break;
                    //case "PendingOrders":
                    //    await Shell.Current.GoToAsync("dashpandingpage");
                    //    break;
                    //case "WarehouseReports":
                    //    await Shell.Current.GoToAsync("dashwarehouserep");
                    //    break;
                    //case "DailyPerformance":
                    //    await Shell.Current.GoToAsync("dashdailyrep");
                    //    break;
                    //case "Orders":
                    //    await Shell.Current.GoToAsync("orderspage");
                    //    break;
                    //case "GoodsReceipt":
                    //    await Shell.Current.GoToAsync("goodsrecpage");
                    //    break;
                    //case "Returns":
                    //    await Shell.Current.GoToAsync("returnspage");
                    //    break;
                    //case "ShippingOperations":
                    //    await Shell.Current.GoToAsync("shippingpage");
                    //    break;
                    //case "CycleCountPlanning":
                    //    await Shell.Current.GoToAsync("countpage");
                    //    break;
                    case "PendingGoodsReceipt":
                        await Shell.Current.GoToAsync("pendinggoodsreceipt");
                        break;
                    default:
                        await DisplayAlert("Hata", "Bilinmeyen iþlem", "Tamam");
                        break;
                }
            }
        }
    }
}
