using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WidgetImage
{  
    class Playlist
    {
        private List<String> _playlist;
        String _name;

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
