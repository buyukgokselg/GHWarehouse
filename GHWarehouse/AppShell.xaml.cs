using GHWarehouseApp.Views;

namespace GHWarehouse
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //SetPageTitle(); 
            //this.Navigated += AppShell_Navigated;
            Routing.RegisterRoute("dashcriticalpage", typeof(DashCriticalPage));
            Routing.RegisterRoute("dashpandingpage", typeof(DashPendingPage));
            Routing.RegisterRoute("dashwarehouserep", typeof(DashWarehouseReportPage));
            Routing.RegisterRoute("dashdailyrep", typeof(DashDailyReportPage));
            Routing.RegisterRoute("orderspage", typeof(OrdersPage));
            Routing.RegisterRoute("goodsrecpage", typeof(GoodsReceiptPage));
            Routing.RegisterRoute("returnspage", typeof(ReturnsPage));
            Routing.RegisterRoute("shippingpage", typeof(ShippingPage));
            Routing.RegisterRoute("countpage", typeof(CycleCountPlanningPage));
            Routing.RegisterRoute("stockpage", typeof(StockOperationsPage));
            Routing.RegisterRoute("returnrequest", typeof(ReturnRequestPage));
            Routing.RegisterRoute("pendinggoodsreceipt", typeof(PendingGoodsReceiptPage));
        }

      
        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
        
            await DisplayAlert("Çıkış", "Oturum sonlandırılıyor...", "Tamam");
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Ayarlar", "Ayarlar açılıyor...", "Tamam");
            
        }
        private async void ProfileSettingsButton_Clicked(object sender, EventArgs e)
        {
           
            await DisplayAlert("Ayarlar", "Ayarlar açılıyor...", "Tamam");
           
        }
       }
        }

    
