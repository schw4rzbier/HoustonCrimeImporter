using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HoustonCrimeImporter.Models
{
    public class Incident
    {
        public string DataSetIdentifier { get; set; }
        public DateTime YMDate { get; set; }
        public int YMCategory { get; set; }
        public int MonthEntered { get; set; }
        public int YearEntered { get; set; }
        public int OffenseReportDate { get; set; }
        [Key]
        public int IncidentNumber { get; set; }
        public int OffenseCode { get; set; }
        public string Beat { get; set; }
        public int ReportingArea { get; set; }
        public int CensusTract { get; set; }
        public int NeighborhoodCode { get; set; }
        public int County { get; set; }
        public int HourOfOffense { get; set; }
        public string MonthAssigned { get; set; }
        public int ClearanceCode { get; set; }
        public string IncidentType { get; set; }
        public int DayOfWeek { get; set; }
        public int DateCleared { get; set; }
        public int DateEntered { get; set; }
        public int DateReviewed { get; set; }
        public string PremiseCode { get; set; }
        public int StreetNumber { get; set; }
        public string StreetSuffix { get; set; }
        public string VehicleDispCode { get; set; }
        public int NumberOfSuspects { get; set; }
        public int NumberOfVictims { get; set; }
        public string Address { get; set; }
        public string City {get;set;}
        public string State { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Incident()
        {
            
        }

        public Incident(string csvLine, string dataSetIdentifier)
        {
            var locstrt = csvLine.IndexOf("\"");
            var loclen = csvLine.Length - locstrt;
            var locationStr = csvLine.Substring(locstrt + 1, loclen - 2);

            var csvProps = csvLine.Substring(0, locstrt);
            
            //Id = Guid.NewGuid();
            var rawPropList = csvProps.Split(',');
            DataSetIdentifier = dataSetIdentifier;
            YMDate = DateTime.Parse(rawPropList[0]);
            YMCategory = int.Parse(rawPropList[1]);
            MonthEntered = int.Parse(rawPropList[2]);
            YearEntered = int.Parse(rawPropList[3]);
            OffenseReportDate = int.Parse(rawPropList[4]);
            IncidentNumber = int.Parse(rawPropList[5]);
            OffenseCode = int.Parse(rawPropList[6]);
            Beat = rawPropList[7];
            ReportingArea  = int.Parse(rawPropList[8]);
            CensusTract  = int.Parse(rawPropList[9]);
            NeighborhoodCode  = int.Parse(rawPropList[10]);
            County  = int.Parse(rawPropList[11]);
            HourOfOffense  = int.Parse(rawPropList[12]);
            MonthAssigned = rawPropList[13];
            ClearanceCode  = int.Parse(rawPropList[14]);
            IncidentType = rawPropList[15];
            DayOfWeek  = int.Parse(rawPropList[16]);
            DateCleared  = int.Parse(rawPropList[17]);
            DateEntered  = int.Parse(rawPropList[18]);
            DateReviewed  = int.Parse(rawPropList[19]);
            PremiseCode = rawPropList[20];
            StreetNumber  = int.Parse(rawPropList[21]);
            StreetSuffix = rawPropList[22];
            VehicleDispCode = rawPropList[23];
            NumberOfSuspects  = int.Parse(rawPropList[24]);
            NumberOfVictims  = int.Parse(rawPropList[25]);
            //shouldn't be any more elements than this.

            var locProps = locationStr.Split(',', '(', ')').ToList();
            locProps.RemoveAll(String.IsNullOrEmpty);
            var index = 0;
            if (locProps.Count > 4)
            {
                Address = locProps[index++];
            }
            City = locProps[index++];
            State = locProps[index++];
            //if (rawPropList.Length < 30) return;
            double outDouble;
            if (double.TryParse(locProps[index++], out outDouble))
            {
                Longitude = outDouble;
            }
            //if (rawPropList.Length < 31) return;
            if (double.TryParse(locProps[index], out outDouble))
            {
                Latitude = outDouble;
            }
        }
    }
}
