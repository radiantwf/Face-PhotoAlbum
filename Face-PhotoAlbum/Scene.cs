using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Face_PhotoAlbum
{
    public class Scence : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool m_isselected;

        public string ImagePath
        {
            get;
            set;
        }

        public bool IsSelected
        {
            get
            {
                return m_isselected;
            }
            set
            {
                m_isselected = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        public Scence(string imagepath)
        {
            ImagePath = imagepath;
            IsSelected = false;
        }
    }

    public class ScenceSelectStatusToBgPathConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return @"/Resources/场景背景selected.png";
            }
            else
            {
                return @"/Resources/场景背景.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Package : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_imagepath;
        private int m_scencenum;
        private string m_packagename;
        private bool m_isselected;

        public string ImagePath
        {
            get
            {
                return m_imagepath;
            }
        }

        public string PackageName
        {
            get
            {
                return m_packagename;
            }
        }

        public string NumStr
        {
            get
            {
                return "共xxx个场景";
            }
        }

        public bool IsSelected
        {
            get
            {
                return m_isselected;
            }
            set
            {
                m_isselected = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        public Package(string packagename, string imagepath, int scencenum = 0)
        {
            m_imagepath = imagepath;
            m_packagename = packagename;
            m_scencenum = scencenum;
            IsSelected = false;
        }

    }

    public class PackageSelectStatusToBgPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return @"/Resources/套系背景selected.png";
            }
            else
            {
                return @"/Resources/套系背景.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
