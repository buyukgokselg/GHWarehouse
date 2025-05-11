using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Controls
{
    public partial class Badge : ContentView
    {
        public Badge()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Badge), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty BadgeColorProperty =
            BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(Badge), Colors.Red);

        public Color BadgeColor
        {
            get => (Color)GetValue(BadgeColorProperty);
            set => SetValue(BadgeColorProperty, value);
        }
    }
}
