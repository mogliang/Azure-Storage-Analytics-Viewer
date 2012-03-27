using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace AzureStorageMetricsViewer
{
    public class PropertyColorDict
    {
        static List<SolidColorBrush> brushes = new List<SolidColorBrush>{
            new SolidColorBrush(Colors.AliceBlue)
        };
        public static Brush GetBrushForProperty(string propname)
        {
            return null;
        }
    }
}
