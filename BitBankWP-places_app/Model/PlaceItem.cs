using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            set { _lat = value; }
        }


        private double _lon;
        /// <summary>
        /// Долгота
        /// </summary>
        public double Lon
        {
            get { return _lon; }
            set { _lon = value; }
        }
        
        
        

    }
}
