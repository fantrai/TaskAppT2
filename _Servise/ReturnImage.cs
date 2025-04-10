using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppT2._Servise
{
    public static class ReturnImage
    {
        static readonly Dictionary<UseImage, string> imgSource = new()
        {
            { UseImage.Trash, "trash.png" },
            { UseImage.ArrayLeft, "arrow_left.png" },
            { UseImage.Cross, "close_circle.png" },
        };

        public static string ReturnStringPath(UseImage image)
        {
            return imgSource[image];
        }
    }

    public enum UseImage
    {
        Trash,
        ArrayLeft,
        Cross,
    }
}
