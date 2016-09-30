using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metrocamp.Places.UI.Web.Models
{
    public class RootWeather
    {
        public Results results { get; set; }
    }

    public class Results
    {
        public String temp { get; set; }
        public String date { get; set; }
        public String time { get; set; }
        public String condition_code { get; set; }
		public String description { get; set; }
		public String currently { get; set; }		
		public String city { get; set; }
		public String img_id { get; set; }
		public Int32 humidity { get; set; }
		public String wind_speedy { get; set; }
		public String sunrise { get; set; }
		public String sunset { get; set; }
		public String condition_slug { get; set; }
        public String city_name { get; set; }
        public List<Forecast> forecast { get; set; }
    }

    public class Forecast
    {
        public String date { get; set; }
        public String weekday { get; set; }
        public String max { get; set; }
        public String min { get; set; }
        public String description { get; set; }
        public String condition { get; set; }
    }

}