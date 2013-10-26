using GalaSoft.MvvmLight;
using BitBankWP_places_app.Model;
using System.Threading.Tasks;
using Parse;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using System;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using System.Windows;
using System.Windows.Threading;
using System.Device.Location;

namespace BitBankWP_places_app.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }

        private UserViewModel _user = new UserViewModel();
        /// <summary>
        /// 
        /// </summary>
        public UserViewModel User
        {
            get { return _user; }
            set { 
                _user = value;
                RaisePropertyChanged("User");
            }
        }
        

        private bool _loading;
        /// <summary>
        /// загрузка
        /// </summary>
        public bool Loading
        {
            get { return _loading; }
            set { 
                _loading = value;
                RaisePropertyChanged("Loading");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadNearPlaces()
        {
            try
            {
                // User's location
                var userGeoPoint = new ParseGeoPoint(MyCoordinate.Latitude, MyCoordinate.Longitude);
                // Create a query for places
                var query = ParseObject.GetQuery("Place");
                //Interested in locations near user.
                query = query.WhereNear("latlon", userGeoPoint);
                // Limit what could be a lot of points.
                query = query.Limit(80);

                IEnumerable<ParseObject> results = await query.FindAsync();

                PlaceItems = new ObservableCollection<PlaceItem>();
                NearestImages = new Collection<string>();
                foreach (var item in results)
                {
                    AddPlaceFromParseObject(item);
                };

                if (PlaceItems.Count < 1)
                {
                    await LoadSomePlaces();
                };
            }
            catch { };
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void AddPlaceFromParseObject(ParseObject item, string type = "nearest")
        {
            try
            {
                var placeItem = new PlaceItem();
                placeItem.Title = item.Get<string>("title");
                placeItem.Address = item.Get<string>("address");
                placeItem.Description = item.Get<string>("description");
                placeItem.Lat = item.Get<double>("lat");
                placeItem.Lon = item.Get<double>("lon");
                placeItem.ShopName = item.Get<string>("shopName");
                placeItem.ShopWorkTime = item.Get<string>("shopWorkTime");
                placeItem.ObjectId = item.ObjectId.ToString();
                try
                {                    
                    var file = item.Get<ParseFile>("photo");
                    placeItem.Image = file.Url.ToString();
                    if (type == "nearest")
                    {
                        NearestImages.Add(file.Url.ToString());
                    };
                }
                catch { };
                if (type == "search")
                {
                    SearchPlaceItems.Add(placeItem);
                }
                else
                {
                    PlaceItems.Add(placeItem);
                    NearestPlaceItems.Add(placeItem);
                };                
            }
            catch { };
        }

        public async Task<bool> LoadSomePlaces()
        {
            try
            {
                var query = ParseObject.GetQuery("Place");
                //var query = from item in ParseObject.GetQuery("Place") select item;
                query = query.Limit(80);
                IEnumerable<ParseObject> results = await query.FindAsync();

                PlaceItems = new ObservableCollection<PlaceItem>();
                NearestImages = new Collection<string>();
                foreach (var item in results)
                {
                    AddPlaceFromParseObject(item);
                };
            }
            catch { };
            return true;
        }

        private Collection<string> _nearestImages = new Collection<string>();
        /// <summary>
        /// 
        /// </summary>
        public Collection<string> NearestImages
        {
            get { return _nearestImages; }
            set { 
                _nearestImages = value;
                RaisePropertyChanged("NearestImages");
            }
        }

        private CommentItem _newComment = new CommentItem();
        /// <summary>
        /// 
        /// </summary>
        public CommentItem NewComment
        {
            get { return _newComment; }
            set { 
                _newComment = value;
                RaisePropertyChanged("NewComment");
            }
        }
        

        private ObservableCollection<PlaceItem> _placeItems = new ObservableCollection<PlaceItem>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<PlaceItem> PlaceItems
        {
            get { return _placeItems; }
            set { 
                _placeItems = value;
                RaisePropertyChanged("PlaceItems");
            }
        }

        private ObservableCollection<PlaceItem> _nearestPlaceItems = new ObservableCollection<PlaceItem>();
        /// <summary>
        /// Nearest places
        /// </summary>
        public ObservableCollection<PlaceItem> NearestPlaceItems
        {
            get { return _nearestPlaceItems; }
            set { 
                _nearestPlaceItems = value;
                RaisePropertyChanged("PlaceItems");
            }
        }


        private string _searchQuery = "";
        /// <summary>
        /// 
        /// </summary>
        public string SearchQuery
        {
            get { return _searchQuery; }
            set { 
                _searchQuery = value;
                RaisePropertyChanged("SearchQuery");
            }
        }
        

        /// <summary>
        /// Search places
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadSearchPlaces()
        {
            this.Loading = true;
            try
            {
                // User's location
                var userGeoPoint = new ParseGeoPoint(MyCoordinate.Latitude, MyCoordinate.Longitude);
                // Create a query for places
                var query = from place in ParseObject.GetQuery("Place")
                            where (place.Get<string>("title").Contains(SearchQuery) || place.Get<string>("title").Contains(SearchQuery.ToLower())) ||
                                  (place.Get<string>("description").Contains(SearchQuery)|| place.Get<string>("title").Contains(SearchQuery.ToLower())) ||
                                  (place.Get<string>("address").Contains(SearchQuery)|| place.Get<string>("title").Contains(SearchQuery.ToLower())) ||
                                  (place.Get<string>("shopName").Contains(SearchQuery)|| place.Get<string>("title").Contains(SearchQuery.ToLower()))
                            select place;
                query = query.Limit(80);

                IEnumerable<ParseObject> results = await query.FindAsync();

                SearchPlaceItems = new ObservableCollection<PlaceItem>();
                //NearestImages = new Collection<string>();
                foreach (var item in results)
                {
                    AddPlaceFromParseObject(item, "search");
                };
            }
            catch(Exception ex) {
                Debug.WriteLine(ex.ToString());
            };
            this.Loading = false;

            return true;
        }

        private ObservableCollection<PlaceItem> _searchPlaceItems;
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<PlaceItem> SearchPlaceItems
        {
            get { return _searchPlaceItems; }
            set { 
                _searchPlaceItems = value;
                RaisePropertyChanged("SearchPlaceItems");
            }
        }
        

        private PlaceItem _currentItem = new PlaceItem();
        /// <summary>
        /// Текущее место
        /// </summary>
        public PlaceItem CurrentItem
        {
            get { return _currentItem; }
            set { 
                _currentItem = value;
                RaisePropertyChanged("CurrentItem");
            }
        }

        private PlaceItem _newPlace = new PlaceItem();
        /// <summary>
        /// 
        /// </summary>
        public PlaceItem NewPlace
        {
            get { return _newPlace; }
            set { 
                _newPlace = value;
                RaisePropertyChanged("NewPlace");
            }
        }

        public long UnixTimeNow()
        {
            TimeSpan _TimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)_TimeSpan.TotalSeconds;
        }

        /// <summary>
        /// 
        /// </summary>
        public GeoCoordinate MyCoordinate
        {
            get;
            set;
        }

        private double _accuracy = 0.0;

        /// <summary>
        /// 
        /// </summary>
        private async Task<bool> GetCurrentCoordinate()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;

            try
            {
                Geoposition currentPosition = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1),
                                                                                   TimeSpan.FromSeconds(10));
                _accuracy = currentPosition.Coordinate.Accuracy;
                //Dispatcher.BeginInvoke(() =>
                //{
                    MyCoordinate = new GeoCoordinate(currentPosition.Coordinate.Latitude, currentPosition.Coordinate.Longitude);
                //});
            }
            catch (Exception ex)
            {
                // Couldn't get current location - location might be disabled in settings
                //MessageBox.Show("Current location cannot be obtained. Check that location service is turned on in phone settings.");
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> SaveItemToParse(PlaceItem item)
        {
            this.Loading = true;
            try
            {
                ParseObject place = new ParseObject("Place");
                place["title"] = item.Title;
                place["description"] = item.Description;
                place["address"] = item.Address;
                place["shopName"] = item.ShopName;
                place["shopWorkTime"] = item.ShopWorkTime;
                place["userId"] = item.UserId;
                place["lat"] = item.Lat;
                place["lon"] = item.Lon;
                place["latlon"] = new ParseGeoPoint(item.Lat, item.Lon);

                place["authorImage"] = ViewModelLocator.MainStatic.User.UserImage;
                place["authorUsername"] = ViewModelLocator.MainStatic.User.Username;

                MemoryStream ms = new MemoryStream();
                Extensions.SaveJpeg(item.ImageSource, ms,
                    item.ImageSource.PixelWidth, item.ImageSource.PixelHeight, 0, 100);

                ParseFile file = new ParseFile("photo" + UnixTimeNow().ToString() + ".jpg", ms.ToArray());
                await file.SaveAsync();

                //byte[] data = System.Text.Encoding.UTF8.GetBytes("Working at Parse is great!");
                //ParseFile file = new ParseFile("resume.txt", data);
                //await file.SaveAsync();
                item.Image = file.Url.ToString();

                place["photo"] = file;
                await place.SaveAsync();

                ViewModelLocator.MainStatic.PlaceItems.Add(item);
            }
            catch(Exception ex) {
                Debug.WriteLine(ex.ToString());
            };
            this.Loading = false;
            return true;
        }

        public async Task<bool> SaveCommentToParse(CommentItem item)
        {
            this.Loading = true;
            try
            {
                ParseObject place = new ParseObject("Comment");
                place["comment"] = item.Comment;
                place["placeId"] = item.PlaceId;
                place["userId"] = ViewModelLocator.MainStatic.User.ObjectId;

                place["userImage"] = ViewModelLocator.MainStatic.User.UserImage;
                place["userName"] = ViewModelLocator.MainStatic.User.Username;

                await place.SaveAsync();

                ViewModelLocator.MainStatic.CurrentItem.CommentItems.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            };
            this.Loading = false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadData()
        {
            this.Loading = true;
            await GetCurrentCoordinate();
            try
            {
                await LoadNearPlaces();
            }
            catch { };
            this.Loading = false;
            return true;
        }       

        /*private readonly IDataService _dataService;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
                RaisePropertyChanged(WelcomeTitlePropertyName);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
        }*/

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}