using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHWarehouse.Behaviors
{
    public class BorderTapAnimationBehavior : Behavior<Border>
    {
        private TapGestureRecognizer _tapGestureRecognizer;

        protected override void OnAttachedTo(Border bindable)
        {
            base.OnAttachedTo(bindable);

            _tapGestureRecognizer = new TapGestureRecognizer();
            _tapGestureRecognizer.Tapped += OnBorderTapped;
            bindable.GestureRecognizers.Add(_tapGestureRecognizer);
        }

        protected override void OnDetachingFrom(Border bindable)
        {
            base.OnDetachingFrom(bindable);
            if (_tapGestureRecognizer != null)
            {
                _tapGestureRecognizer.Tapped -= OnBorderTapped;
                bindable.GestureRecognizers.Remove(_tapGestureRecognizer);
            }
        }

        private async void OnBorderTapped(object sender, EventArgs e)
        {
            if (sender is Border border)
            {
                await AnimateBorder(border);
            }
        }

        private async Task AnimateBorder(Border border)
        {
            try
            {
                await border.ScaleTo(0.9, 100, Easing.Linear); 
                await border.ScaleTo(1, 100, Easing.Linear);  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Animasyon hatası: {ex.Message}");
            }
        }
    }

}
