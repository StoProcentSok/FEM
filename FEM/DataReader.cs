using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FEM
{
    public class DataReader
    {

        public void readData(ref IData inputType, string pathToDataConfig)
        {
            //TODO: implement dynamic types, i.e. IData should not define any specific properties
            
           
            var propsInType = GetTypeProperties(inputType);
            foreach (var line in File.ReadAllLines(pathToDataConfig))
            {
                if (propsInType.Any(e=> line.Contains(e)))
                {
                    
                }
            }
        }

        //public string 

        public int GetPropertyValueFromLine(string line)
        {
            var nameAndValue = line.Split(':');
            var valueText = nameAndValue[nameAndValue.Length - 1].Trim();
            var result = Int32.Parse(valueText);
            return result;
        }

        public static List<string> GetTypeProperties(object atype)
        {
            List<string> result = new List<string>();
            if (atype == null) return result;
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();

            foreach (PropertyInfo prp in props)
            {
                result.Add(prp.Name);
            }
            return result;
        }

        public void SetTypeProperty(string property, int value)
        {

        }

    }

    public class HeatTransferCalculationsData : IData
    {
        public int noOfElements { get; set; }
        public int noOfNodes { get; set; }
        public int totalLengthOfElement { get; set; }
        public int crossSection { get; set; }
        public int singleSubelementLength { get; set; }
        public int heatFlux { get; set; } //gestosc strumienia ciepla q
        public int thermalConductivity { get; set; } //k / lambda
        public int convectionCoefficient { get; set; } // Alpha
        public int ambientTemperature { get; set; } //tx




        public List<string> getDataProperties()
        {
            var dataProps = this.GetType().GetProperties();

            List<string> result = new List<string>();
            foreach (var prop in dataProps)
            {
                result.Add(prop.Name);
            }

            return result;
        }
    }

    public interface IData
    {
        int noOfElements { get; set; }
        int noOfNodes { get; set; }
        int totalLengthOfElement { get; set; }
        int crossSection { get; set; }
        int singleSubelementLength { get; set; }
        int heatFlux { get; set; } //gestosc strumienia ciepla q
        int thermalConductivity { get; set; } //k / lambda
        int convectionCoefficient { get; set; } // Alpha
        int ambientTemperature { get; set; } //tx
    }
}
