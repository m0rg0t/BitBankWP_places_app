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
using BitBankWP_places_app.Model;

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

        private void AddTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new Uri("/Pages/AddPlacePage.xaml", UriKind.Relative));
            }
            catch { };
        }

        private void CategoriesList_ItemTap(object sender, Telerik.Windows.Controls.ListBoxItemTapEventArgs e)
        {
            try
            {
                ViewModelLocator.MainStatic.CurrentItem = (this.NearestPlacesList.SelectedItem as PlaceItem);
                this.NavigationService.Navigate(new Uri("/Pages/ViewPlacePage.xaml", UriKind.Relative));
            }
            catch { };
        }
    }
}