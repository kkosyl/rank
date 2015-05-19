using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RankingHoteli.ViewModels
{
    public class AddOpinionAndShowDetailsViewModel
    {
        public HotelDetailsViewModel Hotel { get; set; }
        public AddOpinionViewModel Opinion { get; set; }
    }
}