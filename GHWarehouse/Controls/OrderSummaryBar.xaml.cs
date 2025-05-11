using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace GHWarehouseApp.Controls
{
    public partial class OrderSummaryBar : ContentView
    {
        public OrderSummaryBar()
        {
            InitializeComponent();
            StartMarquee();
        }

        private async void StartMarquee()
        {
            while (true)
            {
                // Marquee için basit animasyon örneði:
                MarqueeLabel.TranslationX = Width;
                await Task.Delay(100);
                double labelWidth = MarqueeLabel.Width;
                await MarqueeLabel.TranslateTo(-labelWidth, 0, 5000, Easing.Linear);
            }
        }
    }
}
