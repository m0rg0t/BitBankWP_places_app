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
        public async Task<bool> LoadAllPlaces()
        {
            try
            {
                var query = ParseObject.GetQuery("Place");
                //var query = from item in ParseObject.GetQuery("Place") select item;
                IEnumerable<ParseObject> results = await query.FindAsync();
                PlaceItems = new ObservableCollection<PlaceItem>();
                NearestImages = new Collection<string>();
                foreach (var item in results)
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
                            NearestImages.Add(file.Url.ToString());
                        }
                        catch { };
                        PlaceItems.Add(placeItem);
                    }
                    catch { };
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

                MemoryStream ms = new MemoryStream();
                Extensions.SaveJpeg(item.ImageSource, ms,
                    item.ImageSource.PixelWidth, item.ImageSource.PixelHeight, 0, 100);

                //byte[] data = item.ImageSource.
                ParseFile file = new ParseFile("photo" + UnixTimeNow().ToString() + ".jpg", ms.ToArray());
                await file.SaveAsync();

                //byte[] data = System.Text.Encoding.UTF8.GetBytes("Working at Parse is great!");
                //ParseFile file = new ParseFile("resume.txt", data);
                //await file.SaveAsync();

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadData()
        {
            this.Loading = true;
            try
            {
                await LoadAllPlaces();
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