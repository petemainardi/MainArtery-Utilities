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
        /// <summary>
        /// Get a copy of the color with the specified alpha value.
        /// </summary>
        public static Color WithAlpha(this Color color, float alpha)
            => new Color(color.r, color.g, color.b, alpha);

        /// <summary>
        /// Get a copy of the color with the specified luminance value.
        /// </summary>
        public static Color WithValue(this Color color, float value)
        {
            Color.RGBToHSV(color, out float hue, out float saturation, out _);
            return Color.HSVToRGB(hue, saturation, Mathf.Clamp01(value));
        }

        /// <summary>
        /// Choose between either white or black as the best contrasting color to this color.
        /// </summary>
        public static Color BestContrast(this Color color)
        {
            return BestContrast(color, Color.white, Color.black);
        }

        /// <summary>
        /// Choose which from among two colors provides better contrast to this color.
        /// </summary>
        /// <param name="color">The color to compare to</param>
        /// <param name="color1">The first color to compare for contrast</param>
        /// <param name="color2">The second color to compare for contrast</param>
        /// <returns>The given color that best contrasts with this color</returns>
        public static Color BestContrast(this Color color, Color color1, Color color2)
        {
            float lum1 = color1.Luminance();
            float lum2 = color2.Luminance();

            Color lighter = Mathf.Max(lum1, lum2) == lum1 ? color1 : color2;
            Color darker = Mathf.Min(lum1, lum2) == lum1 ? color1 : color2;

            return color.Luminance() > 0.179 ? darker : lighter;
        }

        /// <summary>
        /// Calculate the luminance/value of the color.
        /// </summary>
        /// <remarks>This is the L (or V) of the HSL(V) color model.</remarks>
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

        /// =======================================================================================
        /// Specific Colors
        /// =======================================================================================
        public static readonly Color EditorDefaultLight     = new Color(0.7843f, 0.7843f, 0.7843f);
        public static readonly Color EditorDefaultDark      = new Color(0.2196f, 0.2196f, 0.2196f);
        public static readonly Color EditorSelectedLight    = new Color(0.22745f, 0.447f, 0.6902f);
        public static readonly Color EditorSelectedDark     = new Color(0.1725f, 0.3647f, 0.5294f);
        public static readonly Color EditorSelectedUnfocusedLight = new Color(0.68f, 0.68f, 0.68f);
        public static readonly Color EditorSelectedUnfocusedDark  = new Color(0.3f, 0.3f, 0.3f);
        public static readonly Color EditorHoveredLight     = new Color(0.698f, 0.698f, 0.698f);
        public static readonly Color EditorHoveredDark      = new Color(0.2706f, 0.2706f, 0.2706f);

        /// =======================================================================================
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