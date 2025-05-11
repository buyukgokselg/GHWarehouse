using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GHWarehouseApp.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrderStatus> OrderStatuses { get; set; }

        public OrdersViewModel()
        {
            OrderStatuses = new ObservableCollection<OrderStatus>
            {
                new OrderStatus { Icon = "⏳", StatusName = "Toplama Bekliyor", Count = 12, BadgeColor = Color.FromArgb("#FFC107") },
                new OrderStatus { Icon = "📦", StatusName = "Paketleme Bekliyor", Count = 8, BadgeColor = Color.FromArgb("#007BFF") },
                new OrderStatus { Icon = "🚚", StatusName = "Kargoda", Count = 5, BadgeColor = Color.FromArgb("#28A745") },
                new OrderStatus { Icon = "✅", StatusName = "Tamamlandı", Count = 20, BadgeColor = Color.FromArgb("#6C757D") }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}