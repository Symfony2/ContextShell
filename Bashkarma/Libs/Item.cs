using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bashkarma
{
    public class Item : INotifyPropertyChanged
    {
        private int _pictureID;
        private string _pictureString;
        public int PictureID
        {
            get { return _pictureID; } 
            set 
            {
                _pictureID = value;
                
            }
        }
        public string Name { get; set; }
        public string PictureString 
        {
            get {return "/Images/" + PictureID.ToString() + ".png";}
            set 
            {
                _pictureString = value;
                NotifyPropertyChanged("PictureString");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if(PropertyChanged!=null)PropertyChanged(this,new PropertyChangedEventArgs(property));
        }
    }
}
