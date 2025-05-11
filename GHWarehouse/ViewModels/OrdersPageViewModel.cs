using System.Windows.Input;
using Microsoft.Maui.Controls;

public class OrdersPageViewModel
{
    public ICommand TapCommand { get; }

    public OrdersPageViewModel()
    {
        TapCommand = new Command<string>(OnTapped);
    }

    private void OnTapped(string parameter)
    {
      
        Console.WriteLine($"Tıklanan kart: {parameter}");
      
    }
}
