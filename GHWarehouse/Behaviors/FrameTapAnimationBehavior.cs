using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHWarehouse.Behaviors
{
    public class FrameTapAnimationBehavior : Behavior<Frame>
    {
        private TapGestureRecognizer _tapGestureRecognizer;

        protected override void OnAttachedTo(Frame frame)
        {
            base.OnAttachedTo(frame);
            _tapGestureRecognizer = new TapGestureRecognizer();
            _tapGestureRecognizer.Tapped += OnFrameTapped;
            frame.GestureRecognizers.Add(_tapGestureRecognizer);
        }

        private async void OnFrameTapped(object sender, System.EventArgs e)
        {
            if (sender is Frame frame)
            {
                var originalBorderColor = frame.BorderColor;
                frame.BorderColor = Colors.White;  
                frame.Opacity = 0.8;               
                await frame.FadeTo(1, 100);    
                await Task.Delay(200);             
                frame.BorderColor = originalBorderColor;
                frame.Opacity = 1;
            }
        }

        protected override void OnDetachingFrom(Frame frame)
        {
            base.OnDetachingFrom(frame);
            if (_tapGestureRecognizer != null)
            {
                _tapGestureRecognizer.Tapped -= OnFrameTapped;
            }
        }
    }
}