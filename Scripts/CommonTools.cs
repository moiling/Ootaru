using UnityEngine;

namespace Scripts {
    public class CommonTools {
        
        public static Color StringToColor(string colorStr) {
            if (string.IsNullOrEmpty(colorStr)) {
                return new Color();
            }

            var colorInt = int.Parse(colorStr, System.Globalization.NumberStyles.AllowHexSpecifier);

            return IntToColor(colorInt);
        }

        public static Color IntToColor(int colorInt) {
            float basenum = 255;

            var b = 0xFF & colorInt;
            var g = 0xFF00 & colorInt;
            g >>= 8;
            var r = 0xFF0000 & colorInt;
            r >>= 16;

            return new Color(r / basenum, g / basenum, b / basenum, 1);

        }
    }
}