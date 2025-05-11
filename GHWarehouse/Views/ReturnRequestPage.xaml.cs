using System;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Views
{
    public partial class ReturnRequestPage : ContentPage
    {
        public ReturnRequestPage()
        {
            InitializeComponent();
            // Test i�in �rnek sipari� ekliyoruz.
            OrderPicker.Items.Add("ORD123456");
            // Sipari� se�imi yap�ld���nda sipari� kart�n� g�ncellemek i�in event ekliyoruz.
            OrderPicker.SelectedIndexChanged += OrderPicker_SelectedIndexChanged;
        }

        private void OrderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // E�er sipari� se�ilmi�se, sipari� kart� g�r�n�r olur.
            if (OrderPicker.SelectedIndex != -1)
            {
                OrderCardFrame.IsVisible = true;
                // Burada, se�ilen sipari�e g�re kart i�eri�ini dinamik olarak g�ncelleyebilirsiniz.
                // �rne�in: Sipari� numaras�, sipari� tarihi, fatura tarihi, �r�n bilgileri, adet vb.
            }
            else
            {
                OrderCardFrame.IsVisible = false;
            }
        }

        private async void OnBarcodeScanOrderClicked(object sender, EventArgs e)
        {
            // Burada barkod tarama i�lemi ger�ekle�tirilip, tarat�lan barkoda g�re sipari� listesi filtrelenir.
            await DisplayAlert("Barkod Tarama", "Barkod tarama i�lemi ger�ekle�tirilecek.", "Tamam");
            // �rne�in, taranan barkod ile ilgili sipari� se�ilerek OrderPicker g�ncellenir.
        }

        private void OnShelfPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            // Se�ilen raftan ba�l� olarak depo bilgisi otomatik g�ncellensin.
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
            // �ade adedi ve sipari� se�imi kontrol edilmeli.
            if (OrderPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Hata", "L�tfen iade al�nacak sipari�i se�iniz.", "Tamam");
                return;
            }
            if (string.IsNullOrWhiteSpace(ReturnQuantityEntry.Text) ||
                !int.TryParse(ReturnQuantityEntry.Text, out int qty) || qty <= 0)
            {
                await DisplayAlert("Hata", "L�tfen ge�erli bir iade adedi giriniz.", "Tamam");
                return;
            }
            // Popup ekran� modally a��yoruz.
            var popup = new ReturnBarcodeScanPopup(qty);
            await Navigation.PushModalAsync(popup);
        }

        private async void OnSubmitReturnRequestClicked(object sender, EventArgs e)
        {
            // T�m bilgilerin do�rulu�unu kontrol edip, API �a�r�s� veya veritaban� i�lemi yap�labilir.
            // Bu �rnekte basit bir geri bildirim g�steriyoruz.
            await DisplayAlert("Ba�ar�l�", "�ade talebiniz olu�turuldu.", "Tamam");
            // Formu temizleyebilir veya ba�ka sayfaya y�nlendirebilirsiniz.
        }
    }
}
