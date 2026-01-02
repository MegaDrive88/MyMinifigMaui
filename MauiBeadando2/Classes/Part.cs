using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiBeadando2.Classes {

    public class Part {
        [PrimaryKey]
        public string part_num { get; set; }
        public string name { get; set; }
        public int part_cat_id { get; set; }
        public string part_url { get; set; }
        public string? part_img_url { get; set; }
        public string external_ids_json { get; set; }
        [Ignore]
        public Dictionary<string, string[]> external_ids {
            get => JsonSerializer.Deserialize<Dictionary<string, string[]>>(external_ids_json ?? "{}");
            set => external_ids_json = JsonSerializer.Serialize(value);
        }
    }
    //public class External_Ids {
    //    public string[]? BrickLink { get; set; }
    //    public string[]? BrickOwl { get; set; }
    //    public string[]? Brickset { get; set; }
    //    public string[]? LDraw { get; set; }
    //    public string[]? LEGO { get; set; }
    //}
}
