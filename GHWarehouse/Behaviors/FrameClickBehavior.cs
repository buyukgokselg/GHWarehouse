using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GHWarehouse.Behaviors
{
    public class FrameClickBehavior : Behavior<Frame>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FrameClickBehavior));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FrameClickBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        protected override void OnAttachedTo(Frame bindable)
        {
            base.OnAttachedTo(bindable);
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (Command?.CanExecute(CommandParameter) ?? false)
                        Command.Execute(CommandParameter);
                })
            };
            bindable.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override void OnDetachingFrom(Frame bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.GestureRecognizers.Clear();
        }
    }
}
