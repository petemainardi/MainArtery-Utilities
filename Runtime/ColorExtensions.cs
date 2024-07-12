using UnityEngine;

namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
    *  Extension methods for the Unity Color class.
    *  
    *  For additional info on the contrast functions, see:
    *  https://stackoverflow.com/questions/3942878/how-to-decide-font-color-in-white-or-black-depending-on-background-color
    */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class ColorExtensions
    {
        public static Color BestContrast(this Color color)
        {
            return BestContrast(color, Color.white, Color.black);
        }

        public static Color BestContrast(this Color color, Color color1, Color color2)
        {
            float lum1 = color1.Luminance();
            float lum2 = color2.Luminance();

            Color lighter = Mathf.Max(lum1, lum2) == lum1 ? color1 : color2;
            Color darker = Mathf.Min(lum1, lum2) == lum1 ? color1 : color2;

            return color.Luminance() > 0.179 ? darker : lighter;
        }

        public static float Luminance(this Color color)
        {
            float LuminanceComponent(float c)
            {
                c = c / 255f;
                return (c <= 0.03928f) ? (c / 12.92f) : Mathf.Pow((c + 0.055f) / 1.055f, 2.4f);
            }

            float r = LuminanceComponent(color.r);
            float g = LuminanceComponent(color.g);
            float b = LuminanceComponent(color.b);

            return 0.2126f * r + 0.7152f * g + 0.0722f * b;
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================

    // Some color suggestions:
    //public static Color Health => new Color(240, 76, 63);
    //public static Color Stamina => new Color(86, 182, 93);
    //public static Color Mana => new Color(58, 81, 121);
    //public static Color Mana2 => new Color(100, 118, 150);
    //public static Color Armor => new Color(114, 87, 74);
    //public static Color Water => new Color(102, 195, 218);
}