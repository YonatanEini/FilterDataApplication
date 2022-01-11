using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDataApplication.HandleFilterProperties
{
    /// <summary>
    /// class to keep user selcted icd properties and validate the filter data
    /// </summary>
    public class FilterPropertiesValidator
    {
        public List<string> UserIcdSelctedProperties { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public FilterPropertiesValidator()
        {
            this.UserIcdSelctedProperties = new List<string>();
        }
        public bool CheckFilterProprties(DateTime? startingDate, DateTime? endingDate, DateTime? startingHour, DateTime? endingHour)
        {
            if (startingDate != null && endingDate != null && startingHour != null && endingHour != null)
            {
                DateTime fullStartingDate = new DateTime(startingDate.Value.Year, startingDate.Value.Month, startingDate.Value.Day,
                                                        startingHour.Value.Hour, startingHour.Value.Minute, startingHour.Value.Second);
                DateTime fullEndingDate = new DateTime(endingDate.Value.Year, endingDate.Value.Month, endingDate.Value.Day,
                                                         endingHour.Value.Hour, endingHour.Value.Minute, endingHour.Value.Second);
                StartingDateTime = fullStartingDate;
                EndingDateTime = fullEndingDate;
                if (fullEndingDate <= fullStartingDate)
                {
                    AlertWindow alertwWindow = new AlertWindow("Starting Date Cannot Be Bigger Then Ending Date");
                    alertwWindow.Show();
                }
                return fullStartingDate < fullEndingDate;
            }
            AlertWindow aw = new AlertWindow("Fill All Date Properties!");
            aw.Show();
            return false;
        }
        private static FilterPropertiesValidator _instance;
        //singleton
        public static FilterPropertiesValidator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FilterPropertiesValidator();
            }
            return _instance;
        }

    }
}
