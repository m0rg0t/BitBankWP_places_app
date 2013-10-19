using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using BitBankWP_places_app.ViewModel;

namespace BitBankWP_places_app.Pages
{
    public partial class ViewPlacePage : PhoneApplicationPage
    {
        // Constructor
        public ViewPlacePage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void PlaceMap_Loaded(object sender, RoutedEventArgs e)
        {
            Map item = new Map();
            //item.Center
            try
            {
                /*PlaceMap.Center.Latitude = ViewModelLocator.MainStatic.CurrentItem.Lon;
                PlaceMap.Center.Longitude = ViewModelLocator.MainStatic.CurrentItem.Lat;
                var i = PlaceMap.Center;
                var b = i;*/
            }
            catch { };
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}