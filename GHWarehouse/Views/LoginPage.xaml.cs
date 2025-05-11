using System;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Views
{
    public partial class LoginPage : ContentPage
    {
        private bool _isPasswordVisible = false;

        public LoginPage()
        {
            InitializeComponent();
#if WINDOWS
Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping("NoText", (handler, view) =>
{
    if (handler.PlatformView is Microsoft.UI.Xaml.Controls.ToggleSwitch nativeSwitch)
    {
        nativeSwitch.OnContent = string.Empty;
        nativeSwitch.OffContent = string.Empty;
    }
});
#endif
         

        }

        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            PasswordEntry.IsPassword = !_isPasswordVisible;
            
        }

        // Sýnýf düzeyinde bayrak ve zaman damgasý ekleyin.
        private bool _isPasswordResetSent = false;
        private DateTime _passwordResetSentTime;

        private async void OnForgotPassword(object sender, EventArgs e)
        {
            // Eðer daha önce þifre sýfýrlama gönderimi yapýlmýþsa, 1 dakika bekleyelim.
            if (_isPasswordResetSent && (DateTime.Now - _passwordResetSentTime).TotalSeconds < 60)
            {
                double remainingSeconds = 60 - (DateTime.Now - _passwordResetSentTime).TotalSeconds;
                int remainingSec = (int)Math.Ceiling(remainingSeconds);
                await DisplayAlert("Uyarý", $"Þifre sýfýrlama iþlemi gerçekleþtirilmiþ. Lütfen {remainingSec} saniye bekleyiniz.", "Tamam");
                return;
            }

            // Eðer 1 dakika geçmiþse, bayraðý sýfýrlayalým.
            if (_isPasswordResetSent && (DateTime.Now - _passwordResetSentTime).TotalSeconds >= 60)
            {
                _isPasswordResetSent = false;
            }

            // Kullanýcý adý alaný kontrolü.
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
            {
                await DisplayAlert("Hata", "Kullanýcý adý boþ olamaz!", "Tamam");
                return;
            }

            // Þifre sýfýrlama yöntemi seçimi.
            string choice = await DisplayActionSheet("Þifre sýfýrlama yöntemi seçiniz", "Ýptal", null, "E-mail", "SMS");

            if (choice == "E-mail")
            {
                await DisplayAlert("Þifre Sýfýrlama", "Geçici þifreniz hesabýnýzda tanýmlý mail adresine gönderildi.", "Tamam");
                _isPasswordResetSent = true;
                _passwordResetSentTime = DateTime.Now;
            }
            else if (choice == "SMS")
            {
                await DisplayAlert("Þifre Sýfýrlama", "Geçici þifreniz hesabýnýzda tanýmlý telefon numarasýna gönderildi.", "Tamam");
                _isPasswordResetSent = true;
                _passwordResetSentTime = DateTime.Now;
            }
        }




        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
            {
                await DisplayAlert("Hata", "Kullanýcý adý boþ olamaz!", "Tamam");
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Hata", "Þifre boþ olamaz!", "Tamam");
                return;
            }

            // Örnek: Giriþ doðrulama iþlemi (gerçek doðrulamanýzý buraya ekleyin)
            bool isAuthenticated = await AuthenticateUser(UsernameEntry.Text, PasswordEntry.Text);
            if (!isAuthenticated)
            {
                await DisplayAlert("Hata", "Giriþ bilgileri yanlýþ!", "Tamam");
                return;
            }

            // Giriþ baþarýlýysa, þifre sýfýrlama kýsýtýný kaldýrýyoruz.
            _isPasswordResetSent = false;
            _passwordResetSentTime = DateTime.MinValue;

            // Baþarýlý giriþ sonrasý ana sayfaya yönlendirme
            Application.Current.MainPage = new GHWarehouse.AppShell();
        }

        // Örnek: Kullanýcý doðrulama metodu
        private async Task<bool> AuthenticateUser(string username, string password)
        {
            // Buraya kullanýcý doðrulama iþlemlerini ekleyin.
            // Bu örnekte doðrulama her zaman baþarýlý dönüyor.
            await Task.Delay(500); // Simülasyon için bekletme
            return true;
        }

    }
}
