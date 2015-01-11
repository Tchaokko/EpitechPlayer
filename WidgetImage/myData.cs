using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace WidgetImage
{
    public class MyData
    {
        public MyData() { }
        public MediaElement media { get; set; }
        public Label myPath { get; set; }
        public Label myName { get; set; }
        public Label Data1 { get; set; }
        public Button Add { get; set; }
    }
}
