using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Views
{
    public partial class ReturnBarcodeScanPopup : ContentPage
    {
        // Gelen sipariþe ait iade adedi (örneðin: 2 adet iade bekleniyor)
        private int _requiredQuantity;

        // Taratýlan ürünleri tutan liste
        public ObservableCollection<ProductScan> ScannedProducts { get; set; } = new ObservableCollection<ProductScan>();

        public ReturnBarcodeScanPopup(int requiredQuantity)
        {
            InitializeComponent();
            _requiredQuantity = requiredQuantity;
            // Örnek: Daha önce bu sipariþe ait ürün bilgisi varsa, ScannedProducts'e eklenebilir.
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
                DisplayAlert("Hata", "Lütfen bir barkod giriniz.", "Tamam");
                return;
            }

            if (!int.TryParse(QuantityPopupEntry.Text, out int scannedQty) || scannedQty <= 0)
            {
                DisplayAlert("Hata", "Lütfen geçerli bir adet giriniz.", "Tamam");
                return;
            }

            // Burada, taranan barkod ile sipariþteki ürün bilgisi eþleþtirilebilir.
            // Örnek: Ürün adý, sipariþ adedi gibi bilgiler API veya yerel veriden alýnýr.
            string productName = "Ürün " + barcode; // Örnek isim
            // Eðer ürün daha önce eklenmiþse, adedi güncelle; aksi halde yeni kart ekle.
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
            // Barkod giriþini ve adet alanýný sýfýrla
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
