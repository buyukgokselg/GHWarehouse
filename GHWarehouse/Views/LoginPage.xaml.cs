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

        // S�n�f d�zeyinde bayrak ve zaman damgas� ekleyin.
        private bool _isPasswordResetSent = false;
        private DateTime _passwordResetSentTime;

        private async void OnForgotPassword(object sender, EventArgs e)
        {
            // E�er daha �nce �ifre s�f�rlama g�nderimi yap�lm��sa, 1 dakika bekleyelim.
            if (_isPasswordResetSent && (DateTime.Now - _passwordResetSentTime).TotalSeconds < 60)
            {
                double remainingSeconds = 60 - (DateTime.Now - _passwordResetSentTime).TotalSeconds;
                int remainingSec = (int)Math.Ceiling(remainingSeconds);
                await DisplayAlert("Uyar�", $"�ifre s�f�rlama i�lemi ger�ekle�tirilmi�. L�tfen {remainingSec} saniye bekleyiniz.", "Tamam");
                return;
            }

            // E�er 1 dakika ge�mi�se, bayra�� s�f�rlayal�m.
            if (_isPasswordResetSent && (DateTime.Now - _passwordResetSentTime).TotalSeconds >= 60)
            {
                _isPasswordResetSent = false;
            }

            // Kullan�c� ad� alan� kontrol�.
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
            {
                await DisplayAlert("Hata", "Kullan�c� ad� bo� olamaz!", "Tamam");
                return;
            }

            // �ifre s�f�rlama y�ntemi se�imi.
            string choice = await DisplayActionSheet("�ifre s�f�rlama y�ntemi se�iniz", "�ptal", null, "E-mail", "SMS");

            if (choice == "E-mail")
            {
                await DisplayAlert("�ifre S�f�rlama", "Ge�ici �ifreniz hesab�n�zda tan�ml� mail adresine g�nderildi.", "Tamam");
                _isPasswordResetSent = true;
                _passwordResetSentTime = DateTime.Now;
            }
            else if (choice == "SMS")
            {
                await DisplayAlert("�ifre S�f�rlama", "Ge�ici �ifreniz hesab�n�zda tan�ml� telefon numaras�na g�nderildi.", "Tamam");
                _isPasswordResetSent = true;
                _passwordResetSentTime = DateTime.Now;
            }
        }




        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
            {
                await DisplayAlert("Hata", "Kullan�c� ad� bo� olamaz!", "Tamam");
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Hata", "�ifre bo� olamaz!", "Tamam");
                return;
            }

            // �rnek: Giri� do�rulama i�lemi (ger�ek do�rulaman�z� buraya ekleyin)
            bool isAuthenticated = await AuthenticateUser(UsernameEntry.Text, PasswordEntry.Text);
            if (!isAuthenticated)
            {
                await DisplayAlert("Hata", "Giri� bilgileri yanl��!", "Tamam");
                return;
            }

            // Giri� ba�ar�l�ysa, �ifre s�f�rlama k�s�t�n� kald�r�yoruz.
            _isPasswordResetSent = false;
            _passwordResetSentTime = DateTime.MinValue;

            // Ba�ar�l� giri� sonras� ana sayfaya y�nlendirme
            Application.Current.MainPage = new GHWarehouse.AppShell();
        }

        // �rnek: Kullan�c� do�rulama metodu
        private async Task<bool> AuthenticateUser(string username, string password)
        {
            // Buraya kullan�c� do�rulama i�lemlerini ekleyin.
            // Bu �rnekte do�rulama her zaman ba�ar�l� d�n�yor.
            await Task.Delay(500); // Sim�lasyon i�in bekletme
            return true;
        }

    }
}
