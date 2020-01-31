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

        public void ReadData(ref IData inputType, string pathToDataConfig)
        {
            //TODO: implement dynamic types, i.e. IData should not define any specific properties
            
           
            var propsInType = GetTypeProperties(inputType);
            foreach (var line in File.ReadAllLines(pathToDataConfig))
            {
                if (propsInType.Any(e=> line.Contains(e)))
                {
                    var propertyToSet = GetPropertyNameFromLine(line);
                    var valueToSet = GetPropertyValueFromLine(line);
                    SetTypeProperty(inputType, propertyToSet, valueToSet);
                }
            }
        }

        public string GetPropertyNameFromLine(string line)
        {
            var nameAndValue = line.Split(':');
            var result = nameAndValue[0].Trim();
            return result;
        }

        public double GetPropertyValueFromLine(string line)
        {
            var nameAndValue = line.Split(':');
            var valueText = nameAndValue[nameAndValue.Length - 1].Trim();
            var result = double.Parse(valueText);
            return result;
        }

        public static List<string> GetTypeProperties(object inputType)
        {
            List<string> result = new List<string>();
            if (inputType == null) return result;

            Type t = inputType.GetType();
            PropertyInfo[] props = t.GetProperties();

            foreach (PropertyInfo prp in props)
            {
                result.Add(prp.Name);
            }
            return result;
        }

        public void SetTypeProperty(object inputType, string propertyName, double value)
        {
            Type t = inputType.GetType();
            PropertyInfo[] props = t.GetProperties();
            int intValue = (int)value;
            props.Where(e => e.Name == propertyName).FirstOrDefault().SetValue(inputType, intValue);
        }

    }

    public class HeatTransferCalculationsData : IData
    {
        public int noOfElements { get; set; }
        public int noOfNodes { get; set; }
        public double totalLengthOfElement { get; set; }
        public double crossSection { get; set; }
        public double singleSubelementLength { get; set; }
        public double heatFlux { get; set; } //gestosc strumienia ciepla q
        public double thermalConductivity { get; set; } //k / lambda
        public double convectionCoefficient { get; set; } // Alpha
        public double ambientTemperature { get; set; } //tx




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
        double totalLengthOfElement { get; set; }
        double crossSection { get; set; }
        double singleSubelementLength { get; set; }
        double heatFlux { get; set; } //gestosc strumienia ciepla q
        double thermalConductivity { get; set; } //k / lambda
        double convectionCoefficient { get; set; } // Alpha
        double ambientTemperature { get; set; } //tx
    }
}
