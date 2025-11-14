using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.utility
{
    internal static class FontHelper
    {
        internal static Font Create(string fontName, int fontSize, FontStyle style, int minSize = 6)
        {
            var name = string.IsNullOrEmpty(fontName) ? "Tahoma" : fontName;
            var size = fontSize > 0 ? fontSize : 10;
            size = Math.Max(size, minSize);
            return new Font(name, size, style);
        }
    }
}
