using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Controls
{
    public partial class OperationCard : ContentView
    {
        public OperationCard()
        {
            InitializeComponent();
            // Dokunma (tap) olayını desteklemek için GestureRecognizer ekleniyor.
            var tapGesture = new TapGestureRecognizer();
            tapGesture.SetBinding(TapGestureRecognizer.CommandProperty, new Binding(nameof(Command), source: this));
            this.GestureRecognizers.Add(tapGesture);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(OperationCard));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CountProperty =
            BindableProperty.Create(nameof(Count), typeof(int), typeof(OperationCard), 0);

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(string), typeof(OperationCard), string.Empty);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(OperationCard), 0.0);

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(OperationCard), string.Empty);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
    }
}
