using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using ColourMatcher;

namespace ColourStringTests
    {
    [TestClass]
    public class ColourStringTests
        {
        //testvalue Dictionary - Keys represent test Temperatures, Values represent colours
        Dictionary<string, string> TestValues = new Dictionary<string, string>()
            {
                {"-4", "#41FEE2" }, //Under expected Range
                {"-3","#4514DC" },
                {"0", "#5252CD" },
                {"4", "#6396B9" },
                {"8", "#74C9A4" },
                {"12", "#85EB90" },
                {"16", "#97FC7C" },
                {"20", "#A8FD67" },
                {"24", "#B9ED53" },
                {"28", "#CACD3F" },
                {"32", "#DB9C2A" },
                {"36", "#ED5A16" },
                {"40", "#FE0702" },
                {"44", "#FFA3ED" }//Over expected range
            };

        //matching function - matches if R and B values match exactly.  Not worried about Green, or alpha Values
        private bool colourMatch (Color colour1, Color colour2)
            {
            var redmatch = colour1.R == colour2.R;
            var bluematch = colour1.B == colour2.B;
            return redmatch && bluematch;
            }
        /// <summary>
        /// The top and bottom colours should yeild the predictable colours
        /// </summary>
        [TestMethod]
        public void TestLimits ()
            {
            //testvalue Dictionary - Keys represent test Temperatures, Values represent colours
            Dictionary<double, Color> TestValues = new Dictionary<double, Color>()
            {
                    {-3.9, Color.RoyalBlue },
                    {40.3, Color.Red }
            };
            foreach (var temp in TestValues)
                {
                ColourStringfinder csf = new ColourStringfinder();
                Assert.IsTrue(colourMatch(csf.getColour(temp.Key), temp.Value), $"Colours do not match for Temprature {temp.Key}, Got {csf.getColour(temp.Key)}, but expected {temp.Value}");
                }
            }

        [TestMethod]
        public void SampleValuesIntoStrings ()
            {
            
            foreach (var temp in TestValues)
                {
                ColourStringfinder csf = new ColourStringfinder();

                Assert.AreEqual(temp.Value, csf.getColourHash(temp.Key), $"Colours do not match for Temperature {temp.Key}, Got {csf.getColourHash(temp.Key)}, but expected {temp.Value}");
                }
            }

        [TestMethod]
        public void GenerateHtmlTableInDebug ()
            {
            
            Debug.WriteLine("<table><tr><th>Temperature (C)</th><th>Hash Code</th></tr>");
            foreach (var temp in TestValues)
                {
                ColourStringfinder csf = new ColourStringfinder();
                Debug.WriteLine($"<tr bgcolor={temp.Value}><td>{temp.Key}</td><td>{temp.Value}</td></tr>");
                }
            Debug.WriteLine("</table>");
            }

        }
    }
