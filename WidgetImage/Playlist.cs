using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetImage
{  
    public class Playlist
    {
        public List<String> _playlist { get; set; }
        public String _name { get; set; }

        public Playlist(int n)
        {            
            _name += "PlayList " + n;
            _playlist = new List<String>();
        }

        public void addToList(String newElement)
        {
            _playlist.Add(newElement);
        }

        public void removeElement(String element)
        {
            int at = _playlist.IndexOf(element);
            if (at >= 0)
                _playlist.RemoveAt(at);
        }

        public void removeList()
        {
            _playlist.Clear();
        }
    }
}
