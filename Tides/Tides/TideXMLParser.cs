using System;
using System.Collections.Generic;
using Android.Runtime;
using System.IO;
using System.Xml.Linq;

namespace Tides
{
    public class TideXMLParser
    {
        private List<IDictionary<string, object>> list_of_tides;

        public List<IDictionary<string, object>> ParsedTides { get { return list_of_tides; } }

        public TideXMLParser(Stream xmlStream)
        {
            list_of_tides = new List<IDictionary<string, object>>();

            var root = XElement.Load(xmlStream);
            var data = root.Element("data");

            foreach (var item in data.Elements("item"))
            {
                //takes data elements and adds them to dictionary
                var tide = new JavaDictionary<string, object>();

                tide["date"] = item.Element("date").Value.Trim();

                tide["day"] = item.Element("day").Value.Trim();

                tide["time"] = item.Element("time").Value.Trim();

                tide["feet"] = item.Element("predictions_in_ft").Value.Trim();

                tide["centimeters"] = item.Element("predictions_in_cm").Value.Trim();

                tide["highlow"] = item.Element("highlow").Value.Trim().Equals("H", StringComparison.CurrentCultureIgnoreCase) ? "High" : "Low";


                tide["daydate"] = tide["day"] + " " + tide["date"];
                tide["tidetime"] = tide["time"] + " - " + tide["highlow"];

                list_of_tides.Add(tide);
            }           
        }
    }
}