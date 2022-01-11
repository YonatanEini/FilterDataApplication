using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDataApplication.DecodedFrameModel
{
    public class DecodedItem
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public DecodedItem(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }
        public override string ToString()
        {
            return "name: " + this.Name + ", value: " + this.Value + Environment.NewLine;
        }
    }
}
