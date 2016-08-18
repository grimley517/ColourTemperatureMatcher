using System;
using System.Drawing;
using System.Diagnostics;

namespace ColourMatcher
    {
    public class ColourStringfinder
    {
        private double minTemp { get; set; } = -3.9;
        private double maxTemp { get; set; } = 40.3;
        ///<summary>
        ///Takes a double representing the current temperature and returns a colour representing that colour
        ///</summary>
        ///<param name ="temp">The temperature to represent as a colour</param>
        public Color getColour (double temp)
            {
            Color cold = Color.RoyalBlue;
            Color hot = Color.Red;
            double ratio = (temp - minTemp) / (maxTemp - minTemp);
            int R = (byte)Math.Min((Math.Round(cold.R + ratio * (hot.R - cold.R))),255);
            int G = (byte)Math.Min((Math.Round(ratio * (1 - ratio) * byte.MaxValue * 4)),255); //Green pixel used to brighten middle values
            int B = (byte)Math.Min((Math.Round(cold.B + ratio * (hot.B - cold.B))),255);
            Color result = Color.FromArgb(R, G, B);
            return result;
            }


        ///<summary>
        ///Takes a string representing a temperature, and represents an associated colour in a colour hex string for web use.
        ///</sumary>
        ///<param name ="temp">The temperature to represent as a colour</param>
        public String getColourHash (String temp)
            {
            try
                {
                double dtemp = double.Parse(temp);
                Color result = getColour(dtemp);
                return (String.Format(@"#{0:X2}{1:X2}{2:X2}", result.R, result.G, result.B));
                }
            catch (FormatException e)
                {
                return (@"#cccccc;");
                }
            catch (Exception e)
                {
                Debug.WriteLine(e.Message);
                return (@"#cccccc;");
                }
            }
        }
}
