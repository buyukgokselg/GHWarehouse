using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Views
{
    public partial class PendingGoodsReceiptPage : ContentPage, INotifyPropertyChanged
    {
        private string _filterDocumentNumber;
        private string _filterSupplier;
        private string _filterOrderNumber;
        private DateTime _filterStartDate = DateTime.Now.AddDays(-7);
        private DateTime _filterEndDate = DateTime.Now;

        private bool _isDetailVisible;
        private GoodsReceiptOrder _selectedOrder;

        public ObservableCollection<GoodsReceiptOrder> AllOrders { get; set; } = new ObservableCollection<GoodsReceiptOrder>();
        public ObservableCollection<GoodsReceiptOrder> Orders { get; set; } = new ObservableCollection<GoodsReceiptOrder>();

 
        public ObservableCollection<GoodsReceiptOrder> SelectedOrders { get; set; } = new ObservableCollection<GoodsReceiptOrder>();

        public GoodsReceiptOrder SelectedOrder
        {
            get => _selectedOrder;
            set { _selectedOrder = value; OnPropertyChanged(); IsDetailVisible = _selectedOrder != null; }
        }

     
        public string FilterDocumentNumber
        {
            get => _filterDocumentNumber;
            set { _filterDocumentNumber = value; OnPropertyChanged(); }
        }
        public string FilterSupplier
        {
            get => _filterSupplier;
            set { _filterSupplier = value; OnPropertyChanged(); }
        }
        public string FilterOrderNumber
        {
            get => _filterOrderNumber;
            set { _filterOrderNumber = value; OnPropertyChanged(); }
        }
        public DateTime FilterStartDate
        {
            get => _filterStartDate;
            set { _filterStartDate = value; OnPropertyChanged(); }
        }
        public DateTime FilterEndDate
        {
            get => _filterEndDate;
            set { _filterEndDate = value; OnPropertyChanged(); }
        }

        public bool IsDetailVisible
        {
            get => _isDetailVisible;
            set { _isDetailVisible = value; OnPropertyChanged(); }
        }

   
        public ICommand FilterCommand { get; }
        public ICommand AcceptMultipleOrdersCommand { get; }
        public ICommand AcceptSingleOrderCommand { get; }

        public PendingGoodsReceiptPage()
        {
            InitializeComponent();
            BindingContext = this;

            FilterCommand = new Command(FilterOrders);
            AcceptMultipleOrdersCommand = new Command(AcceptMultipleOrders);
            AcceptSingleOrderCommand = new Command(AcceptSingleOrder);

            LoadMockOrders();
        }

        private void LoadMockOrders()
        {
            AllOrders.Clear();

            AllOrders.Add(new GoodsReceiptOrder
            {
                DocumentNumber = "BR001",
                Supplier = "Tedarikçi A",
                DocumentDate = DateTime.Now.AddDays(-1),
                Warehouse = "Depo 1",
                OrderNumber = "PO1001",
                OrderItems = new ObservableCollection<OrderItem>
                {
                    new OrderItem { ProductCode="P001", ProductName="Ürün 1", ExpectedQuantity=50, AcceptedQuantity=0 },
                    new OrderItem { ProductCode="P002", ProductName="Ürün 2", ExpectedQuantity=30, AcceptedQuantity=0 }
                }
            });

            AllOrders.Add(new GoodsReceiptOrder
            {
                DocumentNumber = "BR002",
                Supplier = "Tedarikçi B",
                DocumentDate = DateTime.Now.AddDays(-2),
                Warehouse = "Depo 2",
                OrderNumber = "PO1002",
                OrderItems = new ObservableCollection<OrderItem>
                {
                    new OrderItem { ProductCode="P003", ProductName="Ürün 3", ExpectedQuantity=20, AcceptedQuantity=0 }
                }
            });

            FilterOrders();
        }

        private void FilterOrders()
        {
            var filtered = AllOrders.Where(o =>
                (string.IsNullOrEmpty(FilterDocumentNumber) || o.DocumentNumber.Contains(FilterDocumentNumber, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(FilterSupplier) || o.Supplier.Contains(FilterSupplier, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(FilterOrderNumber) || o.OrderNumber.Contains(FilterOrderNumber, StringComparison.OrdinalIgnoreCase)) &&
                o.DocumentDate.Date >= FilterStartDate.Date &&
                o.DocumentDate.Date <= FilterEndDate.Date).ToList();

            Orders.Clear();
            foreach (var order in filtered)
                Orders.Add(order);

            SelectedOrder = Orders.FirstOrDefault();
        }

        private void AcceptMultipleOrders()
        {
            foreach (var order in SelectedOrders)
            {
                order.IsAccepted = true;
            }
            FilterOrders();
        }

        private void AcceptSingleOrder()
        {
            if (SelectedOrder != null)
            {
                SelectedOrder.IsAccepted = true;
                IsDetailVisible = false;
                FilterOrders();
            }
        }


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
                                                 
        }
    }

    public class GoodsReceiptOrder : INotifyPropertyChanged
    {
        private string _documentNumber;
        private string _supplier;
        private DateTime _documentDate;
        private string _warehouse;
        private string _orderNumber;
        private string _description;
        private bool _isAccepted;

        public string DocumentNumber
        {
            get => _documentNumber;
            set { _documentNumber = value; OnPropertyChanged(); }
        }
        public string Supplier
        {
            get => _supplier;
            set { _supplier = value; OnPropertyChanged(); }
        }
        public DateTime DocumentDate
        {
            get => _documentDate;
            set { _documentDate = value; OnPropertyChanged(); }
        }
        public string Warehouse
        {
            get => _warehouse;
            set { _warehouse = value; OnPropertyChanged(); }
        }
        public string OrderNumber
        {
            get => _orderNumber;
            set { _orderNumber = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }
        public bool IsAccepted
        {
            get => _isAccepted;
            set { _isAccepted = value; OnPropertyChanged(); }
        }
        public ObservableCollection<OrderItem> OrderItems { get; set; } = new ObservableCollection<OrderItem>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OrderItem : INotifyPropertyChanged
    {
        private string _productCode;
        private string _productName;
        private int _expectedQuantity;
        private int _acceptedQuantity;

        public string ProductCode
        {
            get => _productCode;
            set { _productCode = value; OnPropertyChanged(); }
        }
        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(); }
        }
        public int ExpectedQuantity
        {
            get => _expectedQuantity;
            set { _expectedQuantity = value; OnPropertyChanged(); }
        }
        public int AcceptedQuantity
        {
            get => _acceptedQuantity;
            set { _acceptedQuantity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
