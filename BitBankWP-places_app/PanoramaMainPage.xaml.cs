using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BitBankWP_places_app.ViewModel;

namespace BitBankWP_places_app
{
    public partial class PanoramaMainPage : PhoneApplicationPage
    {
        public PanoramaMainPage()
        {
            InitializeComponent();
        }

        private void LoginTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
            }
            catch { };
        }

        private void SearchTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void MapTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Uri("/Pages/MapPage.xaml", UriKind.Relative));
            }
            catch { };
        }

        private void NearestTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModelLocator.MainStatic.LoadData();
            }
            catch { };
        }
    }
}