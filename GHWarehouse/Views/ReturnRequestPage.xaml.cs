using System;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Views
{
    public partial class ReturnRequestPage : ContentPage
    {
        public ReturnRequestPage()
        {
            InitializeComponent();
            // Test için örnek sipariþ ekliyoruz.
            OrderPicker.Items.Add("ORD123456");
            // Sipariþ seçimi yapýldýðýnda sipariþ kartýný güncellemek için event ekliyoruz.
            OrderPicker.SelectedIndexChanged += OrderPicker_SelectedIndexChanged;
        }

        private void OrderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Eðer sipariþ seçilmiþse, sipariþ kartý görünür olur.
            if (OrderPicker.SelectedIndex != -1)
            {
                OrderCardFrame.IsVisible = true;
                // Burada, seçilen sipariþe göre kart içeriðini dinamik olarak güncelleyebilirsiniz.
                // Örneðin: Sipariþ numarasý, sipariþ tarihi, fatura tarihi, ürün bilgileri, adet vb.
            }
            else
            {
                OrderCardFrame.IsVisible = false;
            }
        }

        private async void OnBarcodeScanOrderClicked(object sender, EventArgs e)
        {
            // Burada barkod tarama iþlemi gerçekleþtirilip, taratýlan barkoda göre sipariþ listesi filtrelenir.
            await DisplayAlert("Barkod Tarama", "Barkod tarama iþlemi gerçekleþtirilecek.", "Tamam");
            // Örneðin, taranan barkod ile ilgili sipariþ seçilerek OrderPicker güncellenir.
        }

        private void OnShelfPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen raftan baðlý olarak depo bilgisi otomatik güncellensin.
            var selectedShelf = ShelfPicker.SelectedItem?.ToString();
            if (selectedShelf == "Raf A")
                WarehouseLabel.Text = "Depo 1";
            else if (selectedShelf == "Raf B")
                WarehouseLabel.Text = "Depo 2";
            else if (selectedShelf == "Raf C")
                WarehouseLabel.Text = "Depo 3";
            else
                WarehouseLabel.Text = "--";
        }

        private async void OnOpenBarcodePopupClicked(object sender, EventArgs e)
        {
            // Ýade adedi ve sipariþ seçimi kontrol edilmeli.
            if (OrderPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Hata", "Lütfen iade alýnacak sipariþi seçiniz.", "Tamam");
                return;
            }
            if (string.IsNullOrWhiteSpace(ReturnQuantityEntry.Text) ||
                !int.TryParse(ReturnQuantityEntry.Text, out int qty) || qty <= 0)
            {
                await DisplayAlert("Hata", "Lütfen geçerli bir iade adedi giriniz.", "Tamam");
                return;
            }
            // Popup ekraný modally açýyoruz.
            var popup = new ReturnBarcodeScanPopup(qty);
            await Navigation.PushModalAsync(popup);
        }

        private async void OnSubmitReturnRequestClicked(object sender, EventArgs e)
        {
            // Tüm bilgilerin doðruluðunu kontrol edip, API çaðrýsý veya veritabaný iþlemi yapýlabilir.
            // Bu örnekte basit bir geri bildirim gösteriyoruz.
            await DisplayAlert("Baþarýlý", "Ýade talebiniz oluþturuldu.", "Tamam");
            // Formu temizleyebilir veya baþka sayfaya yönlendirebilirsiniz.
        }
    }
}
