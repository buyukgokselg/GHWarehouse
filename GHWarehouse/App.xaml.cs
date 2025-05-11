namespace GHWarehouse;
using GHWarehouseApp.Views;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui;



public partial class App : Application
{

	public App()
	{
        InitializeComponent();
        MainPage = new GHWarehouseApp.Views.LoginPage();

    }
}
