using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBankWP_places_app.Model
{
    public class CommentItem: ViewModelBase
    {
        public CommentItem()
        {
        }

        private string _objectId;
        /// <summary>
        /// 
        /// </summary>
        public string ObjectId
        {
            get { return _objectId; }
            set { 
                _objectId = value;
                RaisePropertyChanged("ObjectId");
            }
        }

        private string _comment;
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            get { return _comment; }
            set { 
                _comment = value;
                RaisePropertyChanged("Comment");
            }
        }

        private string _userId;
        /// <summary>
        /// Идентификатор пользователя, добавившего комментарий
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { 
                _userId = value;
                RaisePropertyChanged("UserId");
            }
        }
        
        
    }
}
