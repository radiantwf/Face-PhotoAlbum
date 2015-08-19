using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Face_PhotoAlbum
{
    public class ImageButton : Button
    {
        private string m_imagepath;

        public string ImgPath
        {
            get { return m_imagepath; }
            set { m_imagepath = value; }
        }
    }
}
