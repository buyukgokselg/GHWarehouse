using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Views
{
    public partial class ReturnBarcodeScanPopup : ContentPage
    {
        // Gelen sipari�e ait iade adedi (�rne�in: 2 adet iade bekleniyor)
        private int _requiredQuantity;

        // Tarat�lan �r�nleri tutan liste
        public ObservableCollection<ProductScan> ScannedProducts { get; set; } = new ObservableCollection<ProductScan>();

        public ReturnBarcodeScanPopup(int requiredQuantity)
        {
            InitializeComponent();
            _requiredQuantity = requiredQuantity;
            // �rnek: Daha �nce bu sipari�e ait �r�n bilgisi varsa, ScannedProducts'e eklenebilir.
            BindingContext = this;
        }

        private void OnIncrementClicked(object sender, EventArgs e)
        {
            if (int.TryParse(QuantityPopupEntry.Text, out int qty))
            {
                qty++;
                QuantityPopupEntry.Text = qty.ToString();
            }
        }

        private void OnDecrementClicked(object sender, EventArgs e)
        {
            if (int.TryParse(QuantityPopupEntry.Text, out int qty))
            {
                if (qty > 1)
                {
                    qty--;
                    QuantityPopupEntry.Text = qty.ToString();
                }
            }
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            // Barkod ve adet bilgilerini oku
            string barcode = BarcodeEntry.Text?.Trim();
            if (string.IsNullOrEmpty(barcode))
            {
                DisplayAlert("Hata", "L�tfen bir barkod giriniz.", "Tamam");
                return;
            }

            if (!int.TryParse(QuantityPopupEntry.Text, out int scannedQty) || scannedQty <= 0)
            {
                DisplayAlert("Hata", "L�tfen ge�erli bir adet giriniz.", "Tamam");
                return;
            }

            // Burada, taranan barkod ile sipari�teki �r�n bilgisi e�le�tirilebilir.
            // �rnek: �r�n ad�, sipari� adedi gibi bilgiler API veya yerel veriden al�n�r.
            string productName = "�r�n " + barcode; // �rnek isim
            // E�er �r�n daha �nce eklenmi�se, adedi g�ncelle; aksi halde yeni kart ekle.
            var existing = FindProduct(productName);
            if (existing != null)
            {
                existing.ScannedQuantity += scannedQty;
            }
            else
            {
                ScannedProducts.Add(new ProductScan
                {
                    ProductName = productName,
                    RequiredQuantity = _requiredQuantity,
                    ScannedQuantity = scannedQty
                });
            }
            // Barkod giri�ini ve adet alan�n� s�f�rla
            BarcodeEntry.Text = "";
            QuantityPopupEntry.Text = "1";
        }

        private ProductScan FindProduct(string productName)
        {
            foreach (var item in ScannedProducts)
            {
                if (item.ProductName == productName)
                    return item;
            }
            return null;
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
