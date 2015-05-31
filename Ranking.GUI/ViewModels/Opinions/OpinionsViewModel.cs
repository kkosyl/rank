using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ranking.GUI.ViewModels.Opinions
{
    public class OpinionsViewModel
    {
        public int UserId { get; set; }

        public string UserNickname { get; set; }

        public string Content { get; set; }

        public DateTime AddDate { get; set; }

        public double Rate { get; set; }
    }
}