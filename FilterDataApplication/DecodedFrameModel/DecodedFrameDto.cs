using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDataApplication.DecodedFrameModel
{
    [BsonIgnoreExtraElements]
    public class DecodedFrameDto
    {
        public List<DecodedItem> DecodedItems { get; set; }
        public DateTime DecodingTime { get; set; }
        public string Icdfile { get; set; }
        public string Hour { get; set; }
        public DecodedFrameDto()
        {
            this.DecodedItems = new List<DecodedItem>();
            this.DecodingTime = new DateTime();
            this.Icdfile = "";
            this.Hour = this.DecodingTime.ToString("HH:mm:ss");
        }
        public DecodedFrameDto(List<DecodedItem> decodedItems, DateTime decodedTime, string Icd)
        {
            this.DecodedItems = decodedItems;
            this.DecodingTime = decodedTime;
            this.Icdfile = Icd;
            this.Hour = this.DecodingTime.ToString("HH:mm:ss");
        }
    }
}
