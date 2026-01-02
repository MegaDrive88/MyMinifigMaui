using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBeadando2.Classes {
    public class Minifig {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [NotNull]
        public string Name { get; set; } = "Új figura";

        [NotNull]
        public string? HeadPartId { get; set; }
        [NotNull]
        public string? TorsoPartId { get; set; }
        [NotNull]
        //public string? LegPartId { get => LegPart.part_num; set { LegPartId = value } }
        public string? LegPartId { get; set; }
        public string? HeadItemId { get; set; }
        public string? BackItemId { get; set; }
        public string? AccessoryId { get; set; }
        
        [Ignore]
        public Part? HeadPart { get; set; }
        [Ignore]
        public Part? TorsoPart { get; set; }
        [Ignore]
        public Part? LegPart { get; set; }  
        [Ignore]
        public Part? HeadItem { get; set; }
        [Ignore]
        public Part? BackItem { get; set; }
        [Ignore]
        public Part? Accessory { get; set; }

    }
}
