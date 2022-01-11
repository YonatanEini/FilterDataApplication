using FilterDataApplication.DecodedFrameModel;
using FilterDataApplication.TableViewElementModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDataApplication.DataBaseDataFilter
{
    /// <summary>
    /// Fillter The Data In The Mongodb DataBase By Date Range And List Of Icd Properties
    /// </summary>
    public class DataBaseFilter
    {
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public IMongoCollection<DecodedFrameDto> ClientMongoCollection { get; set; }
        public List<string> SelectedIcdProperties { get; set; }
        public DataBaseFilter(IMongoCollection<DecodedFrameDto> Collection, DateTime startingDate, DateTime endingDate, List<string> userProperties)
        {
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
            this.ClientMongoCollection = Collection;
            this.SelectedIcdProperties = userProperties;
        }
        public List<FilterElement> FilterData()
        {
            List<FilterElement> filterElements = new List<FilterElement>();
            if (SelectedIcdProperties.Count == 0)
            {
                AlertWindow alertwWindow = new AlertWindow("Select At Least One Icd Property");
                alertwWindow.Show();
            }
            else
            {
                int currentId = 1;
                var results = ClientMongoCollection.Find(dbItem => dbItem.DecodingTime > StartingDate && dbItem.DecodingTime < EndingDate).ToList();
                foreach (DecodedFrameDto item in results)
                {
                    // only date format
                    string elementCreationDate = item.DecodingTime.ToString("d");
                    string elemtntCreationHour = item.Hour;
                    foreach (DecodedItem decodedItem in item.DecodedItems)
                    {
                        if (SelectedIcdProperties.Contains(decodedItem.Name))
                        {
                            filterElements.Add(new FilterElement(currentId, decodedItem.Name, decodedItem.Value,
                                                                                        elementCreationDate, elemtntCreationHour));
                        }
                    }
                    currentId++;
                }
            }
            return filterElements;
        }
    }
}
