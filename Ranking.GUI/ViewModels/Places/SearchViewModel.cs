using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels.Places
{
    public class SearchViewModel
    {
        public int PlaceId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public double Rate { get; set; }
    }
}