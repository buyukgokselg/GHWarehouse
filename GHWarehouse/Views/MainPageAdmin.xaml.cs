using Microsoft.Maui.Controls;
using System.Linq;
using System.Threading.Tasks;


namespace GHWarehouseApp.Views
{
    public partial class MainPageAdmin : ContentPage
    {
        public MainPageAdmin()
        {
            InitializeComponent();
        }

        private async void OnBorderTapped(object sender, EventArgs e)
        {
            Border border = null;
            TapGestureRecognizer tapGesture = null;

            // Gönderilen sender, Border mý yoksa direkt TapGestureRecognizer mý kontrol edelim
            if (sender is Border b)
            {
                border = b;
                tapGesture = b.GestureRecognizers.OfType<TapGestureRecognizer>().FirstOrDefault();
            }
            else if (sender is TapGestureRecognizer tap)
            {
                tapGesture = tap;
                // TapGestureRecognizer’ýn Parent’ýný Border olarak almaya çalýþalým
                border = tap.Parent as Border;
            }

            // Eðer Border bulunamadýysa, devam etmiyoruz
            if (border == null)
                return;

            // CommandParameter deðerini alýyoruz
            string commandParam = tapGesture?.CommandParameter?.ToString();

            // Týklama animasyonu
            var originalStroke = border.Stroke;
            border.Stroke = new SolidColorBrush(Colors.White);
            border.Opacity = 0.8;
            await border.FadeTo(1, 100);
            await Task.Delay(200);
            border.Stroke = originalStroke;
            border.Opacity = 1;

            // Navigasyon iþlemleri: CommandParameter'a göre yönlendirme
            if (commandParam == null)
            {
                await DisplayAlert("Hata", "Bilinmeyen iþlem", "Tamam");
                return;
            }

            switch (commandParam)
            {
                case "CriticalStock":
                    await Shell.Current.GoToAsync("dashcriticalpage");
                    break;
                case "PendingOrders":
                    await Shell.Current.GoToAsync("dashpandingpage");
                    break;
                case "WarehouseReports":
                    await Shell.Current.GoToAsync("dashwarehouserep");
                    break;
                case "DailyPerformance":
                    await Shell.Current.GoToAsync("dashdailyrep");
                    break;
                case "Orders":
                    await Shell.Current.GoToAsync("orderspage");
                    break;
                case "GoodsReceipt":
                    await Shell.Current.GoToAsync("goodsrecpage");
                    break;
                case "Returns":
                    await Shell.Current.GoToAsync("returnspage");
                    break;
                case "ShippingOperations":
                    await Shell.Current.GoToAsync("shippingpage");
                    break;
                case "CycleCountPlanning":
                    await Shell.Current.GoToAsync("countpage");
                    break;
                case "StockOperations":
                    await Shell.Current.GoToAsync("stockpage");
                    break;
                default:
                    await DisplayAlert("Hata", "Bilinmeyen iþlem", "Tamam");
                    break;
            }
        }


        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Çýkýþ", "Oturumu kapatmak istediðinize emin misiniz?", "Evet", "Hayýr");
            if (confirm)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("settingspage");
        }
    }
}
