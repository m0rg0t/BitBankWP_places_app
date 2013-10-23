using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using System.Windows.Media.Imaging;
using Parse;

namespace BitBankWP_places_app.Model
{
    public class PlaceItem: ViewModelBase
    {
        public PlaceItem()
        {
        }

        private string _objectId;
        /// <summary>
        /// Идентификатор места
        /// </summary>
        public string ObjectId
        {
            get { return _objectId; }
            set { 
                _objectId = value;               
                RaisePropertyChanged("ObjectId");
            }
        }
        

        private string _description;
        /// <summary>
        /// Описание места
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { 
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private string _shopName;
        /// <summary>
        /// Название магазина
        /// </summary>
        public string ShopName
        {
            get { return _shopName; }
            set { 
                _shopName = value;
                RaisePropertyChanged("ShopName");
            }
        }

        private string _shopWorkTime;
        /// <summary>
        /// Время работы магазина
        /// </summary>
        public string ShopWorkTime
        {
            get { return _shopWorkTime; }
            set { 
                _shopWorkTime = value;
                RaisePropertyChanged("ShopWorkTime");
            }
        }

        private string _title;
        /// <summary>
        /// Название
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { 
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private DateTime _createdAt;
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { 
                _createdAt = value;
                RaisePropertyChanged("CreatedAt");
            }
        }

        private DateTime _updatedAT;
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedAt
        {
            get { return _updatedAT; }
            set { 
                _updatedAT = value;
                RaisePropertyChanged("UpdatedAt");
            }
        }

        private double _lat;
        /// <summary>
        /// Широта
        /// </summary>
        public double Lat
        {
            get { return _lat; }
            set { 
                _lat = value;
                RaisePropertyChanged("Lat");
                RaisePropertyChanged("GeoPoint");
            }
        }


        private double _lon;
        /// <summary>
        /// Долгота
        /// </summary>
        public double Lon
        {
            get { return _lon; }
            set { 
                _lon = value;
                RaisePropertyChanged("Lon");
                RaisePropertyChanged("GeoPoint");
            }
        }

        private string _address;
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { 
                _address = value;
                RaisePropertyChanged("Address");
            }
        }

        private string _image;
        /// <summary>
        /// 
        /// </summary>
        public string Image
        {
            get { return _image; }
            set { 
                _image = value;
                RaisePropertyChanged("Image");
            }
        }

        private string _userId;
        /// <summary>
        /// Идентификатор пользователя, добавившего место
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                RaisePropertyChanged("UserId");
            }
        }

        private GeoCoordinate _position;
        /// <summary>
        /// 
        /// </summary>
        public GeoCoordinate Position
        {
            get {
                _position = new GeoCoordinate(this.Lat, this.Lon);
                return _position; 
            }
            private set {}
        }

        private WriteableBitmap _imageSource;
        /// <summary>
        /// 
        /// </summary>
        public WriteableBitmap ImageSource
        {
            get { return _imageSource; }
            set { 
                _imageSource = value;
                RaisePropertyChanged("ImageSource");
            }
        }

        private ParseGeoPoint _geoPoint = new ParseGeoPoint();
        /// <summary>
        /// 
        /// </summary>
        public ParseGeoPoint GeoPoint
        {
            get {
                ParseGeoPoint _geoPoint = new ParseGeoPoint(this.Lat, this.Lon);
                return _geoPoint;
            }
            set { 
                _geoPoint = value;
                RaisePropertyChanged("GeoPoint");
            }
        }
        
        

    }
}
