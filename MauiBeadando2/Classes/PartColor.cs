using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBeadando2.Classes {
    public class PartColor {
        public int color_id { get; set; }
        public string color_name { get; set; }
        public int num_sets { get; set; }
        public int num_set_parts { get; set; }
        public string part_img_url { get; set; }
        public string[] elements { get; set; }
    }

}
