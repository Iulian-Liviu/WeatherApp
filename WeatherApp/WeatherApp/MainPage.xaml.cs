using InputKit.Shared.Controls;
using UraniumUI.Pages;

namespace WeatherApp
{
    public partial class MainPage : UraniumContentPage
    {
        public MainPage()
        {
            SelectionView.GlobalSetting.CornerRadius = 0;
            InitializeComponent();
        }
    }
}