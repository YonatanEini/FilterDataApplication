using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDataApplication.TableViewElementModel
{
    /// <summary>
    /// The Element That Will Present On The DataGrid View
    /// </summary>
    public class FilterElement
    {
        public int FrameId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public FilterElement(int id, string name, int value, string date, string hour)
        {
            this.FrameId = id;
            this.Name = name;
            this.Value = value;
            this.Date = date;
            this.Hour = hour;
        }

    }
}
